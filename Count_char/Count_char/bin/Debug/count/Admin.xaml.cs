using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_public_holiday add_Public = new Add_public_holiday();
            add_Public.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Specialty specialty1 = new Specialty();
            specialty1.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Anesthesia anesthesia1 = new Anesthesia();
            anesthesia1.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Nurse nurse1 = new Nurse();
            nurse1.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Operation operation1 = new Operation();
            operation1.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Add_Doctor add_ = new Add_Doctor();
            add_.ShowDialog();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            MKB kB = new MKB();
            kB.ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Amenities amenities1 = new Amenities();
            amenities1.ShowDialog();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Study study1 = new Study();
            study1.ShowDialog();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Profile profile1 = new Profile();
            profile1.ShowDialog();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Vacation vacation1 = new Vacation();
            vacation1.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Visibility = Visibility.Visible;
            //Process pro = Process.GetCurrentProcess();
            //pro.Kill();
        }
    }
}
