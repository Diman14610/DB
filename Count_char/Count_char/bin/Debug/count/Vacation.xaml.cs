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
    /// Логика взаимодействия для Vacation.xaml
    /// </summary>
    public partial class Vacation : Window
    {
        public Vacation()
        {
            InitializeComponent();
        }

        System.Windows.Forms.ListView listView1 = new System.Windows.Forms.ListView
        {
            Margin = new Padding(10, 10, 10, 0),
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255)
        };

        System.Windows.Forms.ListView listView2 = new System.Windows.Forms.ListView();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region listView1 отображение врачей
            // детальный вид
            listView1.GridLines = true;          // разделители 
            listView1.FullRowSelect = true;
            //listView1.ContextMenuStrip = strip;
            listView1.Sorting = System.Windows.Forms.SortOrder.None;
            listView1.Columns.Add("Ф.И.О.", 400, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Время приема", 170, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Время обеда", 170, System.Windows.Forms.HorizontalAlignment.Left);//2
            listView1.Columns.Add("Телефон", 100, System.Windows.Forms.HorizontalAlignment.Left);//3
            listView1.Columns.Add("Кабинет", 100, System.Windows.Forms.HorizontalAlignment.Left);//4
            listView1.Columns.Add("Специальность", 250, System.Windows.Forms.HorizontalAlignment.Left);//5
            listView1.Columns.Add("Профиль", 500, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView1.View = View.Details;
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {

                foreach (var v in context.Врач)
                {
                    string[] split_work_day_s = v.Начало_рабочего_дня.ToString().Split(':');
                    string[] split_work_day_e = v.Конец_рабочего_дня.ToString().Split(':');
                    string[] split_dinner_b = v.Начало_перерыва.ToString().Split(':');
                    string[] split_dinner_e = v.Конец_перерыва.ToString().Split(':');
                    listView1.Items.Add(v.Фамилия_врача + " " + v.Имя_врача + " " + v.Отчество_врача);
                    listView1.Items[i].SubItems.Add(split_work_day_s[0] + ":" + split_work_day_s[1] + "—" + split_work_day_e[0] + ":" + split_work_day_e[1]);
                    listView1.Items[i].SubItems.Add(split_dinner_b[0] + ":" + split_dinner_b[1] + "—" + split_dinner_e[0] + ":" + split_dinner_e[1]);
                    listView1.Items[i].SubItems.Add(v.Телефон);
                    listView1.Items[i].SubItems.Add(v.Кабинет);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                    i++;
                }
            }
            forms1.Child = listView1;
            #endregion
            // детальный вид
            listView2.GridLines = true;          // разделители 
            listView2.FullRowSelect = true;
            //listView1.ContextMenuStrip = strip;
            listView2.Sorting = System.Windows.Forms.SortOrder.None;
            listView2.Columns.Add("Ф.И.О.", 400, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView2.Columns.Add("Телефон", 100, System.Windows.Forms.HorizontalAlignment.Left);//3
            listView2.Columns.Add("Кабинет", 100, System.Windows.Forms.HorizontalAlignment.Left);//4
            listView2.Columns.Add("Специальность", 250, System.Windows.Forms.HorizontalAlignment.Left);//5
            listView2.Columns.Add("Профиль", 500, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView2.Columns.Add("Отпуск", 500, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView2.View = View.Details;
            int ii = 0;
            using (context_stat_talon context = new context_stat_talon())
            {

                foreach (var v in context.Отпуск.Where(c => c.Конец_отпуска >= DateTime.Now))
                {
                    listView2.Items.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                    listView2.Items[ii].SubItems.Add(v.Врач.Телефон);
                    listView2.Items[ii].SubItems.Add(v.Врач.Кабинет);
                    listView2.Items[ii].SubItems.Add(v.Врач.Cпециальность.Наименование_специальности);
                    listView2.Items[ii].SubItems.Add(v.Врач.Cпециальность.Профиль.Наименование_профиля);
                    listView2.Items[ii].SubItems.Add(v.Начало_отпуска.ToString().Remove(10,8) + " - " + v.Конец_отпуска.ToString().Remove(10, 8));
                    ii++;
                }
            }
            forms1_Copy.Child = listView2;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Профиль)
                    profile.Items.Add(v.Наименование_профиля);
                foreach (var v in context.Cпециальность)
                    specialty.Items.Add(v.Наименование_специальности);
            }
        }
        string fio { get; set; }
        string phone { get; set; }
        string cab { get; set; }
        int tab_number_doc { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                phone = listView1.Items[d].SubItems[3].Text;
                cab = listView1.Items[d].SubItems[4].Text;
                fio = listView1.Items[d].Text;
            }
            string[] array_fio_doc = fio.Split(' ');
            string fn = array_fio_doc[0];
            string n = array_fio_doc[1];
            string ln = array_fio_doc[2];
            using (context_stat_talon context = new context_stat_talon())
            {
                tab_number_doc = context.Врач.FirstOrDefault(c => c.Телефон == phone
                                    && c.Кабинет == cab && c.Фамилия_врача == fn && c.Имя_врача == n && c.Отчество_врача == ln).Табельный_номер_врача;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void update_list(string SPEC, string PROF)
        {
            listView1.Items.Clear();
            if (PROF != null & SPEC != null)
            {
                int i = 0;
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Врач.Where(d => d.Cпециальность.Профиль.Наименование_профиля == PROF & d.Cпециальность.Наименование_специальности == SPEC);
                    foreach (var v in v1)
                    {
                        string[] split_work_day_s = v.Начало_рабочего_дня.ToString().Split(':');
                        string[] split_work_day_e = v.Конец_рабочего_дня.ToString().Split(':');
                        string[] split_dinner_b = v.Начало_перерыва.ToString().Split(':');
                        string[] split_dinner_e = v.Конец_перерыва.ToString().Split(':');
                        listView1.Items.Add(v.Фамилия_врача + " " + v.Имя_врача + " " + v.Отчество_врача);
                        listView1.Items[i].SubItems.Add(split_work_day_s[0] + ":" + split_work_day_s[1] + "—" + split_work_day_e[0] + ":" + split_work_day_e[1]);
                        listView1.Items[i].SubItems.Add(split_dinner_b[0] + ":" + split_dinner_b[1] + "—" + split_dinner_e[0] + ":" + split_dinner_e[1]);
                        listView1.Items[i].SubItems.Add(v.Телефон);
                        listView1.Items[i].SubItems.Add(v.Кабинет);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                        i++;
                    }
                }
            }
            else if (PROF != null)
            {
                int i = 0;
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Врач.Where(d => d.Cпециальность.Профиль.Наименование_профиля == PROF);
                    foreach (var v in v1)
                    {
                        string[] split_work_day_s = v.Начало_рабочего_дня.ToString().Split(':');
                        string[] split_work_day_e = v.Конец_рабочего_дня.ToString().Split(':');
                        string[] split_dinner_b = v.Начало_перерыва.ToString().Split(':');
                        string[] split_dinner_e = v.Конец_перерыва.ToString().Split(':');
                        listView1.Items.Add(v.Фамилия_врача + " " + v.Имя_врача + " " + v.Отчество_врача);
                        listView1.Items[i].SubItems.Add(split_work_day_s[0] + ":" + split_work_day_s[1] + "—" + split_work_day_e[0] + ":" + split_work_day_e[1]);
                        listView1.Items[i].SubItems.Add(split_dinner_b[0] + ":" + split_dinner_b[1] + "—" + split_dinner_e[0] + ":" + split_dinner_e[1]);
                        listView1.Items[i].SubItems.Add(v.Телефон);
                        listView1.Items[i].SubItems.Add(v.Кабинет);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                        i++;
                    }
                }
            }
            else if (SPEC != null)
            {
                int i = 0;
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Врач.Where(d => d.Cпециальность.Наименование_специальности == SPEC);
                    foreach (var v in v1)
                    {
                        string[] split_work_day_s = v.Начало_рабочего_дня.ToString().Split(':');
                        string[] split_work_day_e = v.Конец_рабочего_дня.ToString().Split(':');
                        string[] split_dinner_b = v.Начало_перерыва.ToString().Split(':');
                        string[] split_dinner_e = v.Конец_перерыва.ToString().Split(':');
                        listView1.Items.Add(v.Фамилия_врача + " " + v.Имя_врача + " " + v.Отчество_врача);
                        listView1.Items[i].SubItems.Add(split_work_day_s[0] + ":" + split_work_day_s[1] + "—" + split_work_day_e[0] + ":" + split_work_day_e[1]);
                        listView1.Items[i].SubItems.Add(split_dinner_b[0] + ":" + split_dinner_b[1] + "—" + split_dinner_e[0] + ":" + split_dinner_e[1]);
                        listView1.Items[i].SubItems.Add(v.Телефон);
                        listView1.Items[i].SubItems.Add(v.Кабинет);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                        listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                        i++;
                    }
                }
            }
        }

        void update_list2()
        {
            listView2.Items.Clear();
            int ii = 0;
            using (context_stat_talon context = new context_stat_talon())
            {

                foreach (var v in context.Отпуск.Where(c => c.Конец_отпуска >= DateTime.Now))
                {
                    listView2.Items.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                    listView2.Items[ii].SubItems.Add(v.Врач.Телефон);
                    listView2.Items[ii].SubItems.Add(v.Врач.Кабинет);
                    listView2.Items[ii].SubItems.Add(v.Врач.Cпециальность.Наименование_специальности);
                    listView2.Items[ii].SubItems.Add(v.Врач.Cпециальность.Профиль.Наименование_профиля);
                    listView2.Items[ii].SubItems.Add(v.Начало_отпуска.ToString().Remove(10, 8) + " - " + v.Конец_отпуска.ToString().Remove(10, 8));
                    ii++;
                }
            }
        }

        private void specialty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string spec = null, pro = null;
            try
            {
                spec = specialty.SelectedItem.ToString();
                pro = profile.SelectedItem.ToString();

            }
            catch
            {

            }
            update_list(spec, pro);
        }

        private void profile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string spec = null, pro = null;
            try
            {
                spec = specialty.SelectedItem.ToString();
                pro = profile.SelectedItem.ToString();

            }
            catch
            {

            }
            update_list(spec, pro);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Отпуск.Add(new Отпуск
                    {
                        Начало_отпуска = datepicker1.SelectedDate.Value,
                        Конец_отпуска = datepicker2.SelectedDate.Value,
                        Табельный_номер_врача = tab_number_doc
                    });
                    context.SaveChanges();
                }
                Close();
            }
            catch
            {
                System.Windows.MessageBox.Show("Не выбран врач");
            }
          
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView2.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                using (context_stat_talon context = new context_stat_talon())
                {
                    string sss1 = listView2.Items[d].Text.Split(' ')[0], sss2 = listView2.Items[d].Text.Split(' ')[1], sss3 = listView2.Items[d].Text.Split(' ')[2];
                    int v2 = context.Врач.FirstOrDefault(c => c.Фамилия_врача == sss1 && c.Имя_врача == sss2 && c.Отчество_врача == sss3).Табельный_номер_врача;
                    var v1 = context.Отпуск.FirstOrDefault(c => c.Табельный_номер_врача == v2);
                    context.Отпуск.Remove(v1);
                    context.SaveChanges();
                }
            }
            update_list2();
        }
    }
}
