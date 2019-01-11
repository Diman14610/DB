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
    /// Логика взаимодействия для Print_write_on_reception.xaml
    /// </summary>
    public partial class Print_write_on_reception : Window
    {
        public Print_write_on_reception()
        {
            InitializeComponent();
        }

        struct write
        {
            public DateTime date { get; set; }
            public TimeSpan? time { get; set; }
            public string patient { get; set; }
            public string doctor { get; set; }
            public write(DateTime d, TimeSpan t, string p, string doc)
            {
                date = d;
                time = t;
                patient = p;
                doctor = doc;
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
            switch (Operator.buff_open_print_write)
            {
                case 1:
                    {
                      
                        string[] vs = DateTime.Now.ToShortTimeString().Split(':');
                        string current = Environment.CurrentDirectory + "\\Запись на день" + "\\ На " + datepicker_date.SelectedDate.Value.ToLongDateString()
                            + " В " + vs[0] + "." + vs[1] + ".docx";
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Запись на день");
                        File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Шаблон запись на прием.docx", current, true);

                        app = new word.Application();
                        doc = new word.Document();

                        app.Visible = false;

                        // Создаём объект приложения
                        // Путь до шаблона документа
                        string source = current;
                        doc = app.Documents.Open(source);
                        // Открываем
                        doc.Activate();

                        word.Bookmarks wBookmarks = doc.Bookmarks;
                        word.Range wRange;
                        int ij = 0;
                        string TextForWord;
                        TextForWord = "Запись на прием на " + datepicker_date.SelectedDate.Value.ToShortDateString() + " К врачу " + Select_doctor.fio;
                        string[] data = new string[1] { TextForWord };

                        foreach (word.Bookmark mark in wBookmarks)
                        {
                            wRange = mark.Range;
                            wRange.Text = data[ij];
                            ij++;
                        }


                        //Таблицу вставляем в начало документа
                        Object start = TextForWord.Length;
                        Object end = TextForWord.Length;
                        word.Range wordrange = doc.Range(ref start, ref end);
                        Object defaultTableBehavior =
                           word.WdDefaultTableBehavior.wdWord9TableBehavior;
                        Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;
                        MessageBox.Show(Select_doctor.doctor_s.Табельный_номер_врача.ToString());
                        int buff_write = 1;
                        using (context_stat_talon context = new context_stat_talon())
                        {
                            foreach (var v in context.Запись_на_прием.Where(c => c.Дата_приема == datepicker_date.SelectedDate.Value & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача))
                            {
                                buff_write++;
                            }
                        }
                        //Добавляем таблицу и получаем объект wordtable 
                        string[] sv = new string[] { "Дата приема", "Время приема", "Ф.И.О. пациента" };
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
                                       where c.Дата_приема == datepicker_date.SelectedDate.Value & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача
                                       orderby c.Дата_приема, c.Время_приема ascending
                                       select c;
                            foreach (var v in v222)
                            {
                                li_w.Add(new write
                                {
                                    date = v.Дата_приема.Value,
                                    time = v.Время_приема.Value,
                                    //doctor = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                                    patient = v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента
                                });
                            }
                            //wordcellrange = doc.Tables[1].Cell(1, 5).Range;
                            for (int i1 = 2; i1 != li_w.Count + 2; i1++)
                            {
                                for (int j1 = 1; j1 != 4; j1++)
                                {
                                    wordcellrange = wordtable.Cell(i1, j1).Range;
                                    wordcellrange.Text = li_w[i1 - 2].date.ToShortDateString();
                                    wordcellrange = wordtable.Cell(i1, 2).Range;
                                    wordcellrange.Text = li_w[i1 - 2].time.ToString().Remove(5, 3);
                                    wordcellrange = wordtable.Cell(i1, 3).Range;
                                    //wordcellrange.Text = li_w[i1 - 2].doctor;
                                    //wordcellrange = wordtable.Cell(i1, 4).Range;
                                    wordcellrange.Text = li_w[i1 - 2].patient;
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
                        doctor11.Close();
                        Thread.Sleep(2500);
                        Activate();
                        break;
                    }
                case 2:
                    {
                        string[] vs = DateTime.Now.ToShortTimeString().Split(':');
                        string current = Environment.CurrentDirectory + "\\Запись на неделю" + "\\С " + DateTime.Now.ToLongDateString() + " По " +
                            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 7).ToLongDateString() + " В " + vs[0] + "." + vs[1] + ".docx";
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Запись на неделю");
                        File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Шаблон запись на прием.docx", current, true);

                        app = new word.Application();
                        doc = new word.Document();

                        app.Visible = false;

                        // Создаём объект приложения
                        // Путь до шаблона документа
                        string source = current;
                        doc = app.Documents.Open(source);
                        // Открываем
                        doc.Activate();

                        string[] vs_label = label_begin_end.Content.ToString().Split(' ');

                        word.Bookmarks wBookmarks = doc.Bookmarks;
                        word.Range wRange;
                        int ij = 0;
                        string TextForWord;
                        TextForWord = "Запись на прием на " + vs_label[0] + "-" + vs_label[1] + " К врачу " + Select_doctor.fio;
                        string[] data = new string[1] { TextForWord };

                        foreach (word.Bookmark mark in wBookmarks)
                        {
                            wRange = mark.Range;
                            wRange.Text = data[ij];
                            ij++;
                        }


                        //Таблицу вставляем в начало документа
                        Object start = TextForWord.Length;
                        Object end = TextForWord.Length;

                        word.Range wordrange = doc.Range(ref start, ref end);
                        Object defaultTableBehavior =
                           word.WdDefaultTableBehavior.wdWord9TableBehavior;
                        Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;

                        int buff_write = 1;
                        
                        DateTime vs_label1 = DateTime.Parse(vs_label[0]);
                        DateTime vs_label2 = DateTime.Parse(vs_label[2]);
                        using (context_stat_talon context = new context_stat_talon())
                        {
                            foreach (var v in context.Запись_на_прием.Where(c => c.Дата_приема >= vs_label1 & c.Дата_приема <= vs_label2 & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача))
                            {
                                buff_write++;
                            }
                        }
                        //Добавляем таблицу и получаем объект wordtable 
                        string[] sv = new string[] { "Дата приема", "Время приема", "Ф.И.О. пациента" };
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
                                       join cd in context.Врач on c.Табельный_номер_врача equals cd.Табельный_номер_врача
                                       where c.Дата_приема >= vs_label1 & c.Дата_приема <= vs_label2 & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача
                                       orderby c.Дата_приема, c.Время_приема ascending
                                       select c;
                            foreach (var v in v222)
                            {
                                li_w.Add(new write
                                {
                                    date = v.Дата_приема.Value,
                                    time = v.Время_приема.Value,
                                    //doctor = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                                    patient = v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента
                                });
                            }
                            //wordcellrange = doc.Tables[1].Cell(1, 5).Range;
                            for (int i1 = 2; i1 != li_w.Count + 2; i1++)
                            {
                                for (int j1 = 1; j1 != 4; j1++)
                                {
                                    wordcellrange = wordtable.Cell(i1, j1).Range;
                                    wordcellrange.Text = li_w[i1 - 2].date.ToShortDateString();
                                    wordcellrange = wordtable.Cell(i1, 2).Range;
                                    wordcellrange.Text = li_w[i1 - 2].time.ToString().Remove(5, 3);
                                    wordcellrange = wordtable.Cell(i1, 3).Range;
                                    //wordcellrange.Text = li_w[i1 - 2].doctor;
                                    //wordcellrange = wordtable.Cell(i1, 4).Range;
                                    wordcellrange.Text = li_w[i1 - 2].patient;
                                }
                            }
                        }
                        word.Dialog dialog = app.Dialogs[word.WdWordDialog.wdDialogFilePrintSetup];

                        label_begin_end.Visibility = Visibility.Hidden;
                        button___.Visibility = Visibility.Hidden;
                        button____.Visibility = Visibility.Hidden;
                        label___.Visibility = Visibility.Visible;
                        button__yes.Visibility = Visibility.Visible;
                        button__no.Visibility = Visibility.Visible;
                        app.Visible = true;
                        app.Activate();
                        doctor11.Close();
                        Thread.Sleep(2500);
                        Activate();
                        break;
                    }
                case 3:
                    {
                        string[] vs = DateTime.Now.ToShortTimeString().Split(':');
                        string current = Environment.CurrentDirectory + "\\Запись на месяц" + "\\ На " + label_begin_end_Copy.Content
                            + " В " + vs[0] + "." + vs[1] + ".docx";
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Запись на месяц");
                        File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Шаблон запись на прием.docx", current, true);

                        app = new word.Application();
                        doc = new word.Document();

                        app.Visible = false;

                        // Создаём объект приложения
                        // Путь до шаблона документа
                        string source = current;
                        doc = app.Documents.Open(source);
                        // Открываем
                        doc.Activate();

                        word.Bookmarks wBookmarks = doc.Bookmarks;
                        word.Range wRange;
                        int ij = 0;
                        string TextForWord;
                        TextForWord = "Запись на прием на " +  label_begin_end_Copy.Content + " К врачу " + Select_doctor.fio;
                        string[] data = new string[1] { TextForWord };

                        foreach (word.Bookmark mark in wBookmarks)
                        {
                            wRange = mark.Range;
                            wRange.Text = data[ij];
                            ij++;
                        }


                        //Таблицу вставляем в начало документа
                        Object start = TextForWord.Length;
                        Object end = TextForWord.Length;

                        word.Range wordrange = doc.Range(ref start, ref end);
                        Object defaultTableBehavior =
                           word.WdDefaultTableBehavior.wdWord9TableBehavior;
                        Object autoFitBehavior = word.WdAutoFitBehavior.wdAutoFitWindow;

                        int buff_write = 1;
                        using (context_stat_talon context = new context_stat_talon())
                        {
                            foreach (var v in context.Запись_на_прием.Where(c => c.Дата_приема.Value.Year == DateTime.Now.Year && c.Дата_приема.Value.Month == DateTime.Now.Month & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача))
                            {
                                buff_write++;
                            }
                        }
                        //Добавляем таблицу и получаем объект wordtable 
                        string[] sv = new string[] { "Дата приема", "Время приема", "Ф.И.О. пациента" };
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
                                       where c.Дата_приема.Value.Year == DateTime.Now.Year && c.Дата_приема.Value.Month == DateTime.Now.Month & c.Врач.Табельный_номер_врача == Select_doctor.doctor_s.Табельный_номер_врача
                                       orderby c.Дата_приема,c.Время_приема ascending
                                       select c;
                            foreach (var v in v222)
                            {
                                li_w.Add(new write
                                {
                                    date = v.Дата_приема.Value,
                                    time = v.Время_приема.Value,
                                    //doctor = splite_fio(v.Врач.Фамилия_врача + " " + v.Врач.Имя_врача + " " + v.Врач.Отчество_врача),
                                    patient = v.Пациент.Фамилия_пациента + " " + v.Пациент.Имя_пациента + " " + v.Пациент.Отчество_пациента
                                });
                            }
                            //wordcellrange = doc.Tables[1].Cell(1, 5).Range;
                            for (int i1 = 2; i1 != li_w.Count + 2; i1++)
                            {
                                for (int j1 = 1; j1 != 4; j1++)
                                {
                                    wordcellrange = wordtable.Cell(i1, j1).Range;
                                    wordcellrange.Text = li_w[i1 - 2].date.ToShortDateString();
                                    wordcellrange = wordtable.Cell(i1, 2).Range;
                                    wordcellrange.Text = li_w[i1 - 2].time.ToString().Remove(5, 3);
                                    wordcellrange = wordtable.Cell(i1, 3).Range;
                                    wordcellrange.Text = li_w[i1 - 2].patient;
                                }
                            }
                        }
                        word.Dialog dialog = app.Dialogs[word.WdWordDialog.wdDialogFilePrintSetup];
                        label_begin_end_Copy.Visibility = Visibility.Hidden;
                        button_print_Copy.Visibility = Visibility.Hidden;
                        button_close_Copy.Visibility = Visibility.Hidden;
                        button_no_Copy.Visibility = Visibility.Visible;
                        button_yes_Copy.Visibility = Visibility.Visible;
                        label__1_Copy.Visibility = Visibility.Visible;
                        app.Visible = true;
                        app.Activate();
                        doctor11.Close();
                        Thread.Sleep(2500);
                        Activate();
                        break;
                    }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fill_document_on_day();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Doctor11 doctor11 = new Doctor11();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            doctor11.Show();
            switch (Operator.buff_open_print_write)
            {
                case 1:
                    {
                        grid_day.Visibility = Visibility.Visible;
                        grid_week.Visibility = Visibility.Collapsed;
                        grid_month.Visibility = Visibility.Collapsed;

                        datepicker_date.SelectedDate = DateTime.Now;
                        button_no.Visibility = Visibility.Hidden;
                        button_yes.Visibility = Visibility.Hidden;
                        label__1.Visibility = Visibility.Hidden;
                        grid_week.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 2:
                    {
                        label_begin_end.Content = DateTime.Now.ToShortDateString() + " — " + new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 7).ToShortDateString();
                        label_begin_end.Visibility = Visibility.Visible;
                        button___.Visibility = Visibility.Visible;
                        button____.Visibility = Visibility.Visible;
                        label___.Visibility = Visibility.Hidden;
                        button__yes.Visibility = Visibility.Hidden;
                        button__no.Visibility = Visibility.Hidden;

                        grid_day.Visibility = Visibility.Collapsed;
                        grid_week.Visibility = Visibility.Visible;
                        grid_month.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 3:
                    {
                        string[] ss_arr = new string[] { "Январь","Февраль","Март","Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" }; 
                        label_begin_end_Copy.Content = ss_arr[DateTime.Now.Month] + " " + DateTime.Now.Year;

                        button_no_Copy.Visibility = Visibility.Hidden;
                        button_yes_Copy.Visibility = Visibility.Hidden;
                        label__1_Copy.Visibility = Visibility.Hidden;

                        grid_day.Visibility = Visibility.Collapsed;
                        grid_week.Visibility = Visibility.Collapsed;
                        grid_month.Visibility = Visibility.Visible;
                        break;
                    }
            }
            Activate();
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Select_doctor select = new Select_doctor();
            select.ShowDialog();
        }
    }
}
