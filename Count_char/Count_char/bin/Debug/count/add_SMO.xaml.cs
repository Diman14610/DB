using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dynamic;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для add_SMO.xaml
    /// </summary>
    public partial class add_SMO : Window
    {
        public add_SMO()
        {
            InitializeComponent();
        }

        private void button_add_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static string smo_name = null;
        public static string smo_ogrn = null;
        public static string smo_adres = null;
        public static string smo_name_dms = null;
        public static string smo_ogrn_dms = null;
        public static string smo_adres_dms = null;
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox_oms.IsChecked.Value)
            {
                if (Checking.cheked_null_and_space(textbox_smo_name) && Checking.cheked_null_and_space(textbox_smo_ogrn) && Checking.cheked_null_and_space(textbox_smo_adres))
                {
                    smo_name = textbox_smo_name.Text;
                    smo_ogrn = textbox_smo_ogrn.Text;
                    smo_adres = textbox_smo_adres.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены");
                }
            }
            else if (checkbox_dms.IsChecked.Value)
            {
                if (Checking.cheked_null_and_space(textbox_smo_name) && Checking.cheked_null_and_space(textbox_smo_ogrn) && Checking.cheked_null_and_space(textbox_smo_adres))
                {
                    smo_name_dms = textbox_smo_name.Text;
                    smo_ogrn_dms = textbox_smo_ogrn.Text;
                    smo_adres_dms = textbox_smo_adres.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены");
                }
            }
        }

        private void textbox_smo_ogrn_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_smo_ogrn.Text = Regex.Replace(textbox_smo_ogrn.Text, @"\D", "");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkbox_oms.IsChecked = true;
        }
    }
}
