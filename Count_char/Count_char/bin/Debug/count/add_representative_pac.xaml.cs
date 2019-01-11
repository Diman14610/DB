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
    /// Логика взаимодействия для add_representative_pac.xaml
    /// </summary>
    public partial class add_representative_pac : Window
    {
        public add_representative_pac()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public static string representive_name { get; set; }
        public static string representive_fname { get; set; }
        public static string representive_lname { get; set; }
        public static string representive_pol { get; set; }
        public static DateTime? representive_date { get; set; }
        public static string representive_pac { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Checking.cheked_null_and_space(textbox_representive_fname) && Checking.cheked_null_and_space(textbox_representive_lname)
                && Checking.cheked_null_and_space(textbox_representive_name) && Checking.cheked_null_and_space(datepicker_representive_happy_of_brith) &&
                Checking.cheked_null_and_space(combobox_representive_pac) && Checking.cheked_null_and_space(combobox_representive_pol))
            {
                representive_name = textbox_representive_name.Text;
                representive_fname = textbox_representive_fname.Text;
                representive_lname = textbox_representive_lname.Text;
                representive_pol = combobox_representive_pol.SelectedValue.ToString();
                representive_date = datepicker_representive_happy_of_brith.SelectedDate.Value;
                representive_pac = combobox_representive_pac.SelectedValue.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void textbox_representive_fname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_representive_fname.Text = Regex.Replace(textbox_representive_fname.Text, @"\d", "");
        }

        private void textbox_representive_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_representive_name.Text = Regex.Replace(textbox_representive_name.Text, @"\d", "");
        }

        private void textbox_representive_lname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_representive_lname.Text = Regex.Replace(textbox_representive_lname.Text, @"\d", "");
        }
    }
}
