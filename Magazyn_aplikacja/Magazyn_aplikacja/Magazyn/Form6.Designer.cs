namespace Magazyn
{
    partial class product_form
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            name_txt = new TextBox();
            price_txt = new TextBox();
            weight_txt = new TextBox();
            quit_btn = new Button();
            add_btn = new Button();
            count_txt = new NumericUpDown();
            cecha_txt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)count_txt).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 1;
            label1.Text = "Nazwa produktu:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(12, 86);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Cechy:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(12, 177);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 3;
            label3.Text = "Cena:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.RoyalBlue;
            label4.Location = new Point(12, 262);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 4;
            label4.Text = "Waga 1szt [kg]:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.RoyalBlue;
            label5.Location = new Point(12, 348);
            label5.Name = "label5";
            label5.Size = new Size(110, 15);
            label5.TabIndex = 5;
            label5.Text = "Ilość w magazynie:";
            // 
            // name_txt
            // 
            name_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            name_txt.Location = new Point(12, 27);
            name_txt.Name = "name_txt";
            name_txt.Size = new Size(138, 23);
            name_txt.TabIndex = 6;
            // 
            // price_txt
            // 
            price_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            price_txt.Location = new Point(12, 195);
            price_txt.Name = "price_txt";
            price_txt.Size = new Size(138, 23);
            price_txt.TabIndex = 7;
            // 
            // weight_txt
            // 
            weight_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            weight_txt.Location = new Point(12, 280);
            weight_txt.Name = "weight_txt";
            weight_txt.Size = new Size(138, 23);
            weight_txt.TabIndex = 8;
            // 
            // quit_btn
            // 
            quit_btn.BackColor = Color.LightSkyBlue;
            quit_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            quit_btn.ForeColor = Color.RoyalBlue;
            quit_btn.Location = new Point(12, 473);
            quit_btn.Name = "quit_btn";
            quit_btn.Size = new Size(131, 23);
            quit_btn.TabIndex = 11;
            quit_btn.Text = "Anuluj";
            quit_btn.UseVisualStyleBackColor = false;
            quit_btn.Click += quit_btn_Click;
            // 
            // add_btn
            // 
            add_btn.BackColor = Color.LightSkyBlue;
            add_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            add_btn.ForeColor = Color.RoyalBlue;
            add_btn.Location = new Point(221, 473);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(131, 23);
            add_btn.TabIndex = 12;
            add_btn.Text = "Zapisz";
            add_btn.UseVisualStyleBackColor = false;
            add_btn.Click += add_btn_Click;
            // 
            // count_txt
            // 
            count_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            count_txt.Location = new Point(12, 366);
            count_txt.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            count_txt.Name = "count_txt";
            count_txt.Size = new Size(120, 23);
            count_txt.TabIndex = 13;
            // 
            // cecha_txt
            // 
            cecha_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cecha_txt.Location = new Point(12, 104);
            cecha_txt.Name = "cecha_txt";
            cecha_txt.Size = new Size(100, 23);
            cecha_txt.TabIndex = 14;
            // 
            // product_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(364, 508);
            ControlBox = false;
            Controls.Add(cecha_txt);
            Controls.Add(count_txt);
            Controls.Add(add_btn);
            Controls.Add(quit_btn);
            Controls.Add(weight_txt);
            Controls.Add(price_txt);
            Controls.Add(name_txt);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "product_form";
            Text = "Dodaj nowy produkt";
            Load += product_form_Load;
            ((System.ComponentModel.ISupportInitialize)count_txt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox name_txt;
        private TextBox price_txt;
        private TextBox weight_txt;
        private Button quit_btn;
        private Button add_btn;
        private NumericUpDown count_txt;
        private TextBox cecha_txt;
    }
}