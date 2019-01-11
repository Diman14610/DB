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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_profile) & Dynamic.Checking.cheked_null_and_space(textbox_name_profile))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Профиль.Add(new Профиль
                    {
                        Код_профиля = textbox_code_profile.Text,
                        Наименование_профиля = textbox_name_profile.Text
                    });
                    context.SaveChanges();
                }
                update_list();
                textbox_code_profile.Text = "";
                textbox_name_profile.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }

        }

        string profile__ { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox_code_profile_Copy.Text = "";
            textbox_name_profile_Copy.Text = "";
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    string ss = listView1.Items[d].Text;
                    var v1 = context.Профиль.FirstOrDefault(c => c.Код_профиля == ss);
                    //System.Windows.MessageBox.Show(v1.Месяц.ToString() + " " + v1.День.ToString() + " " + v1.Наименование_дня);
                    textbox_code_profile_Copy.Text = v1.Код_профиля;
                    textbox_name_profile_Copy.Text = v1.Наименование_профиля;
                    profile__ = v1.Код_профиля;
                }
            }
            grid_add.Visibility = Visibility.Hidden;
            grid_edit.Visibility = Visibility.Visible;
        }

        void update_list()
        {
            listView1.Items.Clear();
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Профиль)
                {
                    listView1.Items.Add(v.Код_профиля);
                    listView1.Items[i].SubItems.Add(v.Наименование_профиля);
                    i++;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                //System.Windows.MessageBox.Show(listView1.Items[d].Text);
                using (context_stat_talon context = new context_stat_talon())
                {
                    string ss = listView1.Items[d].Text;
                    var v1 = context.Профиль.FirstOrDefault(c => c.Код_профиля == ss);
                    context.Профиль.Remove(v1);
                    context.SaveChanges();
                }
            }
            update_list();
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
            listView1.Columns.Add("Код профиля", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Наименование профиля", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Профиль)
                {
                    listView1.Items.Add(v.Код_профиля);
                    listView1.Items[i].SubItems.Add(v.Наименование_профиля);
                    i++;
                }
            }
            forms1.Child = listView1;
            grid_edit.Visibility = Visibility.Hidden;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_profile_Copy) & Dynamic.Checking.cheked_null_and_space(textbox_name_profile_Copy))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    //System.Windows.MessageBox.Show(textbox_name_day_Copy.Text);
                    var v1 = context.Профиль.FirstOrDefault(c => c.Код_профиля == profile__);
                    v1.Наименование_профиля = textbox_name_profile_Copy.Text;
                    v1.Код_профиля = textbox_code_profile_Copy.Text;
                    context.SaveChanges();
                }
                grid_edit.Visibility = Visibility.Hidden;
                grid_add.Visibility = Visibility.Visible;
                textbox_code_profile_Copy.Text = "";
                textbox_name_profile_Copy.Text = "";
                update_list();
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
            textbox_code_profile.Text = "";
            textbox_name_profile.Text = "";
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            grid_edit.Visibility = Visibility.Hidden;
            grid_add.Visibility = Visibility.Visible;
            textbox_code_profile.Text = "";
            textbox_name_profile.Text = "";
            textbox_code_profile_Copy.Text = "";
            textbox_name_profile_Copy.Text = "";
            update_list();
        }

    }
}
