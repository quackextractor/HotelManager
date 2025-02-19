namespace HotelManager.UI
{
    partial class EditOrderForm
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
        private System.Windows.Forms.Label lblOrderRole;
        private System.Windows.Forms.TextBox txtOrderRole;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnRemovePerson;
        private System.Windows.Forms.ListBox lstPersons;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListBox lstPayments;
        private System.Windows.Forms.Button btnAddPayment;
        private System.Windows.Forms.Button btnRemovePayment;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
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
            this.lblOrderRole = new System.Windows.Forms.Label();
            this.txtOrderRole = new System.Windows.Forms.TextBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnRemovePerson = new System.Windows.Forms.Button();
            this.lstPersons = new System.Windows.Forms.ListBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lstPayments = new System.Windows.Forms.ListBox();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.btnRemovePayment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPayments
            // 
            this.lstPayments.FormattingEnabled = true;
            this.lstPayments.ItemHeight = 15;
            this.lstPayments.Location = new System.Drawing.Point(350, 50);
            this.lstPayments.Name = "lstPayments";
            this.lstPayments.Size = new System.Drawing.Size(300, 154);
            this.lstPayments.TabIndex = 18;
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(350, 210);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(100, 23);
            this.btnAddPayment.TabIndex = 19;
            this.btnAddPayment.Text = "Přidat platbu";
            this.btnAddPayment.UseVisualStyleBackColor = true;
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // btnRemovePayment
            // 
            this.btnRemovePayment.Location = new System.Drawing.Point(475, 210);
            this.btnRemovePayment.Name = "btnRemovePayment";
            this.btnRemovePayment.Size = new System.Drawing.Size(100, 23);
            this.btnRemovePayment.TabIndex = 20;
            this.btnRemovePayment.Text = "Odebrat platbu";
            this.btnRemovePayment.UseVisualStyleBackColor = true;
            this.btnRemovePayment.Click += new System.EventHandler(this.btnRemovePayment_Click);
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
            // lblOrderRole
            // 
            this.lblOrderRole.AutoSize = true;
            this.lblOrderRole.Location = new System.Drawing.Point(12, 220);
            this.lblOrderRole.Name = "lblOrderRole";
            this.lblOrderRole.Size = new System.Drawing.Size(95, 15);
            this.lblOrderRole.TabIndex = 11;
            this.lblOrderRole.Text = "Role objednávky:";
            // 
            // txtOrderRole
            // 
            this.txtOrderRole.Location = new System.Drawing.Point(120, 217);
            this.txtOrderRole.Name = "txtOrderRole";
            this.txtOrderRole.Size = new System.Drawing.Size(100, 23);
            this.txtOrderRole.TabIndex = 12;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Location = new System.Drawing.Point(12, 260);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(100, 23);
            this.btnAddPerson.TabIndex = 13;
            this.btnAddPerson.Text = "Přidat osobu";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnRemovePerson
            // 
            this.btnRemovePerson.Location = new System.Drawing.Point(120, 260);
            this.btnRemovePerson.Name = "btnRemovePerson";
            this.btnRemovePerson.Size = new System.Drawing.Size(100, 23);
            this.btnRemovePerson.TabIndex = 14;
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
            this.lstPersons.TabIndex = 15;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(12, 395);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(100, 23);
            this.btnSaveChanges.TabIndex = 16;
            this.btnSaveChanges.Text = "Uložit změny";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(220, 395);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Smazat objednávku";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // EditOrderForm
            // 
            this.ClientSize = new System.Drawing.Size(670, 430);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.lstPersons);
            this.Controls.Add(this.btnRemovePerson);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.txtOrderRole);
            this.Controls.Add(this.lblOrderRole);
            this.Controls.Add(this.cmbRoom);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.chkPaid);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtpCheckinDate);
            this.Controls.Add(this.lblCheckinDate);
            this.Controls.Add(this.txtNights);
            this.Controls.Add(this.lblNights);
            this.Controls.Add(this.txtPricePerNight);
            this.Controls.Add(this.lblPricePerNight);
            this.Controls.Add(this.btnRemovePayment);
            this.Controls.Add(this.btnAddPayment);
            this.Controls.Add(this.lstPayments);
            this.Name = "EditOrderForm";
            this.Text = "Upravit objednávku";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}
