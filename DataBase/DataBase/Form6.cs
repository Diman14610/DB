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

namespace DataBase
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }


        public static List<string> strAlterTable = new List<string>();

        private void Form6_Load_1(object sender, EventArgs e)
        {
            try
            {
                    label1.Text = Form5.strForNameTable[0];
                    label2.Text = Form5.strForNameTable[1];
            }
            catch { }
            textBox1.Text = File.ReadAllText(Form5.liPath1[0]);
            textBox2.Text = textBox1.Text.Replace("CREATE TABLE", "").
                Replace("(", "").Replace(",", "").Replace(")", "").Replace("PRIMARY KEY", "").Replace("NULL", "").
                Replace("NOT", "").Replace("IDENTITY11", "").ToString();
            foreach (var v in textBox2.Lines)
                textBox4.Text += Regex.Replace(v, @"(\s+)\w+", "") + Environment.NewLine;
            textBox4.Text = textBox4.Text.Remove(1, 2);
            textBox4.Text = textBox4.Text.Replace(" ", "");

            textBox3.Text = File.ReadAllText(Form5.liPath1[1]);
            textBox5.Text = textBox3.Text.Replace("CREATE TABLE", "").
               Replace("(", "").Replace(",", "").Replace(")", "").Replace("PRIMARY KEY", "").Replace("NULL", "").
               Replace("NOT", "").Replace("IDENTITY11", "").ToString();
            foreach (var v in textBox5.Lines)
                textBox6.Text += Regex.Replace(v, @"(\s+)\w+", "") + Environment.NewLine;
            textBox6.Text = textBox6.Text.Remove(1, 2);
            textBox6.Text = textBox6.Text.Replace(" ", "");

            for (int rrr = 0; rrr != textBox4.Lines.Count(); rrr++)
            {

                listBox1.Items.Add(textBox4.Lines[rrr]);

            }
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == "")
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == "")
                {
                    listBox1.Items.RemoveAt(i);
                }
            }

            for (int rrr = 0; rrr != textBox6.Lines.Count(); rrr++)
            {

                listBox2.Items.Add(textBox6.Lines[rrr]);

            }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == "")
                {
                    listBox2.Items.RemoveAt(i);
                }
            }
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.Items[i].ToString() == "")
                {
                    listBox2.Items.RemoveAt(i);
                }
            }
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "FK";
            label5.Text = "PK";
            button5.Visible = false;
            button6.Visible = true;
            button6.Text = "PK_" + label2.Text + "_" + textBox8.Text + "_FK_" + label1.Text + "_" + textBox7.Text;//+ " FOREIGN KEY (" + textBox8.Text + ") REFERENCES " + label1.Text + " (" + textBox7.Text + ")";
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "PK";
            label5.Text = "FK";
            button5.Visible = true;
            button6.Visible = false;
            button5.Text = "PK_" + label1.Text + "_" + textBox7.Text + "_FK_" + label2.Text + "_" + textBox8.Text;//+ " FOREIGN KEY (" + textBox8.Text + ") REFERENCES " + label1.Text + " (" + textBox7.Text + ")";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //textBox9.Text = "ALTER TABLE " + label1.Text
            // + Environment.NewLine + "ADD CONSTRAINT " + button6.Text + " FOREIGN KEY (" + textBox7.Text + ") REFERENCES "
            // + label2.Text + " (" + textBox8.Text + ")" + Environment.NewLine;
            //strAlterTable.Add("ALTER TABLE " + label2.Text);
            strAlterTable.Add("\n" + "ADD CONSTRAINT " + button5.Text + " FOREIGN KEY (" + textBox8.Text + ") REFERENCES "
            + label1.Text + " (" + textBox7.Text + ")");
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //textBox9.Text = "ALTER TABLE " + label2.Text
            //+ Environment.NewLine + "ADD CONSTRAINT " + button5.Text + " FOREIGN KEY (" + textBox8.Text + ") REFERENCES "
            //+ label1.Text + " (" + textBox7.Text + ")" + Environment.NewLine;
            //Form6.strAlterTable.AddRange(textBox9.Lines);
            strAlterTable.Add("ALTER TABLE " + label2.Text);
            strAlterTable.Add("ADD CONSTRAINT " + button5.Text + " FOREIGN KEY (" + textBox8.Text + ") REFERENCES "
            + label1.Text + " (" + textBox7.Text + ")");
            this.Close();
        }
    }
}
