namespace HotelManager.UI
{
    partial class TypeManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbTypeCategory;
        private System.Windows.Forms.ListBox lstTypes;
        private System.Windows.Forms.TextBox txtNewType;
        private System.Windows.Forms.Button btnAddType;
        private System.Windows.Forms.Button btnDeleteType;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblNewType;

        private void InitializeComponent()
        {
            this.cmbTypeCategory = new System.Windows.Forms.ComboBox();
            this.lstTypes = new System.Windows.Forms.ListBox();
            this.txtNewType = new System.Windows.Forms.TextBox();
            this.btnAddType = new System.Windows.Forms.Button();
            this.btnDeleteType = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblNewType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbTypeCategory
            // 
            this.cmbTypeCategory.FormattingEnabled = true;
            this.cmbTypeCategory.Location = new System.Drawing.Point(12, 29);
            this.cmbTypeCategory.Name = "cmbTypeCategory";
            this.cmbTypeCategory.Size = new System.Drawing.Size(200, 23);
            this.cmbTypeCategory.TabIndex = 0;
            this.cmbTypeCategory.SelectedIndexChanged += new System.EventHandler(this.cmbTypeCategory_SelectedIndexChanged);
            // 
            // lstTypes
            // 
            this.lstTypes.FormattingEnabled = true;
            this.lstTypes.ItemHeight = 15;
            this.lstTypes.Location = new System.Drawing.Point(12, 70);
            this.lstTypes.Name = "lstTypes";
            this.lstTypes.Size = new System.Drawing.Size(200, 124);
            this.lstTypes.TabIndex = 1;
            // 
            // txtNewType
            // 
            this.txtNewType.Location = new System.Drawing.Point(12, 210);
            this.txtNewType.Name = "txtNewType";
            this.txtNewType.Size = new System.Drawing.Size(200, 23);
            this.txtNewType.TabIndex = 2;
            // 
            // btnAddType
            // 
            this.btnAddType.Location = new System.Drawing.Point(12, 240);
            this.btnAddType.Name = "btnAddType";
            this.btnAddType.Size = new System.Drawing.Size(75, 23);
            this.btnAddType.TabIndex = 3;
            this.btnAddType.Text = "Přidat";
            this.btnAddType.UseVisualStyleBackColor = true;
            this.btnAddType.Click += new System.EventHandler(this.btnAddType_Click);
            // 
            // btnDeleteType
            // 
            this.btnDeleteType.Location = new System.Drawing.Point(137, 240);
            this.btnDeleteType.Name = "btnDeleteType";
            this.btnDeleteType.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteType.TabIndex = 4;
            this.btnDeleteType.Text = "Smazat";
            this.btnDeleteType.UseVisualStyleBackColor = true;
            this.btnDeleteType.Click += new System.EventHandler(this.btnDeleteType_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 9);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(98, 15);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "Kategorie typu:";
            // 
            // lblNewType
            // 
            this.lblNewType.AutoSize = true;
            this.lblNewType.Location = new System.Drawing.Point(12, 190);
            this.lblNewType.Name = "lblNewType";
            this.lblNewType.Size = new System.Drawing.Size(80, 15);
            this.lblNewType.TabIndex = 6;
            this.lblNewType.Text = "Nový typ:";
            // 
            // TypeManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(224, 275);
            this.Controls.Add(this.lblNewType);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.btnDeleteType);
            this.Controls.Add(this.btnAddType);
            this.Controls.Add(this.txtNewType);
            this.Controls.Add(this.lstTypes);
            this.Controls.Add(this.cmbTypeCategory);
            this.Name = "TypeManagementForm";
            this.Text = "Správa typů";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
