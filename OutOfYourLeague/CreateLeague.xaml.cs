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
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for CreateLeague.xaml
    /// </summary>
    public partial class CreateLeague : Window
    {
        public string user = "";
        public CreateLeague()
        {
            InitializeComponent();
        }

        private void addTeam_Click(object sender, RoutedEventArgs e)
        {
            //check if not empty
            if (teamToBeEntered.Text == "") 
            {
                teamToBeEntered.BorderBrush = Brushes.Red;
            }
            else
            {
                //Check if team already added
                int j = 0;
                foreach ( string i in teams.Items)
                {
                    if (i == teams.Items[j].ToString())
                    {
                        teamToBeEntered.BorderBrush = Brushes.Red;
                        MessageBox.Show("Team already added to the league");
                    }
                    j++;
                }
                //Add team to the league listbox
                string team = teamToBeEntered.Text;
                teams.Items.Add(team);
                teamToBeEntered.Clear();
            }            
        }

        private void create_Click(object sender, RoutedEventArgs e)
        {
            //validation
            if (teams.Items.Count < 2)
            { 
                teamToBeEntered.BorderBrush = Brushes.Red;
                MessageBox.Show("There must be at least two teams");
            }
            else
            {
                MainWindow main = new MainWindow();
                Fixtures fixtures2 = new Fixtures();
                try
                {
                    using (main.con)
                    {
                        main.con.Open();
                        SqlCommand cmdcreateleague = new SqlCommand();
                        cmdcreateleague.CommandType = CommandType.Text;


                        //creating the league table and inserting the teams into the table and setting the initial values to zero
                        cmdcreateleague.CommandText = " CREATE TABLE league (" +
                                            " Team varchar(255)," +
                                            " MP int NOT NULL," +
                                            " W int NOT NULL," +
                                            " D int NOT NULL," +
                                            " L int NOT NULL," +
                                            " GF int NOT NULL," +
                                            " GA int NOT NULL," +
                                            " GD int NOT NULL," +
                                            " Points int NOT NULL, " +
                                            " PRIMARY KEY (Team)" +
                                            ") ";
                        cmdcreateleague.Connection = main.con;
                        cmdcreateleague.ExecuteNonQuery();
                        //loop for creating rows with each team name and initial values
                        for (int i = 0; i < teams.Items.Count; i++)
                        {
                            SqlCommand cmdinsertleague = new SqlCommand();
                            cmdinsertleague.CommandType = CommandType.Text;
                            //creating the league table and inserting the teams into the table and setting the initial values to zero
                            cmdinsertleague.CommandText = "INSERT INTO league (Team, P , W , D , L , GF , GA , GD , Points) " +
                                                         $"VALUES (@teaminleague{i}, 0, 0, 0, 0, 0, 0, 0, 0) ";
                            cmdinsertleague.Parameters.AddWithValue($"@teaminleague{i}", teams.Items.GetItemAt(i));
                            cmdinsertleague.Connection = main.con;
                            cmdinsertleague.ExecuteNonQuery();
                        }



                        //creating fixtures
                        SqlCommand cmdcreatefixture = new SqlCommand();
                        cmdcreatefixture.CommandType = CommandType.Text;
                        cmdcreatefixture.CommandText = "SELECT * INTO fixtures " +
                                            "FROM (" +
                                            "SELECT " +
                                            " " +
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
                                           $"FROM league AS team1 " +
                                           $"JOIN league AS team2 " +
                                            //"--cross join" +
                                            "ON 1 = 1 " +
                                            "WHERE team1.team<> team2.team " +
                                            "GROUP BY " +
                                            //"--to get rid of duplicates" +
                                            "CASE WHEN team1.team > team2.team THEN team2.team " +
                                            "ELSE team1.team END, " +
                                            "CASE WHEN team1.team > team2.team THEN team1.team " +
                                            "ELSE team2.team END) AS fixtures;";
                        cmdcreatefixture.Connection = main.con;
                        cmdcreatefixture.ExecuteNonQuery();

                        //Load fixtures to prepare for sorting
                        Fixtures fixtures = new Fixtures();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM fixtures;", main.con);
                        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter("SELECT COUNT(*) FROM league;", main.con);
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);

                        //get the total number of teams
                        SqlCommand cmdnumteams = new SqlCommand();
                        cmdnumteams.CommandType = CommandType.Text;
                        cmdnumteams.CommandText = "SELECT COUNT(team) FROM league;";
                        cmdnumteams.Connection = main.con;
                        int numofteams = Convert.ToInt32(cmdnumteams.ExecuteScalar());

                        //sort the fixtures such that a team plays one match per week with default being 14 teams
                        string[,] weeks = new string[20, 20];
                        int count = 0;
                        foreach (DataRowView dr in dataTable.DefaultView)
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
                                for (int i = 0; i < numofteams * 2; i++)
                                {
                                    for (int j = 0; j < numofteams * 2; j++)
                                    {//if there is no matches that week then insert the match
                                        if (weeks[i, j] == null)
                                        {
                                            index = j;
                                            weeks[i, j] = $"{dr[0]}|" + $"{dr[3]}";
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
                        for (int m = 0; m < 20; m++)
                        {
                            for (int n = 0; n < 20; n++)
                            {
                                //this is to ensure that a team plays at least every other day in the fixture
                                if (m % 2 != 0 && m > 2 && weeks[m, n] != null)
                                {
                                    temp = weeks[m, n];
                                    weeks[m, n] = weeks[m - 1, n];
                                    weeks[m - 1, n] = temp;
                                }
                            }
                        }
                        //get sorted data back to fixture

                        //get sorted fixtures back to fixture table                    
                        SqlCommand cmdfixturesort = new SqlCommand();
                        cmdfixturesort.CommandType = CommandType.Text;
                        cmdfixturesort.CommandText = " CREATE TABLE fixturesorted (" +
                                                    " week int NOT NULL," +
                                                    " teamonleft varchar(255)," +
                                                    " scoreleft int , " +
                                                    " time datetime, " +
                                                    " scoreright int ," +
                                                    " teamonright varchar(255)," +
                                                    " CONSTRAINT match PRIMARY KEY (teamonright, teamonleft) ";
                        cmdfixturesort.Connection = main.con;
                        cmdfixturesort.ExecuteNonQuery();
                        //loop for inserting the now sorted fixtures in fixturesorted table
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < 20; i++)
                            {
                                if (weeks[j, i] != null)
                                {
                                    string teamonleft = weeks[j, i].Split('|')[0];
                                    string teamonright = weeks[j, i].Split('|')[1];
                                    SqlCommand cmdinsertfixturesort = new SqlCommand();
                                    cmdinsertfixturesort.CommandType = CommandType.Text;
                                    cmdinsertfixturesort.CommandText = $"INSERT INTO fixturesorted (week, teamonleft, teamonright) " +
                                                                       $"VALUES (@week, @teamonleft, @teamonright) ";
                                    cmdinsertfixturesort.Parameters.AddWithValue("@teamonleft", teamonleft);
                                    cmdinsertfixturesort.Parameters.AddWithValue("@teamonright", teamonright);
                                    cmdinsertfixturesort.Parameters.AddWithValue("@week", j + 1);
                                    cmdinsertfixturesort.Connection = main.con;
                                    cmdinsertfixturesort.ExecuteNonQuery();
                                }
                            }
                        }

                        //create top goal scorer table
                        SqlCommand cmdtopgoalscorer = new SqlCommand();
                        cmdtopgoalscorer.CommandType = CommandType.Text;
                        cmdtopgoalscorer.CommandText = " CREATE TABLE topgoalscorers (" +
                                                        " Player varchar(255)," +
                                                        " Team varchar(255) ," +
                                                        " Goals int, " +
                                                        " CONSTRAINT pk_topgoalscorer PRIMARY KEY (Player, Team)" +
                                                        " ) ";
                        cmdtopgoalscorer.Connection = main.con;
                        cmdtopgoalscorer.ExecuteNonQuery();

                        //Load fixture for each week default is the first week for now...

                        SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("SELECT teamonleft, scoreleft, scoreright, teamonright  " +
                                                                            "FROM fixturesorted " +
                                                                            "WHERE week=1;"
                                                                            , main.con);
                        DataTable dataTable2 = new DataTable();
                        sqlDataAdapter2.Fill(dataTable2);
                        dataTable2.Columns[0].ReadOnly = true;
                        dataTable2.Columns[3].ReadOnly = true;
                        fixtures2.fixtures.ItemsSource = dataTable2.DefaultView;
                        string lastweek = "";
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
                                            fixtures2.weeks.Items.Add($"Week{sqlDataReader["week"].ToString()}");
                                            lastweek = sqlDataReader["week"].ToString();
                                        }
                                    }

                                }
                            }
                        }
                        Close();
                        fixtures2.user = user;
                        fixtures2.Show();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
        }

        private void main_Click(object sender, RoutedEventArgs e)
        {
            //Go back to main window
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }

        private void teamToBeEntered_GotFocus(object sender, RoutedEventArgs e)
        {
            teamToBeEntered.BorderBrush = Brushes.White;
        }
        BitmapImage image;
        private void browseimage_Click(object sender, RoutedEventArgs e)
        {
            //to browse for the team logo
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                teamlogo.Source = new BitmapImage(new Uri(op.FileName));
                image= new BitmapImage(new Uri(op.FileName)); 
            }
        }


        byte[] arr;
        private void saveimage_Click(object sender, RoutedEventArgs e)
        {
           //save image
           System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            arr = (byte[])converter.ConvertTo(image, typeof(byte[]));
        }
    }
}
