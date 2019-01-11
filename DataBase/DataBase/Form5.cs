using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        int buff_butt = 0;
        public static List<string> lip = new List<string>();
        List<string> liPath = new List<string>();
        public static List<string> liPath1 = new List<string>();
        List<string> li1 = new List<string>();
        List<string> strFile = new List<string>();
        List<string> strForScript = new List<string>();
        List<string> strForDistinct = new List<string>();
        public static List<string> strForNameTable = new List<string>();
        //public static string textPrimaryKey = "";

        void DownloadFile()
        {
            openFileDialog1.Filter = "DBS (*.dbs)|*.dbs";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            foreach (String file in openFileDialog1.FileNames)
            {
                string fileText = File.ReadAllText(file);
                textBox1.Text += fileText;
            }
            li1.Clear();
            li1.AddRange(openFileDialog1.FileNames.Select(c => Path.GetFileName(c).Replace(".dbs","")));
            liPath.Clear();
            liPath.AddRange(openFileDialog1.FileNames);
        }//загрузка файла

        List<Button> but = new List<Button>();
        void DownloadScript()
        {
            groupBox1.Controls.Clear();
            int y = 30;
            int x = 10;
            int count = 0;
            if (li1.Count >= 35)
            {
                for (int i = 0; i < 35; i++)
                {
                    but.Add(new Button
                    {
                        Size = new Size(140, 85),
                        Text = li1[i],
                        BackColor = Color.SandyBrown,
                        ForeColor = Color.OldLace,
                        Font = new Font("Times New Roman", 15),
                        Location = new Point(x, y),
                    });
                    count++;
                    y += 95;
                    if (count == 5)
                    {
                        y = 30;
                        x += 145;
                        count = 0;
                    }
                }
            }
            else
            {
                foreach (string s1 in li1)
                {
                    but.Add(new Button
                    {
                        Size = new Size(140, 85),
                        Text = s1,
                        BackColor = Color.SandyBrown,
                        ForeColor = Color.OldLace,
                        Font = new Font("Times New Roman", 15),
                        Location = new Point(x, y),
                    });
                    count++;
                    y += 95;
                    if (count == 5)
                    {
                        y = 30;
                        x += 145;
                        count = 0;
                    }
                }
            }

            foreach (Button b1 in but)
            {
                b1.Click += EventButton;
                groupBox1.Controls.Add(b1);
            }

        }//загрузка в лист 


        void EventButton(object s, EventArgs e)
        {
            strForNameTable.Clear();
            Button br1 = but.Find(c => c.Text == s.ToString().Remove(0, 35));
            br1.BackColor = Color.Salmon;
            br1.Enabled = false;
            lip.Add(br1.Text);
            buff_butt++;
            if (buff_butt == 2)
            {
                List<string> ssssss = new List<string>();
                foreach (string b in lip)
                {
                    Button b2 = but.Find(c => c.Text == b);
                    b2.BackColor = Color.Salmon;
                    b2.Enabled = true;
                    var v1 = liPath.FirstOrDefault(c => Path.GetFileName(c) == b + ".dbs");
                    ssssss.Add(v1);
                    strForNameTable.Add(b);
                }
                liPath1.AddRange(ssssss);
                strFile.AddRange(ssssss);
                buff_butt = 0;
                Form6 f6 = new Form6();
                f6.ShowDialog();
                foreach (string b in lip)
                {
                    Button b2 = but.Find(c => c.Text == b);
                    b2.BackColor = Color.OliveDrab;
                }
                lip.Clear();
                liPath1.Clear();
                //strFile.Distinct().ToList().ForEach(c => textBox3.Text += c);
                strForScript.Clear();
                strFile.Distinct().ToList().ForEach(c => strForScript.Add(c));
  
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            but.Clear();

            DownloadFile();

            DownloadScript();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new Form();
            //form.Name = "Form7";
            form.Size = new Size(ClientSize.Width, ClientSize.Height / 2);
            form.MaximumSize = new Size(ClientSize.Width/2,ClientSize.Height-100);
            form.MinimumSize = new Size(ClientSize.Width, ClientSize.Height-100);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Text = "Сформированный скрипт";
            TextBox textBox = new TextBox();
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Size = new Size(ClientSize.Width,ClientSize.Height-100);
            textBox.Multiline = true;

            foreach (var file in strForScript)
            {
                textBox.Text += File.ReadAllText(file);
            }
            foreach (var file in Form6.strAlterTable)
            {
                textBox.Text += Environment.NewLine + file;
            }
            textBox.ForeColor = Color.Black;
            form.Controls.Add(textBox);
            form.ShowDialog();
        }
    }
}
