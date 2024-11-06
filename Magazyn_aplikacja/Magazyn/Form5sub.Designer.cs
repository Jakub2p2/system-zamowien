namespace Magazyn
{
    partial class Form5sub
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
            sendpackagebtn = new Button();
            SuspendLayout();
            // 
            // sendpackagebtn
            // 
            sendpackagebtn.Location = new Point(12, 12);
            sendpackagebtn.Name = "sendpackagebtn";
            sendpackagebtn.Size = new Size(127, 23);
            sendpackagebtn.TabIndex = 0;
            sendpackagebtn.Text = "Przeslij do magazynu";
            sendpackagebtn.UseVisualStyleBackColor = true;
            // 
            // Form5sub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(sendpackagebtn);
            Name = "Form5sub";
            Text = "Form5sub";
            ResumeLayout(false);
        }

        #endregion

        private Button sendpackagebtn;
    }
}