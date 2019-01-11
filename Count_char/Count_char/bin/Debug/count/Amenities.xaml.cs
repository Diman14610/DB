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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Amenities.xaml
    /// </summary>
    public partial class Amenities : Window
    {
        public Amenities()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_amen) & Dynamic.Checking.cheked_null_and_space(textbox_name_amen))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Услуга.Add(new Услуга
                    {
                        Код_услуги = int.Parse(textbox_code_amen.Text),
                        Наименование_услуги = textbox_name_amen.Text,
                        Цена_услуги = decimal.Parse(textbox_price_rub.Text + ',' + decimal.Parse(textbox_price_cop.Text))
                    });
                    context.SaveChanges();
                }
                update_list();
                textbox_price_rub.Text = "";
                textbox_price_cop.Text = "";
                textbox_code_amen.Text = "";
                textbox_name_amen.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }

        }

        int amen_ { get; set; }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox_code_amen_Copy.Text = "";
            textbox_name_amen_Copy.Text = "";
            textbox_price_rub_Copy.Text = "";
            textbox_price_cop_Copy.Text = "";
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    int ss = int.Parse(listView1.Items[d].Text);
                    var v1 = context.Услуга.FirstOrDefault(c => c.Код_услуги == ss);
                    //System.Windows.MessageBox.Show(v1.Месяц.ToString() + " " + v1.День.ToString() + " " + v1.Наименование_дня);
                    textbox_code_amen_Copy.Text = ss.ToString();
                    textbox_name_amen_Copy.Text = v1.Наименование_услуги;
                    textbox_price_rub_Copy.Text = v1.Цена_услуги.ToString().Split(',')[0];
                    textbox_price_cop_Copy.Text = v1.Цена_услуги.ToString().Split(',')[1];
                    amen_ = ss;
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
                foreach (var v in context.Услуга)
                {
                    listView1.Items.Add(v.Код_услуги.ToString());
                    listView1.Items[i].SubItems.Add(v.Наименование_услуги);
                    listView1.Items[i].SubItems.Add(v.Цена_услуги.ToString());
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
                    int ss = int.Parse(listView1.Items[d].Text);
                    var v1 = context.Услуга.FirstOrDefault(c => c.Код_услуги == ss);
                    context.Услуга.Remove(v1);
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
            listView1.Columns.Add("Код услуги", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Наименование услуги", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Цена услуги", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Услуга)
                {
                    listView1.Items.Add(v.Код_услуги.ToString());
                    listView1.Items[i].SubItems.Add(v.Наименование_услуги);
                    listView1.Items[i].SubItems.Add(v.Цена_услуги.ToString());
                    i++;
                }
            }
            forms1.Child = listView1;
            grid_edit.Visibility = Visibility.Hidden;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_amen_Copy) & Dynamic.Checking.cheked_null_and_space(textbox_name_amen_Copy) & Dynamic.Checking.cheked_null_and_space(textbox_price_rub_Copy))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    //System.Windows.MessageBox.Show(textbox_name_day_Copy.Text);
                    var v1 = context.Услуга.FirstOrDefault(c => c.Код_услуги == amen_);
                    context.Услуга.Remove(v1);
                    decimal dec = decimal.Parse(textbox_price_rub_Copy.Text + ',' + decimal.Parse(textbox_price_cop_Copy.Text));
                    int irr = int.Parse(textbox_code_amen_Copy.Text);
                    var v2 = context.Услуга.Add(new Услуга
                    {
                        Код_услуги = irr,
                        Наименование_услуги = textbox_name_amen_Copy.Text,
                        Цена_услуги = dec
                    });
                    context.SaveChanges();
                }
                grid_edit.Visibility = Visibility.Hidden;
                grid_add.Visibility = Visibility.Visible;
                textbox_code_amen_Copy.Text = "";
                textbox_name_amen_Copy.Text = "";
                textbox_price_cop_Copy.Text = "";
                textbox_price_rub_Copy.Text = "";
                update_list();
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
            textbox_code_amen.Text = "";
            textbox_name_amen.Text = "";
            textbox_price_rub.Text = "";
            textbox_price_cop.Text = "";
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            grid_edit.Visibility = Visibility.Hidden;
            grid_add.Visibility = Visibility.Visible;
            textbox_code_amen.Text = "";
            textbox_name_amen.Text = "";
            textbox_price_rub.Text = "";
            textbox_price_cop.Text = "";
            textbox_code_amen_Copy.Text = "";
            textbox_name_amen_Copy.Text = "";
            textbox_price_cop_Copy.Text = "";
            textbox_price_rub_Copy.Text = "";
            update_list();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_price_rub.Text = Regex.Replace(textbox_price_rub.Text, @"\D", "");
            textbox_price_rub_Copy.Text = Regex.Replace(textbox_price_rub_Copy.Text, @"\D", "");
        }

        private void textbox_code_amen_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_code_amen.Text = Regex.Replace(textbox_code_amen.Text, @"\D", "");
        }

        private void textbox_code_amen_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_code_amen_Copy.Text = Regex.Replace(textbox_code_amen_Copy.Text, @"\D", "");
        }
    }
}
