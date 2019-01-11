using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using word = Microsoft.Office.Interop.Word;

namespace Stat_talon
{
    /// <summary>
    /// Логика взаимодействия для print_stat_talon.xaml
    /// </summary>
    public partial class print_stat_talon : Window
    {
        public print_stat_talon()
        {
            InitializeComponent();
        }

        struct write
        {
            public DateTime date { get; set; }
            public TimeSpan? time_b { get; set; }
            public TimeSpan? time_m { get; set; }
            public TimeSpan? time_e { get; set; }
            public string patient { get; set; }
            public string doctor { get; set; }
            public string payment { get; set; }
            public string manip { get; set; }
            public int? cout_fol { get; set; }
            public string notes { get; set; }
            public string eco_icsi { get; set; }
            public string s_ma { get; set; }
            public DateTime? back_prog { get; set; }
            public int? cout_transport_ibm { get; set; }
            public string frost_imb { get; set; }
            public write(DateTime d, TimeSpan t_b, TimeSpan t_m, TimeSpan t_e, string p, string doc, string pay, string man, int cout_f, string not, string eco, string sp, DateTime back, int cout_tran, string frost)
            {
                date = d;
                time_b = t_b;
                time_m = t_m;
                time_e = t_e;
                patient = p;
                doctor = doc;
                payment = pay;
                manip = man;
                cout_fol = cout_f;
                notes = not;
                eco_icsi = eco;
                s_ma = sp;
                back_prog = back;
                cout_transport_ibm = cout_tran;
                frost_imb = frost;
            }
        }

        string splite_fio(string template_string)
        {
            string[] slo = template_string.Split(' ');
            string retunt_string1 = slo[0] + " " + slo[1].Remove(1, slo[1].Length - 1) + ". " + slo[2].Remove(1, slo[2].Length - 1) + ".";
            return retunt_string1;
        }

        word.Application app { get; set; }
        word.Document doc { get; set; }
        void fill_document_on_day()
        {
            string[] vs = DateTime.Now.ToShortTimeString().Split(':');
            string current = Environment.CurrentDirectory + "\\Запись на день" + "\\ На " + datepicker_date.SelectedDate.Value.ToLongDateString()
                + " В " + vs[0] + "." + vs[1] + ".docx";
            Directory.CreateDirectory(Environment.CurrentDirectory + "\\Запись на день");
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\temp.docx", current, true);

            app = new word.Application();
            doc = new word.Document();

            app.Visible = false;

            // Создаём объект приложения
            // Путь до шаблона документа
            string source = current;
            doc = app.Documents.Open(source);
            // Открываем
            doc.Activate();
            //Таблицу вставляем в начало документа
            Object start = 0;
            Object end = 0;
            word.Range wordrange = doc.Range(ref start, ref end);
            Object defaultTableBehavior =
               word.WdDefaultTableBehavior.wdWord9TableBehavior;
            Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;

            int buff_write = 1;
            using (context_stat_talon context = new context_stat_talon())
            {
                foreach (var v in context.Запись_на_прием.Where(c => c.Дата_приема == datepicker_date.SelectedDate))
                {
                    buff_write++;
                }
            }
            //Добавляем таблицу и получаем объект wordtable 
            string[] sv = new string[] { "Дата","Время","ФИО пациента","Врач","омс/пл","Манипуляция","Кол-во фолликулов","Примечания",
                    "ЭКО/икси","Сперма: А+В","Предыдущая программа","Кол-во переносимых эмбрионов","Заморозка эмбрионов"};
            word.Table wordtable1 = doc.Tables.Add(wordrange, buff_write, sv.Length,
                              ref defaultTableBehavior, autoFitBehavior);
            //
            word.Table wordtable = doc.Tables[1];
            //
            int i = 1;
            word.Range wordcellrange = doc.Tables[1].Cell(1, 1).Range;
            foreach (string s in sv)
            {
                wordcellrange = wordtable.Cell(1, i).Range;
                wordcellrange.Text = s;
                i++;
            }
            List<write> li_w = new List<write>();
            using (context_stat_talon context = new context_stat_talon())
            {
                var v222 = from c in context.Запись_на_прием
                           where c.Дата_приема == datepicker_date.SelectedDate
                           select c;
                foreach (var v in v222)
                {
                    li_w.Add(new write
                    {
                        date = v.Дата_приема.Value,
                        time_b = new TimeSpan(12, 30, 00),
                        time_e = new TimeSpan(12, 30, 00),
                        back_prog = v.Дата_приема.Value,
                        cout_fol = 22,
                        cout_transport_ibm = 33,
                        doctor = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        eco_icsi = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        frost_imb = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        manip = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        notes = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        patient = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        payment = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                        s_ma = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                    });
                }
                wordcellrange = doc.Tables[1].Cell(4, 13).Range;
                for (int i1 = 2; i1 != li_w.Count + 2; i1++)
                {
                    for (int j1 = 1; j1 != 13; j1++)
                    {
                        wordcellrange = wordtable.Cell(i1, j1).Range;
                        wordcellrange.Text = li_w[i1 - 2].date.ToShortDateString();
                        if (li_w[i1 - 2].time_b == li_w[i1 - 2].time_e & li_w[i1 - 2].time_b == li_w[i1 - 2].time_m)
                        {
                            wordcellrange = wordtable.Cell(i1, 2).Range;
                            wordcellrange.Text = li_w[i1 - 2].time_b.ToString().Remove(5, 3);
                        }
                        else
                        {
                            wordcellrange = wordtable.Cell(i1, 2).Range;
                            wordcellrange.Text = li_w[i1 - 2].time_b.ToString().Remove(5, 3) + "-" + li_w[i1 - 2].time_e.ToString().Remove(5, 3);
                        }
                        wordcellrange = wordtable.Cell(i1, 3).Range;
                        wordcellrange.Text = li_w[i1 - 2].patient;
                        wordcellrange = wordtable.Cell(i1, 4).Range;
                        wordcellrange.Text = li_w[i1 - 2].doctor;
                        wordcellrange = wordtable.Cell(i1, 5).Range;
                        wordcellrange.Text = li_w[i1 - 2].payment;
                        wordcellrange = wordtable.Cell(i1, 6).Range;
                        wordcellrange.Text = li_w[i1 - 2].manip;
                        wordcellrange = wordtable.Cell(i1, 7).Range;
                        wordcellrange.Text = li_w[i1 - 2].cout_fol.ToString();
                        wordcellrange = wordtable.Cell(i1, 8).Range;
                        wordcellrange.Text = li_w[i1 - 2].notes;
                        wordcellrange = wordtable.Cell(i1, 9).Range;
                        wordcellrange.Text = li_w[i1 - 2].eco_icsi;
                        wordcellrange = wordtable.Cell(i1, 10).Range;
                        wordcellrange.Text = li_w[i1 - 2].s_ma;
                        try
                        {
                            wordcellrange = wordtable.Cell(i1, 11).Range;
                            wordcellrange.Text = li_w[i1 - 2].back_prog.Value.ToShortDateString();
                        }
                        catch (Exception)
                        {
                            wordcellrange = wordtable.Cell(i1, 11).Range;
                            wordcellrange.Text = "";
                        }

                        wordcellrange = wordtable.Cell(i1, 12).Range;
                        wordcellrange.Text = li_w[i1 - 2].cout_transport_ibm.ToString();
                        wordcellrange = wordtable.Cell(i1, 13).Range;
                        wordcellrange.Text = li_w[i1 - 2].frost_imb;
                        wordcellrange = wordtable.Cell(i1, 1).Range;
                    }
                }
            }
            word.Dialog dialog = app.Dialogs[word.WdWordDialog.wdDialogFilePrintSetup];

            datepicker_date.Visibility = Visibility.Hidden;
            button_print.Visibility = Visibility.Hidden;
            button_close.Visibility = Visibility.Hidden;
            button_no.Visibility = Visibility.Visible;
            button_yes.Visibility = Visibility.Visible;
            label__1.Visibility = Visibility.Visible;
            app.Visible = true;
            app.Activate();
            Thread.Sleep(2500);
            Activate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fill_document_on_day();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            datepicker_date.SelectedDate = DateTime.Now;
            button_no.Visibility = Visibility.Hidden;
            button_yes.Visibility = Visibility.Hidden;
            label__1.Visibility = Visibility.Hidden;
        }

        private void button_yes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                app.Activate();
                doc.PrintOut();
                doc.Close();
                app.Quit();
                Close();
            }
            catch
            {
                Close();
            }
        }

        private void button_no_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                app.Activate();
                doc.Close();
                app.Quit();
                Close();
            }
            catch
            {
                Close();
            }
        }
    }
}
