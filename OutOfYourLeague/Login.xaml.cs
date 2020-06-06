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
            MainWindow main = new MainWindow();
            //Check if user is signed-up
            string name = "";
            string officialid = "";
            try
            {
                using (main.con)
                {
                    main.con.Open();
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
                    cmdcheckofficial.CommandText = " SELECT officialid" +
                                                    " FROM users " +
                                                    " WHERE username = @username AND password = @password";
                    cmdcheckofficial.Parameters.AddWithValue("@username", username.Text);
                    cmdcheckofficial.Parameters.AddWithValue("@password", password.Password);
                    cmdcheckofficial.Connection = main.con;
                    officialid = cmdcheckofficial.ExecuteScalar().ToString();
                    name = Convert.ToString(cmdcheckuser.ExecuteScalar());
                    if (name == "")
                    {
                        MessageBox.Show("Incorrect login details");
                    }
                    else
                    {
                        if (officialid != "")
                        {
                            //show enter app
                            MessageBox.Show($"Welcome back official {name}");
                            main.user = "official";
                            main.con = con;
                            main.Show();
                        }
                        else
                        {
                            //view only and check stats
                            MessageBox.Show($"Welcome back {name}");
                            main.user = "player";
                            main.con = con;
                            main.createLeague.Visibility = Visibility.Collapsed;
                            main.Show();                            
                        }
                    }
                }
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
