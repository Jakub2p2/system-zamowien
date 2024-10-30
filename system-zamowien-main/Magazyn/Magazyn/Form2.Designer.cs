namespace Magazyn
{
    partial class add_user
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
            role_select = new ComboBox();
            label7 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            close_window = new Button();
            save_button = new Button();
            password_text = new TextBox();
            confirm_pass_text = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 9);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 0;
            label1.Text = "Imię";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 69);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 1;
            label2.Text = "Nazwisko";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 118);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 2;
            label3.Text = "Login";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 167);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 3;
            label4.Text = "Email";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 221);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 4;
            label5.Text = "Rola";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 337);
            label6.Name = "label6";
            label6.Size = new Size(81, 15);
            label6.TabIndex = 5;
            label6.Text = "Powtórz hasło";
            // 
            // role_select
            // 
            role_select.FormattingEnabled = true;
            role_select.Items.AddRange(new object[] { "Brak", "Administrator", "Sprzedawca", "Magazynier" });
            role_select.Location = new Point(12, 239);
            role_select.Name = "role_select";
            role_select.Size = new Size(121, 23);
            role_select.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 278);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 7;
            label7.Text = "Hasło";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(171, 23);
            textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 87);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(169, 23);
            textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 136);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(169, 23);
            textBox3.TabIndex = 10;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(12, 185);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(169, 23);
            textBox4.TabIndex = 11;
            // 
            // close_window
            // 
            close_window.Location = new Point(12, 403);
            close_window.Name = "close_window";
            close_window.Size = new Size(75, 23);
            close_window.TabIndex = 12;
            close_window.Text = "Anuluj";
            close_window.UseVisualStyleBackColor = true;
            close_window.Click += close_window_Click;
            // 
            // save_button
            // 
            save_button.Location = new Point(106, 403);
            save_button.Name = "save_button";
            save_button.Size = new Size(75, 23);
            save_button.TabIndex = 13;
            save_button.Text = "Zapisz";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += save_button_Click;
            // 
            // password_text
            // 
            password_text.Location = new Point(12, 296);
            password_text.Name = "password_text";
            password_text.Size = new Size(169, 23);
            password_text.TabIndex = 14;
            password_text.TextChanged += password_text_TextChanged;
            // 
            // confirm_pass_text
            // 
            confirm_pass_text.Location = new Point(12, 355);
            confirm_pass_text.Name = "confirm_pass_text";
            confirm_pass_text.Size = new Size(169, 23);
            confirm_pass_text.TabIndex = 15;
            confirm_pass_text.TextChanged += confirm_pass_text_TextChanged;
            // 
            // add_user
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(345, 450);
            Controls.Add(confirm_pass_text);
            Controls.Add(password_text);
            Controls.Add(save_button);
            Controls.Add(close_window);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(role_select);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "add_user";
            Text = "Dodaj użytkownika";
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
        private ComboBox role_select;
        private Label label7;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button close_window;
        private Button save_button;
        private TextBox password_text;
        private TextBox confirm_pass_text;
    }
}