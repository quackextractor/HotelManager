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
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.ComboBox cmbRoom;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnRemovePerson;
        private System.Windows.Forms.ListBox lstPersons;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnAddRoom;
        
        // New controls for OrderRole
        private System.Windows.Forms.Label lblOrderRole;
        private System.Windows.Forms.TextBox txtOrderRole;

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
            this.lblRoom = new System.Windows.Forms.Label();
            this.cmbRoom = new System.Windows.Forms.ComboBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnRemovePerson = new System.Windows.Forms.Button();
            this.lstPersons = new System.Windows.Forms.ListBox();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.lblOrderRole = new System.Windows.Forms.Label();
            this.txtOrderRole = new System.Windows.Forms.TextBox();
            
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
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(12, 190);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(91, 15);
            this.lblRoom.TabIndex = 9;
            this.lblRoom.Text = "Číslo místnosti:";
            // 
            // cmbRoom
            // 
            this.cmbRoom.FormattingEnabled = true;
            this.cmbRoom.Location = new System.Drawing.Point(120, 187);
            this.cmbRoom.Name = "cmbRoom";
            this.cmbRoom.Size = new System.Drawing.Size(121, 23);
            this.cmbRoom.TabIndex = 10;
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.Location = new System.Drawing.Point(250, 187);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(75, 23);
            this.btnAddRoom.TabIndex = 15;
            this.btnAddRoom.Text = "Přidat místnost";
            this.btnAddRoom.UseVisualStyleBackColor = true;
            this.btnAddRoom.Click += new System.EventHandler(this.btnAddRoom_Click);
            // 
            // lblOrderRole
            // 
            this.lblOrderRole.AutoSize = true;
            this.lblOrderRole.Location = new System.Drawing.Point(12, 220);
            this.lblOrderRole.Name = "lblOrderRole";
            this.lblOrderRole.Size = new System.Drawing.Size(95, 15);
            this.lblOrderRole.TabIndex = 16;
            this.lblOrderRole.Text = "Role objednávky:";
            // 
            // txtOrderRole
            // 
            this.txtOrderRole.Location = new System.Drawing.Point(120, 217);
            this.txtOrderRole.Name = "txtOrderRole";
            this.txtOrderRole.Size = new System.Drawing.Size(100, 23);
            this.txtOrderRole.TabIndex = 17;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(12, 260);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(100, 23);
            this.btnAddPerson.TabIndex = 18;
            this.btnAddPerson.Text = "Přidat osobu";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnRemovePerson
            // 
            this.btnRemovePerson.Location = new System.Drawing.Point(120, 260);
            this.btnRemovePerson.Name = "btnRemovePerson";
            this.btnRemovePerson.Size = new System.Drawing.Size(100, 23);
            this.btnRemovePerson.TabIndex = 19;
            this.btnRemovePerson.Text = "Odebrat osobu";
            this.btnRemovePerson.UseVisualStyleBackColor = true;
            this.btnRemovePerson.Click += new System.EventHandler(this.btnRemovePerson_Click);
            // 
            // lstPersons
            // 
            this.lstPersons.FormattingEnabled = true;
            this.lstPersons.ItemHeight = 15;
            this.lstPersons.Location = new System.Drawing.Point(12, 295);
            this.lstPersons.Name = "lstPersons";
            this.lstPersons.Size = new System.Drawing.Size(308, 94);
            this.lstPersons.TabIndex = 20;
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Location = new System.Drawing.Point(12, 395);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(100, 23);
            this.btnSaveOrder.TabIndex = 21;
            this.btnSaveOrder.Text = "Uložit objednávku";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // AddOrderForm
            // 
            this.ClientSize = new System.Drawing.Size(332, 430);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.lstPersons);
            this.Controls.Add(this.btnRemovePerson);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.txtOrderRole);
            this.Controls.Add(this.lblOrderRole);
            this.Controls.Add(this.cmbRoom);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.btnAddRoom);
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
