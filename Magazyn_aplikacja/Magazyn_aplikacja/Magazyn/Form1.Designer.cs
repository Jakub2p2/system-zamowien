namespace Magazyn
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_users = new Button();
            tabela = new DataGridView();
            button_client = new Button();
            button_products = new Button();
            button_delivery = new Button();
            button_package = new Button();
            add_btn = new Button();
            button1 = new Button();
            filtr_lbl1 = new Label();
            filtr_lbl2 = new Label();
            filtr_txt1 = new TextBox();
            filtr_txt2 = new TextBox();
            toolStripContainer1 = new ToolStripContainer();
            filtr_lbl9 = new Label();
            filtr_lbl8 = new Label();
            filtr_lbl7 = new Label();
            filtr_lbl6 = new Label();
            filtr_lbl5 = new Label();
            filtr_lbl4 = new Label();
            filtr_lbl3 = new Label();
            filtr_txt7 = new TextBox();
            filtr_txt6 = new TextBox();
            filtr_txt5 = new TextBox();
            filtr_txt4 = new TextBox();
            filtr_txt3 = new TextBox();
            comboBox_txt = new ComboBox();
            date_utworzenia_txt = new DateTimePicker();
            search_btn = new Button();
            clear_btn = new Button();
            menuStrip1 = new MenuStrip();
            user_bar = new ToolStripMenuItem();
            zmieńHasToolStripMenuItem = new ToolStripMenuItem();
            wylogujToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)tabela).BeginInit();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button_users
            // 
            button_users.BackColor = Color.FloralWhite;
            button_users.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button_users.ForeColor = Color.SteelBlue;
            button_users.Location = new Point(12, 83);
            button_users.Name = "button_users";
            button_users.Size = new Size(131, 50);
            button_users.TabIndex = 0;
            button_users.Text = "Użytkownicy";
            button_users.UseVisualStyleBackColor = false;
            button_users.Click += button_users_Click;
            // 
            // tabela
            // 
            tabela.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabela.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabela.EditMode = DataGridViewEditMode.EditOnEnter;
            tabela.Location = new Point(169, 201);
            tabela.Name = "tabela";
            tabela.Size = new Size(793, 388);
            tabela.TabIndex = 1;
            // 
            // button_client
            // 
            button_client.BackColor = Color.FloralWhite;
            button_client.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button_client.ForeColor = Color.SteelBlue;
            button_client.Location = new Point(12, 139);
            button_client.Name = "button_client";
            button_client.Size = new Size(131, 50);
            button_client.TabIndex = 2;
            button_client.Text = "Klienci";
            button_client.UseVisualStyleBackColor = false;
            button_client.Click += button_client_Click;
            // 
            // button_products
            // 
            button_products.BackColor = Color.FloralWhite;
            button_products.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button_products.ForeColor = Color.SteelBlue;
            button_products.Location = new Point(12, 195);
            button_products.Name = "button_products";
            button_products.Size = new Size(131, 50);
            button_products.TabIndex = 3;
            button_products.Text = "Produkty";
            button_products.UseVisualStyleBackColor = false;
            button_products.Click += button_products_Click;
            // 
            // button_delivery
            // 
            button_delivery.BackColor = Color.FloralWhite;
            button_delivery.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button_delivery.ForeColor = Color.SteelBlue;
            button_delivery.Location = new Point(12, 251);
            button_delivery.Name = "button_delivery";
            button_delivery.Size = new Size(131, 50);
            button_delivery.TabIndex = 4;
            button_delivery.Text = "Dostawy";
            button_delivery.UseVisualStyleBackColor = false;
            button_delivery.Click += button_delivery_Click;
            // 
            // button_package
            // 
            button_package.BackColor = Color.FloralWhite;
            button_package.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button_package.ForeColor = Color.SteelBlue;
            button_package.Location = new Point(12, 307);
            button_package.Name = "button_package";
            button_package.Size = new Size(131, 50);
            button_package.TabIndex = 5;
            button_package.Text = "Paczki";
            button_package.UseVisualStyleBackColor = false;
            button_package.Click += button_package_Click;
            // 
            // add_btn
            // 
            add_btn.BackColor = Color.LightSkyBlue;
            add_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            add_btn.ForeColor = Color.RoyalBlue;
            add_btn.Location = new Point(169, 159);
            add_btn.Margin = new Padding(0);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(152, 42);
            add_btn.TabIndex = 10;
            add_btn.Text = "Dodaj";
            add_btn.UseVisualStyleBackColor = false;
            add_btn.Click += add_btn_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FloralWhite;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.SteelBlue;
            button1.Location = new Point(12, 27);
            button1.Name = "button1";
            button1.Size = new Size(131, 50);
            button1.TabIndex = 11;
            button1.Text = "Strona główna";
            button1.UseVisualStyleBackColor = false;
            // 
            // filtr_lbl1
            // 
            filtr_lbl1.AutoSize = true;
            filtr_lbl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl1.ForeColor = Color.RoyalBlue;
            filtr_lbl1.Location = new Point(3, 4);
            filtr_lbl1.Name = "filtr_lbl1";
            filtr_lbl1.Size = new Size(43, 15);
            filtr_lbl1.TabIndex = 12;
            filtr_lbl1.Text = "empty";
            filtr_lbl1.Visible = false;
            // 
            // filtr_lbl2
            // 
            filtr_lbl2.AutoSize = true;
            filtr_lbl2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl2.ForeColor = Color.RoyalBlue;
            filtr_lbl2.Location = new Point(154, 4);
            filtr_lbl2.Name = "filtr_lbl2";
            filtr_lbl2.Size = new Size(43, 15);
            filtr_lbl2.TabIndex = 13;
            filtr_lbl2.Text = "empty";
            filtr_lbl2.Visible = false;
            // 
            // filtr_txt1
            // 
            filtr_txt1.Location = new Point(3, 22);
            filtr_txt1.Name = "filtr_txt1";
            filtr_txt1.Size = new Size(121, 23);
            filtr_txt1.TabIndex = 14;
            filtr_txt1.Visible = false;
            // 
            // filtr_txt2
            // 
            filtr_txt2.Location = new Point(154, 22);
            filtr_txt2.Name = "filtr_txt2";
            filtr_txt2.Size = new Size(100, 23);
            filtr_txt2.TabIndex = 15;
            filtr_txt2.Visible = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl9);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl8);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl7);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl6);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl5);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl4);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl3);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt7);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt6);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt5);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt4);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt3);
            toolStripContainer1.ContentPanel.Controls.Add(comboBox_txt);
            toolStripContainer1.ContentPanel.Controls.Add(date_utworzenia_txt);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt1);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_txt2);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl1);
            toolStripContainer1.ContentPanel.Controls.Add(filtr_lbl2);
            toolStripContainer1.ContentPanel.Size = new Size(966, 104);
            toolStripContainer1.Location = new Point(169, 27);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(966, 129);
            toolStripContainer1.TabIndex = 16;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // filtr_lbl9
            // 
            filtr_lbl9.AutoSize = true;
            filtr_lbl9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl9.ForeColor = Color.RoyalBlue;
            filtr_lbl9.Location = new Point(154, 53);
            filtr_lbl9.Name = "filtr_lbl9";
            filtr_lbl9.Size = new Size(102, 15);
            filtr_lbl9.TabIndex = 29;
            filtr_lbl9.Text = "Data utworzenia:";
            filtr_lbl9.Visible = false;
            // 
            // filtr_lbl8
            // 
            filtr_lbl8.AutoSize = true;
            filtr_lbl8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl8.ForeColor = Color.RoyalBlue;
            filtr_lbl8.Location = new Point(5, 53);
            filtr_lbl8.Name = "filtr_lbl8";
            filtr_lbl8.Size = new Size(71, 15);
            filtr_lbl8.TabIndex = 28;
            filtr_lbl8.Text = "Rola/Status";
            filtr_lbl8.Visible = false;
            // 
            // filtr_lbl7
            // 
            filtr_lbl7.AutoSize = true;
            filtr_lbl7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl7.ForeColor = Color.RoyalBlue;
            filtr_lbl7.Location = new Point(832, 4);
            filtr_lbl7.Name = "filtr_lbl7";
            filtr_lbl7.Size = new Size(42, 15);
            filtr_lbl7.TabIndex = 27;
            filtr_lbl7.Text = "Adres:";
            filtr_lbl7.Visible = false;
            // 
            // filtr_lbl6
            // 
            filtr_lbl6.AutoSize = true;
            filtr_lbl6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl6.ForeColor = Color.RoyalBlue;
            filtr_lbl6.Location = new Point(693, 4);
            filtr_lbl6.Name = "filtr_lbl6";
            filtr_lbl6.Size = new Size(52, 15);
            filtr_lbl6.TabIndex = 26;
            filtr_lbl6.Text = "Telefon:";
            filtr_lbl6.Visible = false;
            // 
            // filtr_lbl5
            // 
            filtr_lbl5.AutoSize = true;
            filtr_lbl5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl5.ForeColor = Color.RoyalBlue;
            filtr_lbl5.Location = new Point(543, 4);
            filtr_lbl5.Name = "filtr_lbl5";
            filtr_lbl5.Size = new Size(43, 15);
            filtr_lbl5.TabIndex = 25;
            filtr_lbl5.Text = "empty";
            filtr_lbl5.Visible = false;
            // 
            // filtr_lbl4
            // 
            filtr_lbl4.AutoSize = true;
            filtr_lbl4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl4.ForeColor = Color.RoyalBlue;
            filtr_lbl4.Location = new Point(407, 4);
            filtr_lbl4.Name = "filtr_lbl4";
            filtr_lbl4.Size = new Size(43, 15);
            filtr_lbl4.TabIndex = 24;
            filtr_lbl4.Text = "empty";
            filtr_lbl4.Visible = false;
            // 
            // filtr_lbl3
            // 
            filtr_lbl3.AutoSize = true;
            filtr_lbl3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filtr_lbl3.ForeColor = Color.RoyalBlue;
            filtr_lbl3.Location = new Point(279, 4);
            filtr_lbl3.Name = "filtr_lbl3";
            filtr_lbl3.Size = new Size(43, 15);
            filtr_lbl3.TabIndex = 23;
            filtr_lbl3.Text = "empty";
            filtr_lbl3.Visible = false;
            // 
            // filtr_txt7
            // 
            filtr_txt7.Location = new Point(832, 22);
            filtr_txt7.Name = "filtr_txt7";
            filtr_txt7.Size = new Size(100, 23);
            filtr_txt7.TabIndex = 22;
            filtr_txt7.Visible = false;
            // 
            // filtr_txt6
            // 
            filtr_txt6.Location = new Point(693, 22);
            filtr_txt6.Name = "filtr_txt6";
            filtr_txt6.Size = new Size(100, 23);
            filtr_txt6.TabIndex = 21;
            filtr_txt6.Visible = false;
            // 
            // filtr_txt5
            // 
            filtr_txt5.Location = new Point(543, 22);
            filtr_txt5.Name = "filtr_txt5";
            filtr_txt5.Size = new Size(100, 23);
            filtr_txt5.TabIndex = 20;
            filtr_txt5.Visible = false;
            // 
            // filtr_txt4
            // 
            filtr_txt4.Location = new Point(407, 22);
            filtr_txt4.Name = "filtr_txt4";
            filtr_txt4.Size = new Size(100, 23);
            filtr_txt4.TabIndex = 19;
            filtr_txt4.Visible = false;
            // 
            // filtr_txt3
            // 
            filtr_txt3.Location = new Point(279, 22);
            filtr_txt3.Name = "filtr_txt3";
            filtr_txt3.Size = new Size(100, 23);
            filtr_txt3.TabIndex = 18;
            filtr_txt3.Visible = false;
            // 
            // comboBox_txt
            // 
            comboBox_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            comboBox_txt.ForeColor = Color.RoyalBlue;
            comboBox_txt.FormattingEnabled = true;
            comboBox_txt.Location = new Point(3, 71);
            comboBox_txt.Name = "comboBox_txt";
            comboBox_txt.Size = new Size(121, 23);
            comboBox_txt.TabIndex = 17;
            comboBox_txt.Visible = false;
            // 
            // date_utworzenia_txt
            // 
            date_utworzenia_txt.CalendarTitleForeColor = SystemColors.ControlText;
            date_utworzenia_txt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            date_utworzenia_txt.Location = new Point(154, 71);
            date_utworzenia_txt.Name = "date_utworzenia_txt";
            date_utworzenia_txt.Size = new Size(200, 23);
            date_utworzenia_txt.TabIndex = 16;
            date_utworzenia_txt.Visible = false;
            // 
            // search_btn
            // 
            search_btn.BackColor = Color.LightSkyBlue;
            search_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            search_btn.ForeColor = Color.RoyalBlue;
            search_btn.Location = new Point(348, 159);
            search_btn.Name = "search_btn";
            search_btn.Size = new Size(75, 23);
            search_btn.TabIndex = 17;
            search_btn.Text = "Szukaj";
            search_btn.UseVisualStyleBackColor = false;
            search_btn.Visible = false;
            search_btn.Click += search_btn_Click;
            // 
            // clear_btn
            // 
            clear_btn.BackColor = Color.LightSkyBlue;
            clear_btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clear_btn.ForeColor = Color.RoyalBlue;
            clear_btn.Location = new Point(429, 159);
            clear_btn.Name = "clear_btn";
            clear_btn.Size = new Size(75, 23);
            clear_btn.TabIndex = 18;
            clear_btn.Text = "Wyczyść";
            clear_btn.UseVisualStyleBackColor = false;
            clear_btn.Visible = false;
            clear_btn.Click += clear_btn_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Gainsboro;
            menuStrip1.Items.AddRange(new ToolStripItem[] { user_bar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1147, 24);
            menuStrip1.TabIndex = 19;
            menuStrip1.Text = "menuStrip1";
            // 
            // user_bar
            // 
            user_bar.DropDownItems.AddRange(new ToolStripItem[] { zmieńHasToolStripMenuItem, wylogujToolStripMenuItem });
            user_bar.Name = "user_bar";
            user_bar.Size = new Size(80, 20);
            user_bar.Text = "Użytkownik";
            user_bar.Click += toolStripMenuItem1_Click;
            // 
            // zmieńHasToolStripMenuItem
            // 
            zmieńHasToolStripMenuItem.Name = "zmieńHasToolStripMenuItem";
            zmieńHasToolStripMenuItem.Size = new Size(141, 22);
            zmieńHasToolStripMenuItem.Text = "Zmień Haslo";
            zmieńHasToolStripMenuItem.Click += zmieńHasToolStripMenuItem_Click;
            // 
            // wylogujToolStripMenuItem
            // 
            wylogujToolStripMenuItem.Name = "wylogujToolStripMenuItem";
            wylogujToolStripMenuItem.Size = new Size(141, 22);
            wylogujToolStripMenuItem.Text = "Wyloguj";
            wylogujToolStripMenuItem.Click += wylogujToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1147, 601);
            Controls.Add(clear_btn);
            Controls.Add(search_btn);
            Controls.Add(toolStripContainer1);
            Controls.Add(button1);
            Controls.Add(add_btn);
            Controls.Add(button_package);
            Controls.Add(button_delivery);
            Controls.Add(button_products);
            Controls.Add(button_client);
            Controls.Add(tabela);
            Controls.Add(button_users);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Magazyn";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)tabela).EndInit();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.ContentPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_users;
        private DataGridView tabela;
        private Button button_client;
        private Button button_products;
        private Button button_delivery;
        private Button button_package;
        private Button add_btn;
        private Button button1;
        private Label filtr_lbl1;
        private Label filtr_lbl2;
        private TextBox filtr_txt1;
        private TextBox filtr_txt2;
        private ToolStripContainer toolStripContainer1;
        private Button search_btn;
        private Button clear_btn;
        private TextBox filtr_txt6;
        private TextBox filtr_txt5;
        private TextBox filtr_txt4;
        private TextBox filtr_txt3;
        private ComboBox comboBox_txt;
        private DateTimePicker date_utworzenia_txt;
        private Label filtr_lbl7;
        private Label filtr_lbl6;
        private Label filtr_lbl5;
        private Label filtr_lbl4;
        private Label filtr_lbl3;
        private TextBox filtr_txt7;
        private Label filtr_lbl9;
        private Label filtr_lbl8;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem user_bar;
        private ToolStripMenuItem zmieńHasToolStripMenuItem;
        private ToolStripMenuItem wylogujToolStripMenuItem;
    }
}
