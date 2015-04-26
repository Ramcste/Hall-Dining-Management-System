using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for StudentUI.xaml
    /// </summary>
    public partial class StudentUI
    {
        public StudentUI()
        {
            InitializeComponent();
        }

        private void Btnupdate_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.Match(Txtboardername1.Text,
                @"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
            {
                MessageBox.Show("Error Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (!Regex.Match(TxtbordermobileNo1.Text, @"^(\d{10}||\d{11})$").Success)
            {

                MessageBox.Show("Error Mobile Number", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!Regex.Match(Txtborderadress1.Text, @"^([A-Z][a-z]+||\d{3}-[A-z][a-z]+\s[A-Z][a-z]+,[A-Z][a-z]+,[A-Z][a-z]+)$").Success)
            {

                MessageBox.Show("Give Full Address", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!Regex.Match(TxtInitialPayment1.Text, @"^([0]|[5-9][0-9]|[1-9][0-9][0-9])$").Success)
            {

                MessageBox.Show("Payment Amount should be grater than 49 and less than 1000", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            else if (Activate())
            {


                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("update  boarder1  set Name='" + Txtboardername1.Text + "', Deptname='" +
                                  Combodeptname1.SelectionBoxItem + "', Batch='" + Txtboarderbatch1.SelectionBoxItem +
                                  "', MobileNo='" + TxtbordermobileNo1.Text + "',Address1='" + Txtborderadress1.Text +
                                  "', Initialpayment='" + TxtInitialPayment1.Text + "' where Id='" +
                                  TxtborderId1.SelectionBoxItem + "'");
                var connection = new SqlConnection(ConnectionString);
                try
                {
                    connection.Open();
                    var cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Ur Data is Updated Successfully!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (Txtboardername.Text == string.Empty)
            {
                MessageBox.Show("Enter Border Name!!");
            }

            else if (Txtborderbatch.SelectionBoxItem == null)
            {
                MessageBox.Show("Select Border  batch!!");
            }

            else if (Combodeptname.SelectionBoxItem == null)
            {
                MessageBox.Show("Select Border Deptarment Name!!");
            }

            else if (Txtborderadress.Text == string.Empty)
            {
                MessageBox.Show("Enter Border Address!!");
            }

            else if (TxtbordermobileNo.Text == string.Empty)
            {
                MessageBox.Show("Enter Border Mobile Number!!");
            }
            else if (TxtInitialPayment.Text == string.Empty)
            {
                MessageBox.Show("Enter InitialPayment!!");
            }


            else
            {
                if (!Regex.Match(Txtboardername.Text,
                        @"^([A-Z][a-z]+||[A-Z][a-z]+\s[A-Z][a-z]+||[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
                {
                    MessageBox.Show("Error Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                else if (!Regex.Match(TxtbordermobileNo.Text, @"^(\d{10}||\d{11})$").Success)
                {

                    MessageBox.Show("Error Mobile Number", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                else if (!Regex.Match(Txtborderadress.Text, @"^([A-Z][a-z]+||\d{3}-[A-z][a-z]+\s[A-Z][a-z]+,[A-Z][a-z]+,[A-Z][a-z]+)$").Success)
                {

                    MessageBox.Show("Give FullS Address", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                else if (!Regex.Match(TxtInitialPayment.Text, @"^(0||[5-9][0-9]||[1-9][0-9][0-9])$").Success)
                {

                    MessageBox.Show(" Payment Amount should be grater than 49 and less than 1000", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }


               
                else if (Activate())
                {


                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    int tfeast = 0;
                    int tmeal = 0;
                    String query = string.Format("insert into boarder1 values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                        TxtborderId.Text,Txtboardername.Text, Combodeptname.SelectionBoxItem, Txtborderbatch.Text, TxtbordermobileNo.Text,
                        Txtborderadress.Text, TxtInitialPayment.Text,tmeal,tfeast);
                    var connection = new SqlConnection(ConnectionString);

                    try
                    {
                        connection.Open();
                        var cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data is inserted Successfully!!");
                        Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
               


            }

    }

    private void GetId()
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from boarder1");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    TxtborderId1.Items.Add(sid);

                    int id = sid + 1;
                    TxtborderId.Text = id.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetId();
           
        }

        private void TxtborderId1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Name,Deptname,Batch,MobileNo,Address1,Initialpayment from boarder1 where Id='" +
                              TxtborderId1.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    string sdeptname = reader[1].ToString();
                    string sbatch = reader[2].ToString();
                    string smobileno = reader[3].ToString();
                    string sadress = reader[4].ToString();
                    string sinitialpayment = reader[5].ToString();

                    Txtboardername1.Text = sname;
                    Txtboarderbatch1.Text = sbatch;
                    Combodeptname1.Text = sdeptname;
                    Txtborderadress1.Text = sadress;
                    TxtbordermobileNo1.Text = smobileno;
                    TxtInitialPayment1.Text = sinitialpayment;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var tab = TC.SelectedItem as TabItem;
            if (tab != null) Title = tab.Header.ToString();
        }


        private void GetBatch()
        {
            
        
            
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Batchname from batch");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
{
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string sname = reader[0].ToString();
                Txtborderbatch.Items.Add(sname);
                Txtboarderbatch1.Items.Add(sname);

            }
                connection.Close();
        }
        
            catch(Exception ex)
{
            MessageBox.Show(ex.Message);
}
        }

        private void GetDepartment()
        {



            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Departmentname from department");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    Combodeptname.Items.Add(sname);
                    Combodeptname1.Items.Add(sname);
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetBatch();
            GetDepartment();
        }
    }
}