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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        public bool a1 = false;
        public bool a2 = false;
        string[] list = new string[4] { "Пациент", "Товар", "Курьер", "Сотрудник" };

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox1.Checked == true)
                textBox2.Text = textBox1.Text + "_" + list[random.Next(0, 3)] + "а";
            else
                textBox2.Text = textBox1.Text + "_" + list[random.Next(0, 3)];
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox2.Checked == true)
                textBox3.Text = "№_" + list[random.Next(0, 3)] + "а";
            else
                textBox3.Text = "ID_" + list[random.Next(0, 3)];
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox2.Checked == true)
                textBox3.Text = "№_" + list[random.Next(0, 3)] + "а";
            else
                textBox3.Text = "id_" + list[random.Next(0, 3)];
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox2.Checked == true)
                textBox3.Text = "№_" + list[random.Next(0, 3)] + "а";
            else
                textBox3.Text = "Код_" + list[random.Next(0, 3)];
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox2.Checked == true)
                textBox3.Text = "№_" + list[random.Next(0, 3)] + "а";
            else
                textBox3.Text = "код_" + list[random.Next(0, 3)];
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkBox2.Checked == true)
                textBox3.Text = "№_" + list[random.Next(0, 3)] + "а";
            else
                textBox3.Text = "№_" + list[random.Next(0, 3)];
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            a2 = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory("Filtres");
            }
            catch { }
            // Создает One.txt и A.txt, в 1 записывает данные поля и записывает в 2 файл если checed == true
            if (!string.IsNullOrEmpty(textBox1.Text) | !string.IsNullOrWhiteSpace(textBox1.Text))
            {

                File.CreateText("Filtres/One.txt").Dispose();
                File.WriteAllText("Filtres/One.txt", textBox1.Text);
                File.WriteAllText("Filtres/Two.txt", "");

                File.CreateText("Filtres/A.txt").Dispose();
                if (checkBox1.Checked == true)
                    File.WriteAllText("Filtres/A.txt", "а");
            }// Условия для radiobutton
            else
            {
                if (radioButton1.Checked == true)
                {
                    File.CreateText("Filtres/Two.txt").Dispose();
                    File.WriteAllText("Filtres/Two.txt", "ID");
                    File.WriteAllText("Filtres/One.txt", "");

                    File.CreateText("Filtres/A.txt").Dispose();
                    if (checkBox2.Checked == true)
                        File.WriteAllText("Filtres/A.txt", "а");
                }
                else if (radioButton2.Checked == true)
                {
                    File.CreateText("Filtres/Two.txt").Dispose();
                    File.WriteAllText("Filtres/Two.txt", "id");
                    File.WriteAllText("Filtres/One.txt", "");

                    File.CreateText("Filtres/A.txt").Dispose();
                    if (checkBox2.Checked == true)
                        File.WriteAllText("Filtres/A.txt", "а");
                }
                else if (radioButton3.Checked == true)
                {
                    File.CreateText("Filtres/Two.txt").Dispose();
                    File.WriteAllText("Filtres/Two.txt", "Код");
                    File.WriteAllText("Filtres/One.txt", "");

                    File.CreateText("Filtres/A.txt").Dispose();
                    if (checkBox2.Checked == true)
                        File.WriteAllText("Filtres/A.txt", "а");
                }
                else if (radioButton4.Checked == true)
                {
                    File.CreateText("Filtres/Two.txt").Dispose();
                    File.WriteAllText("Filtres/Two.txt", "код");
                    File.WriteAllText("Filtres/One.txt", "");

                    File.CreateText("Filtres/A.txt").Dispose();
                    if (checkBox2.Checked == true)
                        File.WriteAllText("Filtres/A.txt", "а");
                }
                else if (radioButton5.Checked == true)
                {
                    File.CreateText("Filtres/Two.txt").Dispose();
                    File.WriteAllText("Filtres/Two.txt", "№");
                    File.WriteAllText("Filtres/One.txt", "");

                    File.CreateText("Filtres/A.txt").Dispose();
                    if (checkBox2.Checked == true)
                        File.WriteAllText("Filtres/A.txt", "а");
                }
            }// Если не стоит галочка в checkeds, то в A.txt записывает пустое поле
            if(checkBox1.Checked == false & checkBox2.Checked == false)
                File.WriteAllText("Filtres/A.txt", "");
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            a1 = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            checkBox2.Checked = false;
            textBox3.Clear();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Close();
        }

        string str1;
        string str2;
        string stra;

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                str1 = File.ReadAllText("Filtres/One.txt");
                str2 = File.ReadAllText("Filtres/Two.txt");
                stra = File.ReadAllText("Filtres/A.txt");
            }
            catch { }
    
            // Проверка если str1 непустой, то в textbox1 записывает занчения из файла. Активация radiobutton, где str2 равен тесту radbut
            if (!string.IsNullOrEmpty(str1) | !string.IsNullOrWhiteSpace(str1))
                textBox1.Text = str1;
            else if (str2 == "ID")
                radioButton1.Checked = true;
            else if (str2 == "id")
                radioButton2.Checked = true;
            else if (str2 == "Код")
                radioButton3.Checked = true;
            else if (str2 == "код")
                radioButton4.Checked = true;
            else if (str2 == "№")
                radioButton5.Checked = true;

            // Если в файле A.txt нету записи и  textbox1 пустой, то активируется checed2 2 группы
            if (stra == "а" & textBox1.Text == "")
                checkBox2.Checked = true;
            else if (stra == "а" & textBox3.Text == "")
                checkBox1.Checked = true;

        }
    }
}
