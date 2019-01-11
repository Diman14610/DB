using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamic;
using WordOffice;


namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] dir = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
            string vf = dir.Select(c => c).FirstOrDefault(df => "AEIP.user" == System.IO.Path.GetFileName(df));
            if (vf == null)
                File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\" + "AEIP.user").Dispose();
            else
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\" + "AEIP.user");
                login_.Text = sr.ReadLine();
                password_.Password = sr.ReadLine();
                sr.Dispose();
            }
            
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Авторизация)
                {
                    login_.Items.Add(v.Логин);
                }
            }

           
        }

        public static int? buff_user_tab_number_doctor { get; set; }
        public static int? buff_user_operator { get; set; }
        public static int buff_user_id { get; set; }
        private void enter__Click(object sender, RoutedEventArgs e)
        {
            bool buff_swap = true;

            using (context_stat_talon context = new context_stat_talon())
            {
                StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + "AEIP.user");
                if (Checking.cheked_null_and_space(login_))
                {
                    foreach (var v in context.Авторизация)
                    {
                         if (login_.Text == v.Логин && password_.Password == v.Пароль)
                        {
                            if (v.Роль == "Врач")
                            {
                                buff_swap = false;
                                sw.WriteLine(v.Логин);
                                sw.WriteLine(v.Пароль);
                                buff_user_tab_number_doctor = context.Врач.FirstOrDefault(c => c.C__учетной_записи == v.C__учетной_записи).Табельный_номер_врача;
                                buff_user_id = v.C__учетной_записи;
                                Doctor doc = new Doctor();
                                doc.Show();
                                sw.Dispose();
                                Hide();
                                break;
                                //Close();
                            }
                             if (v.Роль == "Оператор")
                            {
                                sw.WriteLine(v.Логин);
                                sw.WriteLine(v.Пароль);
                                buff_swap = false;
                                buff_user_operator = context.Оператор.FirstOrDefault(c => c.C__учетной_записи == v.C__учетной_записи).C__оператора;
                                buff_user_id = v.C__учетной_записи;
                                Operator ad = new Operator();
                                ad.Show();
                                sw.Dispose();
                                Hide();
                                break;
                                //Close();
                            }
                            if (v.Роль == "Администратор")
                            {
                                buff_swap = false;
                                Admin admin1 = new Admin();
                                admin1.Show();
                                Hide();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Пользователь не выбран");
                }
                if (!Checking.cheked_null_and_space(password_))
                {
                    System.Windows.MessageBox.Show("Пароль не введён");
                }
                else if (buff_swap)
                {
                    System.Windows.MessageBox.Show("Не правильный пароль");
                }
            }
           
        }

        private void close__Click(object sender, RoutedEventArgs e)
        {
            Process pro = Process.GetCurrentProcess();
            pro.Kill();
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                enter__Click(sender,e);
            }
        }
    }
}
