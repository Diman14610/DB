using Stat_talon.Выбор_дня_оператор;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Write_reception_F.xaml
    /// </summary>
    public partial class Write_reception_F : Window
    {
        public Write_reception_F()
        {
            InitializeComponent();
        }
        static int one = 775;
        string time_speek()
        {
            string rto = one.ToString();
            one += 25;
            if (one.ToString().Remove(0, rto.Length - 2) == "60")
            {
                one += 40;
                int ctor = one.ToString().Replace("0", "").Length;

                if (ctor == 1)
                {
                    return one.ToString().Remove(1, one.ToString().Length - 1) + "0:00";
                }
                if (ctor == 2)
                {
                    return one.ToString().Remove(2, one.ToString().Length - 2) + ":00";
                }

                return one.ToString();
            }
            else
            {
                int tor = one.ToString().Length;
                if (tor == 4)
                {
                    string buff_1 = one.ToString().Remove(2, one.ToString().Length - 2);//1150 - 4
                    string buff_2 = one.ToString().Remove(0, one.ToString().Length - 2);
                    return buff_1 + ":" + buff_2;
                }
                if (tor == 3)
                {
                    string buff_1 = one.ToString().Remove(1, one.ToString().Length - 1);//915 1
                    string buff_2 = one.ToString().Remove(0, one.ToString().Length - 2);//915
                    return "0" + buff_1 + ":" + buff_2;
                }
                return one.ToString();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            label_doctor.Content = Select_doctor_F.fio;
            one = 775;
            Thickness[] thickness_array = new Thickness[]
            {
                new Thickness(26,295,0,0),//279 // 16
                new Thickness(190,295,0,0),
                new Thickness(358,295,0,0),
                new Thickness(26,375,0,0),
                new Thickness(190,375,0,0),
                new Thickness(360,375,0,0),//10
                new Thickness(28,469,0,0),
                new Thickness(190,469,0,0),
                new Thickness(360,469,0,0),
                new Thickness(28,571,0,0),
                new Thickness(537,295,0,0),
                new Thickness(701,295,0,0),
                new Thickness(869,295,0,0),
                new Thickness(537,469,0,0),
                new Thickness(701,469,0,0),//<-
                new Thickness(869,469,0,0),
                new Thickness(539,560,0,0),
                new Thickness(701,560,0,0)
            };
            List<Запись_на_прием> timeSpans = new List<Запись_на_прием>();
            using (context_stat_talon context = new context_stat_talon())
            {
                switch (Form2_.buff_int_switch)
                {
                    case 1:
                        {
                            var v1 = context.Запись_на_прием.Where(d => d.Дата_приема == Form2_.button_click_buff_day && d.Табельный_номер_врача == Select_doctor_F.tab_number_doc);
                            timeSpans.AddRange(v1);
                            break;
                        }
                    case 2:
                        {
                            var v1 = context.Запись_на_прием.Where(d => d.Дата_приема == Form2_.buff_next_month && d.Табельный_номер_врача == Select_doctor_F.tab_number_doc);
                            timeSpans.AddRange(v1);
                            break;
                        }
                    case 3:
                        {
                            var v1 = context.Запись_на_прием.Where(d => d.Дата_приема == Form2_.buff_next_years && d.Табельный_номер_врача == Select_doctor_F.tab_number_doc);
                            timeSpans.AddRange(v1);
                            break;
                        }
                }
            }

            for (int i = 0; i != 17; i++)
            {
                RadioButton rb = new RadioButton();
                rb.FontSize = 42;
                rb.HorizontalAlignment = HorizontalAlignment.Left;
                rb.VerticalAlignment = VerticalAlignment.Top;
                rb.Content = time_speek();
                rb.Width = 250;
                rb.Foreground = Brushes.White;
                rb.Height = 50;
                rb.Margin = thickness_array[i];
                rb.Click += new RoutedEventHandler(radiobattion_click);
                foreach (var v in timeSpans)
                {
                    if (rb.Content.ToString() == v.Время_приема.ToString().Remove(v.Время_приема.ToString().Length - 3, 3))
                    {
                        rb.Foreground = Brushes.Red;
                        rb.IsEnabled = false;
                    }
                }
                grid1.Children.Add(rb);
            }

            //using(context_stat_talon context = new context_stat_talon())
            //{
            //    foreach (var v in context.doctor)
            //        doctor.Items.Add( v.fname + " " + v.name_ + " " + v.lname + " Кабинет: " + v.cabinet + " Специальность: " + v.specialty.name_specialty + " Профиль: " + v.specialty.profile.name_profile);
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Select_doctor doc = new Select_doctor();
            doc.ShowDialog();
        }

        string buff_radio_butt_click { get; set; }

        private void radiobattion_click(object sender, RoutedEventArgs e)
        {
            buff_radio_butt_click = Regex.Replace(sender.ToString(), @"\D[^:1234567890]", "").Remove(0, 2).Replace("e", "");
            buff_radio_butt_click += ":00";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Select_doctor.fio);//Select_doctor.fio;
            //MessageBox.Show(Select_doctor.phone);//Select_doctor.phone;
            //MessageBox.Show(Select_doctor.cab);//Select_doctor.cab;
            //MessageBox.Show(Select_patient.snils);//Select_patient.snils
                using (context_stat_talon context = new context_stat_talon())
                {
                    switch (Form2_.buff_int_switch)
                    {
                        case 1:
                            {
                                Запись_на_прием pr = context.Запись_на_прием.Add(new Запись_на_прием
                                {
                                    Дата_приема = Form2_.button_click_buff_day.Date,
                                    Время_приема = TimeSpan.Parse(buff_radio_butt_click),
                                    C__учетной_записи = MainWindow.buff_user_id,
                                    Табельный_номер_врача = Select_doctor_F.tab_number_doc,
                                    C__пациента = context.Пациент.FirstOrDefault(d => d.СНИЛС == Select_patient.snils).C__пациента
                                });//08:00:00
                                break;
                            }
                        case 2:
                            {
                                Запись_на_прием pr = context.Запись_на_прием.Add(new Запись_на_прием
                                {
                                    Дата_приема = Form2_.buff_next_month.Date,
                                    Время_приема = TimeSpan.Parse(buff_radio_butt_click),
                                    C__учетной_записи = MainWindow.buff_user_id,
                                    Табельный_номер_врача = Select_doctor_F.tab_number_doc,
                                    C__пациента = context.Пациент.FirstOrDefault(d => d.СНИЛС == Select_patient.snils).C__пациента
                                });
                                break;
                            }
                        case 3:
                            {
                            Запись_на_прием pr = context.Запись_на_прием.Add(new Запись_на_прием
                            {
                                    Дата_приема = Form2_.buff_next_years.Date,
                                    Время_приема = TimeSpan.Parse(buff_radio_butt_click),
                                    C__учетной_записи = MainWindow.buff_user_id,
                                    Табельный_номер_врача = Select_doctor_F.tab_number_doc,
                                    C__пациента = context.Пациент.FirstOrDefault(d => d.СНИЛС == Select_patient.snils).C__пациента
                                });
                                break;
                            }
                    }
                    context.SaveChanges();
                }
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Select_patient select_Patient = new Select_patient();
            select_Patient.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MainWindow.buff_user_operator == null)
            {
                Doctor doctor2 = new Doctor();
                doctor2.Show();
                //Close();
            }
            else if (MainWindow.buff_user_tab_number_doctor == null)
            {

            }
        }
    }
}
