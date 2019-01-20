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
            try//Блок, который загружется все имена таблиц в текстовое поле
            {
                var col = new AutoCompleteStringCollection();
                string[] s = Directory.GetFiles("Resource/Tables/", "*.dbs");

                s.ToList().ForEach(r => col.Add(r.Replace(".dbs", "").Replace("Resource/Tables/", "")));
                textBox1.AutoCompleteCustomSource = col;
            }
            catch { }
            sourseNB.AddRange(File.ReadAllLines("Resource/Data/nameB.txt", Encoding.UTF8));//Имена М
            sourseNG.AddRange(File.ReadAllLines("Resource/Data/nameG.txt", Encoding.UTF8));//Имена Ж
            sourseM.AddRange(File.ReadAllLines("Resource/Data/middle.txt", Encoding.UTF8));//Фамилия
            sourseL.AddRange(File.ReadAllLines("Resource/Data/last.txt", Encoding.UTF8));//Отчество
        }

        public List<ComboBox> listcomboBox = new List<ComboBox>();
        public List<ComboBox> listcomboBox2 = new List<ComboBox>();

        private void button2_Click(object sender, EventArgs e)
        {
            var collection = new AutoCompleteStringCollection
            {
                "nvarchar()","int", "bigint", "money","smallmoney","decimal",
                "numeric","datetime","time","date"
            };
            string[] collectionstr =
            {
                "NVARCHAR()","INT" ,"BIGINT", "MONEY","SMALLMONEY","DECIMAL",
                "NUMERIC","DATETIME","TIME","DATE"
            };//decimal/numeric нужны для точного хранения денег!
            panel1.Controls.Clear();
            listcomboBox.Clear();
            listcomboBox2.Clear();
            List<Label> listlabels = new List<Label>();
            List<Label> listlabels2 = new List<Label>();
            List<Label> listlabels3 = new List<Label>();

            int count = Convert.ToInt16(textBox2.Text) + 1;
            int y = 9;
            int yy = 6;//19
            int nameCountLb = 0;
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
                    Name = nameCountLb.ToString(),
                    AutoCompleteCustomSource = collection,

                });
                listcomboBox2.Add(new ComboBox
                {
                    Size = new Size(198, 28),
                    ForeColor = Color.Black,
                    Name = nameCountLb.ToString(),
                    Location = new Point(360, y)

                });
                y += 34;
                yy += 34;
                nameCountLb++;
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
                panel1.Controls.Add(lb);
            }
            foreach (ComboBox lb in listcomboBox)
            {
                lb.SelectedIndexChanged += EventResultText;
            }
        }

        //∨∨ Метод переназначает категорию данных с учетом типа данных ∨∨
        void OverideComboBox2(object s, int name)
        {

            if (s.ToString() == "NVARCHAR()")
            {
                string[] collectioncategory =
                {
                    "Фамилия","Имя","Отчество","Телефон",
                    "Возраст","Возраст {}-{}","Серия и номер паспорта",
                    "Серия паспорта","Номер паспорта",
                };
                listcomboBox2[name].Text = "";
                listcomboBox2[name].Items.Clear();
                listcomboBox2[name].Items.AddRange(collectioncategory);
            }
            else if (s.ToString() == "INT")
            {
                string[] collectioncategory =
                {
                    "Счетчик"
                };
                listcomboBox2[name].Text = "";
                listcomboBox2[name].Items.Clear();
                listcomboBox2[name].Items.AddRange(collectioncategory);
            }
            else if (s.ToString() == "BIGINT")
            {
                string[] collectioncategory =
                {
                    "Штрих код (EAN XIII)"
                };
                listcomboBox2[name].Text = "";
                listcomboBox2[name].Items.Clear();
                listcomboBox2[name].Items.AddRange(collectioncategory);
            }
            else if (s.ToString() == "MONEY" | s.ToString() == "SMALLMONEY")
            {
                string[] collectioncategory =
                {
                    "Деньги","Деньги {}-{}"
                };
                listcomboBox2[name].Text = "";
                listcomboBox2[name].Items.Clear();
                listcomboBox2[name].Items.AddRange(collectioncategory);
            }
            else if (s.ToString() == "TIME")
            {
                string[] collectioncategory =
                {
                    "Время"
                };
                listcomboBox2[name].Text = "";
                listcomboBox2[name].Items.Clear();
                listcomboBox2[name].Items.AddRange(collectioncategory);
            }

        }
        //∧∧ Метод переназначает категорию данных с учетом типа данных ∧∧

        void EventResultText(object s, EventArgs e)
        {
            ComboBox cb = (s as ComboBox);
            OverideComboBox2(cb.SelectedItem, Convert.ToInt16(cb.Name));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt16(textBox2.Text) > 100)
                    textBox2.Text = "100";
            }
            catch { }
        }

        //∨∨ Метод определяющий алгоритм генерации данных ∨∨
        void Algorithm(string str1, string str2, string namefile)
        {
            File.Delete("Resource/Insert/" + namefile + ".txt"); //Удаляет файлы для записи в них

            string strIntValues = str2.Replace("Возраст ","").Replace("Деньги ","").Replace("{","").Replace("}","").Replace("-"," ");
            str2 = System.Text.RegularExpressions.Regex.Replace(str2, @"\d", "");

            if (str1 == "BIGINT" & str2 == "Штрих код (EAN XIII)")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmBigInt(namefile);
            }
            else if (str1 == "INT" & str2 == "Счетчик")
            {
                AlgorithmIntCount(namefile, strGenerationCount);
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
            else if (str1 == "NVARCHAR()" & str2 == "Возраст")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarAge(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Возраст {}-{}")
            {
                string[] das = strIntValues.Split(' ');
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarAgeWithNum(namefile, Convert.ToInt16(das[0]), Convert.ToInt16(das[1]));
            }
            else if (str1 == "NVARCHAR()" & str2 == "Номер паспорта")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarNumberPas(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Серия паспорта")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarSerPas(namefile);
            }
            else if (str1 == "NVARCHAR()" & str2 == "Серия и номер паспорта")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmNvarSerNumPas(namefile);
            }
            else if ((str1 == "MONEY" | str1 == "SMALLMONEY") & str2 == "Деньги")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmMoney(namefile);
            }
            else if ((str1 == "MONEY" | str1 == "SMALLMONEY") & str2 == "Деньги {}-{}")
            {
                string[] das = strIntValues.Split(' ');
                try
                {
                    for (int i = 0; i < strGenerationCount; i++)
                        AlgorithmMoneyWithNum(namefile, Convert.ToInt32(das[0]), Convert.ToInt32(das[1]));
                }
                catch
                {
                    MessageBox.Show(das[0]+"-"+das[1]+ " Числовой диапозон слишком велик или маленький для категории (Деньги {}-{})"+
                        "  --> Попробуйте категорию (Деньги) <--", "Ошибка категории Деньги {}-{} !",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else if (str1 == "TIME" & str2 == "Время")
            {
                for (int i = 0; i < strGenerationCount; i++)
                    AlgorithmTime(namefile);
            }
            else
            {
                MessageBox.Show("Алгоритма с комбинацией полей: " + "(" + str1 + ")" + " " + "(" + str2 + ")" + " нету!");
            }
        }
        //∧∧ Метод определяющий алгоритм генерации данных ∧∧

        //∨∨ Методы генерирующие данные ∨∨
        #region Algorithm<Тип данных>(Перменные)

        Random random = new Random();
        public static List<string> sourseNB = new List<string>();//Коллекция имен М
        public static List<string> sourseNG = new List<string>();//Коллекция имен Ж
        public static List<string> sourseM = new List<string>();//Коллекция фамилий
        public static List<string> sourseL = new List<string>();//Коллекция отчеств

        void AlgorithmBigInt(string namefile)//Штрих код
        {
            string barcode = "";
            for (int i = 0; i < 13; i++)
            {
                barcode += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", barcode + Environment.NewLine);
        }

        void AlgorithmIntCount(string namefile, int count)//Счетчик
        {
            for (int i = 1; i < count + 1; i++)
            {
                File.AppendAllText("Resource/Insert/" + namefile + ".txt", i + Environment.NewLine);
            }

        }

        void AlgorithmBigIntNvarchar(string namefile)//Телефон
        {
            string phone = "";
            for (int i = 0; i < 10; i++)
            {
                phone += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", phone = "\'" + phone.Insert(0, "7") + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarMiddle(string namefile)//Фамилия
        {
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + sourseM[random.Next(0, 251334)] + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarName(string namefile)//Имя
        {
            int b = random.Next(0, 2);
            if (b == 0)
                File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + sourseNB[random.Next(0, 885)] + "\'" + Environment.NewLine);
            else
                File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + sourseNG[random.Next(0, 700)] + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarLast(string namefile)//Отчество
        {
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + sourseL[random.Next(0, 77)] + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarAge(string namefile)//Возраст
        {
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + random.Next(1, 100) + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarAgeWithNum(string namefile,int one,int two)//Возраст {}-{}
        {
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + random.Next(one, two + 1) + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarSerPas(string namefile)//Серия паспорта
        {
            string ser = "";
            for (int i = 0; i < 4; i++)
            {
                ser += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + ser + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarNumberPas(string namefile)//Номер паспорта
        {
            string num = "";
            for (int i = 0; i < 6; i++)
            {
                num += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + num + "\'" + Environment.NewLine);
        }

        void AlgorithmNvarSerNumPas(string namefile)//Серия и номер паспорта
        {
            string snp = "";
            for (int i = 0; i < 10; i++)
            {
                snp += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + snp + "\'" + Environment.NewLine);
        }

        void AlgorithmMoney(string namefile)//Деньги
        {
            string m = "";//922,337,203,685,477
            int r = random.Next(1,16);
            for (int i = 0; i < r; i++)
            {
                m += random.Next(0, 9);
            }
            File.AppendAllText("Resource/Insert/" + namefile + ".txt", m  + Environment.NewLine);
        }

        void AlgorithmMoneyWithNum(string namefile, int one, int two)//Деньги {}-{}
        {
            try
            {
                File.AppendAllText("Resource/Insert/" + namefile + ".txt", random.Next(one, two) + Environment.NewLine);
            }
            catch
            { }//922,337,203,685,477
        }

        void AlgorithmTime(string namefile)//Время
        {
            int t1;//12:46:29
            int t2;
            int t3;
            t1 = random.Next(0, 23);
            t2 = random.Next(0, 60);
            t3 = random.Next(0, 60);

            File.AppendAllText("Resource/Insert/" + namefile + ".txt", "\'" + t1 + ":" + t2 + ":" + t3 + "\'" + Environment.NewLine);
        }

        #endregion
        //∧∧ Методы генерирующие данные ∧∧

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
                        Algorithm(listcomboBox[i].SelectedItem.ToString(), listcomboBox2[i].Text, i.ToString());
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
