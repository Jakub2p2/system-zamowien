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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Magazyn
{
    public partial class product_form : Form
    {
        public string connect_string = "";
        string strConnDtb = "Server=pg-26a19d25-paczkimagazyn.h.aivencloud.com; port=13890; user id=avnadmin; password=AVNS_3UbLex9BxU_ZYRZvxaY; database=paczuszki; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        public string addORedit = "";
        public int edit_id;
        string table = "";
        private Form1 main_form;
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
        public product_form(bool edit, string[] datas, int p_id, Form1 main_form)
        {
            InitializeComponent();
            if (edit)
            {
                name_txt.Text = datas[0];
                cecha_txt.Text = datas[1];
                price_txt.Text = datas[2];
                weight_txt.Text = datas[3];
                count_txt.Text = datas[4];
                edit_id = p_id;
                addORedit = "edit";
                this.Text = "Edytuj Produkt";
            }
            else
            {
                addORedit = "add";
                this.Text = "Dodaj Produkt";
            }

            this.main_form = main_form;

        }
        public bool check_Empty() // sprawdzianie czy pola sa puste (nie dziala ze spacja)
        {
            if (name_txt.TextLength == 0 || cecha_txt.TextLength == 0 || weight_txt.TextLength == 0 || count_txt == null || price_txt.TextLength == 0)
                return true;
            else return false;
        }
        private void product_form_Load(object sender, EventArgs e)
        {
        }

        private void quit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            if (check_Empty()) MessageBox.Show("Nie wszystkie pola są uzupełnione");
            else // jesli wszystkie pola sa zapelnione
            {
                string name = name_txt.Text;
                string cecha = cecha_txt.Text;
                int price = Convert.ToInt32(price_txt.Text);
                int weight = Convert.ToInt32(weight_txt.Text);
                int count = Convert.ToInt32(count_txt.Value);
                Connect_db();
                using (NpgsqlConnection connection = new NpgsqlConnection(connect_string))
                {
                    connection.Open();
                    string query = "";
                    if (addORedit == "add")
                        query = "INSERT INTO produkty(nazwa, cechy, cena, waga, ilosc) VALUES('" + name + "','" + cecha
                        + "'," + price + "," + weight + "," + count + ")";
                    else if (addORedit == "edit")
                        query = "UPDATE produkty SET nazwa = @nazwa, cechy = @cechy, waga = @waga, ilosc = @ilosc WHERE id = @id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection)) // wysyłanie danych
                    {
                        if(addORedit == "edit") command.Parameters.AddWithValue("@id", edit_id);
                        command.Parameters.AddWithValue("@nazwa", name);
                        command.Parameters.AddWithValue("@cechy", cecha);
                        command.Parameters.AddWithValue("@cena", price);
                        command.Parameters.AddWithValue("@waga", weight);
                        command.Parameters.AddWithValue("@ilosc", count);
                        int rowsAffected = command.ExecuteNonQuery(); // wysłanie danych (nie działa)
                        if(addORedit == "add") MessageBox.Show("Dodano nowy produkt!!!");
                        if (addORedit == "edit") MessageBox.Show("Zmieniono produkt!!!");

                        this.Close();
                        main_form.show_table();
                        main_form.create_btn("produkty");
                    }
                }
            }
        }
    }
}
