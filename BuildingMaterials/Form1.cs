using MySql.Data.MySqlClient;
using System.Net;
using System.Windows.Forms;

namespace BuildingMaterials
{
    public partial class Form1 : Form
    {

        SqlConnection connection;
        MySqlDataReader reader;

        public Form1()
        {
            InitializeComponent();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            if (SqlConnection.CheckForInternetConnection())
            {
                if (login.Text == "root")
                {
                    SqlConnection conn = new SqlConnection("localhost", "building_materials", login.Text, password.Text);
                    Form admin = new Admin(conn, this);
                    admin.Show();
                    this.Visible = false;
                }
                if (login.Text == "customer")
                {
                    SqlConnection conn = new SqlConnection("localhost", "building_materials", login.Text, password.Text);
                    Customer customer = new Customer(conn, this);
                    customer.Show();
                    this.Visible = false;
                }

            }
            else
            {
                MessageBox.Show("No connection");
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {

        }

    }
}
