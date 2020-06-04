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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static string connectionstring = ConfigurationManager.ConnectionStrings["OutOfYourLeague.Properties.Settings.Setting"].ConnectionString;
        public SqlConnection con = new SqlConnection(connectionstring);
        public string user = "";
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void createLeague_Click(object sender, RoutedEventArgs e)
        {
            //Go to CreateLeague Window
            CreateLeague league = new CreateLeague();
            Close();
            league.user = user;
            league.Show();
            //MainWindow main = new MainWindow();
            //main.createLeague.Visibility = Visibility.Collapsed;
            //main.Show();
        }

        private void loadLeague_Click(object sender, RoutedEventArgs e)
        {
            //Loading the league data to see standings
            try
            {
                StandingsForLeague standingsForLeague = new StandingsForLeague();
                MainWindow main = new MainWindow();
                using (main.con)
                {
                    main.con.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
                                                                       " FROM league" +
                                                                       " ORDER BY Points DESC;"
                                                                       , con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    standingsForLeague.league.ItemsSource = dataTable.DefaultView;
                }
                Close();
                standingsForLeague.user = user;
                standingsForLeague.Show();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Load fixture for each week default is the first week for now...
            Fixtures fixtures = new Fixtures();
            try
            {
                using (con)
                {
                    con.Open();
                    //get first week of fixture
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT teamonleft, scoreleft, scoreright, teamonright  " +
                                                                       "FROM fixturesorted " +
                                                                       "WHERE week=1;"
                                                                       , con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    fixtures.fixtures.ItemsSource = dataTable.DefaultView;

                    StandingsForLeague standingsForLeague = new StandingsForLeague();
                    string lastweek = "";
                    //get all the weeks to appear on the combobox
                    using (SqlCommand sqlCommand = new SqlCommand(" SELECT week " +
                                                                    " FROM fixturesorted;"
                                                                    , con))
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
                    }
                }
                Hide();
                fixtures.user = user;
                if (fixtures.user == "player")
                {
                    fixtures.updateLeague.Visibility = Visibility.Collapsed;
                }
                fixtures.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void stats_Click(object sender, RoutedEventArgs e)
        {
            TopGoalScorers topGoalScorers = new TopGoalScorers();
            //Loading the topgoalscorer data
            try
            {
                using (con)
                {
                    con.Open();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT *" +
                                                                       "FROM topgoalscorers;"
                                                                       , con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    topGoalScorers.topgoalscorers.ItemsSource = dataTable.DefaultView;
                }
                Hide();
                topGoalScorers.user = user;
                if (topGoalScorers.user == "player")
                {
                    topGoalScorers.addplayer.Visibility = Visibility.Collapsed;
                }
                topGoalScorers.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }         
        }
    }
}
