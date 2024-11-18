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
    public partial class Form9 : Form
    {
        public string connect_string = "";
        string uzyt = "";

        string con_string = String.Format("server=pg-26a19d25-paczkimagazyn.h.aivencloud.com;" + "port=13890;" + "user id=avnadmin;" + "password=AVNS_3UbLex9BxU_ZYRZvxaY; " + "database=paczuszki;"); //string polaczeniowy z baza
        private NpgsqlConnection con;

        public Form9(string usr)
        {
            InitializeComponent();
            uzyt = usr;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }
        private void Form_Login_Load(object sender, EventArgs e)
        {
            con = new NpgsqlConnection(con_string);
            con.Open();
            var sql = @"SELECT nip, region, pesel, adres FROM klienci WHERE name='" + uzyt + "'";
            using var cmd = new NpgsqlCommand(sql, con);
            string[] res = (string[])cmd.ExecuteScalar();
            nametxt.Text = uzyt;
            niptxt.Text = res[0] + " " + res[1] + " " + res[2];
            adrestxt.Text = res[3];
            datetxt.Text = DateTime.Now.ToString();
        }

        private void valuetxt_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //przycisk otwierajacy form ktory dodaje produkt do paczki, zapomnialem zmienic nazwy przycisku :( 
        {
            var add_product = new Form10();
            add_product.Show();
        }
    }
}
