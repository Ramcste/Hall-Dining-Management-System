using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {

            if (Txtname.Text == string.Empty || PasswdBox.Password == string.Empty)
            {
                MessageBox.Show("Enter  values on both fields !!");
            }



            else if (
                !Regex.Match(Txtname.Text,
                    @"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
            {
                MessageBox.Show("Error in User Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!Regex.Match(PasswdBox.Password, @"^([a-z0-9_-]{6,18})$").Success)
            {
                MessageBox.Show("Error in  Password", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (Usertype.Text == string.Empty)
            {
                MessageBox.Show("Select user type!!!");
            }

            else if (Usertype.Text=="Manager")
            {
              
                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    String query =
                        string.Format("select name,passwd from  manager  where name= '" + Txtname.Text +
                                      "' and  passwd='" + PasswdBox.Password + "' ");
                    var connection = new SqlConnection(ConnectionString);

                    try
                    {
                        connection.Open();
                        var cmd = new SqlCommand(query, connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        int count = 0;
                        while (reader.Read())
                        {
                            count++;
                        }
                        if (count == 1)
                        {
                            //  MessageBox.Show("Welcome ur Successfully Logged In");
                            var main = new MainWindow();
                            Close();
                            main.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Username or Password  or Usertype is Wrong try again");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            else if (Usertype.Text == "Admin")
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("select Name,Password from  admin  where Name= '" + Txtname.Text +
                                  "' and  Password='" + PasswdBox.Password + "' ");
                var connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    var cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {

                        ManagerUI manager = new ManagerUI();
                        Close();
                        manager.ShowDialog();
                        //  MessageBox.Show("Welcome ur Successfully Logged In");
                       
                    }
                    else
                    {
                        MessageBox.Show("Username or Password or Usertype is Wrong try again");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                

            }
                

        }

        private void ResetButton_OnClick(object sender, RoutedEventArgs e)
        {
            PasswdBox.Password = string.Empty;
           
        }
    }
}