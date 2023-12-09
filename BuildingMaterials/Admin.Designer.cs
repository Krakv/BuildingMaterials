namespace BuildingMaterials
{
    partial class Admin
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
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            таблицаToolStripMenuItem = new ToolStripMenuItem();
            goodTableMenu = new ToolStripMenuItem();
            goodAvailableTableMenu = new ToolStripMenuItem();
            orderTableMenu = new ToolStripMenuItem();
            workerTableMenu = new ToolStripMenuItem();
            storeTableMenu = new ToolStripMenuItem();
            goodSelledTableMenu = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 372);
            dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { таблицаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // таблицаToolStripMenuItem
            // 
            таблицаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { goodTableMenu, goodAvailableTableMenu, orderTableMenu, workerTableMenu, storeTableMenu, goodSelledTableMenu });
            таблицаToolStripMenuItem.Name = "таблицаToolStripMenuItem";
            таблицаToolStripMenuItem.Size = new Size(82, 24);
            таблицаToolStripMenuItem.Text = "Таблица";
            // 
            // goodTableMenu
            // 
            goodTableMenu.Name = "goodTableMenu";
            goodTableMenu.Size = new Size(225, 26);
            goodTableMenu.Text = "Товары";
            goodTableMenu.Click += goodTableMenu_Click;
            // 
            // goodAvailableTableMenu
            // 
            goodAvailableTableMenu.Name = "goodAvailableTableMenu";
            goodAvailableTableMenu.Size = new Size(225, 26);
            goodAvailableTableMenu.Text = "Товары в наличии";
            goodAvailableTableMenu.Click += goodAvailableTableMenu_Click;
            // 
            // orderTableMenu
            // 
            orderTableMenu.Name = "orderTableMenu";
            orderTableMenu.Size = new Size(225, 26);
            orderTableMenu.Text = "Заказы";
            orderTableMenu.Click += orderTableMenu_Click;
            // 
            // workerTableMenu
            // 
            workerTableMenu.Name = "workerTableMenu";
            workerTableMenu.Size = new Size(225, 26);
            workerTableMenu.Text = "Сотрудники";
            workerTableMenu.Click += workerTableMenu_Click;
            // 
            // storeTableMenu
            // 
            storeTableMenu.Name = "storeTableMenu";
            storeTableMenu.Size = new Size(225, 26);
            storeTableMenu.Text = "Магазины";
            storeTableMenu.Click += storeTableMenu_Click;
            // 
            // goodSelledTableMenu
            // 
            goodSelledTableMenu.Name = "goodSelledTableMenu";
            goodSelledTableMenu.Size = new Size(225, 26);
            goodSelledTableMenu.Text = "Купленные товары";
            goodSelledTableMenu.Click += goodSelledTableMenu_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Admin";
            Text = "Строительные материалы : администратор";
            FormClosed += Admin_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem таблицаToolStripMenuItem;
        private ToolStripMenuItem goodTableMenu;
        private ToolStripMenuItem goodAvailableTableMenu;
        private ToolStripMenuItem orderTableMenu;
        private ToolStripMenuItem workerTableMenu;
        private ToolStripMenuItem storeTableMenu;
        private ToolStripMenuItem goodSelledTableMenu;
    }
}