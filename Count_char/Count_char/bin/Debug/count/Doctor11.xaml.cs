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
    /// Логика взаимодействия для Doctor.xaml
    /// </summary>
    public partial class Doctor11 : Window
    {
        public Doctor11()
        {
            InitializeComponent();
        }


        public static int buff_for_update_patient { get; set; }

        List<string> li1 = new List<string>();
        void update_list_view(object sender, EventArgs e)
        {

            li1.Clear();
           if (active_12_day)
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    int ipp = DateTime.DaysInMonth(DateTime.Now.Day, DateTime.Now.Month);//9
                    int ipp1 = ipp - calendar1.SelectedDate.Value.Day;

                    if (ipp1 >= 12)
                    {
                        var v1 = from c in context.Запись_на_прием
                                 where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                                  c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                                  & c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + 12
                                 orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                 select c;
                        foreach (var v in v1)
                        {
                            li1.Add(v.Дата_приема.Value.ToShortDateString());
                        }
                    }
                    else if (ipp1 < 12)
                    {
                        string[] ssssss = new string[] { "" };
                        int avg = 12 - ipp1;//3         ipp1 // 9
                                            //System.Windows.MessageBox.Show(avg.ToString());
                        var v1 = from c in context.Запись_на_прием

                                 where
                                  c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month &
                                  c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + ipp1
                                 orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                 select c;
                        var v2 = from c in context.Запись_на_прием
                                 where
                                  c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month + 1
                                  & c.Дата_приема.Value.Day <= avg & c.Дата_приема.Value.Day >= 1
                                 orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                 select c;

                        List<Запись_на_прием> li1_write = new List<Запись_на_прием>(v1);
                        li1_write.AddRange(v2);
                        foreach (var v in li1_write)
                        {
                            li1.Add(v.Дата_приема.Value.ToShortDateString());
                        }
                    }

                }

                if (li1.Count == listView1.Items.Count & listView1.Items.Count != 0)
                {
                }
                else
                {
                    listView1.Items.Clear();
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        int ipp = DateTime.DaysInMonth(DateTime.Now.Day, DateTime.Now.Month);//9
                        int ipp1 = ipp - calendar1.SelectedDate.Value.Day;


                        if (ipp1 >= 12)
                        {
                            var v1 = from c in context.Запись_на_прием
                                     where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                                      c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                                      & c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + 12
                                     orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                     select c;
                            int pi = 0;
                            foreach (var v in v1)
                            {
                                listView1.Items.Add(v.Время_приема.Value.ToString());
                                listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                                listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                                listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                                pi++;
                            }
                        }
                        else if (ipp1 < 12)
                        {

                            int avg = 12 - ipp1;//3         ipp1 // 9
                                                //System.Windows.MessageBox.Show(avg.ToString());
                            var v1 = from c in context.Запись_на_прием

                                     where
                                      c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month &
                                      c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + ipp1
                                     orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                     select c;
                            var v2 = from c in context.Запись_на_прием
                                     where
                                      c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month + 1
                                      & c.Дата_приема.Value.Day <= avg & c.Дата_приема.Value.Day >= 1
                                     orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                     select c;
                            int pi = 0;
                            List<Запись_на_прием> li1_write = new List<Запись_на_прием>(v1);
                            li1_write.AddRange(v2);

                            foreach (var v in li1_write)
                            {
                                listView1.Items.Add(v.Время_приема.Value.ToString());
                                listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                                listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                                listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                                pi++;
                            }
                        }
                    }
                }
            }
            else
            {
                using (context_stat_talon context = new context_stat_talon())
                {

                        var v1 = from c in context.Запись_на_прием
                                 where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                                  c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                                  & c.Дата_приема.Value.Day == calendar1.SelectedDate.Value.Day
                                 orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                 select c;
                        foreach (var v in v1)
                        {
                            li1.Add(v.Дата_приема.Value.ToShortDateString());
                        }
                }

                if (li1.Count == listView1.Items.Count & listView1.Items.Count != 0)
                {
                }
                else
                {
                    listView1.Items.Clear();
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        var v1 = from c in context.Запись_на_прием
                                 where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                                  c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                                  & c.Дата_приема.Value.Day == calendar1.SelectedDate.Value.Day
                                 orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                                 select c;
                        int pi = 0;

                        foreach (var v in v1)
                        {
                            listView1.Items.Add(v.Время_приема.Value.ToString());
                            listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                            listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                            listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                            pi++;
                        }

                    }
                }
            }

    }

        System.Windows.Forms.ListView listView1 = new System.Windows.Forms.ListView
        {
            Margin = new Padding(10, 10, 10, 0),
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255)
        };

        bool active_12_day = true;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            calendar1.SelectedDate = DateTime.Now;
            System.Windows.Forms.Timer timer = new Timer();
            timer.Interval = 5000;//2000
            timer.Tick += update_list_view;
            timer.Start();
            listView1.View = View.Details;       // детальный вид
            listView1.GridLines = true;
            listView1.TabIndex = 1;
            // разделители 
            listView1.FullRowSelect = true;
            listView1.Font = new System.Drawing.Font("Time new Roman", 18);
            listView1.Sorting = SortOrder.None;
            listView1.Columns.Add("Время приема", 250, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView1.Columns.Add("Дата приема", 150, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Ф.И.О. пациента", 250, System.Windows.Forms.HorizontalAlignment.Left);//2
            listView1.Columns.Add("Ф.И.О. врача", 250, System.Windows.Forms.HorizontalAlignment.Left);//3

            listView1.Items.Clear();
            using (context_stat_talon context = new context_stat_talon())
            {
                int ipp = DateTime.DaysInMonth(DateTime.Now.Day, DateTime.Now.Month);//9
                int ipp1 = ipp - calendar1.SelectedDate.Value.Day;


                if (ipp1 >= 12)
                {
                     var v1 = from c in context.Запись_на_прием
                             where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                              c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                              & c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + 12
                             orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                              select c;
                    int pi = 0;
                    foreach (var v in v1)
                    {
                        listView1.Items.Add(v.Время_приема.Value.ToString());
                        listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                        listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                        listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                        pi++;
                    }
                }
                else if (ipp1 < 12)
                {
                    
                    int avg = 12 - ipp1;//3         ipp1 // 9
                    //System.Windows.MessageBox.Show(avg.ToString());
                    var v1 = from c in context.Запись_на_прием

                             where
                              c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month &
                              c.Дата_приема.Value.Day >= calendar1.SelectedDate.Value.Day & c.Дата_приема.Value.Day <= calendar1.SelectedDate.Value.Day + ipp1
                             orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                             select c;
                    var v2 = from c in context.Запись_на_прием
                             where
                              c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month + 1
                              & c.Дата_приема.Value.Day <= avg & c.Дата_приема.Value.Day >= 1
                             orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                             select c;
                    int pi = 0;
                    List<Запись_на_прием> li1_write = new List<Запись_на_прием>(v1);
                    li1_write.AddRange(v2);
                    
                    foreach (var v in li1_write)
                    {
                        listView1.Items.Add(v.Время_приема.Value.ToString());
                        listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                        listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                        listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                        pi++;
                    }
                }               
            }
            active_12_day = true;
            windowsFormsHost1.Child = listView1;
   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show(listView1.Items[0].SubItems[1].Text);
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                //System.Windows.MessageBox.Show(listView1.Items[d].Text);
                using (context_stat_talon context = new context_stat_talon())
                {
                    DateTime ss = DateTime.Parse(listView1.Items[d].SubItems[1].Text);//дата приема
                    TimeSpan ss1 = TimeSpan.Parse(listView1.Items[d].Text);
                    var v1 = context.Запись_на_прием.FirstOrDefault(c => c.Дата_приема == ss && c.Время_приема == ss1);
                    context.Запись_на_прием.Remove(v1);
                    context.SaveChanges();
                }
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            listView1.Items.Clear();
            using (context_stat_talon context = new context_stat_talon())
            {
                    var v1 = from c in context.Запись_на_прием
                             where /*c.Дата_записи == calendar1.SelectedDate.Value &*/
                              c.Дата_приема.Value.Year == calendar1.SelectedDate.Value.Year & c.Дата_приема.Value.Month == calendar1.SelectedDate.Value.Month
                              & c.Дата_приема.Value.Day == calendar1.SelectedDate.Value.Day
                             orderby c.Дата_приема.Value, c.Табельный_номер_врача ascending
                             select c;
                    int pi = 0;

                foreach (var v in v1)
                {
                    listView1.Items.Add(v.Время_приема.Value.ToString());
                    listView1.Items[pi].SubItems.Add(v.Дата_приема.Value.ToShortDateString());//6
                    listView1.Items[pi].SubItems.Add(v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента);
                    listView1.Items[pi].SubItems.Add(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача);
                    pi++;
                }
                
            }
            active_12_day = false;
        }

    }
}
