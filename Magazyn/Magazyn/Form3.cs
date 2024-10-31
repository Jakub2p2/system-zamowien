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
    public partial class Form_Login : Form
    {
        public Form_Login()
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
                connect_string = strConnDtb;
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
            this.Hide();
            var add_usr_form = new add_user();
            add_usr_form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect_db();
            var sql = @"SELECT username, password FROM uzytkownicy";
            //using var dataSource = NpgsqlDataSource.Create(connect_string);
            //using var cmd = dataSource.CreateCommand(sql);
            //using var reader = cmd.ExecuteReader(); // czytnik danych
            string password = passtxt.Text;
            string user = usertxt.Text;
            if (user == "admin" && password == "password") // imitacja logowania
            {
                var main_form = new Form1();
                main_form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Błędny login lub hasło!");
            }
        }

        private void usertxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            //usertxt.Text = string.Empty;
            //passtxt.Text = string.Empty;
        }

        private void passtxt_TextChanged(object sender, EventArgs e)
        {
            passtxt.PasswordChar = '*';
        }
    }

}
