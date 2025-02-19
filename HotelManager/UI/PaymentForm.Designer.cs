namespace HotelManager.UI
{
    partial class PaymentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblPaymentDate;
        private System.Windows.Forms.DateTimePicker dtpPaymentDate;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblAmount = new System.Windows.Forms.Label();
            txtAmount = new System.Windows.Forms.TextBox();
            lblPaymentDate = new System.Windows.Forms.Label();
            dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            lblPaymentMethod = new System.Windows.Forms.Label();
            cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            lblNote = new System.Windows.Forms.Label();
            txtNote = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new System.Drawing.Point(12, 15);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new System.Drawing.Size(45, 15);
            lblAmount.TabIndex = 0;
            lblAmount.Text = "Částka:";
            // 
            // txtAmount
            // 
            txtAmount.Location = new System.Drawing.Point(100, 12);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new System.Drawing.Size(200, 23);
            txtAmount.TabIndex = 1;
            // 
            // lblPaymentDate
            // 
            lblPaymentDate.AutoSize = true;
            lblPaymentDate.Location = new System.Drawing.Point(12, 50);
            lblPaymentDate.Name = "lblPaymentDate";
            lblPaymentDate.Size = new System.Drawing.Size(82, 15);
            lblPaymentDate.TabIndex = 2;
            lblPaymentDate.Text = "Datum platby:";
            // 
            // dtpPaymentDate
            // 
            dtpPaymentDate.Location = new System.Drawing.Point(100, 44);
            dtpPaymentDate.Name = "dtpPaymentDate";
            dtpPaymentDate.Size = new System.Drawing.Size(200, 23);
            dtpPaymentDate.TabIndex = 3;
            // 
            // lblPaymentMethod
            // 
            lblPaymentMethod.AutoSize = true;
            lblPaymentMethod.Location = new System.Drawing.Point(12, 85);
            lblPaymentMethod.Name = "lblPaymentMethod";
            lblPaymentMethod.Size = new System.Drawing.Size(86, 15);
            lblPaymentMethod.TabIndex = 4;
            lblPaymentMethod.Text = "Způsob platby:";
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.FormattingEnabled = true;
            cmbPaymentMethod.Location = new System.Drawing.Point(100, 82);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new System.Drawing.Size(200, 23);
            cmbPaymentMethod.TabIndex = 5;
            // 
            // lblNote
            // 
            lblNote.AutoSize = true;
            lblNote.Location = new System.Drawing.Point(12, 120);
            lblNote.Name = "lblNote";
            lblNote.Size = new System.Drawing.Size(65, 15);
            lblNote.TabIndex = 6;
            lblNote.Text = "Poznámka:";
            // 
            // txtNote
            // 
            txtNote.Location = new System.Drawing.Point(100, 117);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.Size = new System.Drawing.Size(200, 50);
            txtNote.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(100, 180);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(75, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "Uložit";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(225, 180);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Zrušit";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PaymentForm
            // 
            ClientSize = new System.Drawing.Size(320, 215);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtNote);
            Controls.Add(lblNote);
            Controls.Add(cmbPaymentMethod);
            Controls.Add(lblPaymentMethod);
            Controls.Add(dtpPaymentDate);
            Controls.Add(lblPaymentDate);
            Controls.Add(txtAmount);
            Controls.Add(lblAmount);
            Text = "Správa platby";
            ResumeLayout(false);
            PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }
    }
}