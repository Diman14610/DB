using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Doctor.xaml
    /// </summary>
    public partial class Doctor : Window
    {
        public Doctor()
        {
            InitializeComponent();
        }

        public static Пациент pac { get; set; }
        public static string operator_fio { get; set; }
        public static Авторизация operator_ { get; set; }
        public static Запись_на_прием write_ { get; set; }
        public static int open_talon { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                //System.Windows.MessageBox.Show(listView1.Items[d].SubItems[1].Text + " " + listView1.Items[d].SubItems[2].Text);
                using (context_stat_talon context = new context_stat_talon())
                {
                    DateTime dt1 = DateTime.Parse(listView1.Items[d].SubItems[1].Text);
                    TimeSpan tm1 = TimeSpan.Parse(listView1.Items[d].SubItems[2].Text);
                    //System.Windows.MessageBox.Show(dt1.ToString() + "  " + tm1.ToString());
                    var v1 = context.Запись_на_прием.FirstOrDefault(c => c.Дата_приема == dt1 && c.Время_приема == tm1);
                    //System.Windows.MessageBox.Show(v1.Пациент.Имя_пациента);
                    pac = v1.Пациент;
                    write_ = v1;
                    var v2 = context.Врач.FirstOrDefault(c => c.C__учетной_записи == v1.C__учетной_записи);
                    var v3 = context.Оператор.FirstOrDefault(c => c.C__учетной_записи == v1.C__учетной_записи);
                    if (v2 != null)
                    {
                        operator_fio = v2.Фамилия_врача + " " + v2.Имя_врача + " " + v2.Отчество_врача;
                        operator_ = v2.Авторизация;
                            //v2.Фамилия_врача + " " + v2.Имя_врача + " " + v2.Отчество_врача;
                    }
                    else if(v3 != null)
                    {
                        operator_= v3.Авторизация;
                        operator_fio = v3.Фамилия_оператора + " " + v3.Имя_оператора + " " + v3.Отчество_оператора;
                        //v3.Фамилия_оператора + " " + v3.Имя_оператора + " " + v3.Отчество_оператора;
                    }

                }
                open_talon = 1;
            }
            //ожид закрт талоны
            foreach (var vtt in listView2.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    DateTime dt1 = DateTime.Parse(listView2.Items[d].SubItems[1].Text);
                    TimeSpan tm1 = TimeSpan.Parse(listView2.Items[d].SubItems[2].Text);
                    Запись_на_прием zp1 = context.Запись_на_прием.FirstOrDefault(c => c.Время_приема == tm1 & c.Дата_приема == dt1);
                    var v1 = context.Статистический_талон.FirstOrDefault(c => c.C__записи_на_прием == zp1.C__записи_на_прием);
                    pac = v1.Пациент;
                    var v2 = context.Врач.FirstOrDefault(c => c.C__учетной_записи == v1.C__учетной_записи);
                    var v3 = context.Оператор.FirstOrDefault(c => c.C__учетной_записи == v1.C__учетной_записи);
                    if (v2 != null)
                    {
                        operator_fio = v2.Фамилия_врача + " " + v2.Имя_врача + " " + v2.Отчество_врача;
                        operator_ = v2.Авторизация;
                        //v2.Фамилия_врача + " " + v2.Имя_врача + " " + v2.Отчество_врача;
                    }
                    else if (v3 != null)
                    {
                        operator_ = v3.Авторизация;
                        operator_fio = v3.Фамилия_оператора + " " + v3.Имя_оператора + " " + v3.Отчество_оператора;
                        //v3.Фамилия_оператора + " " + v3.Имя_оператора + " " + v3.Отчество_оператора;
                    }
                }
                open_talon = 2;
       
            }
            add_stat_talon talon = new add_stat_talon();
            talon.Show();
        }

        bool active_element = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            active_element = true;
            Form2 form2 = new Form2();
            form2.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (active_element)
            {

            }
            else
            {
                MainWindow window = new MainWindow();
                window.Visibility = Visibility.Visible;
            }
        }

        System.Windows.Forms.ListView listView1 = new System.Windows.Forms.ListView
        {
            Margin = new Padding(10, 10, 10, 0),
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255)
        };
        System.Windows.Forms.ListView listView2 = new System.Windows.Forms.ListView();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Статистический_талон> list_write = new List<Статистический_талон>();
            listView2.View = View.Details;       // детальный вид
            listView2.GridLines = true;          // разделители 
            listView2.FullRowSelect = true;
            listView2.Sorting = System.Windows.Forms.SortOrder.None;
            listView2.Columns.Add("Ф.И.О. пациента", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView2.Columns.Add("Дата приема", 250, System.Windows.Forms.HorizontalAlignment.Left);//5
            listView2.Columns.Add("Время приема", 250, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView2.Columns.Add("Ф.И.О. врача", 250, System.Windows.Forms.HorizontalAlignment.Left);//3
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {// c.C__учетной_записи == MainWindow.buff_user_id &&
                foreach (var v in context.Статистический_талон.Where(c => c.Дата_окончания_случия == null))
                {
                    list_write.Add(v);
                    try
                    {
                        listView2.Items.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                    }
                    catch
                    {
                        listView2.Items.Add("");
                    }
                    try
                    {
                        listView2.Items[i].SubItems.Add(v.Запись_на_прием.Дата_приема.Value.ToShortDateString());
                    }
                    catch
                    {
                        listView2.Items[i].SubItems.Add("");
                    }
                    try
                    {
                        listView2.Items[i].SubItems.Add(v.Запись_на_прием.Время_приема.Value.ToString().Remove(5, 3));
                    }
                    catch
                    {
                        listView2.Items[i].SubItems.Add("");
                    }
                    try
                    {
                        listView2.Items[i].SubItems.Add(v.Запись_на_прием.Врач.Фамилия_врача + "" + v.Запись_на_прием.Врач.Имя_врача + " " + v.Запись_на_прием.Врач.Отчество_врача);
                    }
                    catch
                    {
                        listView2.Items[i].SubItems.Add("");
                    }
                    i++;
                }
            }
            forms1_Copy.Child = listView2;

            listView1.View = View.Details;       // детальный вид
            listView1.GridLines = true;          // разделители 
            listView1.FullRowSelect = true;
            listView1.Sorting = System.Windows.Forms.SortOrder.None;
            listView1.Columns.Add("Ф.И.О. пациента", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Дата приема", 250, System.Windows.Forms.HorizontalAlignment.Left);//5
            listView1.Columns.Add("Время приема", 250, System.Windows.Forms.HorizontalAlignment.Left);//6

            i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v11 in context.Запись_на_прием.Where(c => c.Табельный_номер_врача == MainWindow.buff_user_tab_number_doctor && c.Дата_приема >= DateTime.Now))
                {
                    listView1.Items.Add(v11.Пациент.Фамилия_пациента + " " + v11.Пациент.Имя_пациента + " " + v11.Пациент.Отчество_пациента);
                    listView1.Items[i].SubItems.Add(v11.Дата_приема.Value.ToShortDateString());
                    listView1.Items[i].SubItems.Add(v11.Время_приема.Value.ToString().Remove(5, 3));
                    i++;
                }
            }
            forms1.Child = listView1;
            //listView1.Items[d].Text;
            //listView1.Items[d].SubItems[1].Text;
            //listView1.Items[d].SubItems[2].Text;
            //listView1.Items[d].SubItems[3].Text;
            List<string> vs111 = new List<string>();
            List<string> vs112 = new List<string>();
            List<string> vs113 = new List<string>();

            //StreamWriter sw1 = new StreamWriter("tt.txt");
            for (int d = 0; d != listView1.Items.Count; d++)
            {
                for (int tt1 = 0; tt1 != listView2.Items.Count;tt1++)
                {
                    //sw1.WriteLine(listView1.Items[d].Text + " " + listView1.Items[d].SubItems[1].Text + " " + listView1.Items[d].SubItems[2].Text + "   -   " 
                    //    + listView2.Items[tt1].Text + " " + listView2.Items[tt1].SubItems[1].Text + " " + listView2.Items[tt1].SubItems[2].Text);
                    if (listView1.Items[d].Text == listView2.Items[tt1].Text 
                        & listView1.Items[d].SubItems[1].Text == listView2.Items[tt1].SubItems[1].Text
                        & listView1.Items[d].SubItems[2].Text == listView2.Items[tt1].SubItems[2].Text
                        )
                    {
                        listView1.Items.Remove(listView1.Items[d]);
                    }
                }
            }
            //sw1.Dispose();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            print_stat_talon talon = new print_stat_talon();
            talon.ShowDialog();
        }
    }
}
