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
    /// Interaction logic for Fixtures.xaml
    /// </summary>
    public partial class Fixtures : Window
    {
        public Fixtures()
        {
            InitializeComponent();
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Update fixture table 
            foreach (DataRowView dr in fixtures.ItemsSource)
            {
                if (dr[1] != null && dr[2]!=null)
                {
                    using (SqlConnection sqlConnection2 =
           new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;"))
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "UPDATE fixtures " +
                                           "SET scoreleft="+$"{ dr[1]},scoreright={ dr[2]} " +
                                           $"WHERE teamonLeft={ dr[0]} AND teamRight={dr[3]}; ";
                        cmd2.Connection = sqlConnection2;
                        sqlConnection2.Open();
                        cmd2.ExecuteNonQuery();
                        sqlConnection2.Close();
                    }
                }
            }

        }
    }
}
