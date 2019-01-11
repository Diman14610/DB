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
    /// Логика взаимодействия для Anesthesia.xaml
    /// </summary>
    public partial class Anesthesia : Window
    {
        public Anesthesia()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_an) & Dynamic.Checking.cheked_null_and_space(textbox_name_an))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Анестезия.Add(new Анестезия
                    {
                        Код_анестезии = textbox_code_an.Text,
                        Наименование_анестезии = textbox_name_an.Text
                    });
                    context.SaveChanges();
                }
                update_list();
                textbox_code_an.Text = "";
                textbox_name_an.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }

        }

        string an_ { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox_code_an_Copy.Text = "";
            textbox_name_an_Copy.Text = "";
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    string ss = listView1.Items[d].Text;
                    var v1 = context.Анестезия.FirstOrDefault(c => c.Код_анестезии == ss);
                    //System.Windows.MessageBox.Show(v1.Месяц.ToString() + " " + v1.День.ToString() + " " + v1.Наименование_дня);
                    textbox_code_an_Copy.Text = v1.Код_анестезии;
                    textbox_name_an_Copy.Text = v1.Наименование_анестезии;
                    an_ = v1.Код_анестезии;
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
                foreach (var v in context.Анестезия)
                {
                    listView1.Items.Add(v.Код_анестезии);
                    listView1.Items[i].SubItems.Add(v.Наименование_анестезии);
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
                    var v1 = context.Анестезия.FirstOrDefault(c => c.Код_анестезии == ss);
                    context.Анестезия.Remove(v1);
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
            listView1.Columns.Add("Код анестезии", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Наименование анестезии", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Анестезия)
                {
                    listView1.Items.Add(v.Код_анестезии);
                    listView1.Items[i].SubItems.Add(v.Наименование_анестезии);
                    i++;
                }
            }
            forms1.Child = listView1;
            grid_edit.Visibility = Visibility.Hidden;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_an_Copy) & Dynamic.Checking.cheked_null_and_space(textbox_name_an_Copy))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    //System.Windows.MessageBox.Show(textbox_name_day_Copy.Text);
                    var v1 = context.Анестезия.FirstOrDefault(c => c.Код_анестезии == an_);
                    context.Анестезия.Remove(v1);
                    var v2 = context.Анестезия.Add(new Анестезия
                    {
                        Код_анестезии = textbox_code_an_Copy.Text,
                        Наименование_анестезии = textbox_name_an_Copy.Text
                    });
                    context.SaveChanges();
                }
                grid_edit.Visibility = Visibility.Hidden;
                grid_add.Visibility = Visibility.Visible;
                textbox_code_an_Copy.Text = "";
                textbox_name_an_Copy.Text = "";
                update_list();
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
            textbox_code_an.Text = "";
            textbox_name_an.Text = "";
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            grid_edit.Visibility = Visibility.Hidden;
            grid_add.Visibility = Visibility.Visible;
            textbox_code_an_Copy.Text = "";
            textbox_name_an_Copy.Text = "";
            textbox_code_an.Text = "";
            textbox_name_an.Text = "";
            update_list();
        }


    }
}
