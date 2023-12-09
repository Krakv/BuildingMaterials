namespace BuildingMaterials
{
    partial class Customer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            MenuStore = new ToolStripMenuItem();
            store1 = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            good_name = new DataGridViewTextBoxColumn();
            Field_Of_Application = new DataGridViewTextBoxColumn();
            Packaging = new DataGridViewTextBoxColumn();
            Technical_Characteristics = new DataGridViewTextBoxColumn();
            Instructions_For_Use = new DataGridViewTextBoxColumn();
            Precautions = new DataGridViewTextBoxColumn();
            Storage_And_Transportation = new DataGridViewTextBoxColumn();
            Certificates = new DataGridViewTextBoxColumn();
            Manufacturer_Information = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { MenuStore });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1035, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // MenuStore
            // 
            MenuStore.DropDownItems.AddRange(new ToolStripItem[] { store1 });
            MenuStore.Name = "MenuStore";
            MenuStore.Size = new Size(83, 24);
            MenuStore.Text = "Магазин";
            // 
            // store1
            // 
            store1.Name = "store1";
            store1.Size = new Size(164, 26);
            store1.Text = "Магазин 1";
            store1.Click += store1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { good_name, Field_Of_Application, Packaging, Technical_Characteristics, Instructions_For_Use, Precautions, Storage_And_Transportation, Certificates, Manufacturer_Information });
            dataGridView1.Location = new Point(0, 43);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1035, 496);
            dataGridView1.TabIndex = 2;
            // 
            // good_name
            // 
            good_name.HeaderText = "Название товара";
            good_name.MinimumWidth = 6;
            good_name.Name = "good_name";
            good_name.ReadOnly = true;
            good_name.Width = 125;
            // 
            // Field_Of_Application
            // 
            Field_Of_Application.HeaderText = "Область применения";
            Field_Of_Application.MinimumWidth = 6;
            Field_Of_Application.Name = "Field_Of_Application";
            Field_Of_Application.ReadOnly = true;
            Field_Of_Application.Width = 125;
            // 
            // Packaging
            // 
            Packaging.HeaderText = "Упаковка";
            Packaging.MinimumWidth = 6;
            Packaging.Name = "Packaging";
            Packaging.ReadOnly = true;
            Packaging.Width = 125;
            // 
            // Technical_Characteristics
            // 
            Technical_Characteristics.HeaderText = "Технические характеристики";
            Technical_Characteristics.MinimumWidth = 6;
            Technical_Characteristics.Name = "Technical_Characteristics";
            Technical_Characteristics.ReadOnly = true;
            Technical_Characteristics.Width = 125;
            // 
            // Instructions_For_Use
            // 
            Instructions_For_Use.HeaderText = "Инструкция по применению";
            Instructions_For_Use.MinimumWidth = 6;
            Instructions_For_Use.Name = "Instructions_For_Use";
            Instructions_For_Use.ReadOnly = true;
            Instructions_For_Use.Width = 125;
            // 
            // Precautions
            // 
            Precautions.HeaderText = "Меры предосторожности";
            Precautions.MinimumWidth = 6;
            Precautions.Name = "Precautions";
            Precautions.ReadOnly = true;
            Precautions.Width = 125;
            // 
            // Storage_And_Transportation
            // 
            Storage_And_Transportation.HeaderText = "Хранение и транспортировка";
            Storage_And_Transportation.MinimumWidth = 6;
            Storage_And_Transportation.Name = "Storage_And_Transportation";
            Storage_And_Transportation.ReadOnly = true;
            Storage_And_Transportation.Width = 125;
            // 
            // Certificates
            // 
            Certificates.HeaderText = "Сертификаты";
            Certificates.MinimumWidth = 6;
            Certificates.Name = "Certificates";
            Certificates.ReadOnly = true;
            Certificates.Width = 125;
            // 
            // Manufacturer_Information
            // 
            Manufacturer_Information.HeaderText = "Сведения о производителе";
            Manufacturer_Information.MinimumWidth = 6;
            Manufacturer_Information.Name = "Manufacturer_Information";
            Manufacturer_Information.ReadOnly = true;
            Manufacturer_Information.Width = 125;
            // 
            // Customer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 539);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Customer";
            Text = "Строительные материалы";
            Deactivate += Customer_Deactivate;
            FormClosed += Customer_FormClosed;
            Load += Customer_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem MenuStore;
        private ToolStripMenuItem store1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn good_name;
        private DataGridViewTextBoxColumn Field_Of_Application;
        private DataGridViewTextBoxColumn Packaging;
        private DataGridViewTextBoxColumn Technical_Characteristics;
        private DataGridViewTextBoxColumn Instructions_For_Use;
        private DataGridViewTextBoxColumn Precautions;
        private DataGridViewTextBoxColumn Storage_And_Transportation;
        private DataGridViewTextBoxColumn Certificates;
        private DataGridViewTextBoxColumn Manufacturer_Information;
    }
}