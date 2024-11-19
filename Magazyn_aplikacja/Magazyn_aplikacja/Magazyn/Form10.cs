using Microsoft.VisualBasic.ApplicationServices;
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
using System.Xml.Linq;

namespace Magazyn
{
    public partial class Form10 : Form
    {
        string klient = string.Empty;
        string uzytkownik = string.Empty;
        public Form10(string p_klt, string uzyt_p)
        {
            klient = p_klt;
            uzytkownik = uzyt_p;
            InitializeComponent();
        }
        NpgsqlCommand vCmd;
        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            DataTable dt = new DataTable();
            con.Open();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = con;
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            return dt;
        }
        public void show_table() // funkcja pokazujaca tabelę
        {
            try
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData("SELECT nazwa, cechy, cena, ilosc FROM produkty;");
                tabela.DataSource = dtgetdata;
                string product_query = "SELECT nazwa FROM produkty";
                using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(product_query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) products_txt.Items.Add(reader.GetString(0));
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursor.Current;
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            show_table();
        }

        private void tabela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            string selected_product = products_txt.Text;
            int produkt_id = 0;
            double cena = 0, waga = 0;
            string getid_sql = $"SELECT id FROM produkty WHERE nazwa = '{selected_product}'";
            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(getid_sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) produkt_id = reader.GetInt32(0);
                    }
                }
                connection.Close();
            }
            string getprice_sql = $"SELECT cena FROM produkty WHERE nazwa = '{selected_product}'";
            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(getprice_sql, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) cena = reader.GetInt32(0);
                }
                connection.Close();
            }
            string getweigth_sql = $"SELECT waga FROM produkty WHERE nazwa = '{selected_product}'";
            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(getweigth_sql, connection))
                {
                    using (var reader = command.ExecuteReader()) while (reader.Read()) waga = reader.GetInt32(0);
                }
                connection.Close();
            }
            string insert_query = "INSERT INTO paczki(status, wartosc, created_by, klient_id, data_utworzenia) " +
                "VALUES(@status, @wartosc, @created_by, @klient_id, @data_utworzenia)";
            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                string getclient_id = $"SELECT id FROM klienci WHERE nazwa = '{klient}';";
                string getuser_id = $"SELECT id FROM uzytkownicy WHERE login = '{uzytkownik}';";
                int clientid = 0, userid = 0;
                using(var command = new NpgsqlCommand(getclient_id, connection)) 
                    using (var reader = command.ExecuteReader()) if(reader.Read()) clientid = reader.GetInt32(0);
                using (var command = new NpgsqlCommand(getuser_id, connection)) 
                    using (var reader = command.ExecuteReader()) if(reader.Read()) userid = reader.GetInt32(0);
                
                using (var command = new NpgsqlCommand(insert_query, connection))
                {
                    command.Parameters.AddWithValue("@status", "nowy");
                    command.Parameters.AddWithValue("@wartosc", cena);
                    command.Parameters.AddWithValue("@created_by", userid);
                    command.Parameters.AddWithValue("@klient_id", clientid);
                    command.Parameters.AddWithValue("@data_utworzenia", DateTime.Now);
                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            var form_paczki = new Form9(klient, uzytkownik, selected_product, produkt_id, cena, waga, "nowy");
            form_paczki.Show();
            this.Close();
        }
    }
}
