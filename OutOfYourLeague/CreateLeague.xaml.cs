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
    /// Interaction logic for CreateLeague.xaml
    /// </summary>
    public partial class CreateLeague : Window
    {
        public CreateLeague()
        {
            InitializeComponent();
        }

        private void addTeam_Click(object sender, RoutedEventArgs e)
        {//Add team to the league listbox
            string team = teamToBeEntered.Text;
            teams.Items.Add(team);
            teamToBeEntered.Clear();
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            //creating the league table and inserting the teams into the table and setting the initial values to zero
            string leagueName = nameOfLeague.Text;
           using( SqlConnection sqlConnection1 =
            new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                string groupOfInsertionCommands= "";
                //loop for creating rows with each team name and initial values
                for(int i = 0; i < teams.Items.Count; i++)
                {
                    groupOfInsertionCommands += $"INSERT INTO {leagueName} (Team, P , W , D , L , GF , GA , GD , Points) " +
                                                $"VALUES (\'{teams.Items.GetItemAt(i)}\',0,0,0,0,0,0,0,0) ";
                }
                cmd.CommandText =   " CREATE TABLE " + $"{leagueName}" + " (" +
                                    " Team varchar(255)," +
                                    " P int NOT NULL," +
                                    " W int NOT NULL," +
                                    " D int NOT NULL," +
                                    " L int NOT NULL," +
                                    " GF int NOT NULL," +
                                    " FA int NOT NULL," +
                                    " GD int NOT NULL," +
                                    " Points int NOT NULL" +
                                    ") " +
                                    groupOfInsertionCommands;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            
            //creating fixtures
          using ( SqlConnection sqlConnection2 =
            new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText =  "SELECT * INTO fixtures " +
                                    "FROM (" +
                                    "SELECT " +
                                    //"--deals with the leftside" +
                                    "   CASE WHEN team1.team>team2.team THEN team2.team " +
                                    "        ELSE team1.team " +
                                    "   END AS teamonleft," +
                                    "     NULL AS scoreleft," +
                                    "   NULL AS scoreright, " +
                                    //"--deals with the rightside" +
                                    "   CASE WHEN team1.team > team2.team THEN team1.team " +
                                    "        ELSE team2.team " +
                                    "   END AS teamonright " +
                                   $"FROM {leagueName} AS team1 " +
                                   $"JOIN {leagueName} AS team2 " +
                                    //"--cross join" +
                                    "ON 1 = 1 " +
                                    "WHERE team1.team<> team2.team " +
                                    "GROUP BY " +
                                    //"--to get rid of duplicates" +
                                    "CASE WHEN team1.team > team2.team THEN team2.team " +
                                    "ELSE team1.team END, " +
                                    "CASE WHEN team1.team > team2.team THEN team1.team " +
                                    "ELSE team2.team END) AS fixtures; ";
                cmd2.Connection = sqlConnection2;
                sqlConnection2.Open();
                cmd2.ExecuteNonQuery();
                sqlConnection2.Close();
            }
            

            Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
