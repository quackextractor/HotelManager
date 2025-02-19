namespace HotelManager.UI
{
partial class MainWindow
{
/// <summary>
/// Required designer variable.
/// </summary>
private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Panel loadingPanel;
    private System.Windows.Forms.Label loadingLabel;
    private System.Windows.Forms.Panel panelButtons;
    private System.Windows.Forms.Button buttonNewOrder;
    private System.Windows.Forms.Button buttonSearchOrder;
    private System.Windows.Forms.Button buttonLoadTables;
    private System.Windows.Forms.Button buttonExit;

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

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        loadingPanel = new System.Windows.Forms.Panel();
        loadingLabel = new System.Windows.Forms.Label();
        buttonExit = new System.Windows.Forms.Button();
        buttonLoadTables = new System.Windows.Forms.Button();
        buttonSearchOrder = new System.Windows.Forms.Button();
        buttonNewOrder = new System.Windows.Forms.Button();
        panelButtons = new System.Windows.Forms.Panel();
        loadingPanel.SuspendLayout();
        panelButtons.SuspendLayout();
        SuspendLayout();
        // 
        // loadingPanel
        // 
        loadingPanel.BackColor = System.Drawing.Color.White;
        loadingPanel.Controls.Add(loadingLabel);
        loadingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        loadingPanel.Location = new System.Drawing.Point(0, 0);
        loadingPanel.Name = "loadingPanel";
        loadingPanel.Size = new System.Drawing.Size(800, 450);
        loadingPanel.TabIndex = 0;
        // 
        // loadingLabel
        // 
        loadingLabel.AutoSize = true;
        loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        loadingLabel.Location = new System.Drawing.Point(50, 50);
        loadingLabel.Name = "loadingLabel";
        loadingLabel.Size = new System.Drawing.Size(410, 39);
        loadingLabel.TabIndex = 0;
        loadingLabel.Text = "Připojování do databáze...";
        // 
        // buttonExit
        // 
        buttonExit.Font = new System.Drawing.Font("Segoe UI", 20F);
        buttonExit.Location = new System.Drawing.Point(44, 212);
        buttonExit.Name = "buttonExit";
        buttonExit.Size = new System.Drawing.Size(300, 60);
        buttonExit.TabIndex = 3;
        buttonExit.Text = "Konec";
        buttonExit.UseVisualStyleBackColor = true;
        buttonExit.Click += buttonExit_Click;
        // 
        // buttonLoadTables
        // 
        buttonLoadTables.Font = new System.Drawing.Font("Segoe UI", 20F);
        buttonLoadTables.Location = new System.Drawing.Point(44, 146);
        buttonLoadTables.Name = "buttonLoadTables";
        buttonLoadTables.Size = new System.Drawing.Size(300, 60);
        buttonLoadTables.TabIndex = 2;
        buttonLoadTables.Text = "Načíst Tabulky";
        buttonLoadTables.UseVisualStyleBackColor = true;
        buttonLoadTables.Click += buttonLoadTables_Click;
        // 
        // buttonSearchOrder
        // 
        buttonSearchOrder.Font = new System.Drawing.Font("Segoe UI", 20F);
        buttonSearchOrder.Location = new System.Drawing.Point(44, 80);
        buttonSearchOrder.Name = "buttonSearchOrder";
        buttonSearchOrder.Size = new System.Drawing.Size(300, 60);
        buttonSearchOrder.TabIndex = 1;
        buttonSearchOrder.Text = "Vyhledat";
        buttonSearchOrder.UseVisualStyleBackColor = true;
        buttonSearchOrder.Click += buttonSearchOrder_Click;
        // 
        // buttonNewOrder
        // 
        buttonNewOrder.Font = new System.Drawing.Font("Segoe UI", 20F);
        buttonNewOrder.Location = new System.Drawing.Point(44, 14);
        buttonNewOrder.Name = "buttonNewOrder";
        buttonNewOrder.Size = new System.Drawing.Size(300, 60);
        buttonNewOrder.TabIndex = 0;
        buttonNewOrder.Text = "Nová Objednavka";
        buttonNewOrder.UseVisualStyleBackColor = true;
        buttonNewOrder.Click += buttonNewOrder_Click;
        // 
        // panelButtons
        // 
        panelButtons.Controls.Add(buttonNewOrder);
        panelButtons.Controls.Add(buttonSearchOrder);
        panelButtons.Controls.Add(buttonLoadTables);
        panelButtons.Controls.Add(buttonExit);
        panelButtons.Location = new System.Drawing.Point(201, 96);
        panelButtons.Name = "panelButtons";
        panelButtons.Size = new System.Drawing.Size(400, 300);
        panelButtons.TabIndex = 1;
        panelButtons.Visible = false;
        // 
        // MainWindow
        // 
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(panelButtons);
        Controls.Add(loadingPanel);
        Text = "Hotel Manager";
        loadingPanel.ResumeLayout(false);
        loadingPanel.PerformLayout();
        panelButtons.ResumeLayout(false);
        ResumeLayout(false);
    }
}

}