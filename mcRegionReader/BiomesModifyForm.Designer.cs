namespace mcRegionReader
{
    partial class BiomesModifyForm
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
            this.button11 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(419, 303);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(144, 41);
            this.button11.TabIndex = 17;
            this.button11.Text = "read biomes.json";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(396, 557);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "id";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 300;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(414, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 270);
            this.label1.TabIndex = 19;
            this.label1.Text = "id\r\nname\r\nicon\r\ntempe\r\ncolor\r\nzh_cn\r\nzh_tw\r\nen_us\r\njp";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(497, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(234, 25);
            this.textBox1.TabIndex = 20;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(497, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(234, 25);
            this.textBox2.TabIndex = 21;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(497, 73);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(234, 25);
            this.textBox3.TabIndex = 22;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(497, 104);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(234, 25);
            this.textBox4.TabIndex = 23;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(497, 135);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(234, 25);
            this.textBox5.TabIndex = 24;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(497, 166);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(234, 25);
            this.textBox6.TabIndex = 25;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(497, 197);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(234, 25);
            this.textBox7.TabIndex = 26;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(497, 228);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(234, 25);
            this.textBox8.TabIndex = 27;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(497, 259);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(234, 25);
            this.textBox9.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(587, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 41);
            this.button1.TabIndex = 29;
            this.button1.Text = "add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(587, 528);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 41);
            this.button2.TabIndex = 30;
            this.button2.Text = "save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BiomesModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 581);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button11);
            this.Name = "BiomesModifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BiomesModifyForm";
            this.Load += new System.EventHandler(this.BiomesModifyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}