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

namespace Magazyn
{
    public partial class package_form : Form
    {
        NpgsqlCommand vCmd;
        public package_form()
        {
            InitializeComponent();
        }
        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;"
            + "port=13890;" + "user id=avnadmin;"
            + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;
        private void Form5_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            con.Open();

        }
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            DataTable dt = new DataTable();
            vCmd = new NpgsqlCommand();
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }
        public void show_table() // funkcja pokazujaca tabelę
        {
            try
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData("SELECT * FROM klienci;");
                tabela.DataSource = dtgetdata;
            }
            catch
            {
                MessageBox.Show("Bład połączenia z tabelą", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursor.Current;
        }

        private void tabela_CellContentClick(object sender, DataGridViewCellEventArgs e) //funkcja owtorzy nowy formularz
        {

        }
    }
}
