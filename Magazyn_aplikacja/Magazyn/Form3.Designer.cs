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
            label1.Location = new Point(-1, 9);
            label1.Name = "label1";
            label1.Size = new Size(276, 15);
            label1.TabIndex = 0;
            label1.Text = "System wspomagający wysyłkę paczek z magazynu";
            // 
            // usertxt
            // 
            usertxt.Location = new Point(73, 44);
            usertxt.Name = "usertxt";
            usertxt.PlaceholderText = "login";
            usertxt.Size = new Size(138, 23);
            usertxt.TabIndex = 1;
            usertxt.Text = "admin";
            usertxt.TextChanged += usertxt_TextChanged;
            // 
            // passtxt
            // 
            passtxt.Location = new Point(73, 73);
            passtxt.Name = "passtxt";
            passtxt.PlaceholderText = "password";
            passtxt.Size = new Size(138, 23);
            passtxt.TabIndex = 2;
            passtxt.Text = "password";
            passtxt.TextChanged += passtxt_TextChanged;
            // 
            // logbtn
            // 
            logbtn.Location = new Point(98, 102);
            logbtn.Name = "logbtn";
            logbtn.Size = new Size(81, 22);
            logbtn.TabIndex = 3;
            logbtn.Text = "login";
            logbtn.UseVisualStyleBackColor = true;
            logbtn.Click += button1_Click;
            // 
            // add_usr
            // 
            add_usr.Location = new Point(82, 143);
            add_usr.Name = "add_usr";
            add_usr.Size = new Size(113, 22);
            add_usr.TabIndex = 4;
            add_usr.Text = "stworz konto";
            add_usr.UseVisualStyleBackColor = true;
            add_usr.Click += button2_Click;
            // 
            // Form_Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 224);
            Controls.Add(add_usr);
            Controls.Add(logbtn);
            Controls.Add(passtxt);
            Controls.Add(usertxt);
            Controls.Add(label1);
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