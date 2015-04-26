using System.ComponentModel;
using System.Windows;

namespace WpfApplication4
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       // string dataconnection = ConfigurationManager.ConnectionStrings[WpfApplication4.Properties.Settings.NHDMSDBConnectionString].ConnectionString;


        private void StudentItem_OnClick(object sender, RoutedEventArgs e)
        {
            var aStudentUI = new StudentUI();
            aStudentUI.ShowDialog();
        }


        private void MealEntryItem_OnClick(object sender, RoutedEventArgs e)
        {
            var meal = new MealUI();
            meal.ShowDialog();
        }


        private void FeastItem_OnClick(object sender, RoutedEventArgs e)
        {
          //  this.NavigationService.Navigate(new Uri("FeastUI.xaml",UriKind.Relative));
           FeastUI mUI=new FeastUI();
            mUI.ShowDialog();
        }


        private void CalculationItem_OnClick(object sender, RoutedEventArgs e)
        {
            var mCalculationUi = new CalculationUI();
            mCalculationUi.ShowDialog();
        }

        private void BlackListItem_OnClick(object sender, RoutedEventArgs e)
        {
            var mBlaclkListUI = new BlackListUI();
            mBlaclkListUI.ShowDialog();
        }

        private void AboutDmsItem_OnClick(object sender, RoutedEventArgs e)
        {
            var abouUi = new AboutUi();
            abouUi.ShowDialog();

        }


        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            MessageBoxResult key = MessageBox.Show("Are you sure you want to quit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
        }

      

       
    }
}