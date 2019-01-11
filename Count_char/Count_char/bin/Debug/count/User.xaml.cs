using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        System.Windows.Forms.ListView listView1 = new System.Windows.Forms.ListView
        {
            Margin = new Padding(10, 10, 10, 0),
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255)
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listView1.View = View.Details;       // детальный вид
            listView1.GridLines = true;          // разделители 
            listView1.FullRowSelect = true;
            //listView1.ContextMenuStrip = strip;
            listView1.Sorting = System.Windows.Forms.SortOrder.None;
            listView1.Columns.Add("Логин", 250, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Пароль", 150, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Роль", 200, System.Windows.Forms.HorizontalAlignment.Left);

            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Авторизация.Select(c => c);
                foreach (var v in v1)
                {
                    listView1.Items.Add(v.Логин);
                    listView1.Items[i].SubItems.Add(v.Пароль);
                    listView1.Items[i].SubItems.Add(v.Роль);             
                    i++;
                }
            }
            forms1.Child = listView1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Авторизация.Add(new Авторизация
                {
                    Логин = textbox_login.Text,
                    Роль = combobox_role.SelectedValue.ToString(),
                    Пароль = passwordbox_pas.Password
                });
                context.SaveChanges();
            }
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string sn = null;
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                sn = listView1.Items[d].Text;
            }
            using(context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Авторизация.FirstOrDefault(s => s.Логин == sn);
                context.Авторизация.Remove(v1);
                context.SaveChanges();
            }
            Close();
        }


        private void combobox_month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }

    }
}
