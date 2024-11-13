using Npgsql;
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
    public partial class add_client : Form
    {
        public string connect_string = "";
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        string table = "";
        private void Connect_db() // Polaczenie z baza danych
        {
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
        public add_client()
        {
            InitializeComponent();
        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (email_txt.Text == " ")
                return true;
            else return false;
        }
        private void close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (check_Empty()) MessageBox.Show("Email musi być podany");
            else // jesli wszystkie pola sa zapelnione
            {
                string name = name_txt.Text;
                string nip = nip_txt.Text;
                string region = region_txt.Text;
                string pesel = pesel_txt.Text;
                string email = email_txt.Text;
                string tel = tel_txt.Text;
                string adres = adres_txt.Text;
                Connect_db();
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    string get_id_query = "SELECT COUNT(*) FROM klienci;";
                    int id_rows = 0;
                    using (NpgsqlCommand command = new NpgsqlCommand(get_id_query, connection)) // liczenie ilości wierszy w tabeli
                    {
                        object result = command.ExecuteScalar();
                        id_rows = Convert.ToInt32(result);
                        id_rows++;
                    }
                    string query = "INSERT INTO klienci(id, nazwa, nip, region, pesel, email, telefon, adres) VALUES(" + id_rows + ",'" + name + "','" + nip
                        + "','" + region + "','" + pesel + "','" + email + "','" + tel + "','" + adres + "')";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                    {
                        command.Parameters.AddWithValue("id", id_rows);
                        command.Parameters.AddWithValue("nazwa", name);
                        command.Parameters.AddWithValue("nip", nip);
                        command.Parameters.AddWithValue("region", region);
                        command.Parameters.AddWithValue("pesel", pesel);
                        command.Parameters.AddWithValue("email", email);
                        command.Parameters.AddWithValue("telefon", tel);
                        command.Parameters.AddWithValue("adres", adres);
                        int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych (nie działa)
                        MessageBox.Show("Dodano nowego klienta!!!");
                        this.Close();
                    }
                }
            }
        }
    }
}
