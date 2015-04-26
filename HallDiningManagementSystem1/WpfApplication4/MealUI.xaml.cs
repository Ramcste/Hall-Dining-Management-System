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
    ///     Interaction logic for MealUI.xaml
    /// </summary>
    public partial class MealUI
    {
        public MealUI()
        {
            InitializeComponent();

         
        }

        
        

        private void Btnokay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btnupdate_Click(object sender, RoutedEventArgs e)
        {
            int i;
            bool value1 = Regex.IsMatch(Txtmeal.Text, @"^[12]$");

            bool value2 = Regex.IsMatch(Txtpayment1.Text, @"^([0]||[5-9][0-9]||[1-9][0-9][0-9])$");

            if (Combomealtime.SelectionBoxItem.Equals("noon"))
            {
               
                i = 1;
            }
            else
            {
               
               i = 2;
            }

            if (value1 && value2)
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            
                String query =
                    string.Format("update  meal  set Noofmeal='" + Txtmeal.Text + "',Payment='" + Txtpayment1.Text +
                                  "' where Id='" + ComId.SelectionBoxItem + "' and Mealdate='" + Dtpicker1.SelectedDate +
                                  "' and Mealtime='" + i + "'");
               
                int txpayment = int.Parse(Txtpayment1.Text) + GetPayment();
                int tmeal = int.Parse(Txtmeal.Text) + GetMeal1();
                
                string query1 = string.Format("update boarder1 set Initialpayment='"+txpayment +"' , totalmeal='"+tmeal+"'where Id='"+ComId.SelectionBoxItem+"'");
                
                var connection = new SqlConnection(ConnectionString);
                
                try
                {
                    connection.Open();
                    var cmd = new SqlCommand(query, connection);
                   var cmd1 = new SqlCommand(query1, connection);
                     cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
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
                MessageBox.Show("Payment Amount should be grater than 49 and less than 1000 and meal can be 1 or 2 ");
            }
        }

        private void ComboborderId1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query =
                string.Format("select Mealdate,Noofmeal,Payment from meal where Id='" + ComboborderId1.SelectedItem +
                              "'");
            var connection = new SqlConnection(ConnectionString);
            var ds = new DataSet();
            var adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                Listmeal.DataContext = ds.Tables[0].DefaultView; // ListView


                if (ds.Tables[0].DefaultView.Count == 0)
                {
                    MessageBox.Show("U havenot taken any meal!!");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboborderId.Text == string.Empty)
            {
                MessageBox.Show("Select a boarder Id");
            }

            else if (Combotime.Text == string.Empty)
            {
                MessageBox.Show("Select a mealtime");
            }

            else if (TxtNoofmeal.Text == string.Empty)
            {
                MessageBox.Show("Enter Noof meal u want!!");
            }

            else if (Txtpayment.Text == string.Empty)
            {
                MessageBox.Show("Enetr amount u want to pay!!");
            }

            else
            {
                bool value1 = Regex.IsMatch(TxtNoofmeal.Text, @"^[12]$");
                bool value2 = Regex.IsMatch(Txtpayment.Text, @"^([0]||[5-9][0-9]||[1-9][0-9][0-9])$");
                var boarder = new Boarder();

                boarder.Id = int.Parse(ComboborderId.SelectionBoxItem.ToString());
                boarder.MealDate = Dtpicker.Text;
                boarder.Mealtime = Combotime.SelectionBoxItem.ToString();
                boarder.Noofmeal = int.Parse(TxtNoofmeal.Text);
                boarder.Payment = int.Parse(Txtpayment.Text);

                if (GetPayment1() >= GetMinimumAmount())
                {
                    string msg;

                    if (value1 && value2)
                    {
                        if (Combotime.SelectionBoxItem.Equals("noon"))
                        {
                            msg = ComboborderId.SelectionBoxItem + "/" + Dtpicker.Text + "/" +
                                  Combotime.SelectionBoxItem;
                            boarder.Mealtime = 1.ToString(CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            msg = ComboborderId.SelectionBoxItem + "/" + Dtpicker.Text + "/" +
                                  Combotime.SelectionBoxItem;
                            boarder.Mealtime = 2.ToString(CultureInfo.InvariantCulture);
                        }


                        String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                        String query = string.Format("insert into meal values('{0}','{1}','{2}','{3}','{4}','{5}')",
                            boarder.Id,
                            boarder.MealDate, boarder.Mealtime, msg, boarder.Noofmeal, boarder.Payment);

                        var connection = new SqlConnection(ConnectionString);
                        try
                        {
                            connection.Open();
                            var cmd = new SqlCommand(query, connection);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data is inserted Successfully!!");
                            connection.Close();
                        }
                        catch (Exception )
                        {
                            MessageBox.Show("Only onetime meal is given ");
                        }

                        if (Activate())
                        {
                            int totalpayment = boarder.Payment + GetPayment1();
                            int tmeal = int.Parse(TxtNoofmeal.Text) + GetMeal();
                            String query1 =
                                string.Format("update boarder1 set Initialpayment='" + totalpayment + "' , totalmeal='"+tmeal+"' where Id='" +
                                              ComboborderId.SelectionBoxItem + "'");
                            try
                            {
                                connection.Open();
                                var cmd1 = new SqlCommand(query1, connection);
                                cmd1.ExecuteNonQuery();
                                MessageBox.Show("Payment and meal updated ");
                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            connection.Close();
                        }


                    }
                    else
                    {
                        MessageBox.Show(
                            "Payment Amount should be greater than 49 and less than 1000 and meal can be 1 or 2");
                    }
                }

               
            
                else
                {
                    MessageBox.Show("To start meal Pay Minimum amount first!!");
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
                    ComId.Items.Add(sid);
                    ComboborderId.Items.Add(sid);
                    ComboborderId1.Items.Add(sid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetId();
            Dtpicker.Text = DateTime.Today.ToString(CultureInfo.InvariantCulture);
        }


        private void Btnshow_Click(object sender, RoutedEventArgs e)
        {
            string mealtime;

            mealtime = Combomealtime.SelectionBoxItem.Equals("noon") ? 1.ToString(CultureInfo.InvariantCulture) : 2.ToString(CultureInfo.InvariantCulture);


            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Noofmeal,Payment from meal where Id='" + ComId.SelectionBoxItem + "'and Mealtime='" +
                              mealtime + "' and Mealdate='" + Dtpicker1.Text + "'");
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
                        string snoofmeal = reader[0].ToString();
                        string spayment = reader[1].ToString();
                        Txtmeal.Text = snoofmeal;
                        Txtpayment1.Text = spayment;
                    }
                }
                else
                {
                    Txtmeal.Text = string.Empty;
                    Txtpayment1.Text = string.Empty;
                    MessageBox.Show("U have not taken meal at that time!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetPayment()
        {
            int sinitialpayment = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Initialpayment from boarder1 where Id='" + ComId.SelectionBoxItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sinitialpayment = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sinitialpayment;
        }

        private int GetPayment1()
        {
            int sinitialpayment = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Initialpayment from boarder1 where Id='" + ComboborderId.SelectionBoxItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sinitialpayment = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sinitialpayment;
        }

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ti = TC.SelectedItem as TabItem;
            if (ti != null) Title = ti.Header.ToString();
        }

        private int GetMinimumAmount()
        {
            int amount = 0;
       
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
                amount = int.Parse(reader[0].ToString());
                
            }
        }
        
            catch(Exception ex)
{
            MessageBox.Show(ex.Message);
}

            return amount;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {


            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query = string.Format("select Id,Name,Deptname,Initialpayment,totalmeal,totalfeast from boarder1");
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                MealDataGrid.DataContext = ds.Tables[0].DefaultView;

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
  
        }

        private int GetMeal()
        {
            int smeal = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select totalmeal from boarder1 where Id='" + ComboborderId.SelectionBoxItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    smeal = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return smeal;
        }

        private int GetMeal1()
        {
            int smeal = 0;
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select totalmeal from boarder1 where Id='" + ComId.SelectionBoxItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    smeal = int.Parse(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return smeal;
        }

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}