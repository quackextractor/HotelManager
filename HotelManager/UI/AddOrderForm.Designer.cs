namespace HotelManager.UI
{
    partial class AddOrderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPricePerNight;
        private System.Windows.Forms.TextBox txtPricePerNight;
        private System.Windows.Forms.Label lblNights;
        private System.Windows.Forms.TextBox txtNights;
        private System.Windows.Forms.Label lblCheckinDate;
        private System.Windows.Forms.DateTimePicker dtpCheckinDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckBox chkPaid;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnRemovePerson;
        private System.Windows.Forms.ListBox lstPersons;
        private System.Windows.Forms.Button btnSaveOrder;

        /// <summary>
        /// Metoda pro inicializaci ovládacích prvků
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPricePerNight = new System.Windows.Forms.Label();
            this.txtPricePerNight = new System.Windows.Forms.TextBox();
            this.lblNights = new System.Windows.Forms.Label();
            this.txtNights = new System.Windows.Forms.TextBox();
            this.lblCheckinDate = new System.Windows.Forms.Label();
            this.dtpCheckinDate = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.chkPaid = new System.Windows.Forms.CheckBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnRemovePerson = new System.Windows.Forms.Button();
            this.lstPersons = new System.Windows.Forms.ListBox();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPricePerNight
            // 
            this.lblPricePerNight.AutoSize = true;
            this.lblPricePerNight.Location = new System.Drawing.Point(12, 15);
            this.lblPricePerNight.Name = "lblPricePerNight";
            this.lblPricePerNight.Size = new System.Drawing.Size(95, 15);
            this.lblPricePerNight.TabIndex = 0;
            this.lblPricePerNight.Text = "Cena za noc:";
            // 
            // txtPricePerNight
            // 
            this.txtPricePerNight.Location = new System.Drawing.Point(120, 12);
            this.txtPricePerNight.Name = "txtPricePerNight";
            this.txtPricePerNight.Size = new System.Drawing.Size(100, 23);
            this.txtPricePerNight.TabIndex = 1;
            // 
            // lblNights
            // 
            this.lblNights.AutoSize = true;
            this.lblNights.Location = new System.Drawing.Point(12, 50);
            this.lblNights.Name = "lblNights";
            this.lblNights.Size = new System.Drawing.Size(37, 15);
            this.lblNights.TabIndex = 2;
            this.lblNights.Text = "Noci:";
            // 
            // txtNights
            // 
            this.txtNights.Location = new System.Drawing.Point(120, 47);
            this.txtNights.Name = "txtNights";
            this.txtNights.Size = new System.Drawing.Size(100, 23);
            this.txtNights.TabIndex = 3;
            // 
            // lblCheckinDate
            // 
            this.lblCheckinDate.AutoSize = true;
            this.lblCheckinDate.Location = new System.Drawing.Point(12, 85);
            this.lblCheckinDate.Name = "lblCheckinDate";
            this.lblCheckinDate.Size = new System.Drawing.Size(83, 15);
            this.lblCheckinDate.TabIndex = 4;
            this.lblCheckinDate.Text = "Datum příjezdu:";
            // 
            // dtpCheckinDate
            // 
            this.dtpCheckinDate.Location = new System.Drawing.Point(120, 79);
            this.dtpCheckinDate.Name = "dtpCheckinDate";
            this.dtpCheckinDate.Size = new System.Drawing.Size(200, 23);
            this.dtpCheckinDate.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 120);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 15);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(120, 117);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(121, 23);
            this.cmbStatus.TabIndex = 7;
            // 
            // chkPaid
            // 
            this.chkPaid.AutoSize = true;
            this.chkPaid.Location = new System.Drawing.Point(12, 155);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Size = new System.Drawing.Size(70, 19);
            this.chkPaid.TabIndex = 8;
            this.chkPaid.Text = "Zaplaceno";
            this.chkPaid.UseVisualStyleBackColor = true;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(12, 190);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(100, 23);
            this.btnAddPerson.TabIndex = 9;
            this.btnAddPerson.Text = "Přidat osobu";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnRemovePerson
            // 
            this.btnRemovePerson.Location = new System.Drawing.Point(120, 190);
            this.btnRemovePerson.Name = "btnRemovePerson";
            this.btnRemovePerson.Size = new System.Drawing.Size(100, 23);
            this.btnRemovePerson.TabIndex = 10;
            this.btnRemovePerson.Text = "Odebrat osobu";
            this.btnRemovePerson.UseVisualStyleBackColor = true;
            this.btnRemovePerson.Click += new System.EventHandler(this.btnRemovePerson_Click);
            // 
            // lstPersons
            // 
            this.lstPersons.FormattingEnabled = true;
            this.lstPersons.ItemHeight = 15;
            this.lstPersons.Location = new System.Drawing.Point(12, 230);
            this.lstPersons.Name = "lstPersons";
            this.lstPersons.Size = new System.Drawing.Size(308, 94);
            this.lstPersons.TabIndex = 11;
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Location = new System.Drawing.Point(12, 340);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(100, 23);
            this.btnSaveOrder.TabIndex = 12;
            this.btnSaveOrder.Text = "Uložit objednávku";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // AddOrderForm
            // 
            this.ClientSize = new System.Drawing.Size(332, 375);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.lstPersons);
            this.Controls.Add(this.btnRemovePerson);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.chkPaid);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpCheckinDate);
            this.Controls.Add(this.lblCheckinDate);
            this.Controls.Add(this.txtNights);
            this.Controls.Add(this.lblNights);
            this.Controls.Add(this.txtPricePerNight);
            this.Controls.Add(this.lblPricePerNight);
            this.Name = "AddOrderForm";
            this.Text = "Přidat objednávku";
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
