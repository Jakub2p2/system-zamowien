using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class Form9 : Form
    {
        public string connect_string = "";
        string uzyt = "";
        string uzytkownik = string.Empty;
        string produkt = string.Empty;
        int id_produkt = 0, id_paczki = 0;
        double price = 0, weigth = 0;
        string state = string.Empty, next_state = string.Empty;
        string dostawca = string.Empty, nr_listu = string.Empty, data_odb = string.Empty, data_dos = string.Empty;
        double? ubz;
        double koszt_wys = 0;
        NpgsqlCommand vCmd;
        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;

        public Form9(string usr, string uzyt_p, string produkt_p, int p_id, double p_price, double p_weigth, string state_p,
            string dostawca_p, string nr_listu_p, string data_odb_p, string data_dos_p, double koszt_p, double? ubz_p, int paczki_id_p)
        {
            InitializeComponent();
            uzyt = usr;
            uzytkownik = uzyt_p;
            produkt = produkt_p;
            id_produkt = p_id;
            price = p_price;
            state = state_p;
            weigth = p_weigth;
            dostawca = dostawca_p;
            nr_listu = nr_listu_p;
            data_odb = data_odb_p;
            data_dos = data_dos_p;
            koszt_wys = koszt_p;
            ubz = ubz_p;
            couriertxt.Text = dostawca;
            letter_num_txt.Text = nr_listu;
            package_delivered_date_txt.Text = data_dos;
            package_received_date_txt.Text = data_odb;
            delivery_cost_txt.Text = koszt_wys + " zł";
            insurance_txt.Text = ubz + " zł";
            id_paczki = paczki_id_p;
        }
        public Form9(string usr, string uzyt_p, int paczki_id_p)
        {
            InitializeComponent();
            uzyt = usr;
            uzytkownik = uzyt_p;
            id_paczki = paczki_id_p;
        }
        public Form9(string usr, string uzyt_p, string produkt_p, int p_id, double p_price, double p_weigth, string state_p, int paczki_id_p)
        {
            InitializeComponent();
            uzyt = usr;
            uzytkownik = uzyt_p;
            produkt = produkt_p;
            id_produkt = p_id;
            price = p_price;
            state = state_p;
            weigth = p_weigth;
            id_paczki = paczki_id_p;
        }
        private void Connect_db() // Polaczenie z baza danych
        {
            try
            {
                con = new NpgsqlConnection();
                con.ConnectionString = con_string;
                connect_string = con.ConnectionString;
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
            }
            catch
            { // w razie braku połączenia z bazą
                MessageBox.Show("Błąd połączenia z bazą danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            Connect_db();
            DataTable dt = new DataTable();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = con;
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            return dt;
        }
        public void change_state()
        {
            if (state == "nowy")
            {
                state_paczka.Value = 0;
                next_state = "zamowiony";
                next_button.Text = "Prześlij do magazynu";
            }
            else if (state == "zamowiony")
            {
                state_paczka.Value = 20;
                next_state = "kompletacja";
                next_button.Text = "Kompletacja paczki";
                button2.Visible = false;

            }
            else if (state == "kompletacja")
            {
                state_paczka.Value = 40;
                next_state = "Przygotowany do wysyłki";
                next_button.Text = "Towar przygotowany do wysyłki";
                using (var connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    using (var com = new NpgsqlCommand($"UPDATE paczki_produkty SET spakowany = @spakowany WHERE id = {id_paczki}", connection))
                    {
                        com.Parameters.AddWithValue("@spakowany", true);
                        int rowsAffected = com.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                wstrzymaj_btn.Visible = true;
            }
            else if (state == "Przygotowany do wysyłki")
            {
                state_paczka.Value = 60;
                next_state = "oczekiwanie";
                next_button.Text = "Oczekiwanie na kuriera";
                wstrzymaj_btn.Visible = false;
            }
            else if (state == "oczekiwanie")
            {
                state_paczka.Value = 80;
                next_state = "wydany";
                next_button.Text = "Towar odebrany przez kuriera";
                look_paczka.Visible = true;
            }
            else if (state == "wydany")
            {
                state_paczka.Value = 100;
                next_button.Visible = false;
            }
            else if(state == "wstrzymany")
            {
                wstrzymaj_btn.Visible = false;
                next_button.Text = "Towar przygotowany do wysyłki";
                next_state = "Przygotowany do wysyłki";
                state_paczka.Value = 40;
            }
            if (state_paczka.Value >= 60) spakowany_bar.Value = 100;
        }
        private void Form_Login_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            con.Open();
            var sql = $"SELECT nip, region, pesel, adres FROM klienci WHERE nazwa = '{uzyt}';";
            using (var cmd = new NpgsqlCommand(sql, con))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string[] res = new string[]
                        {
                            reader["nip"]?.ToString(),
                            reader["region"]?.ToString(),
                            reader["pesel"]?.ToString(),
                            reader["adres"]?.ToString()
                        };
                        nametxt.Text = uzyt;
                        niptxt.Text = res[0] + " " + res[1] + " " + res[2];
                        adrestxt.Text = res[3];
                        datetxt.Text = DateTime.Now.ToString();
                        madebytxt.Text = uzytkownik;
                        valuetxt.Text = price.ToString() + " zł";
                        weighttxt.Text = weigth.ToString() + " kg";
                        statustxt.Text = state;
                        id_paczkitxt.Text = "ID paczki: " + id_paczki.ToString();
                    }
                }
            }
            var produkt_sql = $"SELECT * FROM produkty WHERE id = {id_produkt}";
            using (var cmd = new NpgsqlCommand(sql, con))
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData(produkt_sql);
                tabela_produkt.DataSource = dtgetdata;
            }
            change_state();
        }
        private void button2_Click(object sender, EventArgs e) //przycisk otwierajacy form ktory dodaje produkt do paczki, zapomnialem zmienic nazwy przycisku :( 
        {
            this.Close();
            var add_product = new Form10(uzyt, uzytkownik);
            add_product.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (state == "Przygotowany do wysyłki")
            {
                this.Close();
                var choose_delivery = new Form11(uzyt, uzytkownik, produkt, id_produkt, price, weigth, state, id_paczki);
                choose_delivery.Show();
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                if (!state.Equals("Przygotowany do wysyłki")) state = next_state;
                string getclient_id = $"SELECT id FROM klienci WHERE nazwa = '{uzyt}';";
                string getuser_id = $"SELECT id FROM uzytkownicy WHERE login = '{uzytkownik}';";
                //string getid_query = "SELECT id FROM paczki WHERE klient_id = @klient_id AND created_by = @created_by;";
                string getid_query = "SELECT MAX(id) FROM paczki;";
                int clientid = 0, userid = 0, id = 0;
                using (var command = new NpgsqlCommand(getclient_id, connection))
                using (var reader = command.ExecuteReader()) if (reader.Read()) clientid = reader.GetInt32(0);
                using (var command = new NpgsqlCommand(getuser_id, connection))
                using (var reader = command.ExecuteReader()) if (reader.Read()) userid = reader.GetInt32(0);
                using (var command = new NpgsqlCommand(getid_query, connection))
                using (var reader = command.ExecuteReader()) if (reader.Read()) id = reader.GetInt32(0);
                using (var command = new NpgsqlCommand($"UPDATE paczki SET status = @status WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@status", state);
                    command.Parameters.AddWithValue("@klient_id", clientid);
                    command.Parameters.AddWithValue("user_id", userid);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                change_state();
                statustxt.Text = state;
            }
        }

        private void look_paczka_Click(object sender, EventArgs e)
        {
            string dostawa_link = string.Empty;
            using (var connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                using (var command = new NpgsqlCommand($"SELECT link_do_śledzenia FROM dostawy WHERE nazwa = '{dostawca}'", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) dostawa_link = reader.GetString(0);
                    }
                }
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://www." + dostawa_link,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: Nieznaleziono takiej strony. " + ex.Message);
                }
                connection.Close();
            }
        }

        private void wstrzymaj_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Czy napewno chcesz wstrzymać paczkę?", "UWAGA!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
                {
                    connection.Open();
                    string getid_query = "SELECT MAX(id) FROM paczki;";
                    int id = 0;
                    using (var command = new NpgsqlCommand(getid_query, connection))
                    using (var reader = command.ExecuteReader()) if (reader.Read()) id = reader.GetInt32(0);
                    using (var command = new NpgsqlCommand($"UPDATE paczki SET status = @status WHERE id = @id", connection))
                    {
                        command.Parameters.AddWithValue("@status", "wstrzymany");
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                wstrzymaj_btn.Visible = false;
            }
        }

        private void datetxt_Click(object sender, EventArgs e)
        {

        }
    }
}
