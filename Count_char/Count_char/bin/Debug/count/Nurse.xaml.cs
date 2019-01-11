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
    /// Логика взаимодействия для Nurse.xaml
    /// </summary>
    public partial class Nurse : Window
    {
        public Nurse()
        {
            InitializeComponent();
        }



        System.Windows.Forms.ListView listView1 = new System.Windows.Forms.ListView
        {
            Margin = new Padding(10, 10, 10, 0),
            BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255)
        };

        void update_list()
        {
            listView1.Items.Clear();
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Медсестра)
                {
                    listView1.Items.Add(v.Фамилия_медсестры + " " + v.Имя__медсестры + " " + v.Отчество__медсестры);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                    //listView1.Items[i].SubItems.Add(v.Кабинет);
                    listView1.Items[i].SubItems.Add(v.Телефон);
                    listView1.Items[i].SubItems.Add(v.Начало_рабочего_дня.ToString().Remove(5, 3) + " - " + v.Конец_рабочего_дня.ToString().Remove(5, 3));
                    //listView1.Items[i].SubItems.Add(v.Начало_перерыва.ToString().Remove(5, 3) + " - " + v.Конец_перерыва.ToString().Remove(5, 3));
                    //listView1.Items[i].SubItems.Add(v.Авторизация.Логин);
                    i++;
                }
            }
        }

        void update_combobox()
        {
            combobox_spec.Items.Clear();
            combobox_profile.Items.Clear();
            combobox_spec_Copy.Items.Clear();
            combobox_profile_Copy.Items.Clear();
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Cпециальность)
                    combobox_spec.Items.Add(v.Наименование_специальности);

                foreach (var v in context.Профиль)
                    combobox_profile.Items.Add(v.Наименование_профиля);
                foreach (var v in context.Cпециальность)
                    combobox_spec_Copy.Items.Add(v.Наименование_специальности);

                foreach (var v in context.Профиль)
                    combobox_profile_Copy.Items.Add(v.Наименование_профиля);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid_add_doctor.Visibility = Visibility.Collapsed;
            grid_edit_doctor.Visibility = Visibility.Collapsed;
            one = 795;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Cпециальность)
                    combobox_spec.Items.Add(v.Наименование_специальности);

                foreach (var v in context.Профиль)
                    combobox_profile.Items.Add(v.Наименование_профиля);
                foreach (var v in context.Cпециальность)
                    combobox_spec_Copy.Items.Add(v.Наименование_специальности);

                foreach (var v in context.Профиль)
                    combobox_profile_Copy.Items.Add(v.Наименование_профиля);
            }
            for (int iii = 0; iii != 193; iii++)
            {
                string st1 = time_speek().Replace("24:00", "0:00");
                combobox_rab_dayB.Items.Add(st1);
                combobox__rab_dayE.Items.Add(st1);
                combobox_rab_dayB_Copy.Items.Add(st1);
                combobox__rab_dayE_Copy.Items.Add(st1);
            }

            listView1.View = View.Details;       // детальный вид
            listView1.GridLines = true;          // разделители 
            listView1.FullRowSelect = true;
            //listView1.ContextMenuStrip = strip;
            listView1.Sorting = System.Windows.Forms.SortOrder.None;
            listView1.Columns.Add("Ф.И.О. медсестры", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Специальность", 300, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("Профиль", 300, System.Windows.Forms.HorizontalAlignment.Left);//2
            //listView1.Columns.Add("Кабинет", 100, System.Windows.Forms.HorizontalAlignment.Left);//2
            listView1.Columns.Add("Телефон", 150, System.Windows.Forms.HorizontalAlignment.Left);//2
            listView1.Columns.Add("Рабочий день", 175, System.Windows.Forms.HorizontalAlignment.Left);//2
            //listView1.Columns.Add("Время перерыва", 175, System.Windows.Forms.HorizontalAlignment.Left);//2
            //listView1.Columns.Add("Учетная запись", 175, System.Windows.Forms.HorizontalAlignment.Left);//2
            int i = 0;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Медсестра)
                {
                    listView1.Items.Add(v.Фамилия_медсестры + " " + v.Имя__медсестры + " " + v.Отчество__медсестры);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Наименование_специальности);
                    listView1.Items[i].SubItems.Add(v.Cпециальность.Профиль.Наименование_профиля);
                    //listView1.Items[i].SubItems.Add(v.Кабинет);
                    listView1.Items[i].SubItems.Add(v.Телефон);
                    listView1.Items[i].SubItems.Add(v.Начало_рабочего_дня.ToString().Remove(5, 3) + " - " + v.Конец_рабочего_дня.ToString().Remove(5, 3));
                    //listView1.Items[i].SubItems.Add(v.Начало_перерыва.ToString().Remove(5, 3) + " - " + v.Конец_перерыва.ToString().Remove(5, 3));
                    //listView1.Items[i].SubItems.Add(v.Авторизация.Логин);
                    i++;
                }
            }
            forms1.Child = listView1;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        void items_claer()
        {
            textbox_fname.Clear();
            textbox_lname.Clear();
            textbox_name.Clear();
            textbox_phone.Clear();
            combobox_profile.Text = "";
            combobox_rab_dayB.Text = "";
            combobox_spec.Text = "";
            combobox__rab_dayE.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (
                 Dynamic.CheckingColor.cheked_null_and_space(textbox_fname)
                & Dynamic.CheckingColor.cheked_null_and_space(textbox_name)
                //& Dynamic.CheckingColor.cheked_null_and_space(textbox_phone)
                & Dynamic.CheckingColor.cheked_null_and_space(textbox_lname)
                //& Dynamic.CheckingColor.cheked_null_and_space(combobox_preB, "Не выбрано начало перерыва")
                //& Dynamic.CheckingColor.cheked_null_and_space(combobox_preE, "Не выбран конец перерыва")
                & Dynamic.CheckingColor.cheked_null_and_space(combobox_rab_dayB, "Не выбрано начало рабочего дня")
                & Dynamic.CheckingColor.cheked_null_and_space(combobox__rab_dayE, "Не выбран конец рабочего дня")
                & Dynamic.CheckingColor.cheked_null_and_space(combobox_spec, "Не выбрана специальность")
                & Dynamic.CheckingColor.cheked_null_and_space(combobox_profile, "Не выбран профиль")
                )
            {
                try
                {
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        var v2 = context.Медсестра.Add(new Медсестра
                        {
                            Cпециальность = context.Cпециальность.FirstOrDefault(c => c.Наименование_специальности == combobox_spec.SelectedValue.ToString()),
                            Фамилия_медсестры = textbox_fname.Text,
                            Имя__медсестры = textbox_name.Text,
                            Отчество__медсестры = textbox_lname.Text,
                            Телефон = textbox_phone.Text,
                            Начало_рабочего_дня = TimeSpan.Parse(combobox_rab_dayB.Text),
                            Конец_рабочего_дня = TimeSpan.Parse(combobox__rab_dayE.Text),
                        });
                        context.SaveChanges();
                    }
                    System.Windows.MessageBox.Show("Успешно");
                    grid_add_doctor.Visibility = Visibility.Hidden;
                    grid_pre_doctor.Visibility = Visibility.Visible;
                }
                catch
                {
                    System.Windows.MessageBox.Show("Медсестра не добавлена");
                }
            }
            items_claer();
            update_list();
        }

        static int one = 795;
        string time_speek()
        {
            string rto = one.ToString();
            one += 5;

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

        private void combobox_spec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combobox_profile.Items.Clear();
            using (context_stat_talon context = new context_stat_talon())
            {
                try
                {
                    foreach (var v in context.Cпециальность.Where(c => c.Наименование_специальности == combobox_spec.SelectedValue.ToString()))
                        combobox_profile.Items.Add(v.Профиль.Наименование_профиля);
                }
                catch
                {
                    foreach (var v in context.Cпециальность)
                        combobox_profile.Items.Add(v.Профиль.Наименование_профиля);
                }
            }
        }

        private void textbox_fname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_fname.Text = Regex.Replace(textbox_fname.Text, @"\d", "");
            textbox_fname_Copy.Text = Regex.Replace(textbox_fname_Copy.Text, @"\d", "");
        }

        private void textbox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_name.Text = Regex.Replace(textbox_name.Text, @"\d", "");
            textbox_name_Copy.Text = Regex.Replace(textbox_name_Copy.Text, @"\d", "");
        }

        private void textbox_lname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_lname.Text = Regex.Replace(textbox_lname.Text, @"\d", "");
            textbox_lname_Copy.Text = Regex.Replace(textbox_lname_Copy.Text, @"\d", "");
        }

        private void textbox_phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_phone.Text = Regex.Replace(textbox_phone.Text, @"\D", "");
            textbox_phone_Copy.Text = Regex.Replace(textbox_phone_Copy.Text, @"\D", "");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            update_list();
            items_claer();
            items_claer_copy();
            grid_add_doctor.Visibility = Visibility.Hidden;
            grid_edit_doctor.Visibility = Visibility.Hidden;
            grid_pre_doctor.Visibility = Visibility.Visible;
        }

        private void textbox_fname_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_fname.Background = Brushes.White;
        }

        private void textbox_name_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_name.Background = Brushes.White;
        }

        private void textbox_lname_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_lname.Background = Brushes.White;
        }

        private void textbox_phone_GotFocus(object sender, RoutedEventArgs e)
        {
            textbox_phone.Background = Brushes.White;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            grid_add_doctor.Visibility = Visibility.Visible;
            grid_pre_doctor.Visibility = Visibility.Hidden;
            update_combobox();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                string[] stv = listView1.Items[d].SubItems[0].Text.Split(' ');
                string str1 = stv[0], str2 = stv[1], str3 = stv[2], if1 = listView1.Items[d].SubItems[1].Text, if2 = listView1.Items[d].SubItems[2].Text;

                using (context_stat_talon context = new context_stat_talon())
                {
                    var v1 = context.Медсестра.FirstOrDefault(
                        c => c.Cпециальность.Наименование_специальности == if1
                    && c.Cпециальность.Профиль.Наименование_профиля == if2
                    && c.Фамилия_медсестры == str1
                    && c.Имя__медсестры == str2
                    && c.Отчество__медсестры == str3);
                    context.Медсестра.Remove(v1);
                    context.SaveChanges();
                }
            }
            update_list();
        }

        Медсестра tab_doc { get; set; }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            foreach (var vtt in listView1.SelectedIndices)
            {
                int d = int.Parse(vtt.ToString());
                string[] stv = listView1.Items[d].SubItems[0].Text.Split(' ');
                string str1 = stv[0], str2 = stv[1], str3 = stv[2], if1 = listView1.Items[d].SubItems[1].Text, if2 = listView1.Items[d].SubItems[2].Text;
                //System.Windows.MessageBox.Show(if1 + " " + if2 + " " + str1 + " " + str2 + " " + str3);
                update_combobox();
                using (context_stat_talon context = new context_stat_talon())
                {
                    tab_doc = context.Медсестра.FirstOrDefault(
                        c => c.Cпециальность.Наименование_специальности == if1
                    && c.Cпециальность.Профиль.Наименование_профиля == if2
                    && c.Фамилия_медсестры == str1
                    && c.Имя__медсестры == str2
                    && c.Отчество__медсестры == str3);
                    textbox_fname_Copy.Text = tab_doc.Фамилия_медсестры;
                    textbox_lname_Copy.Text = tab_doc.Отчество__медсестры;
                    textbox_name_Copy.Text = tab_doc.Имя__медсестры;
                    textbox_phone_Copy.Text = tab_doc.Телефон;
                    combobox_profile_Copy.Text = tab_doc.Cпециальность.Профиль.Наименование_профиля;
                    combobox_rab_dayB_Copy.Text = tab_doc.Начало_рабочего_дня.ToString().Remove(5, 3);
                    combobox__rab_dayE_Copy.Text = tab_doc.Конец_рабочего_дня.ToString().Remove(5, 3);
                    combobox_spec_Copy.Text = tab_doc.Cпециальность.Наименование_специальности; ;
                }
            }
            grid_edit_doctor.Visibility = Visibility.Visible;
            grid_pre_doctor.Visibility = Visibility.Hidden;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (
             Dynamic.CheckingColor.cheked_null_and_space(textbox_fname_Copy)
             & Dynamic.CheckingColor.cheked_null_and_space(textbox_name_Copy)
             //& Dynamic.CheckingColor.cheked_null_and_space(textbox_phone)
             & Dynamic.CheckingColor.cheked_null_and_space(textbox_lname_Copy)
             //& Dynamic.CheckingColor.cheked_null_and_space(combobox_preB, "Не выбрано начало перерыва")
             //& Dynamic.CheckingColor.cheked_null_and_space(combobox_preE, "Не выбран конец перерыва")
             & Dynamic.CheckingColor.cheked_null_and_space(combobox_rab_dayB_Copy, "Не выбрано начало рабочего дня")
             & Dynamic.CheckingColor.cheked_null_and_space(combobox__rab_dayE_Copy, "Не выбран конец рабочего дня")
             & Dynamic.CheckingColor.cheked_null_and_space(combobox_spec_Copy, "Не выбрана специальность")
             & Dynamic.CheckingColor.cheked_null_and_space(combobox_profile_Copy, "Не выбран профиль")
             )
            {
                try
                {
                    using (context_stat_talon context = new context_stat_talon())
                    {
                        var v23 = context.Медсестра.FirstOrDefault(dddd => dddd.Табельный_номер_медсестры == tab_doc.Табельный_номер_медсестры);
                        //System.Windows.MessageBox.Show(v1.Роль + " " + v1.Логин + " " + v1.Пароль );
                        v23.Cпециальность = context.Cпециальность.FirstOrDefault(c => c.Наименование_специальности == combobox_spec_Copy.SelectedValue.ToString());
                        v23.Фамилия_медсестры = textbox_fname_Copy.Text;
                        v23.Имя__медсестры = textbox_name_Copy.Text;
                        v23.Отчество__медсестры = textbox_lname_Copy.Text;
                        v23.Телефон = textbox_phone_Copy.Text;
                        v23.Начало_рабочего_дня = TimeSpan.Parse(combobox_rab_dayB_Copy.Text);
                        v23.Конец_рабочего_дня = TimeSpan.Parse(combobox__rab_dayE_Copy.Text);
                        context.SaveChanges();
                    }
                    System.Windows.MessageBox.Show("Успешно");
                    grid_edit_doctor.Visibility = Visibility.Hidden;
                    grid_pre_doctor.Visibility = Visibility.Visible;
                }
                catch
                {
                    System.Windows.MessageBox.Show("Данные медсестры не изменены");
                }
            }
            items_claer_copy();
            update_list();
        }

        void items_claer_copy()
        {
            textbox_fname_Copy.Clear();
            textbox_lname_Copy.Clear();
            textbox_name_Copy.Clear();
            textbox_phone_Copy.Clear();
            combobox_profile_Copy.Text = "";
            combobox_rab_dayB_Copy.Text = "";
            combobox_spec_Copy.Text = "";
            combobox__rab_dayE_Copy.Text = "";
        }
    }
}
