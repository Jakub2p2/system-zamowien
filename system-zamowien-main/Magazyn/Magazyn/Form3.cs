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
using Npgsql;

namespace Magazyn
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public string connect_string = "";

        private void Connect_db() // Polaczenie z baza danych
        {
            string strConnDtb = "Server=185.157.80.106; port=5432; user id=postgres; password=123; database=Uzytkownicy; ";
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

        private void button2_Click(object sender, EventArgs e)
        {
            var add_usr_form = new add_user();
            add_usr_form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sql = @"SELECT username, password FROM uzytkownicy";
            using var dataSource = NpgsqlDataSource.Create(connect_string);
            using var cmd = dataSource.CreateCommand(sql);
            using var reader = cmd.ExecuteReader(); // czytnik danych
            string password = passtxt.Text;
            string user = usertxt.Text;
        }
    }

}
