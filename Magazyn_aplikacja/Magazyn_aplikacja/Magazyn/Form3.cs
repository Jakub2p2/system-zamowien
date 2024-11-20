using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Magazyn
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
        }
        public string connect_string = "";

       
        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;
        private Form1 main_form;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var add_usr_form = new add_user(false, [], 0, main_form);
            add_usr_form.Show();
        }
        static string algorytm_md5(string input) // szyfrowanie hasla za pomocą md5
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string descript_pass = algorytm_md5(passtxt.Text);
            try
            {
                
                con.Open();
                var sql = @"SELECT * FROM user_login(:_username,:_password)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("_username", usertxt.Text);
                cmd.Parameters.AddWithValue("_password", descript_pass);


                int res = (int)cmd.ExecuteScalar();

                con.Close();
                if (res == 1) //kwerenda zakonczona sukcesem
                {
                    string uzytkownik = usertxt.Text;
                    string pass = passtxt.Text;
                    this.Hide();
                    var main_form = new Form1(uzytkownik, pass);
                    main_form.Show();
                }
                else if (res == 0) //blad
                {
                    MessageBox.Show("Prosze sprawdz dane", "Błąd logowania", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Coś poszło nie tak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }

        private void usertxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            //usertxt.Text = string.Empty;
            //passtxt.Text = string.Empty;
        }

        private void passtxt_TextChanged(object sender, EventArgs e)
        {
            passtxt.PasswordChar = '*';
        }
    }

}
