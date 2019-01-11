namespace DataBase
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.автоВставкаСловаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(405, 107);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(124, 24);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "PK, Шаг(1,1)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Имя поля";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Имя таблицы";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(131, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(396, 26);
            this.textBox2.TabIndex = 2;
            this.textBox2.Enter += new System.EventHandler(this.textBox2_Enter);
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(131, 38);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(308, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(11, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(518, 52);
            this.button1.TabIndex = 5;
            this.button1.Text = "Новая таблица (Закрыть текующую таблицу)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(11, 242);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(518, 40);
            this.button2.TabIndex = 6;
            this.button2.Text = "Сформировать скрипт";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(131, 106);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(267, 26);
            this.textBox3.TabIndex = 3;
            this.textBox3.Enter += new System.EventHandler(this.textBox3_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Тип данных";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(446, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 26);
            this.button3.TabIndex = 1;
            this.button3.Text = "ОК (Esc)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.textBox4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox4.Location = new System.Drawing.Point(536, 41);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(384, 239);
            this.textBox4.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(11, 137);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(518, 41);
            this.button4.TabIndex = 4;
            this.button4.Text = "Добавить (Enter)";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.25F);
            this.textBox5.Location = new System.Drawing.Point(9, 98);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(16, 9);
            this.textBox5.TabIndex = 9;
            this.textBox5.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.InitialDirectory = ".txt";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Old English Text MT", 14F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.автоВставкаСловаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(933, 31);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // автоВставкаСловаToolStripMenuItem
            // 
            this.автоВставкаСловаToolStripMenuItem.Image = global::DataBase.Properties.Resources.rep;
            this.автоВставкаСловаToolStripMenuItem.Name = "автоВставкаСловаToolStripMenuItem";
            this.автоВставкаСловаToolStripMenuItem.Size = new System.Drawing.Size(242, 27);
            this.автоВставкаСловаToolStripMenuItem.Text = "Авто-подстановка слов";
            this.автоВставкаСловаToolStripMenuItem.Click += new System.EventHandler(this.автоВставкаСловаToolStripMenuItem_Click);
            // 
            // Form2
            // 
            this.AcceptButton = this.button4;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 293);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(949, 331);
            this.MinimumSize = new System.Drawing.Size(949, 331);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание таблиц";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem автоВставкаСловаToolStripMenuItem;
    }
}