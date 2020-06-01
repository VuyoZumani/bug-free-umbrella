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

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void createLeague_Click(object sender, RoutedEventArgs e)
        { 
            //Go to CreateLeague Window
            Hide();
            CreateLeague league = new CreateLeague();
            league.Show();    
        }

        private void loadLeague_Click(object sender, RoutedEventArgs e)
        {
            //Loading the league data to see standings
            StandingsForLeague standingsForLeague = new StandingsForLeague();
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
                                                                   " FROM league" +
                                                                   " ORDER BY Points DESC;"
                                                                   ,sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                standingsForLeague.league.ItemsSource=dataTable.DefaultView;
            }
            Hide();
            standingsForLeague.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Load fixture for each week default is the first week for now...
            Fixtures fixtures = new Fixtures();
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT teamonleft,scoreleft,scoreright,teamonright  " +
                                                                   "FROM fixturesorted " +
                                                                   "WHERE week=1;"
                                                                   , sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                fixtures.fixtures.ItemsSource = dataTable.DefaultView;
            }
            Hide();
            fixtures.Show();
        }

        private void stats_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
