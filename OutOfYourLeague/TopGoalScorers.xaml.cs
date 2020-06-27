using System;
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

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for TopGoalScorers.xaml
    /// </summary>
    public partial class TopGoalScorers : Window
    {
        public string user = "";
        public TopGoalScorers()
        {
            InitializeComponent();
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            //Go back to main window
            MainWindow main = new MainWindow();
            Close();
            main.user = user;
            main.Show();
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
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT team " +
                                                                       " FROM league" +
                                                                       " ORDER BY Points DESC;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    //Show position
                    int count = 1;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        row[0] = $"{count}.  {row[0]}";

                        count++;
                    }
                    //make columns read only
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        col.ReadOnly = true;
                    }

                    standingsForLeague.teams.ItemsSource = dataTable.DefaultView;
                    SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(" SELECT MP,W,D,L,GF,GA,GD,Points as Pts " +
                                                                       " FROM league" +
                                                                       " ORDER BY Points DESC;"
                                                                       , main.con);
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter2.Fill(dataTable2);
                    //make columns read only
                    foreach (DataColumn col in dataTable2.Columns)
                    {
                        col.ReadOnly = true;
                    }

                    standingsForLeague.league.ItemsSource = dataTable2.DefaultView;

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

        private void fixtures_Click(object sender, RoutedEventArgs e)
        {
            //Load fixture for each week default is the first week for now...
            Fixtures fixtures = new Fixtures();
            MainWindow main = new MainWindow();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    //get first week of fixture
                    SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("SELECT teamonleft AS hometeam, scoreleft as homescore, time as datetime, scoreright as awayscore, teamonright AS awayteam, field  " +
                                                                  "FROM fixturesorted " +
                                                                  $"WHERE week=1;"
                                                                  , main.con);
                    StandingsForLeague standingsForLeague = new StandingsForLeague();
                    string lastweek = "";
                    //get all the weeks to appear on the combobox
                    SqlCommand sqlCommand = new SqlCommand(" SELECT MAX(week) " +
                                                                    " FROM fixturesorted;"
                                                                    , main.con);
                    int maxnumofweeks = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    int loop = 0;
                    while (loop < maxnumofweeks)
                    {
                        fixtures.weeks.Items.Add($"Week {loop + 1}");
                        loop++;
                    }
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter2.Fill(dataTable);
                    fixtures.user = user;
                    if (fixtures.user == "player")
                    {
                        fixtures.updateLeague.Visibility = Visibility.Collapsed;
                        fixtures.updatetime.Visibility = Visibility.Collapsed;
                        fixtures.updatefield.Visibility = Visibility.Collapsed;
                        //make columns read only
                        dataTable.Columns[1].ReadOnly = true;
                        dataTable.Columns[2].ReadOnly = true;
                        dataTable.Columns[3].ReadOnly = true;
                        dataTable.Columns[5].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[4].ReadOnly = true;

                    Close();
                    fixtures.user = user;
                    fixtures.fixtures.ItemsSource = dataTable.DefaultView;
                    fixtures.Show();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void addplayer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            //Load the AddNewPlayer window
            AddNewPlayerToScore addNewPlayerToScore = new AddNewPlayerToScore();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    //get teams to appear in the combobox
                    using (SqlCommand sqlCommand = new SqlCommand(" SELECT team " +
                                                                    " FROM league" +
                                                                    " ORDER BY Points DESC;", main.con))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader != null)
                            {
                                while (sqlDataReader.Read())
                                {
                                    addNewPlayerToScore.teamofplayer.Items.Add(sqlDataReader["team"].ToString());
                                }
                            }
                        }
                    }
                }
                Hide();
                addNewPlayerToScore.user = user;
                addNewPlayerToScore.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void updatetopgoalscorer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    //Update top goal scorer tables 
                    foreach (DataRowView dr in topgoalscorers.ItemsSource)
                    {
                          SqlCommand cmdupdatetopogoalscorer = new SqlCommand();
                            cmdupdatetopogoalscorer.CommandType = CommandType.Text;
                            cmdupdatetopogoalscorer.CommandText = "UPDATE topgoalscorers" +
                                                                  " SET goals = @goals" +
                                                                  " WHERE player = @player AND team = @team";
                            cmdupdatetopogoalscorer.Parameters.AddWithValue("@player", dr[0]);
                            cmdupdatetopogoalscorer.Parameters.AddWithValue("@team", dr[1]);
                            cmdupdatetopogoalscorer.Parameters.AddWithValue("@goals", dr[2]);
                            cmdupdatetopogoalscorer.Connection = main.con;
                            cmdupdatetopogoalscorer.ExecuteNonQuery();                        
                    }                    
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void logoff_Click(object sender, RoutedEventArgs e)
        {
            //Go to login
            Close();
            Login login = new Login();
            login.Show();
        }
    }
}
