﻿using Npgsql;
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

namespace Magazyn
{
    public partial class package_form : Form
    {
        string usr = "";
        string uzytkownik = "";
        public string connect_string;
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        public package_form(string uzyt)
        {
            InitializeComponent();
            uzytkownik = uzyt;
        }

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
        private void Form5_Load(object sender, EventArgs e)
        {
            show_table();
            getData("SELECT nazwa, nip, region, pesel, email, telefon, adres FROM klienci;");
        }
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            Connect_db();
            DataTable dt = new DataTable();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            vCon.Close();
            return dt;
        }
        public void show_table() // funkcja pokazujaca tabelę
        {
            try
            {
                Connect_db();
                string client_query = "SELECT nazwa FROM klienci";
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(client_query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) klienci_txt.Items.Add(reader.GetString(0));
                        }
                    }
                    connection.Close();
                    vCon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bład połączenia z tabelą" + ex, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable dtgetdata = new DataTable();
            dtgetdata = getData("SELECT nazwa, nip, region, pesel, email, telefon, adres FROM klienci;");
            tabela.DataSource = dtgetdata;
            Cursor.Current = Cursor.Current;
        }
        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (nametxtbox.Text == String.Empty && niptxtbox.Text == String.Empty && regiontxtbox.Text == String.Empty && peseltxtbox.Text == String.Empty && emailtxtbox.Text == String.Empty && teltxtbox.Text == String.Empty && adrestxtbox.Text == String.Empty)
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData("SELECT nazwa, nip, region, pesel, email, telefon, adres FROM klienci;");
                tabela.DataSource = dtgetdata;
            } else
            {
                try
                {
                    DataTable dtgetdata = new DataTable();
                    dtgetdata = getData("SELECT * FROM klienci WHERE nazwa = '" + nametxtbox.Text + "' OR nip = '" + niptxtbox.Text +
                            "' OR region = '" + regiontxtbox.Text + "' OR pesel = '" + peseltxtbox.Text + "' OR email = '" + emailtxtbox.Text +
                            "' OR telefon = '" + teltxtbox.Text + "' OR adres = '" + adrestxtbox.Text + "' ;");
                    tabela.DataSource = dtgetdata;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bład połączenia z tabelą: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nametxtbox.Text = string.Empty;
            niptxtbox.Text = string.Empty;
            peseltxtbox.Text = string.Empty;
            regiontxtbox.Text = string.Empty;
            emailtxtbox.Text = string.Empty;
            teltxtbox.Text = string.Empty;
            adrestxtbox.Text = string.Empty;
        }

        private void confirm_btn_Click(object sender, EventArgs e)
        {
            usr = klienci_txt.Text;
            using (var con = new NpgsqlConnection(connect_string))
            {
                con.Open();
                string max_paczki_id = "SELECT MAX(id) FROM paczki;";
                int max_id = 0;
                using (var command = new NpgsqlCommand(max_paczki_id, con)) using (var reader = command.ExecuteReader()) if (reader.Read()) max_id = reader.GetInt32(0);
                
                using (var command = new NpgsqlCommand(max_paczki_id, con)) using (var reader = command.ExecuteReader()) if (reader.Read()) max_id = reader.GetInt32(0);

                string insert_paczki_query = "INSERT INTO paczki_produkty(paczka_id, ilosc, created_at, spakowany) " +
                    "VALUES(@paczka_id, @ilosc, @created_at, @spakowany)";

                /*using (var command = new NpgsqlCommand(insert_query, connection))
                {
                    command.Parameters.AddWithValue("@status", "nowy");
                    command.Parameters.AddWithValue("@wartosc", cena);
                    command.Parameters.AddWithValue("@created_by", userid);
                    command.Parameters.AddWithValue("@klient_id", clientid);
                    command.Parameters.AddWithValue("@data_utworzenia", DateTime.Now);
                    int rowsAffected = command.ExecuteNonQuery();
                }*/
                con.Close();
                var form_paczki = new Form9(usr, uzytkownik, max_id++);
                form_paczki.Show();
            }
            
            this.Close();
            
        }
    }
}
