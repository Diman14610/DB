using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            List<string> sourse = new List<string>();
            List<string> countFields = new List<string>();
            for (int l = 0; l < Form7.strCountFiles + 1; l++)//Добавляет в коллекцию данные с файлов по кол-ву файлов
            {
                sourse.AddRange(File.ReadLines("Insert/" + l + ".txt"));
            }

            int i = 0;//Счетчик для последовального добавления с коллекции sourse
            for (; i < Form7.strGenerationCount; i++)
            {
                textBox1.Text += "INSERT INTO " + Form7.strName + " VALUES (" + sourse[i] + "," + Environment.NewLine;
            }
            /* listdata - Буфер обмена
                т.е. 
                    str1 = INSERT INTO <Таблица> VALUES (<Пример 1>,<Пример 2>)
                         str2 = srt1 + <Пример 3>
             */
            List<string> listdata = new List<string>(); 


            listdata.AddRange(textBox1.Lines);
            textBox1.Clear();
            for (int m = 0; m < Form7.strCountFiles; m++)
            {
                for (int j = 0; j < Form7.strGenerationCount; j++)
                {
                    textBox1.Text += listdata[j] + sourse[i] + "," + Environment.NewLine;
                    i++;
                }
                listdata.Clear();
                listdata.AddRange(textBox1.Lines);
                textBox1.Clear();
            }
            try
            {
                listdata.ToList().ForEach(r => textBox1.Text += r.Remove(r.Count() - 1, 1) + ")" + Environment.NewLine);
            }
            catch { }
       

        }
    }
}
