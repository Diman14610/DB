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
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace DataBase
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string path = "";

        bool count = true;
        bool primary = true;

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = false;
            button3.Enabled = false;
            radioButton1.Enabled = true;
            path = textBox1.Text;

            try
            {
                Directory.CreateDirectory("Tables");
            }
            catch { }
            File.CreateText("Tables/" + textBox1.Text + ".dbs").Dispose();
            textBox4.Text += "CREATE TABLE " + path + " (";
            textBox5.Text += "CREATE TABLE " + path + " (";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = textBox4.Text.Remove(textBox4.TextLength - 1);
            textBox5.Text = textBox5.Text.Remove(textBox5.TextLength - 1);
            textBox4.Text += Environment.NewLine + ")"+Environment.NewLine;
            textBox5.Text += Environment.NewLine + ")" + Environment.NewLine;
            File.WriteAllText("Tables/" + path +".dbs", textBox5.Text);
            textBox1.Enabled = true;
            textBox1.Clear();
            button3.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            radioButton1.Enabled = false;
            textBox5.Clear();
            count = true;
            primary = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // При нажатии radiobutton записывает первичный ключ
            if (radioButton1.Checked == true)
            {
                textBox4.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " PRIMARY KEY IDENTITY(1,1),";
                textBox5.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " PRIMARY KEY IDENTITY(1,1),";
                radioButton1.Checked = false;
                textBox2.Clear();
                textBox2.Focus();
                textBox3.Clear();
            }
            else
            {
                if (count == true)
                {
                    if (!string.IsNullOrEmpty(str1) | !string.IsNullOrWhiteSpace(str1))
                    {
                        string strr = textBox2.Text;
                        textBox2.Text = str1 + "_" + strr;
                        textBox2.AppendText(stra);
                    }
                    else if (!string.IsNullOrEmpty(str2) | !string.IsNullOrWhiteSpace(str2))
                    {
                        string strr = textBox2.Text;
                        textBox2.Text = str2 + "_" + strr;
                        textBox2.AppendText(stra);
                    }
                    count = false;
                }
                else
                {
                    textBox4.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " NULL,";
                    textBox5.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " NULL,";
                    textBox2.Clear();
                    textBox2.Focus();
                    textBox3.Clear();
                    primary = false;
                }
            }

            // Добавляет перивчный ключ 1 раз при создании таблицы
            if (primary == true)
            {
                textBox4.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " PRIMARY KEY IDENTITY(1,1),";
                textBox5.Text += Environment.NewLine + textBox2.Text + " " + textBox3.Text.ToUpper() + " PRIMARY KEY IDENTITY(1,1),";
                radioButton1.Checked = false;
                textBox2.Clear();
                textBox2.Focus();
                textBox3.Clear();
                primary = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20)
            {
                e.KeyChar = '\0';//при нажатие пробела не будет отступа
                textBox2.AppendText("_");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Text = "INT";
            button4.Enabled = true;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            button4.Enabled = true;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            button4.Enabled = false;
        }

        string str1;
        string str2;
        string stra;

        private void Form2_Load(object sender, EventArgs e)
        {
            var collection = new AutoCompleteStringCollection
            {
                "int", "nvarchar()", "bigint", "money","float",
                "real","datetime","image","bit","time","date"
            };
            textBox3.AutoCompleteCustomSource = collection;
            try
            {
                str1 = File.ReadAllText("Filtres/One.txt");
                str2 = File.ReadAllText("Filtres/Two.txt");
                stra = File.ReadAllText("Filtres/A.txt");
            }
            catch { }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox5.Text.Remove(textBox5.TextLength - 1);
            textBox5.Text += Environment.NewLine + ")" + Environment.NewLine;
            File.WriteAllText("Tables/" + path + ".dbs", textBox5.Text);
            textBox4.Text = textBox4.Text.Remove(textBox4.TextLength - 1);
            textBox4.Text += Environment.NewLine + ")" + Environment.NewLine;
            saveFileDialog1.Filter = "TXT File (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            File.WriteAllText(filename, textBox4.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = "INT";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string str = textBox1.Text.First().ToString();
            textBox1.Text = str.ToUpper() + textBox1.Text.Remove(0,1);
        }

        private void автоВставкаСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
            try
            {
                str1 = File.ReadAllText("Filtres/One.txt");
                str2 = File.ReadAllText("Filtres/Two.txt");
                stra = File.ReadAllText("Filtres/A.txt");
            }
            catch { }
        }
    }
}
