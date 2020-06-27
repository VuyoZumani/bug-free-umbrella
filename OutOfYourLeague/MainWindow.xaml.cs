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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
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
            //Check if league has been already been created

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
                    topGoalScorers.user = user;
                    if (topGoalScorers.user == "player")
                    {
                        topGoalScorers.addplayer.Visibility = Visibility.Collapsed;
                        topGoalScorers.updatetopgoalscorer.Visibility = Visibility.Collapsed;
                        //make columns read only
                        dataTable.Columns[2].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[1].ReadOnly = true;
                    topGoalScorers.topgoalscorers.ItemsSource = dataTable.DefaultView;
                }
                Hide();
                topGoalScorers.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }         
        }

        private void topgoalscorer_Click(object sender, RoutedEventArgs e)
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
                    topGoalScorers.user = user;
                    if (topGoalScorers.user == "player")
                    {
                        topGoalScorers.addplayer.Visibility = Visibility.Collapsed;
                        topGoalScorers.updatetopgoalscorer.Visibility = Visibility.Collapsed;
                        //make columns read only
                        dataTable.Columns[2].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[1].ReadOnly = true;
                    topGoalScorers.topgoalscorers.ItemsSource = dataTable.DefaultView;
                }
                Hide();
                topGoalScorers.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fixture_Click(object sender, RoutedEventArgs e)
        {
            //Load fixture for each week default is the first week for now...
            Fixtures fixtures = new Fixtures();
            try
            {
                using (con)
                {
                    con.Open();
                    //get first week of fixture
                    SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("SELECT teamonleft AS hometeam, scoreleft, time, scoreright, teamonright AS awayteam  " +
                                                                  "FROM fixturesorted " +
                                                                  "WHERE week=1;"
                                                                  , con);
                    StandingsForLeague standingsForLeague = new StandingsForLeague();
                    string lastweek = "";
                    //get all the weeks to appear on the combobox
                    SqlCommand sqlCommand = new SqlCommand(" SELECT MAX(week) " +
                                                                    " FROM fixturesorted;"
                                                                    , con);
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
                        //make columns read only
                        dataTable.Columns[1].ReadOnly = true;
                        dataTable.Columns[2].ReadOnly = true;
                        dataTable.Columns[3].ReadOnly = true;
                    }
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[4].ReadOnly = true;
                    fixtures.fixtures.ItemsSource = dataTable.DefaultView;
                    Close();
                    fixtures.user = user;
                    fixtures.Show();
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
           
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
