using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for ManagerUI.xaml
    /// </summary>
    public partial class ManagerUI
    {
        public ManagerUI()
        {
            InitializeComponent();
        }

        private void Btnmanagerave_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtmanagernameBox.Text == string.Empty)
            {
                MessageBox.Show("Enter manager name first");
            }

            else if (PasswdBox.Password == string.Empty)
            {
                MessageBox.Show("Enter Password first!!");
            }
            else if (PasswdBox1.Password == string.Empty)
            {
                MessageBox.Show("Enter Password Again To confirm!!");
            }


            else
            {

                //@"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$" 

                bool value1 = Regex.IsMatch(TxtmanagernameBox.Text,
                    @"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$");
                bool value2 = Regex.IsMatch(PasswdBox1.Password, @"^[a-z0-9_-]{6,18}$");
                bool value3 = Regex.IsMatch(PasswdBox.Password, @"^[a-z0-9_-]{6,18}$");

                int id = int.Parse(Txtmanagerid.Text);

                if (value1 && value2 && value3)
                {
                    if (PasswdBox.Password == PasswdBox1.Password)
                    {

                        if (id<= 4)
                        {
                            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                            String query = string.Format("insert into manager values('{0}','{1}','{2}','{3}')",
                                id, TxtmanagernameBox.Text,
                                PasswdBox.Password, Dtstartmanagering);
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
                            MessageBox.Show("Manager Cann't be more than four");
                        }
                    }

                    else
                    {
                        MessageBox.Show("Enter same Password to Proceed");
                    }


                }
                else
                {
                    MessageBox.Show(
                        "Password is mustbe the combination of digits and alphabet and at least 6 length !!");
                }

            }


        }

        private void GetAdminId()
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from manager");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    sid += 1;

                    Txtmanagerid.Text = sid.ToString(CultureInfo.InvariantCulture);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            Dtstartmanagering.Text = DateTime.Today.ToString(CultureInfo.InvariantCulture);
            GetId();
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query = string.Format("select Id,name,passwd from manager");
            var connection = new SqlConnection(ConnectionString);
            var ds = new DataSet();
            var adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                LstManager.DataContext = ds.Tables[0].DefaultView; // ListView
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOkay_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnUpadate_Click(object sender, RoutedEventArgs e)
        {
            if (TxtConfirmpassBox.Password == string.Empty)
            {
                MessageBox.Show("Enter again Password to confirm it");
            }

            else
            {
                if (TxtpassBox.Text == TxtConfirmpassBox.Password)
                {
                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    String query =
                        string.Format("update  manager  set name='" + Txtmanagername.Text + "' , passwd='" +
                                      TxtpassBox.Text +
                                      "' where Id='" + ComboId.SelectedItem + "'");
                    var connection = new SqlConnection(ConnectionString);
                    try
                    {
                        connection.Open();
                        var cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Ur Data is Updated Successfully!!!");
                        ClearValue();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                else
                {
                    MessageBox.Show("Enter again Password to confirm it!!");
                }
            }
        }

        private void ClearValue()
        {
            TxtConfirmpassBox.Password = string.Empty;
        }

        private void ComboId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select name,passwd from manager where Id='" + ComboId.SelectedItem + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    string spasswd = reader[1].ToString();

                    Txtmanagername.Text = sname;
                    TxtpassBox.Text = spasswd;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            Load();

        }

        private void Load()
        {
            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query = string.Format("select Id from manager");
            var connection = new SqlConnection(ConnectionString);


            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sid = reader[0].ToString();
                    ComboId.Items.Add(sid);
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btnokay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnShow_Click(object sender, RoutedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select name from manager where dateofstartingmanagering='" + TxtDate + "'");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    ListManager.Items.Add(sname);
                    //MessageBox.Show(sname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ti = TC.SelectedItem as TabItem;
            if (ti != null) Title = ti.Header.ToString();
        }


        private void EditBatchButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (BatchTextBox.Text == string.Empty)
            {
                MessageBox.Show("Fill  the box!! ");
            }

            else if (!Regex.Match(BatchTextBox.Text, @"^(1st||2nd||3rd||[4-9]th||[12][0-9]th)$").Success)
            {
                MessageBox.Show(
                    "Error in Batch format.Batch should be written as firstnumber and then(st,nd,rdand th)", "Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }



            else if (Activate())
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("update  batch set Batchname='" + BatchTextBox.Text + "' where Id='" +
                                  BatchIdComboBox.SelectionBoxItem + "'");
                SqlConnection connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ur data is Updated!!");
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

            else if (
                !Regex.Match(DepartmentBox.Text,
                    @"^([A-Z]+|[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$")
                    .Success)
            {
                MessageBox.Show(
                    "Error in Department name format.Department Name should be written as (CSTE,Pharmacy and Computer Science and Telecommunication Engineering)",
                    "Message",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }



            else if (Activate())
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query = string.Format("insert into department values('{0}','{1}')", DeptIdTextBox.Text,
                    DepartmentBox.Text);
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
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query = string.Format("insert into batch values('{0}','{1}')", BatchIdBox.Text, BatchBox.Text);
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

        private void EditDepartmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DepartmentBox1.Text == string.Empty)
            {
                MessageBox.Show("Enter Department u want to update");
            }

            else if (
                !Regex.Match(DepartmentBox1.Text,
                    @"^([A-Z]+|[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$")
                    .Success)
            {
                MessageBox.Show("Error In Department Format", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Activate())
            {
                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("update  department  set Departmentname='" + DepartmentBox1.Text + "' where Id='" +
                                  DeptIdCombobox.SelectionBoxItem + "'");
                SqlConnection connection = new SqlConnection(ConnectionString);
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
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

        private void AddAdminButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == string.Empty || AdminPasswordBox.Password == string.Empty ||
                PositionComboBox.Text == string.Empty)
            {
                MessageBox.Show("Enter all the fields and select position!!");
            }

            else if (
                !Regex.Match(NameTextBox.Text,
                    @"^([A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+|[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)

            {
                MessageBox.Show("Error In Admin Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else if (!Regex.Match(AdminPasswordBox.Password, @"^[a-z0-9_-]{6,18}$").Success)
            {
                MessageBox.Show("Error in Password ,Password length must be 6.", "Message", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            else if (PositionComboBox.Text == string.Empty)
            {
                MessageBox.Show("Select a Postion for the Admin");
            }

            else if (Activate())
            {
                if (GetCountAdminId() > 4)
                {
                    String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                    String query = string.Format("insert into admin values('{0}','{1}','{2}','{3}')",
                        AdminIdTextBox.Text, NameTextBox.Text, AdminPasswordBox.Password,
                        PositionComboBox.SelectionBoxItem);
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
                else
                {
                    MessageBox.Show("Admin Can't be more than four!!!");
                }
            }
        }

        private void UpdateAdminButton_OnClick(object sender, RoutedEventArgs e)
        {


            if (
                !Regex.Match(NameTextBox1.Text,
                    @"^([A-Z][a-z]+||[A-Z][a-z]+\s[A-Z][a-z]+||[A-Z][a-z]+\s[A-Z][a-z]+\s[A-Z][a-z]+)$").Success)
            {
                MessageBox.Show("Error In Admin Name", "Message", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else if (!Regex.Match(AdminPasswordBox1.Password, @"^[a-z0-9_-]{6,18}$").Success)
            {
                MessageBox.Show("Error in Password ,Password length must be 6.", "Message", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            else if (!Regex.Match(NewAdminPasswordBox.Password, @"^[a-z0-9_-]{6,18}$").Success)
            {
                MessageBox.Show("Error in Password ,Password length must be 6.", "Message", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }


            else if (Activate())
            {

                String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
                String query =
                    string.Format("update admin set Name='" + NameTextBox1.Text + "' , Password='" +
                                  NewAdminPasswordBox.Password + "' , Position='" + PositionComboBox1.Text +
                                  "' where Id='" + AdminIdComboBox.SelectionBoxItem + "'");
                SqlConnection connection = new SqlConnection(ConnectionString);

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data is updated Successfully!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
        }

        private void OkayButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetId()
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from Admin");
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                var cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int sid = int.Parse(reader[0].ToString());
                    AdminIdComboBox.Items.Add(sid);

                    int id = sid + 1;
                    AdminIdTextBox.Text = id.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetAdminId();
            LoadData();
        }

        private void LoadData()
        {
            String ConnectionString = @"server=Roshani; Database=Dinning; Integrated Security=true";
            String query = string.Format("select * from Admin");
            SqlConnection connection = new SqlConnection(ConnectionString);
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                AdminListView.DataContext = ds.Tables[0].DefaultView; // ListView

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            GetAdminId();
        }

        private void AdminIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Name,Password,Position from admin where Id='" + AdminIdComboBox.SelectedItem + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string sname = reader[0].ToString();
                    string spassword = reader[1].ToString();
                    string sposition = reader[2].ToString();

                    NameTextBox1.Text = sname;
                    AdminPasswordBox1.Password = spassword;
                    PositionComboBox1.Text = sposition;
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private int GetCountManagerId()
        {
            int i = 0;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Count(Id) from manager");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = int.Parse(reader[0].ToString());


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return i;

        }

        private int GetCountAdminId()
        {
            int i = 0;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select count(Id) from admin ");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = int.Parse(reader[0].ToString());


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return i;

        }



        private void ManagerUI_OnClosing(object sender, CancelEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.YesNo,
                MessageBoxImage.Question, MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
        }

        private void GetBatchId()
        {
            int i;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from batch");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = int.Parse(reader[0].ToString());
                    BatchIdComboBox.Items.Add(i);
                    BatchIdBox.Text = (i + 1).ToString(CultureInfo.InvariantCulture);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDepartmentId()
        {
            int i;

            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Id from department");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i = int.Parse(reader[0].ToString());
                    DeptIdCombobox.Items.Add(i);
                    DeptIdTextBox.Text = (i + 1).ToString(CultureInfo.InvariantCulture);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabItem_Loaded_3(object sender, RoutedEventArgs e)
        {
            GetBatchId();
        }

        private void TabItem_Loaded_4(object sender, RoutedEventArgs e)
        {
            GetDepartmentId();
        }

        private void DeptIdCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query =
                string.Format("select Departmentname from department  where Id='" + DeptIdCombobox.SelectedItem + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sdept = reader[0].ToString();

                    DepartmentBox1.Text = sdept;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BatchIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String ConnectionString = @"server=Roshani;Database=Dinning;Integrated Security=true";
            String query = string.Format("select Batchname from batch  where Id='" + BatchIdComboBox.SelectedItem + "'");
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string sbatch = reader[0].ToString();

                    BatchTextBox.Text = sbatch;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}


