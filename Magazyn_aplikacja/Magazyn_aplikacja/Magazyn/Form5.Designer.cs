namespace Magazyn
{
    partial class package_form
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
            nametxtbox = new TextBox();
            niptxtbox = new TextBox();
            emailtxtbox = new TextBox();
            regiontxtbox = new TextBox();
            teltxtbox = new TextBox();
            adrestxtbox = new TextBox();
            peseltxtbox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            searchbtn = new Button();
            button1 = new Button();
            tabela = new DataGridView();
            klienci_txt = new ComboBox();
            label8 = new Label();
            confirm_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)tabela).BeginInit();
            SuspendLayout();
            // 
            // nametxtbox
            // 
            nametxtbox.Location = new Point(41, 82);
            nametxtbox.Name = "nametxtbox";
            nametxtbox.Size = new Size(104, 23);
            nametxtbox.TabIndex = 0;
            // 
            // niptxtbox
            // 
            niptxtbox.Location = new Point(158, 57);
            niptxtbox.Name = "niptxtbox";
            niptxtbox.Size = new Size(104, 23);
            niptxtbox.TabIndex = 1;
            // 
            // emailtxtbox
            // 
            emailtxtbox.Location = new Point(158, 105);
            emailtxtbox.Name = "emailtxtbox";
            emailtxtbox.Size = new Size(104, 23);
            emailtxtbox.TabIndex = 2;
            // 
            // regiontxtbox
            // 
            regiontxtbox.Location = new Point(277, 57);
            regiontxtbox.Name = "regiontxtbox";
            regiontxtbox.Size = new Size(104, 23);
            regiontxtbox.TabIndex = 3;
            // 
            // teltxtbox
            // 
            teltxtbox.Location = new Point(277, 105);
            teltxtbox.Name = "teltxtbox";
            teltxtbox.Size = new Size(104, 23);
            teltxtbox.TabIndex = 4;
            // 
            // adrestxtbox
            // 
            adrestxtbox.Location = new Point(396, 105);
            adrestxtbox.Name = "adrestxtbox";
            adrestxtbox.Size = new Size(104, 23);
            adrestxtbox.TabIndex = 5;
            // 
            // peseltxtbox
            // 
            peseltxtbox.Location = new Point(396, 57);
            peseltxtbox.Name = "peseltxtbox";
            peseltxtbox.Size = new Size(104, 23);
            peseltxtbox.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(16, 60);
            label1.Name = "label1";
            label1.Size = new Size(139, 15);
            label1.TabIndex = 7;
            label1.Text = "Nazwa/Imie i Nazwisko:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(158, 39);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 8;
            label2.Text = "NIP:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(158, 90);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 9;
            label3.Text = "Email:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.RoyalBlue;
            label4.Location = new Point(281, 39);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 10;
            label4.Text = "REGION:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.RoyalBlue;
            label5.Location = new Point(281, 90);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 11;
            label5.Text = "Telefon:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.ForeColor = Color.RoyalBlue;
            label6.Location = new Point(396, 90);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 12;
            label6.Text = "Adres:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.ForeColor = Color.RoyalBlue;
            label7.Location = new Point(396, 39);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 13;
            label7.Text = "PESEL:";
            // 
            // searchbtn
            // 
            searchbtn.BackColor = Color.LightSkyBlue;
            searchbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            searchbtn.ForeColor = Color.RoyalBlue;
            searchbtn.Location = new Point(551, 105);
            searchbtn.Name = "searchbtn";
            searchbtn.Size = new Size(99, 23);
            searchbtn.TabIndex = 14;
            searchbtn.Text = "Szukaj";
            searchbtn.UseVisualStyleBackColor = false;
            searchbtn.Click += searchbtn_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.LightSkyBlue;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.RoyalBlue;
            button1.Location = new Point(551, 57);
            button1.Name = "button1";
            button1.Size = new Size(99, 23);
            button1.TabIndex = 15;
            button1.Text = "Wyczyść";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // tabela
            // 
            tabela.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabela.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabela.Location = new Point(16, 188);
            tabela.Name = "tabela";
            tabela.Size = new Size(666, 232);
            tabela.TabIndex = 16;
            // 
            // klienci_txt
            // 
            klienci_txt.FormattingEnabled = true;
            klienci_txt.Location = new Point(66, 149);
            klienci_txt.Name = "klienci_txt";
            klienci_txt.Size = new Size(121, 23);
            klienci_txt.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label8.ForeColor = Color.RoyalBlue;
            label8.Location = new Point(16, 152);
            label8.Name = "label8";
            label8.Size = new Size(47, 15);
            label8.TabIndex = 18;
            label8.Text = "Klienci:";
            // 
            // confirm_btn
            // 
            confirm_btn.BackColor = Color.LightSkyBlue;
            confirm_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            confirm_btn.ForeColor = Color.RoyalBlue;
            confirm_btn.Location = new Point(193, 149);
            confirm_btn.Name = "confirm_btn";
            confirm_btn.Size = new Size(114, 23);
            confirm_btn.TabIndex = 19;
            confirm_btn.Text = "Zatwierdź klienta";
            confirm_btn.UseVisualStyleBackColor = false;
            confirm_btn.Click += confirm_btn_Click;
            // 
            // package_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(734, 450);
            Controls.Add(confirm_btn);
            Controls.Add(label8);
            Controls.Add(klienci_txt);
            Controls.Add(tabela);
            Controls.Add(button1);
            Controls.Add(searchbtn);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(peseltxtbox);
            Controls.Add(adrestxtbox);
            Controls.Add(teltxtbox);
            Controls.Add(regiontxtbox);
            Controls.Add(emailtxtbox);
            Controls.Add(niptxtbox);
            Controls.Add(nametxtbox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "package_form";
            Text = "Dodaj Nową Paczkę";
            Load += Form5_Load;
            ((System.ComponentModel.ISupportInitialize)tabela).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nametxtbox;
        private TextBox niptxtbox;
        private TextBox emailtxtbox;
        private TextBox regiontxtbox;
        private TextBox teltxtbox;
        private TextBox adrestxtbox;
        private TextBox peseltxtbox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button searchbtn;
        private Button button1;
        private DataGridView tabela;
        private ComboBox klienci_txt;
        private Label label8;
        private Button confirm_btn;
    }
}