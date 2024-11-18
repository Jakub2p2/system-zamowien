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

namespace Magazyn
{
    public partial class Form10 : Form
    {
        public Form10()
        {
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
        }

        private void tabela_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
