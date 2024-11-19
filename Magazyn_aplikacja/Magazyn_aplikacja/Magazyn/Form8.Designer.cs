namespace Magazyn
{
    partial class Form8
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
            currentpasstxt = new TextBox();
            newpasstxt = new TextBox();
            confirmpasstxt = new TextBox();
            confirmbtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(83, 19);
            label1.Name = "label1";
            label1.Size = new Size(120, 15);
            label1.TabIndex = 0;
            label1.Text = "Wpisz obecne Haslo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(83, 93);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 1;
            label2.Text = "Wpisz nowe Haslo:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.RoyalBlue;
            label3.Location = new Point(83, 164);
            label3.Name = "label3";
            label3.Size = new Size(168, 15);
            label3.TabIndex = 2;
            label3.Text = "Wpisz nowe Haslo ponownie:";
            // 
            // currentpasstxt
            // 
            currentpasstxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            currentpasstxt.Location = new Point(83, 37);
            currentpasstxt.Name = "currentpasstxt";
            currentpasstxt.Size = new Size(100, 23);
            currentpasstxt.TabIndex = 3;
            // 
            // newpasstxt
            // 
            newpasstxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            newpasstxt.Location = new Point(83, 111);
            newpasstxt.Name = "newpasstxt";
            newpasstxt.Size = new Size(100, 23);
            newpasstxt.TabIndex = 4;
            // 
            // confirmpasstxt
            // 
            confirmpasstxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            confirmpasstxt.Location = new Point(83, 182);
            confirmpasstxt.Name = "confirmpasstxt";
            confirmpasstxt.Size = new Size(100, 23);
            confirmpasstxt.TabIndex = 5;
            // 
            // confirmbtn
            // 
            confirmbtn.BackColor = Color.LightSkyBlue;
            confirmbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            confirmbtn.ForeColor = Color.RoyalBlue;
            confirmbtn.Location = new Point(83, 224);
            confirmbtn.Name = "confirmbtn";
            confirmbtn.Size = new Size(106, 23);
            confirmbtn.TabIndex = 6;
            confirmbtn.Text = "Zmien Haslo";
            confirmbtn.UseVisualStyleBackColor = false;
            confirmbtn.Click += confirmbtn_Click;
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(321, 285);
            Controls.Add(confirmbtn);
            Controls.Add(confirmpasstxt);
            Controls.Add(newpasstxt);
            Controls.Add(currentpasstxt);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form8";
            Text = "Zmień Haslo";
            Load += Form_Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox currentpasstxt;
        private TextBox newpasstxt;
        private TextBox confirmpasstxt;
        private Button confirmbtn;
    }
}