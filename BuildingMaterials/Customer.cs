using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuildingMaterials
{
    public partial class Customer : Form
    {
        SqlConnection connection;
        MySqlDataReader reader;
        Form1 owner;

        public Customer(SqlConnection connection, Form1 owner)
        {
            this.owner = owner;
            this.connection = connection;
            string temp = "select good_available.good_id from good_available where good_available.store_id = 1";
            reader = connection.data("select good.good_name, " +
                "good.Field_Of_Application, " +
                "good.Packaging, " +
                "good.Technical_Characteristics, " +
                "good.Instructions_For_Use, " +
                "good.Precautions, " +
                "good.Storage_And_Transportation, " +
                "good.Certificates, " +
                "good.Manufacturer_information " +
                "from good, good_available where good.good_id = good_available.good_id AND good_available.store_id = 1");
            InitializeComponent();
            Reload();
        }


        private void store1_Click(object sender, EventArgs e)
        {
            reader = connection.data("select * from goods where good.store_id = 1");
        }

        public void Reload()
        {
            dataGridView1.Rows.Clear();

            while (reader.Read()) // запись выгруженных данных в dataGridView 
            {
                dataGridView1.Rows.Add(reader.GetString("good_name").ToString(),
                    reader.GetString("Field_Of_Application").ToString(),
                    reader.GetString("Packaging").ToString(),
                    reader.GetString("Technical_Characteristics").ToString(),
                    reader.GetString("Instructions_For_Use").ToString(),
                    reader.GetString("Precautions").ToString(),
                    reader.GetString("Storage_And_Transportation").ToString(),
                    reader.GetString("Certificates").ToString(),
                    reader.GetString("Manufacturer_information").ToString()
                    );
            }
        }

        private void Customer_Deactivate(object sender, EventArgs e)
        {
            owner.ShowDialog();
            owner.Focus();
        }

        private void Customer_Load(object sender, EventArgs e)
        {

        }
    }
}
