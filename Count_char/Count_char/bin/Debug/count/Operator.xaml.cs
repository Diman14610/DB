using Stat_talon.Выбор_дня_оператор;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Operator.xaml
    /// </summary>
    public partial class Operator : Window
    {
        public Operator()
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
            listView1.View = View.Details;       // детальный вид
            listView1.GridLines = true;          // разделители 
            listView1.FullRowSelect = true;
            //listView1.ContextMenuStrip = strip;
            listView1.Sorting = System.Windows.Forms.SortOrder.None;
            listView1.Columns.Add("Ф.И.О.", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Пол", 70, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Дата рождения", 100, System.Windows.Forms.HorizontalAlignment.Left);//2
            listView1.Columns.Add("Адрес прописки", 250, System.Windows.Forms.HorizontalAlignment.Left);//3
            listView1.Columns.Add("Адрес проживания", 250, System.Windows.Forms.HorizontalAlignment.Left);//4
            listView1.Columns.Add("Паспорт", 300, System.Windows.Forms.HorizontalAlignment.Left);//5
            listView1.Columns.Add("СНИЛС", 250, System.Windows.Forms.HorizontalAlignment.Left);//6
            listView1.Columns.Add("Представитель пациента", 100, System.Windows.Forms.HorizontalAlignment.Left);//7
            listView1.Columns.Add("Ф.И.О. представителя пациента", 300, System.Windows.Forms.HorizontalAlignment.Left);//8
            listView1.Columns.Add("Дата рождения представителя пациента", 100, System.Windows.Forms.HorizontalAlignment.Left);//9
            listView1.Columns.Add("ДМС", 150, System.Windows.Forms.HorizontalAlignment.Left);//10
            listView1.Columns.Add("Место работы/учебы", 150, System.Windows.Forms.HorizontalAlignment.Left);//11
            listView1.Columns.Add("Кем направлен", 100, System.Windows.Forms.HorizontalAlignment.Left);//12
            listView1.Columns.Add("СМО наименование", 200, System.Windows.Forms.HorizontalAlignment.Left);//13
            listView1.Columns.Add("СМО ОГРН", 200, System.Windows.Forms.HorizontalAlignment.Left);//14
            listView1.Columns.Add("СМО адрес", 200, System.Windows.Forms.HorizontalAlignment.Left);//15
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Пациент.Select(c => c);
                foreach (var v in v1)
                {
                    listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                    listView1.Items[i].SubItems.Add(v.Пол);
                    listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                    listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                    listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                    listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                    listView1.Items[i].SubItems.Add(v.СНИЛС);
                    listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                    try
                    {
                        listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                    }
                    catch
                    {
                        listView1.Items[i].SubItems.Add("");
                    }
                    listView1.Items[i].SubItems.Add(v.ДМС);
                    listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                    listView1.Items[i].SubItems.Add(v.Кем_направлен);
                    listView1.Items[i].SubItems.Add(v.СМО_наименование);
                    listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                    listView1.Items[i].SubItems.Add(v.СМО_адрес);
                    i++;
                }
            }
            windows1.Child = listView1;

        }//

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            add_pac a_pac = new add_pac();
            a_pac.Show();
            chekk = true;
            Close();
        }
        public static int? int_x_buff = null;
        public static string snils { get; set; }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                snils = listView1.Items[d].SubItems[6].Text;
            }
            update_pac u_pac = new update_pac();
            u_pac.ShowDialog();
            //try
            //{
            //    listview_buff.SelectedIndex = datagrid_pac.SelectedIndex;
            //    int_x_buff = int.Parse(listview_buff.SelectedValue.ToString());
            //    update_pac u_pac = new update_pac();
            //    u_pac.ShowDialog();
            //    Close();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Не выбрано поле");
            //}
            update_list();
        }

        void update_list()
        {
            listView1.Items.Clear();
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Пациент.Select(c => c);
                foreach (var v in v1)
                {
                    listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                    listView1.Items[i].SubItems.Add(v.Пол);
                    listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                    listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                    listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                    listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                    listView1.Items[i].SubItems.Add(v.СНИЛС);
                    listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                    try
                    {
                        listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                    }
                    catch
                    {
                        listView1.Items[i].SubItems.Add("");
                    }
                    listView1.Items[i].SubItems.Add(v.ДМС);
                    listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                    listView1.Items[i].SubItems.Add(v.Кем_направлен);
                    listView1.Items[i].SubItems.Add(v.СМО_наименование);
                    listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                    listView1.Items[i].SubItems.Add(v.СМО_адрес);
                    i++;
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                snils = listView1.Items[d].SubItems[6].Text;
            }
            using(context_stat_talon context = new context_stat_talon())
            {
                var v1 = context.Пациент.FirstOrDefault(c => c.СНИЛС == snils);
                context.Пациент.Remove(v1);
                context.SaveChanges();
                update_list();
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            print_stat_talon print_Stat_Talon = new print_stat_talon();
            print_Stat_Talon.Show();
        }

        public static int buff_open_print_write { get; set; }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            buff_open_print_write = 1;
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Select_doctor_F write = new Select_doctor_F();
            write.Show();
            chekk = true;
        }

        private void windows1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            buff_open_print_write = 2;
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();

        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            buff_open_print_write = 3;
            Print_write_on_reception print_Write_On_ = new Print_write_on_reception();
            print_Write_On_.ShowDialog();
        }

        bool chekk = false;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (chekk)
            {
               
            }
            else
            {
                MainWindow mainWindow1 = new MainWindow();
                mainWindow1.Visibility = Visibility.Visible;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textbox_filter.Text.Length == 1)
                {
                    listView1.Items.Clear();
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        int i = 0;
                        foreach (var v in context.Пациент)
                        {
                            //System.Windows.MessageBox.Show(v.fname + " " + v.name_ + " " + v.lname);
                            if (Regex.IsMatch(v.Фамилия_пациента[0].ToString(), textbox_filter.Text, RegexOptions.IgnoreCase))
                            {
                                listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                                listView1.Items[i].SubItems.Add(v.Пол);
                                listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                                listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                                listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                                listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                                listView1.Items[i].SubItems.Add(v.СНИЛС);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                                try
                                {
                                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                                }
                                catch { }
                                listView1.Items[i].SubItems.Add(v.ДМС);
                                listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                                listView1.Items[i].SubItems.Add(v.Кем_направлен);
                                listView1.Items[i].SubItems.Add(v.СМО_наименование);
                                listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                                listView1.Items[i].SubItems.Add(v.СМО_адрес);
                                i++;
                            }
                        }
                        //update_listview1(v1);
                    }
                }
                else if (textbox_filter.Text.Length == 2)
                {
                    listView1.Items.Clear();
                    char[] ch = textbox_filter.Text.ToArray();
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        int i = 0;
                        foreach (var v in context.Пациент)
                        {
                            if (Regex.IsMatch(v.Фамилия_пациента[0].ToString(), ch[0].ToString(), RegexOptions.IgnoreCase) && Regex.IsMatch(v.Имя_пациента[0].ToString(), ch[1].ToString(), RegexOptions.IgnoreCase))
                            {
                                listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                                listView1.Items[i].SubItems.Add(v.Пол);
                                listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                                listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                                listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                                listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                                listView1.Items[i].SubItems.Add(v.СНИЛС);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                                try
                                {
                                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                                }
                                catch { }
                                listView1.Items[i].SubItems.Add(v.ДМС);
                                listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                                listView1.Items[i].SubItems.Add(v.Кем_направлен);
                                listView1.Items[i].SubItems.Add(v.СМО_наименование);
                                listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                                listView1.Items[i].SubItems.Add(v.СМО_адрес);
                                i++;
                            }
                        }
                    }
                }
                else if (textbox_filter.Text.Length == 3)
                {
                    listView1.Items.Clear();
                    char[] ch = textbox_filter.Text.ToArray();

                    using (context_stat_talon context = new context_stat_talon())
                    {
                        int i = 0;
                        foreach (var v in context.Пациент)
                        {
                            if (Regex.IsMatch(v.Фамилия_пациента[0].ToString(), ch[0].ToString(), RegexOptions.IgnoreCase) && Regex.IsMatch(v.Имя_пациента[0].ToString(), ch[1].ToString(), RegexOptions.IgnoreCase)
                                && Regex.IsMatch(v.Отчество_пациента[0].ToString(), ch[2].ToString(), RegexOptions.IgnoreCase))
                            {
                                listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                                listView1.Items[i].SubItems.Add(v.Пол);
                                listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                                listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                                listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                                listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                                listView1.Items[i].SubItems.Add(v.СНИЛС);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                                try
                                {
                                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                                }
                                catch { }
                                listView1.Items[i].SubItems.Add(v.ДМС);
                                listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                                listView1.Items[i].SubItems.Add(v.Кем_направлен);
                                listView1.Items[i].SubItems.Add(v.СМО_наименование);
                                listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                                listView1.Items[i].SubItems.Add(v.СМО_адрес);
                                i++;
                            }
                        }
                    }

                }
                else if (textbox_filter.Text.Length == 11)
                {
                    listView1.Items.Clear();
                    char[] ch = textbox_filter.Text.ToArray();
                    using (context_stat_talon context = new context_stat_talon())
                    {

                        var v1 = context.Пациент.Where(c => c.Фамилия_пациента == ch[0].ToString() && c.Имя_пациента == ch[1].ToString() && c.Отчество_пациента == ch[2].ToString()
                        && ch[3].ToString().Insert(1, ch[4].ToString()) == c.Дата_рождения.Value.Day.ToString() && ch[5].ToString().Insert(1, ch[6].ToString()) == c.Дата_рождения.Value.Month.ToString()
                        && ch[7].ToString().Insert(1, ch[8].ToString()).Insert(2, ch[9].ToString()).Insert(3, ch[10].ToString()) == c.Дата_рождения.Value.Year.ToString()).Select(c => c);
                        int i = 0;
                        DateTime dt = new DateTime(int.Parse(ch[7].ToString().Insert(1, ch[8].ToString()).Insert(2, ch[9].ToString()).Insert(3, ch[10].ToString())),
                            int.Parse(ch[5].ToString().Insert(1, ch[6].ToString())), int.Parse(ch[3].ToString().Insert(1, ch[4].ToString())));
                        foreach (var v in context.Пациент)
                        {
                            if (Regex.IsMatch(v.Фамилия_пациента[0].ToString(), ch[0].ToString(), RegexOptions.IgnoreCase) && Regex.IsMatch(v.Имя_пациента[0].ToString(), ch[1].ToString(), RegexOptions.IgnoreCase)
                                && Regex.IsMatch(v.Отчество_пациента[0].ToString(), ch[2].ToString(), RegexOptions.IgnoreCase) &&
                                dt == v.Дата_рождения)
                            {
                                listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                                listView1.Items[i].SubItems.Add(v.Пол);
                                listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                                listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                                listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                                listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                                listView1.Items[i].SubItems.Add(v.СНИЛС);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                                try
                                {
                                    listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                                }
                                catch { }
                                listView1.Items[i].SubItems.Add(v.ДМС);
                                listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                                listView1.Items[i].SubItems.Add(v.Кем_направлен);
                                listView1.Items[i].SubItems.Add(v.СМО_наименование);
                                listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                                listView1.Items[i].SubItems.Add(v.СМО_адрес);
                                i++;
                            }
                        }
                    }
                }
                else if (textbox_filter.Text.Length == 0)
                {
                    listView1.Items.Clear();
                    int i = 0;
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        var v1 = context.Пациент.Select(c => c);
                        foreach (var v in v1)
                        {
                            listView1.Items.Add(v.Фамилия_пациента + " " + v.Имя_пациента + " " + v.Отчество_пациента);
                            listView1.Items[i].SubItems.Add(v.Пол);
                            listView1.Items[i].SubItems.Add(v.Дата_рождения.Value.ToShortDateString());
                            listView1.Items[i].SubItems.Add(v.Адресс_регистрации);
                            listView1.Items[i].SubItems.Add(v.Адресс_фактического_проживания);
                            listView1.Items[i].SubItems.Add(v.Паспорт_серия + " " + v.Паспорт_номер);
                            listView1.Items[i].SubItems.Add(v.СНИЛС);
                            listView1.Items[i].SubItems.Add(v.Представитель_пациента);
                            listView1.Items[i].SubItems.Add(v.Представитель_пациента_фамилия + " " + v.Представитель_пациента_имя + " " + v.Представитель_пациента_отчество);
                            try
                            {
                                listView1.Items[i].SubItems.Add(v.Представитель_пациента_дата_рождения.Value.ToShortDateString());
                            }
                            catch
                            {
                                listView1.Items[i].SubItems.Add("");
                            }
                            listView1.Items[i].SubItems.Add(v.ДМС);
                            listView1.Items[i].SubItems.Add(v.Место_учёбы_работы__должность);
                            listView1.Items[i].SubItems.Add(v.Кем_направлен);
                            listView1.Items[i].SubItems.Add(v.СМО_наименование);
                            listView1.Items[i].SubItems.Add(v.СМО_ОГРН);
                            listView1.Items[i].SubItems.Add(v.СМО_адрес);
                            i++;
                        }
                    }
                }
            }
            catch { }
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Doctor11 doctor11 = new Doctor11();
            doctor11.ShowDialog();
        }
    }
}
