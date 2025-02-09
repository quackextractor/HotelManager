namespace HotelManager.UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem objednavkaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spravaTypuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;

        /// <summary>
        /// Metoda pro inicializaci ovládacích prvků
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.objednavkaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spravaTypuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.objednavkaToolStripMenuItem,
                this.spravaTypuToolStripMenuItem,
                this.loadConfigToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // objednavkaToolStripMenuItem
            // 
            this.objednavkaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.addOrderToolStripMenuItem,
                this.editOrderToolStripMenuItem,
                this.searchOrderToolStripMenuItem});
            this.objednavkaToolStripMenuItem.Name = "objednavkaToolStripMenuItem";
            this.objednavkaToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.objednavkaToolStripMenuItem.Text = "Objednávka";
            // 
            // addOrderToolStripMenuItem
            // 
            this.addOrderToolStripMenuItem.Name = "addOrderToolStripMenuItem";
            this.addOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addOrderToolStripMenuItem.Text = "Přidat objednávku";
            this.addOrderToolStripMenuItem.Click += new System.EventHandler(this.addOrderToolStripMenuItem_Click);
            // 
            // editOrderToolStripMenuItem
            // 
            this.editOrderToolStripMenuItem.Name = "editOrderToolStripMenuItem";
            this.editOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editOrderToolStripMenuItem.Text = "Upravit objednávku";
            this.editOrderToolStripMenuItem.Click += new System.EventHandler(this.editOrderToolStripMenuItem_Click);
            // 
            // searchOrderToolStripMenuItem
            // 
            this.searchOrderToolStripMenuItem.Name = "searchOrderToolStripMenuItem";
            this.searchOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.searchOrderToolStripMenuItem.Text = "Vyhledat objednávku";
            this.searchOrderToolStripMenuItem.Click += new System.EventHandler(this.searchOrderToolStripMenuItem_Click);
            // 
            // spravaTypuToolStripMenuItem
            // 
            this.spravaTypuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.manageTypesToolStripMenuItem});
            this.spravaTypuToolStripMenuItem.Name = "spravaTypuToolStripMenuItem";
            this.spravaTypuToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.spravaTypuToolStripMenuItem.Text = "Správa typů";
            // 
            // manageTypesToolStripMenuItem
            // 
            this.manageTypesToolStripMenuItem.Name = "manageTypesToolStripMenuItem";
            this.manageTypesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manageTypesToolStripMenuItem.Text = "Správa typů";
            this.manageTypesToolStripMenuItem.Click += new System.EventHandler(this.manageTypesToolStripMenuItem_Click);
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.loadConfigToolStripMenuItem.Text = "Načíst konfiguraci";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.loadConfigToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Hotel Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// Uvolnění prostředků.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
