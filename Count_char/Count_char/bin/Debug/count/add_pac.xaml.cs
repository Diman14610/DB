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
using Dynamic;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для add_pac.xaml
    /// </summary>
    public partial class add_pac : Window
    {
        public add_pac()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }//
        
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (Checking.cheked_null_and_space(textbox_fname) && Checking.cheked_null_and_space(textbox_lname) &&
                 Checking.cheked_null_and_space(textbox_name) && Checking.cheked_null_and_space(textbox_adres_reg) &&
                  Checking.cheked_null_and_space(textbox_adres_residence) &&
                   Checking.cheked_null_and_space(textbox_number_pas) 
                      &&
                     Checking.cheked_null_and_space(textbox_seria_pas) && Checking.cheked_null_and_space(textbox_SNILS) &&
                      Checking.cheked_null_and_space(combobox_social_status) &&//убран Checking.cheked_null_and_space(textbox_who_directed)
                      Checking.cheked_null_and_space(datepicker_happi_brith) && Checking.cheked_null_and_space(combobox_pol)
                      && Checking.cheked_null_and_space(combobox_resident_Orenburg_region) 
                      && Checking.cheked_null_and_space(combobox_resident_Orenburg_region_city_or_village))
            {
                using (context_stat_talon context = new context_stat_talon())
                {
                    var v = context.Пациент.Add(new Пациент
                    {
                        Адресс_фактического_проживания = textbox_adres_residence.Text,
                        Адресс_регистрации = textbox_adres_reg.Text,
                        Дата_рождения = datepicker_happi_brith.SelectedDate.Value,
                        ДМС = textbox_DMS.Text,
                        Фамилия_пациента = textbox_fname.Text,
                        Пол = combobox_pol.SelectedValue.ToString(),
                        Отчество_пациента = textbox_lname.Text,
                        Имя_пациента = textbox_name.Text,
                        Паспорт_номер = int.Parse(textbox_number_pas.Text),
                        Паспорт_серия = int.Parse(textbox_seria_pas.Text),
                        Место_учёбы_работы__должность = textbox_place_of_work_study_post.Text,
                        Полис_ОМС = textbox_polis_OMS.Text,
                        Полис_ОМС_серия = textbox_polis_seria.Text,
                        СНИЛС = textbox_SNILS.Text,
                        Социальный_статус = combobox_social_status.SelectedValue.ToString(),
                        Представитель_пациента = add_representative_pac.representive_pac,
                        Представитель_пациента_дата_рождения = add_representative_pac.representive_date,
                        Представитель_пациента_фамилия = add_representative_pac.representive_fname,
                        Представитель_пациента_пол = add_representative_pac.representive_pol,
                        Представитель_пациента_отчество = add_representative_pac.representive_lname,
                        Представитель_пациента_имя = add_representative_pac.representive_name,
                        Кем_направлен = textbox_who_directed.Text,
                        Житель_Оренбургской_области = combobox_resident_Orenburg_region.SelectedValue.ToString(),
                        Оренбургской_области_городской_или_сельский = combobox_resident_Orenburg_region_city_or_village.SelectedValue.ToString(),
                        СМО_ДМС_адрес = add_SMO.smo_adres_dms,
                        СМО_ДМС_наименование = add_SMO.smo_name_dms,
                        СМО_адрес = add_SMO.smo_adres,
                        СМО_наименование = add_SMO.smo_name,
                        СМО_ОГРН = add_SMO.smo_ogrn,
                    });
                    context.SaveChanges();
                }
                //Operator op = new Operator();
                //op.Show();
                Close();       
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }

            
        }//

        private void button_SMO_Click(object sender, RoutedEventArgs e)
        {
            add_SMO smo = new add_SMO();
            smo.Show();
        }

        private void button_representative_pac_Click(object sender, RoutedEventArgs e)
        {
            add_representative_pac pac = new add_representative_pac();
            pac.Show();
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

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            //Operator op = new Operator();
            //op.Show();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Operator op = new Operator();
            op.Show();
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

        //private void Window_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Smart_scale.smart_resize(this, textbox_fname);
        //}

        //private void Window_MouseMove(object sender, MouseEventArgs e)
        //{       
        //}
    }
}
