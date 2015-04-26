using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for BlackListUI.xaml
    /// </summary>
    public partial class BlackListUI
    {
        public BlackListUI()
        {
            InitializeComponent();
        }

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ti = TC.SelectedItem as TabItem;
            if (ti != null) Title = ti.Header.ToString();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MinimumamountTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Minimum Amount to start meal");
            }
            else
            {

                bool value = Regex.IsMatch(MinimumamountTextBox.Text, @"^([0]||[5-9][0-9]||[1-9][0-9][0-9])$");

                if (value)
                {
                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    String query = string.Format("insert into minamount values('{0}')", MinimumamountTextBox.Text);
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data is inserted Succesfully!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("Error in Minimum Amount Format");

                }

            }
        }

        private void SubmitButton1_Click(object sender, RoutedEventArgs e)
        {
            if (MinimumTextBox.Text.Length == 0)
            {
                MessageBox.Show("Enter Minimum Amount Know the List!!");
            }

            else
            {
                bool value = Regex.IsMatch(MinimumTextBox.Text, @"([0]||[5-9][0-9]|[1-9][0-9][0-9])$");

                if (value)
                {


                    String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
                    String query =
                        string.Format("select Id,Name,Deptname,Initialpayment from boarder where (InitialPayment< '" +
                                      MinimumTextBox.Text + "')");
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    DataSet ds = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                        BorderListView.DataContext = ds.Tables[0].DefaultView; // ListView
                        MessageBox.Show("This is the list of boarder whose payment is equal or less than " +
                                        LowestAmountPayed());
                    }


                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    MessageBox.Show("Error in Minimum Amount Format");
                }
            }

        }

        private int LowestAmountPayed()
        {
            int slowestamount = 0;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Minamount from minamount");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    slowestamount = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return slowestamount;
        }

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Okay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}