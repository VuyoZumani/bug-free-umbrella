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
    /// Interaction logic for AddNewPlayerToScore.xaml
    /// </summary>
    public partial class AddNewPlayerToScore : Window
    {
        public AddNewPlayerToScore()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void teamofplayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void addplayer_Click(object sender, RoutedEventArgs e)
        {
            //add a goal scorer to the topgoalscorers table
            MainWindow main = new MainWindow();

            TopGoalScorers topGoalScorers = new TopGoalScorers();
            try
            {               
                using (main.con)
                {
                    main.con.Open();                   
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"INSERT INTO topgoalscorers (player, team, goals) VALUES (\'{player.Text}\',\'{teamofplayer.SelectedItem.ToString()}\',{goals.Text})";
                    cmd.Connection = main.con;
                    
                    //Loading the topgoalscorer data
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(" SELECT * " +
                                                                       " FROM topgoalscorers" +
                                                                       " ORDER BY goals DESC;"
                                                                       , main.con);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
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
    } 
}
