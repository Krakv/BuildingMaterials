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




        private void tableTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.ShowDialog();
        }

        private void tableTB_Leave(object sender, EventArgs e)
        {
             
        }

        private void tableTB_Validated(object sender, EventArgs e)
        {
            
        }
    }
}
