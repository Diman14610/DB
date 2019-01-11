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

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Update_representative_pac.xaml
    /// </summary>
    public partial class Update_representative_pac : Window
    {
        public Update_representative_pac()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (context_stat_talon context = new context_stat_talon())
            {

                var v = context.Пациент
                         .Where(c => c.C__пациента == Operator.int_x_buff)
                         .Select(d => d)
                         .FirstOrDefault();
                textbox_representive_fname.Text = v.Представитель_пациента_фамилия;
                textbox_representive_lname.Text = v.Представитель_пациента_отчество;
                textbox_representive_name.Text = v.Представитель_пациента_имя;
                combobox_representive_pac.Text = v.Представитель_пациента;
                combobox_representive_pol.Text = v.Пол;
            }
        }

        public static string representive_name = null;
        public static string representive_fname = null;
        public static string representive_lname = null;
        public static string representive_pol = null;
        public static DateTime? representive_date = null;
        public static string representive_pac = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            representive_name = textbox_representive_name.Text;
            representive_fname = textbox_representive_fname.Text;
            representive_lname = textbox_representive_lname.Text;
            representive_pol = combobox_representive_pol.SelectedValue.ToString();
            representive_date = datepicker_representive_happy_of_brith.SelectedDate;
            representive_pac = combobox_representive_pac.SelectedValue.ToString();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //cancel
            Close();
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
