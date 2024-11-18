namespace Magazyn
{
    partial class Form11
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
            label1 = new Label();
            dostawcy_txt = new ComboBox();
            checkBox1 = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBox1 = new TextBox();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            search_btn = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 0;
            label1.Text = "Dostawy:";
            // 
            // dostawcy_txt
            // 
            dostawcy_txt.FormattingEnabled = true;
            dostawcy_txt.Location = new Point(12, 27);
            dostawcy_txt.Name = "dostawcy_txt";
            dostawcy_txt.Size = new Size(121, 23);
            dostawcy_txt.TabIndex = 1;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkBox1.ForeColor = Color.RoyalBlue;
            checkBox1.Location = new Point(12, 68);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(108, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Ubezpieczenie";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(12, 108);
            label3.Name = "label3";
            label3.Size = new Size(164, 15);
            label3.TabIndex = 4;
            label3.Text = "Numer listu przewozowego:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.RoyalBlue;
            label4.Location = new Point(12, 175);
            label4.Name = "label4";
            label4.Size = new Size(159, 15);
            label4.TabIndex = 5;
            label4.Text = "Data odbioru przez kuriera:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.RoyalBlue;
            label5.Location = new Point(12, 244);
            label5.Name = "label5";
            label5.Size = new Size(190, 15);
            label5.TabIndex = 6;
            label5.Text = "Przewidywana data dostarczenia:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 126);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CalendarTitleForeColor = SystemColors.ControlText;
            dateTimePicker2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dateTimePicker2.Location = new Point(12, 262);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(222, 23);
            dateTimePicker2.TabIndex = 17;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarTitleForeColor = SystemColors.ControlText;
            dateTimePicker1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dateTimePicker1.Location = new Point(12, 193);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(222, 23);
            dateTimePicker1.TabIndex = 18;
            // 
            // search_btn
            // 
            search_btn.BackColor = Color.LightSkyBlue;
            search_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            search_btn.ForeColor = Color.RoyalBlue;
            search_btn.Location = new Point(12, 324);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(75, 23);
            search_btn.TabIndex = 19;
            search_btn.Text = "Zapisz";
            search_btn.UseVisualStyleBackColor = false;
            search_btn.Click += search_btn_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.LightSkyBlue;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.RoyalBlue;
            button1.Location = new Point(137, 324);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 20;
            button1.Text = "Zamknij";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form11
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(348, 370);
            Controls.Add(button1);
            Controls.Add(search_btn);
            Controls.Add(dateTimePicker1);
            Controls.Add(dateTimePicker2);
            Controls.Add(textBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(checkBox1);
            Controls.Add(dostawcy_txt);
            Controls.Add(label1);
            Name = "Form11";
            Text = "Dane dostawy:";
            Load += Form11_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox dostawcy_txt;
        private CheckBox checkBox1;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private Button search_btn;
        private Button button1;
    }
}