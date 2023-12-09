using MySql.Data.MySqlClient;
using System;
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
    public partial class Admin : Form
    {
        Form1 owner;
        SqlConnection connection;

        public Admin(SqlConnection connection, Form1 owner)
        {
            this.connection = connection;
            this.owner = owner;
            InitializeComponent();
            Reload("good");
        }

        public void Reload(string name)
        {
            Program.InitializeTables(dataGridView1, name, connection);
        }

        private void goodTableMenu_Click(object sender, EventArgs e)
        {
            Reload("good");
        }

        private void goodAvailableTableMenu_Click(object sender, EventArgs e)
        {
            Reload("good_available");
        }

        private void orderTableMenu_Click(object sender, EventArgs e)
        {
            Reload("order_t");
        }

        private void workerTableMenu_Click(object sender, EventArgs e)
        {
            Reload("worker");
        }

        private void storeTableMenu_Click(object sender, EventArgs e)
        {
            Reload("store");
        }

        private void goodSelledTableMenu_Click(object sender, EventArgs e)
        {
            Reload("good_selled");
        }
        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.Visible = true;
            owner.Activate();
        }
    }
}
