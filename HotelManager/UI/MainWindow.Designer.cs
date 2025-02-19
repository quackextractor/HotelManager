namespace HotelManager.UI
{
partial class MainWindow
{
private System.ComponentModel.IContainer components = null;
private System.Windows.Forms.MenuStrip menuStrip1;
private System.Windows.Forms.ToolStripMenuItem objednavkaToolStripMenuItem;
private System.Windows.Forms.ToolStripMenuItem addOrderToolStripMenuItem;
private System.Windows.Forms.ToolStripMenuItem searchOrderToolStripMenuItem;
private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
private Panel loadingPanel;
private Label loadingLabel;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.objednavkaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.addOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.searchOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.loadingPanel = new System.Windows.Forms.Panel();
        this.loadingLabel = new System.Windows.Forms.Label();

        this.menuStrip1.SuspendLayout();
        this.loadingPanel.SuspendLayout();
        this.SuspendLayout();

        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objednavkaToolStripMenuItem,
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
        // searchOrderToolStripMenuItem
        // 
        this.searchOrderToolStripMenuItem.Name = "searchOrderToolStripMenuItem";
        this.searchOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        this.searchOrderToolStripMenuItem.Text = "Vyhledat objednávku";
        this.searchOrderToolStripMenuItem.Click += new System.EventHandler(this.searchOrderToolStripMenuItem_Click);

        // 
        // loadConfigToolStripMenuItem
        // 
        this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
        this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
        this.loadConfigToolStripMenuItem.Text = "Načíst konfiguraci";
        this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);

        // 
        // loadingPanel
        // 
        this.loadingPanel.Controls.Add(this.loadingLabel);
        this.loadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.loadingPanel.BackColor = System.Drawing.Color.White;
        this.loadingPanel.Visible = false;
        this.loadingPanel.Location = new System.Drawing.Point(0, 24);
        this.loadingPanel.Size = new System.Drawing.Size(800, 426);

        // 
        // loadingLabel
        // 
        this.loadingLabel.AutoSize = true;
        this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, 
                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.loadingLabel.Location = new System.Drawing.Point(50, 50);
        this.loadingLabel.Text = "Connecting to database...";

        // 
        // MainWindow
        // 
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.loadingPanel);
        this.Controls.Add(this.menuStrip1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "MainWindow";
        this.Text = "Hotel Manager";

        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.loadingPanel.ResumeLayout(false);
        this.loadingPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}

}