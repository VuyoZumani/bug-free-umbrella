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
using System.Data.SqlClient;
using System.Data;

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for StandingsForLeague.xaml
    /// </summary>
    public partial class StandingsForLeague : Window
    {
        public string user = "";
        public StandingsForLeague()
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

        private void logoff_Click(object sender, RoutedEventArgs e)
        {
            //Go to login
            Close();
            Login login = new Login();
            login.Show();
        }

        private void fixture_Click(object sender, RoutedEventArgs e)
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

        private void topgoalscorer_Click(object sender, RoutedEventArgs e)
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
                        topGoalScorers.updatetopgoalscorer.Visibility = Visibility.Collapsed;
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
    }
}
