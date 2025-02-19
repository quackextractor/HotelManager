namespace HotelManager.UI
{
    partial class SearchOrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbSearchType;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.DateTimePicker dtpSearchDate;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbSearchType = new System.Windows.Forms.ComboBox();
            txtSearch = new System.Windows.Forms.TextBox();
            dtpSearchDate = new System.Windows.Forms.DateTimePicker();
            btnSearch = new System.Windows.Forms.Button();
            dgvOrders = new System.Windows.Forms.DataGridView();
            lblSearchType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // cmbSearchType
            // 
            cmbSearchType.CausesValidation = false;
            cmbSearchType.FormattingEnabled = true;
            cmbSearchType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cmbSearchType.Items.AddRange(new object[] { "Číslo objednávky", "Jméno osoby", "Datum", "Číslo místnosti" });
            cmbSearchType.Location = new System.Drawing.Point(12, 29);
            cmbSearchType.Name = "cmbSearchType";
            cmbSearchType.Size = new System.Drawing.Size(150, 23);
            cmbSearchType.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(180, 29);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(150, 23);
            txtSearch.TabIndex = 1;
            // 
            // dtpSearchDate
            // 
            dtpSearchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dtpSearchDate.Location = new System.Drawing.Point(180, 29);
            dtpSearchDate.Name = "dtpSearchDate";
            dtpSearchDate.Size = new System.Drawing.Size(150, 23);
            dtpSearchDate.TabIndex = 1;
            dtpSearchDate.Visible = false;
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(350, 29);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(75, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Vyhledat";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvOrders
            // 
            dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new System.Drawing.Point(12, 70);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.Size = new System.Drawing.Size(600, 300);
            dgvOrders.TabIndex = 3;
            dgvOrders.CellDoubleClick += dgvOrders_CellDoubleClick;
            // 
            // lblSearchType
            // 
            lblSearchType.AutoSize = true;
            lblSearchType.Location = new System.Drawing.Point(12, 9);
            lblSearchType.Name = "lblSearchType";
            lblSearchType.Size = new System.Drawing.Size(89, 15);
            lblSearchType.TabIndex = 4;
            lblSearchType.Text = "Vyhledat podle:";
            // 
            // SearchOrderForm
            // 
            ClientSize = new System.Drawing.Size(624, 381);
            Controls.Add(lblSearchType);
            Controls.Add(dgvOrders);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(dtpSearchDate);
            Controls.Add(cmbSearchType);
            Text = "Vyhledat objednávku";
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

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
    }
}