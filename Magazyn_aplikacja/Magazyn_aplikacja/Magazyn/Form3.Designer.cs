namespace Magazyn
{
    partial class Form_Login
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
            usertxt = new TextBox();
            passtxt = new TextBox();
            logbtn = new Button();
            add_usr = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(162, 30);
            label1.TabIndex = 0;
            label1.Text = "System wspomagający \r\nwysyłkę paczek z magazynu\r\n";
            // 
            // usertxt
            // 
            usertxt.BackColor = SystemColors.Window;
            usertxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            usertxt.Location = new Point(73, 66);
            usertxt.Name = "usertxt";
            usertxt.PlaceholderText = "login";
            usertxt.Size = new Size(138, 23);
            usertxt.TabIndex = 1;
            usertxt.TextChanged += usertxt_TextChanged;
            // 
            // passtxt
            // 
            passtxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            passtxt.Location = new Point(73, 95);
            passtxt.Name = "passtxt";
            passtxt.PlaceholderText = "password";
            passtxt.Size = new Size(138, 23);
            passtxt.TabIndex = 2;
            passtxt.TextChanged += passtxt_TextChanged;
            // 
            // logbtn
            // 
            logbtn.BackColor = Color.MidnightBlue;
            logbtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            logbtn.ForeColor = Color.LightSkyBlue;
            logbtn.Location = new Point(93, 133);
            logbtn.Name = "logbtn";
            logbtn.Size = new Size(81, 22);
            logbtn.TabIndex = 3;
            logbtn.Text = "login";
            logbtn.UseVisualStyleBackColor = false;
            logbtn.Click += button1_Click;
            // 
            // add_usr
            // 
            add_usr.BackColor = Color.LightSkyBlue;
            add_usr.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            add_usr.ForeColor = Color.MidnightBlue;
            add_usr.Location = new Point(82, 190);
            add_usr.Name = "add_usr";
            add_usr.Size = new Size(113, 22);
            add_usr.TabIndex = 4;
            add_usr.Text = "stworz konto";
            add_usr.UseVisualStyleBackColor = false;
            add_usr.Click += button2_Click;
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(279, 224);
            Controls.Add(add_usr);
            Controls.Add(logbtn);
            Controls.Add(passtxt);
            Controls.Add(usertxt);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form_Login";
            Text = "Login";
            Load += Form_Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox usertxt;
        private TextBox passtxt;
        private Button logbtn;
        private Button add_usr;
    }
}