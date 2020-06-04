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
        public string user = "";
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
                    cmd.CommandText = $"INSERT INTO topgoalscorers (player, team, goals) VALUES (@player, @teamofplayer, @goals)";
                    cmd.Parameters.AddWithValue("@player", player.Text);
                    cmd.Parameters.AddWithValue("@teamofplayer", teamofplayer.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@goals", goals.Text);
                    cmd.Connection = main.con;
                    cmd.ExecuteNonQuery();
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

        private void main_Click(object sender, RoutedEventArgs e)
        {
            //Go back to main window
            MainWindow main = new MainWindow();
            Close();
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
                    dataTable.Columns[0].ReadOnly = true;
                    dataTable.Columns[1].ReadOnly = true;
                    topGoalScorers.topgoalscorers.ItemsSource = dataTable.DefaultView;

                }
                Close();
                topGoalScorers.user = user;
                topGoalScorers.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    } 
}
