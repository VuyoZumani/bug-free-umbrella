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
            using (SqlConnection sqlConnection1 =
             new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                string groupOfInsertionCommands = "";
                //loop for creating rows with each team name and initial values
                for (int i = 0; i < teams.Items.Count; i++)
                {
                    groupOfInsertionCommands += $"INSERT INTO {leagueName} (Team, P , W , D , L , GF , GA , GD , Points) " +
                                                $"VALUES (\'{teams.Items.GetItemAt(i)}\',0,0,0,0,0,0,0,0) ";
                }
                cmd.CommandText = " CREATE TABLE " + $"{leagueName}" + " (" +
                                    " Team varchar(255)," +
                                    " P int NOT NULL," +
                                    " W int NOT NULL," +
                                    " D int NOT NULL," +
                                    " L int NOT NULL," +
                                    " GF int NOT NULL," +
                                    " GA int NOT NULL," +
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
            using (SqlConnection sqlConnection2 =
              new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT * INTO fixtures " +
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
            //Load fixtures to prepare for sorting
            Fixtures fixtures = new Fixtures();
            using (SqlConnection sqlConnection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM fixtures;", sqlConnection);
                SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("SELECT COUNT(*) FROM league;", sqlConnection);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                fixtures.fixtures.ItemsSource = dataTable.DefaultView;
            }
            using (SqlConnection sqlConnection3 =
              new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
            {
                //get the total number of teams
                SqlCommand cmd3 = new SqlCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "SELECT COUNT(team) FROM league;";
                cmd3.Connection = sqlConnection3;
                sqlConnection3.Open();
                int numofteams = Convert.ToInt32(cmd3.ExecuteScalar());
                sqlConnection3.Close();

                //sort the fixtures such that a team plays one match per week with default being 14 teams
                string[,] weeks = new string[20, 20];
                int count = 0;
                foreach (DataRowView dr in fixtures.fixtures.ItemsSource)
                {
                    if (count < numofteams - 1)
                    {
                        weeks[count, 0] = $"{dr[0]}|" + $"{dr[3]}";
                    }
                    else
                    {
                        //check if week has does not have both teams
                        bool inweek = false;
                        int index = 0;
                        for (int i = 0; i < numofteams*2 ; i++)
                        {
                            for (int j = 0; j < numofteams*2; j++)
                            {//if there is no matches that week then insert the match
                                if (weeks[i, j] == null)
                                {
                                    index = j;
                                    weeks[i, j]= $"{dr[0]}|" + $"{dr[3]}";
                                    break;
                                }
                                else
                                {//if one of the teams already has a match that week 
                                    if (weeks[i, j].Contains($"{dr[0]}") || weeks[i, j].Contains($"{dr[3]}"))
                                    {
                                        inweek = true;
                                        break;
                                    }
                                }
                            }
                            //not in the week, then can be inserted
                            if (inweek == false)
                            {
                                weeks[i, index] = $"{dr[0]}|" + $"{dr[3]}";
                                break;                              
                            }
                            inweek = false;            
                        }
                    }
                    count++;
                }
                    //shuffle the weeks
                    string temp = "";
                for(int m = 0; m < 20; m++)
                    {
                        for(int n = 0; n < 20; n++)
                        {
                            //this is to ensure that a team plays at least every other day in the fixture
                            if(m%2!=0 && m > 2 && weeks[m,n]!=null)
                            {
                                temp = weeks[m, n];
                                weeks[m, n] = weeks[m - 1, n];
                                weeks[m - 1, n] = temp;
                            } 
                        }
                    }
                //get sorted data back to fixture
                string groupOfInsertionCommands = "";
                //loop for inserting the now sorted fixtures in fixturesorted table
                for (int j = 0; j < 20; j++)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (weeks[j,i] != null)
                        {
                            string teamonleft = weeks[j, i].Split('|')[0];
                            string teamonright = weeks[j, i].Split('|')[1];
                            groupOfInsertionCommands += $"INSERT INTO fixturesorted (week,teamonleft,teamonright) " +
                                                    $"VALUES (\'{j+1}\',\'{teamonleft}\',\'{teamonright}\') ";                          
                        }
                    }
                }
                //get sorted fixtures back to fixture table
                using (SqlConnection sqlConnection1 =
                 new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = " CREATE TABLE fixturesorted (" +
                                        " week int NOT NULL," +
                                        " teamonleft varchar(255)," +
                                        " scoreleft int ," +
                                        " scoreright int ," +
                                        " teamonright varchar(255)" +
                                        ") " +
                                        groupOfInsertionCommands;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection1.Close();
                }     
            }
        }
    }
}
