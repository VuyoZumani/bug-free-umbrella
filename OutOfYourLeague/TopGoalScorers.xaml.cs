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
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
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

        private void fixtures_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            //Load fixture for each week default is the first week for now...
            Fixtures fixtures = new Fixtures();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    //get first week of fixture
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT teamonleft, scoreleft, scoreright, teamonright  " +
                                                                       "FROM fixturesorted " +
                                                                       "WHERE week=1;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    fixtures.fixtures.ItemsSource = dataTable.DefaultView;

                    StandingsForLeague standingsForLeague = new StandingsForLeague();
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
    }
}
