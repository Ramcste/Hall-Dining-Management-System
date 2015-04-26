using System.Windows;

namespace WpfApplication4
{
    /// <summary>
    /// Interaction logic for AboutUi.xaml
    /// </summary>
    public partial class AboutUi
    {
        public AboutUi()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           Close();
        }
    }
}
