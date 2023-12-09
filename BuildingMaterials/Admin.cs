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
        
        public Admin(Form1 owner)
        {
            this.owner = owner;
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void Admin_Deactivate(object sender, EventArgs e)
        {
            owner.ShowDialog();
            owner.Focus();
        }
    }
}
