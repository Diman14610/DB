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
using Dynamic;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Select_doctor.xaml
    /// </summary>
    public partial class Select_doctor : Window
    {
        public Select_doctor()
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
            using(context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Профиль)
                    profile.Items.Add(v.Наименование_профиля);
                foreach (var v in context.Cпециальность)
                    specialty.Items.Add(v.Наименование_специальности);
            }
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
                    var v1 = context.Врач.Where(d => d.Cпециальность.Профиль.Код_профиля == PROF);
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
                    var v1 = context.Врач.Where(d => d. Cпециальность.Наименование_специальности == SPEC);
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
                 spec =  specialty.SelectedItem.ToString();
                 pro = profile.SelectedItem.ToString();
           
            }
            catch
            {

            }
            update_list(spec, pro);
        }

        public static string fio { get; set; }
        public static string phone { get; set; }
        public static string cab { get; set; }
        public static Врач doctor_s { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                phone = listView1.Items[d].SubItems[3].Text;
                cab = listView1.Items[d].SubItems[4].Text;
                fio = listView1.Items[d].Text;
            }
            using (context_stat_talon context = new context_stat_talon())
            {
                string fn = fio.Split(' ')[0], n = fio.Split(' ')[1], ln = fio.Split(' ')[2];
                doctor_s = context.Врач.FirstOrDefault(c => c.Телефон == phone & c.Кабинет == cab & c.Фамилия_врача == fn & c.Имя_врача == n & c.Отчество_врача == ln);
            }
                Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
