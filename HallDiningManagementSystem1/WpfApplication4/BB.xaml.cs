using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfApplication4
{
    /// <summary>
    /// Interaction logic for BB.xaml
    /// </summary>
    public partial class BB
    {
        public BB()
        {
            InitializeComponent();
        }



        private void BatchButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (BatchBox.Text == string.Empty)
            {
                MessageBox.Show("Fill all the boxes!! ");
            }

            else if (!Regex.Match(BatchBox.Text, @"^(1st||2nd||3rd||[4-9]th||[12][0-9]th)$").Success)
            {
                MessageBox.Show(
                    "Error in Batch format.Batch should be written as firstnumber and then(st,nd,rdand th)", "Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


      
            else if (Activate())
            {
                String ConnectionString = @"server=Roshani-PC;Database=Dinning;Integrated Security=true";
                String query = string.Format("insert into batch values('{0}','{1}')",BatchIdBox.Text ,BatchBox.Text );
                SqlConnection connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data is inserted Successfully!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DepartmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DepartmentBox.Text == string.Empty)
            {
                MessageBox.Show("Fill all the boxes!! ");
            }

            else if (!Regex.Match(DepartmentBox.Text, @"^([A-Z]+|[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
            {
                MessageBox.Show(
                    "Error in Department name format.Department Name should be written as (CSTE,Pharmacy and Computer Science and Telecommunication Engineering)", "Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if (Activate())
            {
                String ConnectionString = @"server=Roshani-PC;Database=Dinning;Integrated Security=true";
                String query = string.Format("insert into department values('{0}','{1}')", DeptIdTextBox.Text, DepartmentBox.Text);
                SqlConnection connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data is inserted Successfully!!");
                
                connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


       private void GetId()
        {
            String ConnectionString = @"server=Roshani-PC;Database=Dinning;Integrated Security=true";
             String query = string.Format("select Id from batch");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    BatchIdComboBox.Items.Add(sid);

                    int id = sid + 1;
                    BatchIdBox.Text = id.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       private void GetDepartmentId()
       {
           String ConnectionString = @"server=Roshani-PC;Database=Dinning;Integrated Security=true";
           String query = string.Format("select Id from department");
           var connection = new SqlConnection(ConnectionString);
           try
           {
               connection.Open();
               var cmd = new SqlCommand(query, connection);
               SqlDataReader reader = cmd.ExecuteReader();
               while (reader.Read())
               {
                   int sid = int.Parse(reader[0].ToString());
                   DeptIdCombobox.Items.Add(sid);

                   int id = sid + 1;
                   DeptIdTextBox.Text = id.ToString(CultureInfo.InvariantCulture);
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
           
        }

        private void EditBatchButton_OnClick(object sender, RoutedEventArgs e)
        {
          
        }

       
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetId();
            GetDepartmentId();
        }

        
    }
}
