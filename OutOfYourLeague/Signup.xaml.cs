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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void submitsignupinfo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Fixtures fixtures2 = new Fixtures();
            try
            {
                using (main.con)
                {
                    main.con.Open();
                    SqlCommand cmdadduser = new SqlCommand();
                    cmdadduser.CommandType = CommandType.Text;

                    if (officialID.Text != "")
                    {
                       //Update the details of official: first login
                        cmdadduser.CommandText = "UPDATE users " +
                                                 "SET firstname = @firstname, username = @username, password = @password " +
                                                 "WHERE officialid = @officialid";
                        cmdadduser.Parameters.AddWithValue("@firstname", firstname.Text);
                        cmdadduser.Parameters.AddWithValue("@username", username.Text);
                        cmdadduser.Parameters.AddWithValue("@password", password.Password);
                        cmdadduser.Parameters.AddWithValue("@officialid", officialID.Text);
                        cmdadduser.Connection = main.con;
                        cmdadduser.ExecuteNonQuery();
                    }
                    else
                    {
                        //Insert new user
                        cmdadduser.CommandText = "INSERT INTO users (firstname, username, password) " +
                                                 "VALUES (@firstname, @username, @password)";
                        cmdadduser.Parameters.AddWithValue("@firstname", firstname.Text);
                        cmdadduser.Parameters.AddWithValue("@username", username.Text);
                        cmdadduser.Parameters.AddWithValue("@password", password.Password);
                        cmdadduser.Connection = main.con;
                        cmdadduser.ExecuteNonQuery();
                    }
                    
                }
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Close();
            Login login = new Login();
            login.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Login login = new Login();
            login.Show();
        }
    }
}
