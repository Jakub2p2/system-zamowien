namespace Magazyn
{
    partial class Form10
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
            nametxt = new Label();
            characteristicstxt = new Label();
            namebox = new TextBox();
            charbox = new TextBox();
            searchbtn = new Button();
            clearbtn = new Button();
            tabela = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)tabela).BeginInit();
            SuspendLayout();
            // 
            // nametxt
            // 
            nametxt.AutoSize = true;
            nametxt.ForeColor = Color.RoyalBlue;
            nametxt.Location = new Point(44, 23);
            nametxt.Name = "nametxt";
            nametxt.Size = new Size(45, 15);
            nametxt.TabIndex = 0;
            nametxt.Text = "Nazwa:";
            // 
            // characteristicstxt
            // 
            characteristicstxt.AutoSize = true;
            characteristicstxt.ForeColor = Color.RoyalBlue;
            characteristicstxt.Location = new Point(162, 23);
            characteristicstxt.Name = "characteristicstxt";
            characteristicstxt.Size = new Size(43, 15);
            characteristicstxt.TabIndex = 1;
            characteristicstxt.Text = "Cechy:";
            // 
            // namebox
            // 
            namebox.Location = new Point(44, 41);
            namebox.Name = "namebox";
            namebox.Size = new Size(100, 23);
            namebox.TabIndex = 2;
            // 
            // charbox
            // 
            charbox.Location = new Point(162, 41);
            charbox.Name = "charbox";
            charbox.Size = new Size(100, 23);
            charbox.TabIndex = 3;
            // 
            // searchbtn
            // 
            searchbtn.BackColor = Color.LightSkyBlue;
            searchbtn.ForeColor = Color.RoyalBlue;
            searchbtn.Location = new Point(311, 15);
            searchbtn.Name = "searchbtn";
            searchbtn.Size = new Size(75, 23);
            searchbtn.TabIndex = 4;
            searchbtn.Text = "Szukaj";
            searchbtn.UseVisualStyleBackColor = false;
            // 
            // clearbtn
            // 
            clearbtn.BackColor = Color.LightSkyBlue;
            clearbtn.ForeColor = Color.RoyalBlue;
            clearbtn.Location = new Point(392, 15);
            clearbtn.Name = "clearbtn";
            clearbtn.Size = new Size(75, 23);
            clearbtn.TabIndex = 5;
            clearbtn.Text = "Wyczysc";
            clearbtn.UseVisualStyleBackColor = false;
            // 
            // tabela
            // 
            tabela.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabela.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tabela.EditMode = DataGridViewEditMode.EditOnEnter;
            tabela.Location = new Point(12, 114);
            tabela.Name = "tabela";
            tabela.Size = new Size(703, 324);
            tabela.TabIndex = 6;
            tabela.CellContentClick += tabela_CellContentClick;
            // 
            // Form10
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(727, 450);
            Controls.Add(tabela);
            Controls.Add(clearbtn);
            Controls.Add(searchbtn);
            Controls.Add(charbox);
            Controls.Add(namebox);
            Controls.Add(characteristicstxt);
            Controls.Add(nametxt);
            Name = "Form10";
            Text = "Wybierz Produkt";
            Load += Form10_Load;
            ((System.ComponentModel.ISupportInitialize)tabela).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nametxt;
        private Label characteristicstxt;
        private TextBox namebox;
        private TextBox charbox;
        private Button searchbtn;
        private Button clearbtn;
        private DataGridView tabela;
    }
}