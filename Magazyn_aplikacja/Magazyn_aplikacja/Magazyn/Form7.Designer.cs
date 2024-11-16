namespace Magazyn
{
    partial class add_client
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
            label6 = new Label();
            label7 = new Label();
            name_txt = new TextBox();
            nip_txt = new TextBox();
            region_txt = new TextBox();
            pesel_txt = new TextBox();
            email_txt = new TextBox();
            tel_txt = new TextBox();
            adres_txt = new TextBox();
            close_btn = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 0;
            label1.Text = "Imie";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 75);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 1;
            label2.Text = "NIP";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 141);
            label3.Name = "label3";
            label3.Size = new Size(49, 15);
            label3.TabIndex = 2;
            label3.Text = "REGION";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 205);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 3;
            label4.Text = "PESEL";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 267);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 4;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 331);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 5;
            label6.Text = "Telefon";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 389);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 6;
            label7.Text = "Adres";
            // 
            // name_txt
            // 
            name_txt.Location = new Point(12, 27);
            name_txt.Name = "name_txt";
            name_txt.Size = new Size(100, 23);
            name_txt.TabIndex = 7;
            // 
            // nip_txt
            // 
            nip_txt.Location = new Point(12, 93);
            nip_txt.Name = "nip_txt";
            nip_txt.Size = new Size(100, 23);
            nip_txt.TabIndex = 8;
            // 
            // region_txt
            // 
            region_txt.Location = new Point(12, 159);
            region_txt.Name = "region_txt";
            region_txt.Size = new Size(100, 23);
            region_txt.TabIndex = 9;
            // 
            // pesel_txt
            // 
            pesel_txt.Location = new Point(12, 223);
            pesel_txt.Name = "pesel_txt";
            pesel_txt.Size = new Size(100, 23);
            pesel_txt.TabIndex = 10;
            // 
            // email_txt
            // 
            email_txt.Location = new Point(12, 285);
            email_txt.Name = "email_txt";
            email_txt.Size = new Size(100, 23);
            email_txt.TabIndex = 11;
            // 
            // tel_txt
            // 
            tel_txt.Location = new Point(12, 349);
            tel_txt.Name = "tel_txt";
            tel_txt.Size = new Size(100, 23);
            tel_txt.TabIndex = 12;
            // 
            // adres_txt
            // 
            adres_txt.Location = new Point(12, 407);
            adres_txt.Name = "adres_txt";
            adres_txt.Size = new Size(100, 23);
            adres_txt.TabIndex = 13;
            // 
            // close_btn
            // 
            close_btn.Location = new Point(12, 456);
            close_btn.Name = "close_btn";
            close_btn.Size = new Size(75, 23);
            close_btn.TabIndex = 14;
            close_btn.Text = "Anuluj";
            close_btn.UseVisualStyleBackColor = true;
            close_btn.Click += close_btn_Click;
            // 
            // button2
            // 
            button2.Location = new Point(148, 456);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 15;
            button2.Text = "Dodaj";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // add_client
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 502);
            Controls.Add(button2);
            Controls.Add(close_btn);
            Controls.Add(adres_txt);
            Controls.Add(tel_txt);
            Controls.Add(email_txt);
            Controls.Add(pesel_txt);
            Controls.Add(region_txt);
            Controls.Add(nip_txt);
            Controls.Add(name_txt);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "add_client";
            Text = "Dodaj klienta";
            Load += add_client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox name_txt;
        private TextBox nip_txt;
        private TextBox region_txt;
        private TextBox pesel_txt;
        private TextBox email_txt;
        private TextBox tel_txt;
        private TextBox adres_txt;
        private Button close_btn;
        private Button button2;
    }
}