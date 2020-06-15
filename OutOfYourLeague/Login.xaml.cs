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
using System.Configuration;

namespace OutOfYourLeague
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        static string connectionstring = ConfigurationManager.ConnectionStrings["OutOfYourLeague.Properties.Settings.Setting"].ConnectionString;
        public SqlConnection con = new SqlConnection(connectionstring);
        public Login()
        {
            InitializeComponent();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            //Check if there are users
            MainWindow main = new MainWindow();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    SqlCommand cmdcheckusertableexists = new SqlCommand();
                    cmdcheckusertableexists.CommandType = CommandType.Text;
                    //creating the league table and inserting the teams into the table and setting the initial values to zero
                    cmdcheckusertableexists.CommandText = " IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES" +
                                                          " WHERE TABLE_NAME = N'users') " +
                                                          " BEGIN CREATE TABLE users (" +
                                                          " firstname varchar(255)," +
                                                          " username varchar(255)," +
                                                          " password varchar(255)," +
                                                          " officialid varchar(255)" +
                                                          ") " +
                                                          //add officialIDs for all the officials
                                                          "INSERT INTO users (officialid) VALUES (123)"+
                                                          "END";
                    cmdcheckusertableexists.Connection = main.con;
                    cmdcheckusertableexists.ExecuteNonQuery();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //Go to sign up window
            Signup signup = new Signup();
            Close();
            signup.Show();        
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {
            //validation
            if (username.Text == "")
            {
                username.BorderBrush = Brushes.Red;
            }
            if (password.Password == ""){
                password.BorderBrush = Brushes.Red;
             }
            if(password.Password!="" && username.Text!="")
            {
                MainWindow main = new MainWindow();
                //Check if user is signed-up
                string name = "";
                int officialidcount = 0;
                try
                {
                    
                    using (main.con)
                    {
                        main.con.Open();
                        SqlCommand cmdcheckleagueexists = new SqlCommand();
                        cmdcheckleagueexists.CommandType = CommandType.Text;


                        //creating the league table and inserting the teams into the table and setting the initial values to zero
                        cmdcheckleagueexists.CommandText = " IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES" +
                                                            " WHERE TABLE_NAME = N'league') " +
                                                            " BEGIN SELECT -1 " +
                                                            " END" +
                                                            " ELSE SELECT 1";
                        cmdcheckleagueexists.Connection = main.con;
                        string g = cmdcheckleagueexists.ExecuteScalar().ToString();
                        
                        
                        SqlCommand cmdcheckuser = new SqlCommand();
                        cmdcheckuser.CommandType = CommandType.Text;
                        cmdcheckuser.CommandText = " SELECT firstname" +
                                                    " FROM users " +
                                                    " WHERE username = @username AND password = @password";
                        cmdcheckuser.Parameters.AddWithValue("@username", username.Text);
                        cmdcheckuser.Parameters.AddWithValue("@password", password.Password);
                        cmdcheckuser.Connection = main.con;

                        SqlCommand cmdcheckofficial = new SqlCommand();
                        cmdcheckofficial.CommandType = CommandType.Text;
                        cmdcheckofficial.CommandText = " SELECT COUNT(officialid)" +
                                                        " FROM users " +
                                                        " WHERE username = @username AND password = @password";
                        cmdcheckofficial.Parameters.AddWithValue("@username", username.Text);
                        cmdcheckofficial.Parameters.AddWithValue("@password", password.Password);
                        cmdcheckofficial.Connection = main.con;
                        officialidcount = Convert.ToInt32(cmdcheckofficial.ExecuteScalar());
                        name = Convert.ToString(cmdcheckuser.ExecuteScalar());
                        if (name == "")
                        {
                            MessageBox.Show("Incorrect login details", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (g == "-1" && officialidcount != 0)
                            {
                                MessageBox.Show($"Welcome official {name}");
                                CreateLeague createLeague = new CreateLeague();
                                Close();
                                createLeague.Show();

                            }
                            else if (officialidcount != 0)
                            {
                                //show enter app
                                MessageBox.Show($"Welcome back official {name}");
                                main.user = "official";
                                main.con = con;
                                Close();
                                main.Show();
                            }
                            else
                            {
                                //view only and check stats
                                if (g == "-1")
                                {
                                    MessageBox.Show($"Welcome user {name}. Enter reg123 to see what officials can do");
                                }
                                else
                                {
                                    MessageBox.Show($"Welcome back {name}");
                                    main.user = "player";
                                    main.con = con;
                                    Close();
                                    main.Show();
                                }
                               
                            }
                        }
                        
                        
                    }
                   
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
        }

        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            password.BorderBrush = Brushes.White;
        }

        private void username_GotFocus(object sender, RoutedEventArgs e)
        {
            username.BorderBrush = Brushes.White;
        }
    }
}
