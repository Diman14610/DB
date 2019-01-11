using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace DataBase
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }


        private void Form7_Load(object sender, EventArgs e)
        {
            try/*Блок, который загружется все имена таблиц в текстовое поле*/
            {
                var col = new AutoCompleteStringCollection();
                string[] s = Directory.GetFiles("Tables/", "*.dbs");

                s.ToList().ForEach(r => col.Add(r.Replace(".dbs", "").Replace("Tables/", "")));
                textBox1.AutoCompleteCustomSource = col;
            }
            catch { }
            sourse.AddRange(File.ReadAllLines("Data/name.txt", Encoding.UTF8));//Имя
            sourseM.AddRange(File.ReadAllLines("Data/middle.txt", Encoding.UTF8));//Фамилия
            sourseL.AddRange(File.ReadAllLines("Data/last.txt", Encoding.UTF8));//Отчество
        }

        public List<ComboBox> listcomboBox = new List<ComboBox>();
        public List<ComboBox> listcomboBox2 = new List<ComboBox>();

        private void button2_Click(object sender, EventArgs e)
        {
            var collection = new AutoCompleteStringCollection
            {
                "nvarchar()", "bigint", "money","float",
                "real","datetime","image","bit","time","date"
            };
            string[] collectionstr =
            {
                "NVARCHAR()", "BIGINT", "MONEY","FLOAT",
                "REAL","DATETIME","IMAGE","BIT","TIME","DATE"
            };
            string[] collectioncategory =
            {
                "Телефон","Имя","Фамилия","Отчество",
                "Возраст","Дата и время","Дата","Время",
                "Деньги","Штрих код (EAN 13)","Серия и номер паспорта",
                "Серия паспорта","Номер паспорта"
            };
            panel1.Controls.Clear();
            listcomboBox.Clear();
            listcomboBox2.Clear();
            List<Label> listlabels = new List<Label>();
            List<Label> listlabels2 = new List<Label>();
            List<Label> listlabels3 = new List<Label>();

            int count = Convert.ToInt16(textBox2.Text) + 1;
            int y = 9;
            int yy = 6;//19
            for (int i = 1; i < count; i++)
            {
                listlabels.Add(new Label
                {
                    Text = i.ToString(),
                    ForeColor = Color.Black,
                    Location = new Point(6, y),
                    Size = new Size(45, 20)

                });
                listlabels2.Add(new Label
                {
                    Text = "Тип данных",
                    ForeColor = Color.Black,
                    Location = new Point(248, y),
                    Size = new Size(106, 20)

                });
                listlabels3.Add(new Label
                {
                    Text = "Категория данных",
                    ForeColor = Color.Black,
                    Location = new Point(564, y),
                    Size = new Size(165, 20)

                });
                listcomboBox.Add(new ComboBox
                {
                    AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                    AutoCompleteSource = AutoCompleteSource.CustomSource,
                    Size = new Size(177, 28),
                    ForeColor = Color.Black,
                    Location = new Point(65, y),
                    AutoCompleteCustomSource = collection,

                });
                listcomboBox2.Add(new ComboBox
                {
                    AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                    AutoCompleteSource = AutoCompleteSource.CustomSource,
                    Size = new Size(198, 28),
                    ForeColor = Color.Black,
                    Location = new Point(360, y)

                });
                y += 34;
                yy += 34;
            }

            foreach (Label lb in listlabels)
            {
                panel1.Controls.Add(lb);
            }
            foreach (Label lb in listlabels2)
            {
                panel1.Controls.Add(lb);
            }
            foreach (Label lb in listlabels3)
            {
                panel1.Controls.Add(lb);
            }
            foreach (ComboBox lb in listcomboBox)
            {
                lb.Items.AddRange(collectionstr);
                panel1.Controls.Add(lb);
            }
            foreach (ComboBox lb in listcomboBox2)
            {
                lb.Items.AddRange(collectioncategory);
                panel1.Controls.Add(lb);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(textBox2.Text) > 100)
                textBox2.Text = "100";
        }

        void Algorithm(string str1, string str2, string namefile)
        {
            // string[] collectionstr =
            //{
            //     "INT", "NVARCHAR()", "BIGINT", "MONEY","FLOAT",
            //     "REAL","DATETIME","IMAGE","BIT","TIME","DATE"
            // };
            // string[] collectioncategory =
            //{
            //     "Телефон","Имя","Фамилия","Отчество",
            //     "Возраст","Дата и время","Дата","Время",
            //     "Деньги","Штрих код (EAN 13)","Серия и номер паспорта"
            // };
            File.Delete("Insert/" + namefile + ".txt"); //Удаляет файлы для записи в них
            if (str1 == "BIGINT" & str2 == "Штрих код (EAN 13)")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmBigInt(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Телефон")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmBigIntNvarchar(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Имя")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarName(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Фамилия")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarMiddle(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Отчество")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarLast(namefile);
            }
            //else if (str1 == "NVARCHAR()" & str2 == "Возраст")
            //{
            //    int age = Convert.ToInt16(str1.Replace("NVARCHAR(", "").Replace(")", ""));
            //    MessageBox.Show(age + "");
            //    if (age.ToString() == "")
            //    {
            //        for (int i = 0; i < strGenerationCount; i++)
            //            AlgorithmNvarAge(namefile);
            //    }
            //    else
            //    {
            //        for (int i = 0; i < strGenerationCount; i++)
            //            AlgorithmNvarAgeWith(namefile, age);
            //    }
            //}
            else
            {
                MessageBox.Show("Алгоритма с комбинацией полей: " + "(" + str1 + ")" + " " + "(" + str2 + ")" + " нету!");
            }
        }

        Random random = new Random();

        /*********************************************************************************************************************/

        void AlgorithmBigInt(string namefile)
        {
            string barcode = "";
            for (int i = 0; i < 13; i++)
            {
                barcode += random.Next(0, 9);
            }
            File.AppendAllText("Insert/" + namefile + ".txt", barcode + Environment.NewLine);
        }

        void AlgorithmBigIntNvarchar(string namefile)
        {
            string phone = "";
            for (int i = 0; i < 10; i++)
            {
                phone += random.Next(0, 9);
            }
            File.AppendAllText("Insert/" + namefile + ".txt", phone = "\'" + phone.Insert(0, "8") + "\'" + Environment.NewLine);
        }

        public static List<string> sourse = new List<string>();//Коллекция имен
        public static List<string> sourseM = new List<string>();//Коллекция фамилий
        public static List<string> sourseL = new List<string>();//Коллекция отчеств

        void AlgorithmNvarName(string namefile)
        {
            File.AppendAllText("Insert/" + namefile + ".txt", "\'" + sourse[random.Next(0, 1987)] + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarMiddle(string namefile)
        {
            File.AppendAllText("Insert/" + namefile + ".txt", "\'" + sourseM[random.Next(0, 251334)] + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarLast(string namefile)
        {
            File.AppendAllText("Insert/" + namefile + ".txt", "\'" + sourseL[random.Next(0, 77)] + "\'" + Environment.NewLine);
        }

        //void AlgorithmNvarAgeWith(string namefile, int age)
        //{
        //    File.AppendAllText("Insert/" + namefile + ".txt", "\'" + random.Next(1, age) + "\'" + Environment.NewLine);
        //}

        //void AlgorithmNvarAge(string namefile)
        //{
        //    File.AppendAllText("Insert/" + namefile + ".txt", "\'" + random.Next(1, 100) + "\'" + Environment.NewLine);
        //}
        /*********************************************************************************************************************/

        public static int strGenerationCount;//Кол-во генерируемых полей
        public static string strName;//Имя таблицы
        public static int strCountFiles;//Кол-во файлов
        public static int strCountComboBox;//Кол-во combobox

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) | string.IsNullOrWhiteSpace(textBox1.Text))
                return;
            else
            {
                strCountComboBox = listcomboBox.Count;
                strGenerationCount = Convert.ToInt16(numericUpDown1.Value);
                strName = textBox1.Text;
                for (int i = 0; i < Convert.ToInt16(textBox2.Text); i++)
                {
                    try
                    {
                        Algorithm(listcomboBox[i].SelectedItem.ToString(), listcomboBox2[i].SelectedItem.ToString(), i.ToString());
                        Thread.Sleep(15);
                    }
                    catch { }
                    strCountFiles = i;
                }
                Form8 form = new Form8();
                form.ShowDialog();
            }
        }
    }
}
