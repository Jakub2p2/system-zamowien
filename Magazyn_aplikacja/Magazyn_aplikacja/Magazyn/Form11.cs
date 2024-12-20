﻿using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Magazyn
{
    public partial class Form11 : Form
    {
        NpgsqlCommand vCmd;
        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;
        string klient = string.Empty;
        string uzytkownik = string.Empty;
        string produkt = string.Empty;
        int id_produkt = 0, paczki_id = 0;
        double price = 0, weigth = 0;
        string state = string.Empty, next_state = string.Empty;
        public Form11(string usr, string uzyt_p, string produkt_p, int p_id, double p_price, double p_weigth, string state_p, int paczkid_p)
        {
            klient = usr;
            uzytkownik = uzyt_p;
            produkt = produkt_p;
            id_produkt = p_id;
            price = p_price;
            state = state_p;
            weigth = p_weigth;
            paczki_id = paczkid_p;
            InitializeComponent();
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            string dostawa = dostawcy_txt.Text;
            string list = textBox1.Text;

            DateTime data_odb = dateTimePicker1.Value.Date;  
            DateTime data_przew = dateTimePicker2.Value.Date;
            double? ubz = 0; 
            double cena = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();

                int id = 0;
                using (var command = new NpgsqlCommand("SELECT MAX(id) FROM paczki", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) id = reader.GetInt32(0);
                    }
                }

                int dostawcy_id = 0;
                string getdelivery_id = $"SELECT id FROM dostawy WHERE nazwa = @dostawa";
                using (var command = new NpgsqlCommand(getdelivery_id, connection))
                {
                    command.Parameters.AddWithValue("@dostawa", dostawa);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) dostawcy_id = reader.GetInt32(0);
                    }
                }
                if (checkBox1.Checked)
                {
                    using (var command = new NpgsqlCommand("SELECT cena_ubezpieczenia FROM dostawy WHERE id = @dostawa_id", connection))
                    {
                        command.Parameters.AddWithValue("@dostawa_id", dostawcy_id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) ubz = reader.GetInt32(0);
                        }
                    }
                }
                using (var command = new NpgsqlCommand("SELECT cena_za_kg FROM dostawy WHERE id = @dostawa_id", connection))
                {
                    command.Parameters.AddWithValue("@dostawa_id", dostawcy_id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) cena = reader.GetInt32(0);
                    }
                }
                string update_paczki = "UPDATE paczki SET data_odbioru = @data_odbioru, data_dostarczenia = @data_dostarczenia, " +
                                       "ubezpieczenie = @ubezpieczenie, nr_listu = @nr_listu, koszt_transportu = @koszt_transportu, dostawa_id = @dostawa_id WHERE id = @id;";

                using (var command = new NpgsqlCommand(update_paczki, connection))
                {
                    command.Parameters.AddWithValue("@data_odbioru", data_odb); 
                    command.Parameters.AddWithValue("@data_dostarczenia", data_przew);
                    command.Parameters.AddWithValue("@ubezpieczenie", ubz);
                    command.Parameters.AddWithValue("@nr_listu", list);
                    command.Parameters.AddWithValue("@koszt_transportu", cena);
                    command.Parameters.AddWithValue("@dostawa_id", dostawcy_id);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
                string name_dostawa = string.Empty;
                using (var command = new NpgsqlCommand($"SELECT nazwa FROM dostawy WHERE id = {dostawcy_id};", connection)) {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) name_dostawa = reader.GetString(0);
                    }
                }
                connection.Close();
                this.Close();
                var form_paczki = new Form9(klient, uzytkownik, produkt, id_produkt, cena, weigth, "oczekiwanie", name_dostawa, list, 
                    data_odb.ToString("yyyy-MM-dd"), data_przew.ToString("yyyy-MM-dd"), cena, ubz, paczki_id);
                form_paczki.Show();
            }
        }


        private void Form11_Load(object sender, EventArgs e)
        {
            string delivery_query = "SELECT nazwa FROM dostawy";
            using (NpgsqlConnection connection = new NpgsqlConnection(con_string))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(delivery_query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) dostawcy_txt.Items.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
