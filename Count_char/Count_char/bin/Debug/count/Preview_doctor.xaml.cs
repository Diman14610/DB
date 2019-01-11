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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для Preview_doctor.xaml
    /// </summary>
    public partial class Preview_doctor : Page
    {
        public Preview_doctor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add_Doctor add_ = new Add_Doctor();
            add_.ShowDialog();
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
            listView1.Columns.Add("Наименование выходного дня", 300, System.Windows.Forms.HorizontalAlignment.Left);//0
            listView1.Columns.Add("Месяц", 70, System.Windows.Forms.HorizontalAlignment.Left);//1
            listView1.Columns.Add("День", 100, System.Windows.Forms.HorizontalAlignment.Left);//2
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
            forms1.Child = listView1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
