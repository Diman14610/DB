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
    /// Логика взаимодействия для update_pac.xaml
    /// </summary>
    public partial class update_pac : Window
    {
        public update_pac()
        {
            InitializeComponent();
        }

        private void button_SMO_Click(object sender, RoutedEventArgs e)
        {
            Update_smo smo = new Update_smo();
            smo.Show();
        }

        private void button_representative_pac_Click(object sender, RoutedEventArgs e)
        {
            Update_representative_pac pac = new Update_representative_pac();
            pac.Show();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            using (context_stat_talon context = new context_stat_talon())
            {
                var v = context.Пациент
         .Where(c => c.C__пациента == Operator.int_x_buff)
         .Select(d => d)
         .FirstOrDefault();
                //
                v.Житель_Оренбургской_области = combobox_resident_Orenburg_region.Text;
                v.Житель_Оренбургской_области = combobox_resident_Orenburg_region_city_or_village.Text;
                v.Адресс_фактического_проживания = textbox_adres_residence.Text;
                v.Адресс_регистрации = textbox_adres_reg.Text;
                v.Дата_рождения = datepicker_happi_brith.SelectedDate.Value;
                v.ДМС = textbox_DMS.Text;
                v.Фамилия_пациента = textbox_fname.Text;
                v.Пол = combobox_pol.SelectedValue.ToString();
                v.Отчество_пациента = textbox_lname.Text;
                v.Имя_пациента = textbox_name.Text;
                v.Паспорт_номер = int.Parse(textbox_number_pas.Text);
                v.Паспорт_серия = int.Parse(textbox_seria_pas.Text);
                v.Место_учёбы_работы__должность = textbox_place_of_work_study_post.Text;
                v.Полис_ОМС = textbox_polis_OMS.Text;
                v.Полис_ОМС_серия = textbox_polis_seria.Text;
                if (v.ДМС == null)
                {
                    v.СМО_адрес = Update_smo.smo_adres;
                    v.СМО_наименование = Update_smo.smo_name;
                    v.СМО_ОГРН = Update_smo.smo_ogrn;
                }
                else if (v.Полис_ОМС == null)
                {
                    v.СМО_ДМС_адрес = Update_smo.smo_adres;
                    v.СМО_ДМС_наименование = Update_smo.smo_name;
                }
               
                v.СНИЛС = textbox_SNILS.Text;
                v.Социальный_статус = combobox_social_status.SelectedValue.ToString();
                v.Представитель_пациента = Update_representative_pac.representive_pac;
                v.Представитель_пациента_дата_рождения = Update_representative_pac.representive_date;
                v.Представитель_пациента_фамилия = Update_representative_pac.representive_fname;
                v.Представитель_пациента_пол = Update_representative_pac.representive_pol;
                v.Представитель_пациента_отчество = Update_representative_pac.representive_lname;
                v.Представитель_пациента_имя = Update_representative_pac.representive_name;
                v.Кем_направлен = textbox_who_directed.Text;
                //
                context.SaveChanges();
            }
            Close();
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            textbox_adres_residence.Clear();
            textbox_adres_reg.Clear();
            textbox_DMS.Clear();
            textbox_fname.Clear();
            textbox_lname.Clear();
            textbox_name.Clear();
            textbox_number_pas.Clear();
            textbox_seria_pas.Clear();
            textbox_place_of_work_study_post.Clear();
            textbox_polis_OMS.Text = "";
            textbox_polis_seria.Clear();
            textbox_SNILS.Clear();
            textbox_who_directed.Clear();
            combobox_social_status.Text = "";
            combobox_pol.Text = "";
            combobox_resident_Orenburg_region.Text = "";
            combobox_resident_Orenburg_region_city_or_village.Text = "";
            datepicker_happi_brith.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (context_stat_talon context = new context_stat_talon())
            {
                var v = context.Пациент.FirstOrDefault(c => c.СНИЛС == Operator.snils);
                combobox_resident_Orenburg_region.Text = v.Житель_Оренбургской_области;
                combobox_resident_Orenburg_region_city_or_village.Text = v.Оренбургской_области_городской_или_сельский;
                textbox_adres_reg.Text = v.Адресс_регистрации;
                textbox_adres_residence.Text = v.Адресс_фактического_проживания;
                textbox_DMS.Text = v.ДМС;
                textbox_fname.Text = v.Фамилия_пациента;
                textbox_lname.Text = v.Отчество_пациента;
                textbox_name.Text = v.Имя_пациента;
                textbox_number_pas.Text = v.Паспорт_номер.ToString();
                textbox_place_of_work_study_post.Text = v.Место_учёбы_работы__должность;
                textbox_polis_OMS.Text = v.Полис_ОМС;
                textbox_polis_seria.Text = v.Полис_ОМС_серия;
                textbox_seria_pas.Text = v.Паспорт_серия.ToString();
                textbox_SNILS.Text = v.СНИЛС;
                textbox_who_directed.Text = v.Кем_направлен;
                combobox_pol.Text = v.Пол;
                combobox_social_status.Text = v.Социальный_статус;
                datepicker_happi_brith.SelectedDate = v.Дата_рождения;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }


        private void textbox_SNILS_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_SNILS.Text = Regex.Replace(textbox_SNILS.Text, @"\D", "");
        }

        private void textbox_seria_pas_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_seria_pas.Text = Regex.Replace(textbox_seria_pas.Text, @"\D", "");
        }

        private void textbox_number_pas_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_number_pas.Text = Regex.Replace(textbox_number_pas.Text, @"\D", "");
        }

        private void textbox_fname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_fname.Text = Regex.Replace(textbox_fname.Text, @"\d", "");
        }

        private void textbox_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_name.Text = Regex.Replace(textbox_name.Text, @"\d", "");
        }

        private void textbox_lname_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_lname.Text = Regex.Replace(textbox_lname.Text, @"\d", "");
        }

        private void textbox_polis_seria_TextChanged(object sender, TextChangedEventArgs e)
        {
            textbox_polis_seria.Text = Regex.Replace(textbox_polis_seria.Text, @"\D", "");
        }

    }
}
