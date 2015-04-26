using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for CalculationUI.xaml
    /// </summary>
    public partial class CalculationUI
    {
        public CalculationUI()
        {
            InitializeComponent();
        }

        private void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (Combomanagerid.SelectedItem == null)
            {
                MessageBox.Show("Select a manager Id!!");
            }

            else if (Txtamount.Text == string.Empty)
            {
                MessageBox.Show("Enter amount");
            }
            else
            {
                bool value = Regex.IsMatch(Txtamount.Text, @"^([0]||[1-9][0-9]||[1-9][0-9][0-9]||[1-9][0-9][0-9][0-9]||[1-9][0-9][0-9][0-9][0-9])$");

                if (value)
                {

                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    String query = string.Format("insert into market values('{0}','{1}','{2}','{3}')",
                        Combomanagerid.SelectionBoxItem, Combomanagername.SelectionBoxItem, DtPicker.Text,
                        Txtamount.Text);
                    var connection = new SqlConnection(ConnectionString);

                    try
                    {
                        connection.Open();
                        var cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data is inserted Successfully!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Error in Amount Format");
                }
            }
        }

        private void Btnokay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Combomanagerid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query =
                string.Format("select Marketingdate,Amount from market where Id='" + CombomanagerId1.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            var ds = new DataSet();
            var adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                Listmanager.DataContext = ds.Tables[0].DefaultView; // ListView

                if (ds.Tables[0].DefaultView.Count == 0)
                {
                    MessageBox.Show("Not found!!");
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btnupdate_Click(object sender, RoutedEventArgs e)
        {
            bool value = Regex.IsMatch(Txtamount1.Text, @"^([0]||[1-9][0-9]||[1-9][0-9][0-9]||[1-9][0-9][0-9][0-9]||[1-9][0-9][0-9][0-9][0-9])$");

            if (value)
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("update  market  set Amount='" + Txtamount1.Text + "'  where Id='" +
                                  CombomanagerId2.SelectionBoxItem + "'");
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
            else
            {
                MessageBox.Show("Marketing Expenses must be greater than 9 and lessthan 100000");
            }
        }

        private void Btnokay1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btncalculate_Click(object sender, RoutedEventArgs e)
        {
            Txttotalmeal.Text = TotalMeal().ToString(CultureInfo.InvariantCulture);
            Txtmarketingexpenses.Text = TotalMarketExpense().ToString(CultureInfo.InvariantCulture);
            Txtmealrate.Text = (TotalMarketExpense() / TotalMeal()).ToString(CultureInfo.InvariantCulture);
        }

        private int TotalMarketExpense()
        {
            int market = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select sum(Amount) from market");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    market = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return market;
        }

        private int TotalMeal()
        {
            int meal = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select sum(Noofmeal) from meal");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    meal = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return meal;
        }

        private void Btncheck_Click(object sender, RoutedEventArgs e)
        {
            if (Txtcheckid.Text == string.Empty)
            {
                MessageBox.Show("Enetr Id first to check!!");
            }

            else
            {
                bool value = Regex.IsMatch(Txtcheckid.Text, @"^([0-9]||[1-9][0-9]||[1-9][0-9][0-9]||[1-9][0-9][0-9][0-9])$");

                if (value)
                {
                    if (Txtcheckid.Text==Checkid().ToString(CultureInfo.InvariantCulture))
                    {


                        int totalmoney = InitalPayment();
                        int money = IndivisulsalTotalMeal()*(TotalMarketExpense()/TotalMeal()) - totalmoney;

                        if (money > 0)
                        {
                            MessageBox.Show(" Mr." + Name1() + ",ur total meal is " + IndivisulsalTotalMeal() +
                                            " and u have paid tk." + totalmoney + ",so now u have pay tk." + money +
                                            " more to the manager.");
                        }
                        else
                        {
                            money = money*(-1);
                            MessageBox.Show(" Mr." + Name1() + ",ur total meal is " + IndivisulsalTotalMeal() +
                                            " and u have paid tk." + totalmoney + ",so now u will get back tk." + money +
                                            " from the manager.");
                        }
                    }


                    else
                    {
                        MessageBox.Show("Invalid Id Number");
                    }
                }

                else
                {
                    MessageBox.Show("Error in Id Format");
                }
            }
        }

        private string Name1()
        {

            string name = "";
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Name from boarder where Id='" + Txtcheckid.Text + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name = reader[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return name;
        }

        private int IndivisulsalTotalMeal()
        {
            int meal = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select sum(Noofmeal) from meal where Id='" + Txtcheckid.Text + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    meal = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return meal;
        }

        private int InitalPayment()
        {
            int initialpayment = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select InitialPayment from boarder where Id='" + Txtcheckid.Text + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    initialpayment = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return initialpayment;
        }

        private void GetIdname()
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id,name from manager");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sid = reader[0].ToString();
                    string sname = reader[1].ToString();
                    Combomanagerid.Items.Add(sid);
                    CombomanagerId1.Items.Add(sid);
                    Combomanagername.Items.Add(sname);
                    CombomanagerId2.Items.Add(sid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetIdname();
            DtPicker.Text = DateTime.Today.ToString(CultureInfo.InvariantCulture);
        }

        private void Combomanagerid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Name from manager where Id='" + Combomanagerid.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    Combomanagername.Text = sname;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CombomanagerId2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Marketingdate,Amount from market where Id='" + CombomanagerId2.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string smarketingdate = reader[0].ToString();
                        string samount = reader[1].ToString();
                        DtPicker1.Text = smarketingdate;
                        Txtamount1.Text = samount;
                    }
                }
                else
                {
                    DtPicker1.Text = string.Empty;
                    Txtamount1.Text = string.Empty;
                    MessageBox.Show("Not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int Checkid()
        {
            int id = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from boarder where Id='" + Txtcheckid.Text + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    id = int.Parse(reader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return id;
        }
        

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ti = TC.SelectedItem as TabItem;
            if (ti != null) Title = ti.Header.ToString();
        }

      
    }
}