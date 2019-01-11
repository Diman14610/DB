using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DataBase
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        List<string> liname = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();//txb1
            textBox3.Clear();//txb2
            textBox5.Clear();
            textBox6.Clear();
            openFileDialog1.Filter = "DBS (*.dbs)|*.dbs";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Path.GetFullPath("Tables");
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // Read the files 
            foreach (String file in openFileDialog1.FileNames)
            {
                string fileText = File.ReadAllText(file);
                liname.Add(Path.GetFileName(file).Replace(".dbs", "").ToString());
                //MessageBox.Show( Path.GetFileName(file).Replace(".dbs","").ToString());
                if (string.IsNullOrEmpty(textBox1.Text) & string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    label1.Text = Path.GetFileName(file).Replace(".dbs", "").ToString();
                    textBox1.Text = fileText;
                    textBox2.Text = textBox1.Text.Replace("CREATE TABLE", "").
                Replace("(", "").Replace(",", "").Replace(")", "").Replace("PRIMARY KEY", "").Replace("NULL", "").
                Replace("NOT", "").Replace("IDENTITY11", "").ToString();
                    foreach (var v in textBox2.Lines)
                        textBox4.Text += Regex.Replace(v, @"(\s+)\w+", "") + Environment.NewLine;
                    textBox4.Text = textBox4.Text.Remove(1, 1);
                    textBox4.Text = textBox4.Text.Replace(" ", "");

                    for (int rrr = 0; rrr != textBox4.Lines.Count(); rrr++)
                    {
                        if (textBox4.Lines[rrr].Count() == 0)
                        {

                        }
                        else
                        {
                            listBox1.Items.Add(textBox4.Lines[rrr]);

                        }
                    }
                }
                else
                {
                    label2.Text = Path.GetFileName(file).Replace(".dbs", "").ToString();
                    textBox3.Text = fileText;
                    textBox5.Text = textBox3.Text.Replace("CREATE TABLE", "").
                    Replace("(", "").Replace(",", "").Replace(")", "").Replace("PRIMARY KEY", "").Replace("NULL", "").
                    Replace("NOT", "").Replace("IDENTITY11", "").ToString();
                    foreach (var v in textBox5.Lines)
                        textBox6.Text += Regex.Replace(v, @"(\s+)\w+", "") + Environment.NewLine;
                    textBox6.Text = textBox6.Text.Remove(1, 1);
                    textBox6.Text = textBox6.Text.Replace(" ", "");

                    for (int rrr = 0; rrr != textBox6.Lines.Count(); rrr++)
                    {
                        if (textBox6.Lines[rrr].Count() == 0)
                        {

                        }
                        else
                        {
                            listBox2.Items.Add(textBox6.Lines[rrr]);

                        }
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox7.Text = listBox1.SelectedItem.ToString();
            }
            catch { }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox8.Text = listBox2.SelectedItem.ToString();
            }
            catch { }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "PK";
            label4.Text = "FK";
            button5.Visible = true;
            button6.Visible = false;
            button5.Text = "PK_" + label1.Text + "_" + textBox7.Text + "_FK_" + label2.Text + "_" + textBox8.Text;//+ " FOREIGN KEY (" + textBox8.Text + ") REFERENCES " + label1.Text + " (" + textBox7.Text + ")";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox9.Text += "ALTER TABLE " + label2.Text
                + Environment.NewLine + "ADD CONSTRAINT " + button5.Text + " FOREIGN KEY (" + textBox8.Text + ") REFERENCES "
                + label1.Text + " (" + textBox7.Text + ")" + Environment.NewLine;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "FK";
            label4.Text = "PK";
            button5.Visible = false;
            button6.Visible = true;
            button6.Text = "PK_" + label2.Text + "_" + textBox8.Text + "_FK_" + label1.Text + "_" + textBox7.Text;//+ " FOREIGN KEY (" + textBox8.Text + ") REFERENCES " + label1.Text + " (" + textBox7.Text + ")";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox9.Text += "ALTER TABLE " + label1.Text 
                + Environment.NewLine + "ADD CONSTRAINT " + button6.Text + " FOREIGN KEY (" + textBox7.Text + ") REFERENCES "
                + label2.Text + " (" + textBox8.Text + ")" + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var lidist = liname.Distinct();
            string str;
            str = textBox9.Text;
            textBox9.Clear();
            foreach (var file in lidist)
            {
                textBox9.Text += File.ReadAllText("Tables/" + file + ".dbs");
            }
            textBox9.Text += str;
            saveFileDialog1.Filter = "TXT File (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            File.WriteAllText(filename, textBox9.Text);
            textBox9.Clear();
        }
    }
}
