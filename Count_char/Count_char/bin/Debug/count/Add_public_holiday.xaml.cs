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
    /// Логика взаимодействия для Add_public_holiday.xaml
    /// </summary>
    public partial class Add_public_holiday : Window
    {
        public Add_public_holiday()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_name_day) & Dynamic.Checking.cheked_null_and_space(combobox_day) & Dynamic.Checking.cheked_null_and_space(combobox_month))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    int m = int.Parse(combobox_month.Text);
                    int d = int.Parse(combobox_day.Text);
                    var v1 = context.Красный_день_календаря.Add(new Красный_день_календаря
                    {
                        Месяц = m,
                        День = d,
                        Наименование_дня = textbox_name_day.Text
                    });
                    context.SaveChanges();
                }
                update_list();
                textbox_name_day.Text = "";
                combobox_day.Text = "";
                combobox_month.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox_name_day.Text = "";
            combobox_day.Text = "";
            combobox_month.Text = "";
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    string ss = listView1.Items[d].Text;
                    var v1 = context.Красный_день_календаря.FirstOrDefault(c => c.Наименование_дня == ss);
                    //System.Windows.MessageBox.Show(v1.Месяц.ToString() + " " + v1.День.ToString() + " " + v1.Наименование_дня);
                    combobox_month_Copy.Text = v1.Месяц.ToString();
                    combobox_day_Copy.Text = v1.День.ToString();
                    textbox_name_day_Copy.Text = v1.Наименование_дня;
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
                foreach (var v in context.Красный_день_календаря)
                {
                    listView1.Items.Add(v.Наименование_дня);
                    listView1.Items[i].SubItems.Add(v.Месяц.ToString());
                    listView1.Items[i].SubItems.Add(v.День.ToString());
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
                    var v1 = context.Красный_день_календаря.FirstOrDefault(c => c.Наименование_дня == ss);
                    context.Красный_день_календаря.Remove(v1);
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
            listView1.Columns.Add("Наименование выходного дня", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Месяц", 70, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("День", 100, System.Windows.Forms.HorizontalAlignment.Left);//2
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Красный_день_календаря)
                {
                    listView1.Items.Add(v.Наименование_дня);
                    listView1.Items[i].SubItems.Add(v.Месяц.ToString());
                    listView1.Items[i].SubItems.Add(v.День.ToString());
                    i++;
                }
            }
            forms1.Child = listView1;
            grid_edit.Visibility = Visibility.Hidden;
        }

        private void combobox_month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                combobox_day.Items.Clear();
                for (int i = 1; i != DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(combobox_month.SelectedValue.ToString())) + 1; i++)
                {
                    combobox_day.Items.Add(i);
                }
            }
            catch
            {

            }
        }

        private void combobox_month_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                combobox_day_Copy.Items.Clear();
                for (int i = 1; i != DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(combobox_month_Copy.SelectedValue.ToString())) + 1; i++)
                {
                    combobox_day_Copy.Items.Add(i);
                }
            }
            catch
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_name_day_Copy) & Dynamic.Checking.cheked_null_and_space(combobox_day_Copy) & Dynamic.Checking.cheked_null_and_space(combobox_month_Copy))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    //System.Windows.MessageBox.Show(textbox_name_day_Copy.Text);
                    string sds = textbox_name_day_Copy.Text;
                    var v1 = context.Красный_день_календаря.FirstOrDefault(c => c.Наименование_дня == sds);
                    int m = int.Parse(combobox_month_Copy.Text);
                    int d = int.Parse(combobox_day_Copy.Text);
                    v1.День = d;
                    v1.Месяц = m;
                    v1.Наименование_дня = textbox_name_day_Copy.Text;
                    context.SaveChanges();
                }
                grid_edit.Visibility = Visibility.Hidden;
                grid_add.Visibility = Visibility.Visible;
                textbox_name_day_Copy.Text = "";
                combobox_day_Copy.Text = "";
                combobox_month_Copy.Text = "";
                update_list();
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
            textbox_name_day.Text = "";
            combobox_day.Text = "";
            combobox_month.Text = "";
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            grid_edit.Visibility = Visibility.Hidden;
            grid_add.Visibility = Visibility.Visible;
            textbox_name_day_Copy.Text = "";
            combobox_day_Copy.Text = "";
            combobox_month_Copy.Text = "";
            textbox_name_day.Text = "";
            combobox_day.Text = "";
            combobox_month.Text = "";
            update_list();
        }
    }
}
