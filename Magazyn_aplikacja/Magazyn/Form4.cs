﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class delivery_form : Form
    {
        public delivery_form()
        {
            InitializeComponent();
        }
        public string connect_string = "";
        private void Connect_db() // Polaczenie z baza danych
        {
            string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
            NpgsqlConnection vCon;
            NpgsqlCommand vCmd;
            try
            {
                vCon = new NpgsqlConnection();
                vCon.ConnectionString = strConnDtb;
                connect_string = vCon.ConnectionString;
                if (vCon.State == System.Data.ConnectionState.Closed) vCon.Open();
            }
            catch
            { // w razie braku połączenia z bazą
                MessageBox.Show("Błąd połączenia z bazą danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void close_window_Click(object sender, EventArgs e) // zamykanie okna
        {
            this.Close();
            var login_form = new Form_Login();
            login_form.ShowDialog();
        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0 || textBox3.TextLength == 0 || numericUpDown1 == null)
                return true;
            else return false;
        }
        private void save_button_Click(object sender, EventArgs e)
        {

        }

        private void save_button_Click_1(object sender, EventArgs e)
        {
            if (check_Empty()) MessageBox.Show("Nie wszystkie pola są uzupełnione");
            else // jesli wszystkie pola sa zapelnione
            {
                string name = textBox1.Text;
                int cena_ubz = Convert.ToInt32(textBox2.Text);
                string link = textBox3.Text;
                double cena_kg = Convert.ToDouble(numericUpDown1.Value);
                Connect_db();
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    string query = "INSERT INTO dostawy(nazwa, cena_za_kg, cena_ubezpieczenia, link_do_śledzenia) VALUES('" + name + "'," + cena_kg + "," + cena_ubz + ",'" + link + "')";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                    {
                        command.Parameters.AddWithValue("nazwa", name);
                        command.Parameters.AddWithValue("cena_za_kg", cena_kg);
                        command.Parameters.AddWithValue("cena_ubezpieczenia", cena_ubz);
                        command.Parameters.AddWithValue("link_do_śledzenia", link);
                        int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych (nie działa)
                        MessageBox.Show("Dodano sposób dostawy!!!");
                        this.Close();
                    }
                }
            }
        }

        private void close_window_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void delivery_form_Load(object sender, EventArgs e)
        {

        }
    }
}
