﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for Fixtures.xaml
    /// </summary>
    public partial class Fixtures : Window
    {
        public string user = "";
        public Fixtures()
        {
            InitializeComponent();
            
        }
        int weeknum = 1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            StandingsForLeague standingsForLeague = new StandingsForLeague();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    //Update fixture tables 
                    foreach (DataRowView dr in fixtures.ItemsSource)
                       
                    {
                        if ($"{dr[1]}" != "" && $"{dr[2]}" != "")
                        {

                            SqlCommand cmdupdatefixture = new SqlCommand();
                            cmdupdatefixture.CommandType = CommandType.Text;
                            cmdupdatefixture.CommandText = "UPDATE fixtures" +
                                                          " SET scoreleft = @scoreleft," +
                                                          "     scoreright = @scoreright" +
                                                          " WHERE teamonleft = @teamonleft AND teamonright = @teamonright" +
                                                          " UPDATE fixturesorted" +
                                                          " SET scoreleft = @scoreleft," +
                                                          " scoreright = @scoreright " +
                                                          " WHERE teamonleft = @teamonleft AND teamonright = @teamonright; ";
                            cmdupdatefixture.Parameters.AddWithValue("@scoreleft", dr[1]);
                            cmdupdatefixture.Parameters.AddWithValue("@scoreright", dr[2]);
                            cmdupdatefixture.Parameters.AddWithValue("@teamonleft", dr[0]);
                            cmdupdatefixture.Parameters.AddWithValue("@teamonright", dr[3]);
                            cmdupdatefixture.Connection = main.con;
                            cmdupdatefixture.ExecuteNonQuery();

                            
                        }
                    }
                    //update the league table through the updated fixture table
                    SqlCommand cmdupdateleague = new SqlCommand();
                    cmdupdateleague.CommandType = CommandType.Text;
                    cmdupdateleague.CommandText =
                        //"--prepare for new update league" +
                        " UPDATE league" +
                        " SET P = 0, W = 0, D = 0,L = 0, GF = 0, GA = 0, Points = 0" +

                        //"--update number of games played in league" +
                        " UPDATE league" +
                        " SET P = played" +
                        " FROM (" +
                        //" --number of wins a team has" +
                        "       SELECT team, Count(*) AS played" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL AND scoreright IS NOT NULL)  AND  (Team = teamonleft OR Team = teamonright)" +
                        "       GROUP BY team ) AS playedtable" +
                        "       WHERE  playedtable.team = league.team;" +

                        //" --update number of wins in league" +
                        " UPDATE league" +
                        " SET W = wins" +
                        " FROM (" +
                        //" --number of wins a team has" +
                        "       SELECT team, Count(*) AS wins" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL AND scoreright IS NOT NULL)  AND (scoreleft > scoreright AND Team = teamonleft) OR (scoreleft < scoreright AND Team = teamonright)" +
                        "       GROUP BY team) AS wintable" +
                        " WHERE wintable.team = league.team;" +

                        //" --update number of losses in league" +
                        " UPDATE league" +
                        " SET L = losses" +
                        " FROM(" +
                        //" --number of losses a team has in fixtures" +
                        "       SELECT team, Count(*) AS losses" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL AND scoreright IS NOT NULL)  AND (scoreleft < scoreright AND Team = teamonleft) OR(scoreleft > scoreright AND Team = teamonright)" +
                        "       GROUP BY team) AS wintable" +
                        " WHERE wintable.team = league.team;" +

                        //" --update number of draws in league" +
                        " UPDATE league" +
                        " SET D = draws" +
                        " FROM (" +
                        //" --number of draws a team has in fixtures" +
                        "       SELECT team, Count(*) AS draws" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL AND scoreright IS NOT NULL)  AND (scoreleft = scoreright AND team = teamonleft) OR (scoreleft = scoreright AND Team = teamonright)" +
                        "       GROUP BY team) AS wintable" +
                        " WHERE wintable.team = league.team;" +

                        //" --update goals for each team"  +
                        " UPDATE league" +
                        " SET GF = goalsfor" +
                        " FROM (" +
                        //" --get goal for each team" +
                        "       SELECT" +
                        //" --to deal with cases when team is not present one side of the fixture" +
                        "           CASE " +
                        "               WHEN team1 IS NULL THEN team2" +
                        "               WHEN team2 IS NULL THEN team1" +
                        "               ELSE team1" +
                        "           END AS team," +
                        //" --similarly deals with cases where there is no score on a side since a team is never on some side of the fixture" +
                        "           CASE " +
                        "               WHEN totalonleft IS NULL THEN totalonright" +
                        "               WHEN totalonright IS NULL THEN totalonleft" +
                        "               ELSE totalonleft + totalonright " +
                        "           END AS goalsfor" +
                        "       FROM(" +
                        //" --returns a union of for the fixture" +
                        "       SELECT *" +
                        "       FROM(" +
                        //" --gets the total goals scored when playing on the left side of fixture" +
                        "       SELECT team AS team1, SUM(scoreleft) AS totalonleft" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL)  AND team = teamonleft" +
                        "       GROUP BY team" +
                        "       ) AS tableforgoalsonleft" +
                        " FULL JOIN(" +
                        //" --gets the total goals scored when on the right side of fixture" +
                        "       SELECT team AS team2, SUM(scoreright) AS totalonright" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreright IS NOT NULL)  AND team = teamonright" +
                        "       GROUP BY team) AS tableforgoalsonright" +
                        "       ON tableforgoalsonleft.team1 = tableforgoalsonright.team2) AS allinonetable) AS jointgftable" +
                        " WHERE  league.team = jointgftable.team;" +

                        //" --update goals against teams" +
                        " UPDATE league" +
                        " SET GA = goalsagainst" +
                        " FROM(" +
                        //" --get goal for a team" +
                        "       SELECT" +
                        //" --to deal with cases when team is not present one side of the fixture" +
                        "           CASE " +
                        "               WHEN team1 IS NULL THEN team2" +
                        "               WHEN team2 IS NULL THEN team1" +
                        "               ELSE team1 " +
                        "           END AS team," +
                        //" --similarly deals with cases where there is no score on a side since a team is never on some side of the fixture" +
                        "           CASE " +
                        "               WHEN totalonleft IS NULL THEN totalonright" +
                        "               WHEN totalonright IS NULL THEN totalonleft" +
                        "               ELSE totalonleft + totalonright " +
                        "           END AS goalsagainst" +
                        "       FROM(" +
                        //" --returns a union of for the fixture" +
                        "       SELECT *" +
                        "       FROM(" +
                        //" --gets the total goals scored against when playing on the left side of fixture" +
                        "           SELECT team AS team1, SUM(scoreright) AS totalonleft" +
                        "           FROM fixtures, league" +
                        "           WHERE (scoreright IS NOT NULL)  AND team = teamonleft" +
                        "           GROUP BY team" +
                        "           ) AS tableforgoalsagainstonleft" +
                        " FULL JOIN(" +
                        //" --gets the total goals scored against when on the right side of fixture" +
                        "       SELECT team AS team2, SUM(scoreleft) AS totalonright" +
                        "       FROM fixtures, league" +
                        "       WHERE (scoreleft IS NOT NULL)  AND team = teamonright" +
                        "       GROUP BY team) AS tableforgoalsagainstonright" +
                        "       ON tableforgoalsagainstonleft.team1 = tableforgoalsagainstonright.team2) AS allinonetable) AS jointgatable" +
                        " WHERE league.team = jointgatable.team;" +

                        //" --update goal different for each team" +
                        " UPDATE league" +
                        " SET GD = goaldifference" +
                        " FROM(" +
                        //" --gets the goal difference for each team" +
                        "       SELECT team, SUM(GF - GA) AS goaldifference" +
                        "       FROM league" +
                        "       GROUP BY team" +
                        "       ) AS goaldifftable" +
                        " WHERE league.team = goaldifftable.team; " +

                        //"--update points for each team" +
                        " UPDATE league" +
                        " SET Points = pts" +
                        " FROM(" +
                        //" --gets points for each team" +
                        "       SELECT team, SUM(3 * W + D) AS pts" +
                        "       FROM league" +
                        "       GROUP BY team) AS pointstable" +
                        " WHERE league.team = pointstable.team; ";
                        cmdupdateleague.Connection = main.con;
                        cmdupdateleague.ExecuteNonQuery();


                    //Loading the league data to see standings
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
                                                                       " FROM league" +
                                                                       " ORDER BY Points DESC;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    standingsForLeague.league.ItemsSource = dataTable.DefaultView;
                    Hide();
                    standingsForLeague.user = user;
                    standingsForLeague.Show();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void weeks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow main = new MainWindow();
            string selecteditm= weeks.SelectedItem.ToString();
            int weeknum = Convert.ToInt32(selecteditm.Substring(selecteditm.Length - 1));
            //Load fixtures to prepare for sorting
            Fixtures fixtures = new Fixtures();
            using (main.con)
            {
                main.con.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"SELECT teamonleft, scoreleft, scoreright, teamonright " +
                                                                   $"FROM fixturesorted " +
                                                                   $"WHERE week = {weeknum};"
                                                                   , main.con);  
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                fixtures.fixtures.ItemsSource = dataTable.DefaultView;
                string lastweek = "";
                //get all the weeks to appear on the combobox
                using (SqlCommand sqlCommand = new SqlCommand(" SELECT week " +
                                                                " FROM fixturesorted;"
                                                                , main.con))
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader != null)
                        {
                            while (sqlDataReader.Read())
                            {
                                if (lastweek != sqlDataReader["week"].ToString())
                                {
                                    fixtures.weeks.Items.Add($"Week{sqlDataReader["week"].ToString()}");
                                    lastweek = sqlDataReader["week"].ToString();
                                }
                            }
                        }
                    }
                    fixtures.user = user;
                    if (fixtures.user == "player")
                    {
                        fixtures.updateLeague.Visibility = Visibility.Collapsed;
                        //make columns read only
                        dataTable.Columns[1].ReadOnly = true;
                        dataTable.Columns[2].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[3].ReadOnly = true;
                    fixtures.fixtures.ItemsSource = dataTable.DefaultView;
                }
                Close();
                fixtures.Show();
            }

        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            //Go back to main window
            MainWindow main = new MainWindow();
            Close();
            main.user = user;
            main.Show();
        }

        private void topgoalscorers_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            TopGoalScorers topGoalScorers = new TopGoalScorers();
            //Load top goal scorers...
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT *" +
                                                                       "FROM topgoalscorers;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    topGoalScorers.user = user;
                    if (topGoalScorers.user == "player")
                    {
                        topGoalScorers.addplayer.Visibility = Visibility.Collapsed;
                        //make columns read only
                        dataTable.Columns[2].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[1].ReadOnly = true;
                    topGoalScorers.topgoalscorers.ItemsSource = dataTable.DefaultView;
                }
                Close();
                topGoalScorers.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void standings_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            //Loading the league data to see standings
            StandingsForLeague standingsForLeague = new StandingsForLeague();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
                                                                       " FROM league" +
                                                                       " ORDER BY Points DESC;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                    //make columns read only
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        col.ReadOnly = true;
                    }
                    standingsForLeague.league.ItemsSource = dataTable.DefaultView;
                }
                Close();
                standingsForLeague.user = user;
                standingsForLeague.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
