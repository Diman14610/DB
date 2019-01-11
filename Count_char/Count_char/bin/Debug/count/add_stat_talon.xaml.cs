using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для add_stat_talon.xaml
    /// </summary>
    public partial class add_stat_talon : Window
    {
        public add_stat_talon()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Посмотри в код там написана ошибка");
            /*в конце нужно добавлять когда добавить id_stat_talon после чего можно будет легко взять и связать диагноз*/

            //using (context_stat_talon context = new context_stat_talon())
            //{
            //    diagnoses d = context.diagnoses.Add(new diagnoses
            //    {
            //        one_ = combobox_one.Text,
            //        diagnosis = textbox_diagnoses.Text,
            //        code_mkb = combobox_code_mkb.Text,
            //        characteristic_of_disease = combobox_characterisric_disease.Text,
            //        exacerbation = combobox_exacerbation.Text,
            //        instead_of_previously_registered_diagnoses_code_mkb = combobox_instead_previously_code_mrb.Text,
            //        instead_of_previously_registered_diagnoses_date_registration = datepicker_instead_previously_date_registration.SelectedDate,
            //        tab_number_doctor = int.Parse(combobox_tab_number_doctor.Text),
            //        dispensary_records_state = combobox_dispensary_records_state.Text,
            //        dispensary_records_group = combobox_dispensary_records_group.Text,
            //        dispensary_records_date_of_next_appearance = datepicker_dispensary_records_date_next_appearence.SelectedDate
            //    });
            //    //m_diagnoses_state_talon m_d =
            //    context.SaveChanges();
            //}
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }

        System.Windows.Forms.ListView li1 = new System.Windows.Forms.ListView();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region диагноз
            li1.View = View.Details;       // детальный вид
            li1.GridLines = true;          // разделители 
            li1.FullRowSelect = true;
            li1.Sorting = System.Windows.Forms.SortOrder.None;
            li1.Columns.Add("основной/спопуствующий", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            li1.Columns.Add("Диагноз", 120, System.Windows.Forms.HorizontalAlignment.Left);//1
            li1.Columns.Add("МКБ", 150, System.Windows.Forms.HorizontalAlignment.Left);//2
            li1.Columns.Add("Характерное зоболевания", 250, System.Windows.Forms.HorizontalAlignment.Left);//3
            li1.Columns.Add("Обострение", 250, System.Windows.Forms.HorizontalAlignment.Left);//4
            li1.Columns.Add("Вместо ранее зарегистрированных диагнозов МКБ", 300, System.Windows.Forms.HorizontalAlignment.Left);//5
            li1.Columns.Add("Вместо ранее зарегистрированных диагнозов дата регистрации", 250, System.Windows.Forms.HorizontalAlignment.Left);//6
            li1.Columns.Add("Табельный номер врача", 100, System.Windows.Forms.HorizontalAlignment.Left);//7
            li1.Columns.Add("Диспансерный учёт состояние", 300, System.Windows.Forms.HorizontalAlignment.Left);//8
            li1.Columns.Add("Диспансерный учёт группа", 100, System.Windows.Forms.HorizontalAlignment.Left);//9
            li1.Columns.Add("Диспансерный учёт дата след. явки", 150, System.Windows.Forms.HorizontalAlignment.Left);//10

            form_diagnoses.Child = li1;
            #endregion

            combobox_view_injury_code_mkb.Visibility = Visibility.Hidden;
            textblock_buff.Visibility = Visibility.Hidden;
            switch (Doctor.open_talon)
            {
                case 1:
                    {
                        using (context_stat_talon context = new context_stat_talon())
                        {
                            var v1 = context.Статистический_талон.Add(new Статистический_талон
                            {

                            });
                            context.SaveChanges();
                            var v2 = v1;
                            v2.C__учетной_записи = Doctor.operator_.C__учетной_записи;
                            v2.C__пациента = Doctor.pac.C__пациента;
                            v2.C__записи_на_прием = Doctor.write_.C__записи_на_прием;
                            context.SaveChanges();
                        }
                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
       
            textblock_fio.Text += Doctor.pac.Фамилия_пациента + " " + Doctor.pac.Имя_пациента + "" + Doctor.pac.Отчество_пациента;
            textblock_pas_seria.Text += Doctor.pac.Паспорт_серия + " " + Doctor.pac.Паспорт_номер;
            textblock_snils.Text += Doctor.pac.СНИЛС;
            textblock_pol.Text += Doctor.pac.Пол;
            try
            {
                textblock_date_birth.Text += Doctor.pac.Дата_рождения.Value.ToShortDateString();
            }
            catch
            {
                textblock_date_birth.Text += "";
            }
            textblock_gitel.Text += Doctor.pac.Житель_Оренбургской_области + " " + Doctor.pac.Оренбургской_области_городской_или_сельский;
            textblock_adres_po_reg.Text += Doctor.pac.Адресс_регистрации;
            textblock_adres_f_pro.Text += Doctor.pac.Адресс_фактического_проживания;
            textblock_pred_pac.Text += Doctor.pac.Адресс_фактического_проживания;
            textblock_pred_pac_fio.Text += Doctor.pac.Представитель_пациента_фамилия + " " + Doctor.pac.Представитель_пациента_имя + " " + Doctor.pac.Представитель_пациента_отчество;
            try
            {
                textblock_pred_pac_date_birth.Text += Doctor.pac.Представитель_пациента_дата_рождения.Value.ToShortDateString();
            }
            catch
            {
                textblock_pred_pac_date_birth.Text += "";
            }
            textblock_pred_pac_pol.Text += Doctor.pac.Представитель_пациента_пол;
            textblock_polis_oms.Text += Doctor.pac.Полис_ОМС;
            textblock_smo_name.Text += Doctor.pac.СМО_наименование;
            textblock_smo_ogrn.Text += Doctor.pac.СМО_ОГРН;
            textblock_smo_adres.Text += Doctor.pac.СМО_адрес;
            textblock_dms.Text += Doctor.pac.ДМС;
            textblock_dms_smo.Text += Doctor.pac.СМО_ДМС_наименование;
            textblock_dms_adres.Text += Doctor.pac.СМО_ДМС_адрес;
            textblock_status.Text += Doctor.pac.Социальный_статус;
            textblock_work.Text += Doctor.pac.Место_учёбы_работы__должность;
            textblock_who_nap.Text += Doctor.pac.Кем_направлен;
            textblock_operator.Text += Doctor.operator_fio;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Посмотри в код там написана ошибка");
            /*в конце нужно добавлять когда добавить id_stat_talon после чего можно будет легко взять и связать диагноз*/
            //using (context_stat_talon context = new context_stat_talon())
            //{
            //    Surgery see = context.Surgery.Add(new Surgery
            //    {
            //        date_ = datepicker_Surgery_date.SelectedDate.Value,
            //        tab_number_doctor = int.Parse(combobox_Surgery_tab_number_doctor.Text),
            //        tab_number_nurse = int.Parse(combobox_Surgery_tab_number_nurse.Text),
            //        name_operation = textbox_Surgery_name_operation.Text,
            //        code_operation = int.Parse(combobox_Surgery_code_operation.Text),
            //        code_anesthesia = int.Parse(combobox_Surgery_code_anesthesia.Text),
            //        type_of_payment_code_operation = combobox_Surgery_view_payment.Text,
            //        type_of_payment_code_anesthesia = combobox_Surgery_view_anesthesia.Text
            //    });
            //    context.SaveChanges();
            //}
        }

        private void combobox_view_injury_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (combobox_view_injury.SelectedValue.ToString() == "не связанная с производством: прочие-10" ||
                    combobox_view_injury.Text == "не связанная с производством: прочие-10")
                {
                    combobox_view_injury_code_mkb.Visibility = Visibility.Visible;
                    textblock_buff.Visibility = Visibility.Visible;
                }
                else
                {
                    combobox_view_injury_code_mkb.Visibility = Visibility.Hidden;
                    textblock_buff.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception)
            {
                combobox_view_injury_code_mkb.Visibility = Visibility.Hidden;
                textblock_buff.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //
            WindowsFormsHost host = new WindowsFormsHost();
            System.Windows.Forms.ListView listView = new System.Windows.Forms.ListView
            {
                Margin = new Padding(10, 311, 0, 0),
                BackColor = System.Drawing.Color.GreenYellow
            };
            host.Child = listView;            
            this.grid1.Children.Add(host);
            //
        }
    }
}
