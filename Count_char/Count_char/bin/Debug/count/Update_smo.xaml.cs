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
    /// Логика взаимодействия для Update_smo.xaml
    /// </summary>
    public partial class Update_smo : Window
    {
        public Update_smo()
        {
            InitializeComponent();
        }

        public static string smo_name = null;
        public static string smo_ogrn = null;
        public static string smo_adres = null;
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (dd.ДМС == null)
            {
                smo_name = textbox_smo_name.Text;
                smo_ogrn = textbox_smo_ogrn.Text;
                smo_adres = textbox_smo_adres.Text;
                Close();
            }
            else if(dd.Полис_ОМС == null)
            {
                smo_name = textbox_smo_name.Text;
                smo_ogrn = textbox_smo_ogrn.Text;
                smo_adres = textbox_smo_adres.Text;
            }
        }

        private void button_add_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Пациент dd { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dd = new Пациент();
            using (context_stat_talon context = new context_stat_talon())
            {
                var v = context.Пациент.FirstOrDefault(c => c.C__пациента == Operator.int_x_buff);
                dd = v;
                if (v.ДМС == null)
                {
                    textbox_smo_adres.Text = v.СМО_адрес;
                    textbox_smo_name.Text = v.СМО_наименование;
                    textbox_smo_ogrn.Text = v.СМО_ОГРН;
                }
                else if (v.Полис_ОМС == null)
                {
                    textbox_smo_adres.Text = v.СМО_адрес;
                    textbox_smo_name.Text = v.СМО_наименование;
                    textbox_smo_ogrn.Text = v.СМО_ОГРН;
                }
            }
        }

        private void textbox_smo_ogrn_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_smo_ogrn.Text = Regex.Replace(textbox_smo_ogrn.Text, @"\D", "");
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
