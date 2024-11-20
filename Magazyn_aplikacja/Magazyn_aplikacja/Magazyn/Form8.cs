using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Magazyn
{
    public partial class Form8 : Form
    {
        string uzyt = "";
        string password = "";
        public Form8(string usr, string pass)
        {
            InitializeComponent();
            uzyt = usr;
            password = pass;
        }
        public string connect_string = "";


        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;
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
        private void confirmbtn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                var sql = @"SELECT * FROM user_login(:_username,:_password)";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("_username", uzyt);
                cmd.Parameters.AddWithValue("_password", algorytm_md5(password));
                int res = (int)cmd.ExecuteScalar();
                string query = "";
                if (res == 1) //kwerenda zakonczona sukcesem
                {
                    if (newpasstxt.Text == confirmpasstxt.Text)
                    {
                        query = "UPDATE uzytkownicy SET haslo = @hashed_pass WHERE login = @login;";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, con)) 
                        {
                            command.Parameters.AddWithValue("@login", uzyt);
                            command.Parameters.AddWithValue("@hashed_pass", algorytm_md5(newpasstxt.Text)); ;
                            command.ExecuteNonQuery();
                            MessageBox.Show("Haslo zostalo zmienione!");
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nowe Hasla nie sa zgodne", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        con.Close();
                        return;
                    }
                }
                else if (res == 0) //blad
                {
                    MessageBox.Show("Niepoprawne haslo", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    con.Close();
                    return;
                }
                
            } catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Coś poszło nie tak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            
        }
        private void Form_Login_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            
        }
    }
}
