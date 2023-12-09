using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
            reader = connection.data("select good.good_name AS \"Наименование товара\", " +
                "good.brc_code AS \"Код КСР\", " +
                "good.Field_Of_Application AS \"Область применения\", " +
                "good.Packaging AS \"Упаковка\", " +
                "good.Technical_Characteristics AS \"Технические характеристики\", " +
                "good.Instructions_For_Use AS \"Инструкция по применению\", " +
                "good.Precautions AS \"Предостережения\", " +
                "good.Storage_And_Transportation AS \"Хранение и транспортировка\", " +
                "good.Certificates AS \"Сертификаты\", " +
                "good.Manufacturer_information AS \"Сведения о производителе\"" +
                "from good, good_available where good.good_id = good_available.good_id AND good_available.store_id = 1");
            InitializeComponent();
            Reload();
        }


        private void store1_Click(object sender, EventArgs e)
        {
            reader = connection.data("select good.good_name AS \"Наименование товара\", " +
                "good.brc_code AS \"Код КСР\", " +
                "good.Field_Of_Application AS \"Область применения\", " +
                "good.Packaging AS \"Упаковка\", " +
                "good.Technical_Characteristics AS \"Технические характеристики\", " +
                "good.Instructions_For_Use AS \"Инструкция по применению\", " +
                "good.Precautions AS \"Предостережения\", " +
                "good.Storage_And_Transportation AS \"Хранение и транспортировка\", " +
                "good.Certificates AS \"Сертификаты\", " +
                "good.Manufacturer_information AS \"Сведения о производителе\"" +
                "from good, good_available where good.good_id = good_available.good_id AND good_available.store_id = 1");
        }

        public void Reload()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataGridView1.Columns.Add(columnName: reader.GetName(i), headerText: reader.GetName(i));
            }
            while (reader.Read())
            {
                string[] arr = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    arr[i] = reader.GetString(reader.GetName(i)).ToString();
                }
                dataGridView1.Rows.Add(arr);
            }
            reader.Close();
        }

        private void Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.Visible = true;
            owner.Activate();
        }
    }
}
