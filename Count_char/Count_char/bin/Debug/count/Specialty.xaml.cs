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
    /// Логика взаимодействия для Specialty.xaml
    /// </summary>
    public partial class Specialty : Window
    {
        public Specialty()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_spec) & Dynamic.Checking.cheked_null_and_space(textbox_name_spec))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    string buff_prof = combobox_profile.Text;
                    var v2 = context.Профиль.FirstOrDefault(c => c.Наименование_профиля == buff_prof);
                    var v1 = context.Cпециальность.Add(new Cпециальность
                    {
                        Код_специальности = textbox_code_spec.Text,
                        Наименование_специальности = textbox_name_spec.Text,
                        Профиль = v2

                    });
                    context.SaveChanges();
                }
                update_list();
                textbox_code_spec.Text = "";
                textbox_name_spec.Text = "";
                combobox_profile.Text = "";
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox_code_spec_Copy.Text = "";
            textbox_name_spec_Copy.Text = "";
            combobox_profile_Copy.Text = "";
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    string ss = listView1.Items[d].Text;
                    var v1 = context.Cпециальность.FirstOrDefault(c => c.Код_специальности == ss);
                    //System.Windows.MessageBox.Show(v1.Месяц.ToString() + " " + v1.День.ToString() + " " + v1.Наименование_дня);
                    textbox_code_spec_Copy.Text = v1.Код_специальности;
                    textbox_name_spec_Copy.Text = v1.Наименование_специальности;
                    try
                    {
                        combobox_profile_Copy.Text = v1.Профиль.Наименование_профиля;
                    }
                    catch
                    {
                        combobox_profile_Copy.Text = "";
                    }
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
                foreach (var v in context.Cпециальность)
                {
                    listView1.Items.Add(v.Код_специальности);
                    listView1.Items[i].SubItems.Add(v.Наименование_специальности);
                    try
                    {
                        listView1.Items[i].SubItems.Add(v.Профиль.Наименование_профиля);
                    }
                    catch
                    {
                        listView1.Items[i].SubItems.Add("");
                    }
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
                    var v1 = context.Cпециальность.FirstOrDefault(c => c.Код_специальности == ss);
                    context.Cпециальность.Remove(v1);
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
            listView1.Columns.Add("Код специальности", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Наименование специальности", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Профиль", 300, System.Windows.Forms.HorizontalAlignment.Left);//2
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Cпециальность)
                {
                    listView1.Items.Add(v.Код_специальности);
                    listView1.Items[i].SubItems.Add(v.Наименование_специальности);
                    try
                    {
                        listView1.Items[i].SubItems.Add(v.Профиль.Наименование_профиля);
                    }
                    catch
                    {
                        listView1.Items[i].SubItems.Add("");
                    }
                    i++;
                }
            }
            forms1.Child = listView1;
            grid_edit.Visibility = Visibility.Hidden;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Профиль)
                {
                    combobox_profile.Items.Add(v.Наименование_профиля);
                    combobox_profile_Copy.Items.Add(v.Наименование_профиля);
                }
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Dynamic.Checking.cheked_null_and_space(textbox_code_spec_Copy) & Dynamic.Checking.cheked_null_and_space(textbox_name_spec_Copy))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    //System.Windows.MessageBox.Show(textbox_name_day_Copy.Text);
                    string sds = combobox_profile_Copy.Text;
                    var v1 = context.Cпециальность.FirstOrDefault(c => c.Код_специальности == textbox_code_spec_Copy.Text);
                    v1.Код_специальности = textbox_code_spec_Copy.Text;
                    v1.Наименование_специальности = textbox_name_spec_Copy.Text;
                    try
                    {
                        v1.Код_профиля = context.Профиль.FirstOrDefault(c => c.Наименование_профиля == combobox_profile_Copy.Text).Код_профиля;
                    }
                    catch
                    {
                        v1.Код_профиля = null;
                    }
                    context.SaveChanges();
                }
                grid_edit.Visibility = Visibility.Hidden;
                grid_add.Visibility = Visibility.Visible;
                textbox_code_spec_Copy.Text = "";
                textbox_name_spec_Copy.Text = "";
                combobox_profile_Copy.Text = "";
                update_list();
            }
            else
            {
                System.Windows.MessageBox.Show("Не все поля введены");
                update_list();
            }
            textbox_code_spec.Text = "";
            textbox_name_spec.Text = "";
            combobox_profile.Text = "";
        }



        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            grid_edit.Visibility = Visibility.Hidden;
            grid_add.Visibility = Visibility.Visible;
            textbox_code_spec.Text = "";
            textbox_code_spec_Copy.Text = "";
            textbox_name_spec.Text = "";
            textbox_name_spec_Copy.Text = "";
            combobox_profile.Text = "";
            combobox_profile_Copy.Text = "";
            update_list();
        }

    }
}
