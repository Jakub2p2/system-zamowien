using Npgsql;
using System.Configuration;
using System.Data;
namespace Magazyn{
    public partial class Form1 : Form
    {
        string strConnDtb = "Server=185.157.80.106; port=5432; user id=postgres; password=123; database=Uzytkownicy; ";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        string table = "";
        private void Connect_db() // Polaczenie z baza danych
        {
            try
            {
                vCon = new NpgsqlConnection();
                vCon.ConnectionString = strConnDtb;
                if (vCon.State == System.Data.ConnectionState.Closed) vCon.Open();
            }
            catch
            { // w razie braku po³¹czenia z baz¹
                MessageBox.Show("B³¹d po³¹czenia z baz¹ danych", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public DataTable getData(string sql) // pobieranie danych z bazy
        {
            DataTable dt = new DataTable();
            Connect_db();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = sql;
            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public void show_table() // funkcja pokazujaca tabelê
        {
            try
            {
                DataTable dtgetdata = new DataTable();
                dtgetdata = getData("SELECT * FROM " + table + ";");
                tabela.DataSource = dtgetdata;
            }
            catch
            {
                MessageBox.Show("B³ad po³¹czenia z tabel¹", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursor.Current;
        }
        private void Form1_Load(object sender, EventArgs e) // przy za³adowaniu aplikacji
        {
            Connect_db();
            // var login = new Form_Login();
            // login.ShowDialog();
        }
        /// ZDARZENIA PRZYCISKÓW
        private void button_users_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "uzytkownicy";
            show_table();
        }
        private void button_client_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "klienci";
            show_table();
        }
        private void button_products_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "produkty";
            show_table();
        }
        private void button_delivery_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "dostawy";
            show_table();
        }
        private void button_package_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            table = "paczki";
            show_table();
        }

        private void addusr_btn_Click(object sender, EventArgs e) // otwiera formularz z tworzeniem nowego uzytkownika
        {
            var add_usr_form = new add_user();
            add_usr_form.Show();
        }

        private void addelivery_btn_Click(object sender, EventArgs e) //otwiera formularz z tworzeniem nowego sposobu dostawu
        {
            var addelivery_form = new delivery_form();
            addelivery_form.Show();
        }

        private void addpackage_Click(object sender, EventArgs e)
        {
            var addpackage_form = new package_form();
            addpackage_form.Show();
        }

        private void addproduct_btn_Click(object sender, EventArgs e)
        {
            var addproduct_form = new product_form();
            addproduct_form.Show();
        }
    }
}
