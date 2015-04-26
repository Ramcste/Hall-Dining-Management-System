using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication4
{
    /// <summary>
    /// Interaction logic for FeastUI.xaml
    /// </summary>
    public partial class FeastUI
    {
        public FeastUI()
        {
            InitializeComponent();

        }



        private void FeastEntryButton_Click(object sender, RoutedEventArgs e)
        {
            if (Feastrate.Text.Length == 0)
            {
                MessageBox.Show("Enter feast rate first!!");
            }

            else if (!Regex.Match(Feastrate.Text, @"^([5-9][0-9]|1[0-9][0-9])$").Success)
            {
                MessageBox.Show("Feasrate must be greater than or equal to 50 and less than 100", "Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";

                SqlConnection connection = new SqlConnection(ConnectionString);


                String query = string.Format("insert into feastrate values('{0}')", Feastrate.Text);


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

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int tfeast;

            if (!Regex.Match(IdTextBox.Text, @"^([1-9]|[1-9][0-9]|[1-9][0-9][0-9]}[1-9][0-9][0-9][0-9])$").Success)
            {
                MessageBox.Show("Error in ID", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!Regex.Match(NooffeastTextBox.Text, @"[1-9]|[1-9][0-9]").Success)
            {
                MessageBox.Show("Error in feast entry!!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else if (!Regex.Match(AmountTextBox.Text, @"^([0]||[5-9][0-9]||1[0-9][0-9])$").Success)
            {
                MessageBox.Show("Error in feast amount entry!!", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            else if ((IdTextBox.Text.Length == 0) && (NooffeastTextBox.Text.Length == 0) &&
                     (AmountTextBox.Text.Length == 0))
            {
                MessageBox.Show("Enter Id ,NoofFeast and  Amount !!!");
            }


            else
            {
                int id = Convert.ToInt32(IdTextBox.Text);
                int nooffeast = Convert.ToInt32(NooffeastTextBox.Text);
                int amount = Convert.ToInt32(AmountTextBox.Text);
                int feastrate = Convert.ToInt32(FeastrateText.Text);

                if (id == Convert.ToInt32(CheckId()))
                {
                    MessageBox.Show("This id is already used by other person.");


                }

                else
                {

                    if (amount <= feastrate*nooffeast || amount > feastrate*nooffeast)
                    {
                        int amount1 = amount;
                        int no = amount/feastrate;
                        int returntk = amount1 - feastrate*no;
                        int feastamount = feastrate*no;

                        if (returntk < 0)
                        {
                            returntk = -1*returntk;
                        }

                        int noOfFeast;
                        if (no > 0)
                        {
                            noOfFeast = no;
                            tfeast = no+Getfeast();
                        }
                        else
                        {
                            noOfFeast = 0;
                            tfeast = 0+Getfeast();
                        }
                        String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";

                        String query = string.Format("insert into feastentry values('{0}','{1}','{2}','{3}')", id,FeastDatePicker.Text ,noOfFeast,
                            feastamount);

                        string query1 =
                            string.Format("update boarder1 set totalfeast='" + tfeast + "' where Id='" + IdTextBox.Text + "'");

                        SqlConnection connection = new SqlConnection(ConnectionString);


                        try
                        {
                            connection.Open();
                            SqlCommand cmd = new SqlCommand(query, connection);
                            SqlCommand cmd1=new SqlCommand(query1,connection);
                            cmd.ExecuteNonQuery();
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("U will get " + no + " feast and tk. " + returntk + " back and nooffeast is updated");
                          Reset();
                        }




                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }




                    }
                    //ClearBoxes();

                }
            }
        }

        private void ClearBoxes()
        {
            IdTextBox.Text = string.Empty;
            NooffeastTextBox.Text = string.Empty;
            AmountTextBox.Text = string.Empty;
            CheckIdTextBox.Text = string.Empty;
        }




        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            String ConnectionString =
                @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Id,Nooffeast from feastentry where Id='" + CheckIdTextBox.Text + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int sid = int.Parse(reader[0].ToString());
                        int snumber = int.Parse(reader[1].ToString());


                        MessageBox.Show("Ur Registration is done ur id is " + sid + " and u will get " + snumber +
                                        " feast.");
                    }

                }
                else
                {
                    MessageBox.Show("Ur id is not registered for feast!!");
                }
                ClearBoxes();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetData()
        {
            string id;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Amount from feastrate ");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    FeastrateText.Text = id;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }



        private string CheckId()
        {
            string id = null;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from feastentry");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id;
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ti = TC.SelectedItem as TabItem;
            if (ti != null) Title = ti.Header.ToString();
        }

        private void Reset()
        {
            NooffeastTextBox.Text = string.Empty;
            AmountTextBox.Text = string.Empty;
            IdTextBox.Text = string.Empty;

        }

        private int Getfeast()
        {
            int sfeast = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select totalfeast from boarder1 where Id='" + IdTextBox.Text + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sfeast = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sfeast;
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            FeastDatePicker.Text = DateTime.Today.ToString(CultureInfo.InvariantCulture);
        }
}
}



