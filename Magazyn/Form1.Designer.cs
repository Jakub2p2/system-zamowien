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
            addusr_btn = new Button();
            addelivery_btn = new Button();
            addpackage = new Button();
            addproduct_btn = new Button();
            ((System.ComponentModel.ISupportInitialize)tabela).BeginInit();
            SuspendLayout();
            // 
            // button_users
            // 
            button_users.Location = new Point(41, 12);
            button_users.Name = "button_users";
            button_users.Size = new Size(131, 83);
            button_users.TabIndex = 0;
            button_users.Text = "Użytkownicy";
            button_users.UseVisualStyleBackColor = true;
            button_users.Click += button_users_Click;
            // 
            // tabela
            // 
            tabela.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabela.Location = new Point(41, 201);
            tabela.Name = "tabela";
            tabela.Size = new Size(743, 274);
            tabela.TabIndex = 1;
            // 
            // button_client
            // 
            button_client.Location = new Point(193, 12);
            button_client.Name = "button_client";
            button_client.Size = new Size(131, 83);
            button_client.TabIndex = 2;
            button_client.Text = "Klienci";
            button_client.UseVisualStyleBackColor = true;
            button_client.Click += button_client_Click;
            // 
            // button_products
            // 
            button_products.Location = new Point(344, 12);
            button_products.Name = "button_products";
            button_products.Size = new Size(131, 83);
            button_products.TabIndex = 3;
            button_products.Text = "Produkty";
            button_products.UseVisualStyleBackColor = true;
            button_products.Click += button_products_Click;
            // 
            // button_delivery
            // 
            button_delivery.Location = new Point(502, 12);
            button_delivery.Name = "button_delivery";
            button_delivery.Size = new Size(131, 83);
            button_delivery.TabIndex = 4;
            button_delivery.Text = "Dostawy";
            button_delivery.UseVisualStyleBackColor = true;
            button_delivery.Click += button_delivery_Click;
            // 
            // button_package
            // 
            button_package.Location = new Point(653, 12);
            button_package.Name = "button_package";
            button_package.Size = new Size(131, 83);
            button_package.TabIndex = 5;
            button_package.Text = "Paczki";
            button_package.UseVisualStyleBackColor = true;
            button_package.Click += button_package_Click;
            // 
            // addusr_btn
            // 
            addusr_btn.Location = new Point(41, 163);
            addusr_btn.Name = "addusr_btn";
            addusr_btn.Size = new Size(131, 23);
            addusr_btn.TabIndex = 6;
            addusr_btn.Text = "Dodaj użytkownika";
            addusr_btn.UseVisualStyleBackColor = true;
            addusr_btn.Click += addusr_btn_Click;
            // 
            // addelivery_btn
            // 
            addelivery_btn.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 238);
            addelivery_btn.Location = new Point(502, 163);
            addelivery_btn.Name = "addelivery_btn";
            addelivery_btn.Size = new Size(131, 23);
            addelivery_btn.TabIndex = 7;
            addelivery_btn.Text = "Dodaj sposób dostawy";
            addelivery_btn.UseVisualStyleBackColor = true;
            addelivery_btn.Click += addelivery_btn_Click;
            // 
            // addpackage
            // 
            addpackage.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 238);
            addpackage.Location = new Point(653, 163);
            addpackage.Name = "addpackage";
            addpackage.Size = new Size(131, 23);
            addpackage.TabIndex = 8;
            addpackage.Text = "Dodaj nową paczkę";
            addpackage.UseVisualStyleBackColor = true;
            addpackage.Click += addpackage_Click;
            // 
            // addproduct_btn
            // 
            addproduct_btn.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, 238);
            addproduct_btn.Location = new Point(344, 163);
            addproduct_btn.Name = "addproduct_btn";
            addproduct_btn.Size = new Size(131, 23);
            addproduct_btn.TabIndex = 9;
            addproduct_btn.Text = "Dodaj nowy produkt";
            addproduct_btn.UseVisualStyleBackColor = true;
            addproduct_btn.Click += addproduct_btn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 487);
            Controls.Add(addproduct_btn);
            Controls.Add(addpackage);
            Controls.Add(addelivery_btn);
            Controls.Add(addusr_btn);
            Controls.Add(button_package);
            Controls.Add(button_delivery);
            Controls.Add(button_products);
            Controls.Add(button_client);
            Controls.Add(tabela);
            Controls.Add(button_users);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Magazyn";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)tabela).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button_users;
        private DataGridView tabela;
        private Button button_client;
        private Button button_products;
        private Button button_delivery;
        private Button button_package;
        private Button addusr_btn;
        private Button addelivery_btn;
        private Button addpackage;
        private Button addproduct_btn;
    }
}
