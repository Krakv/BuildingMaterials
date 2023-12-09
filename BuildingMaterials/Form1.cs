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
                try
                {
                    if (login.Text == "manager")
                    {
                            SqlConnection conn = new SqlConnection("localhost", "building_materials", login.Text, password.Text);
                            Form admin = new Admin(conn, this);
                            admin.Show();
                            this.Visible = false;
                    }
                    else if (login.Text == "customer")
                    {
                        SqlConnection conn = new SqlConnection("localhost", "building_materials", login.Text, password.Text);
                        Customer customer = new Customer(conn, this);
                        customer.Show();
                        this.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла ошибка\n" + ex.Message);
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
