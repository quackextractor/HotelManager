using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HotelManager.UI
{
    public partial class TypeManagementForm : Form
    {
        // Pro demonstraci používáme slovník, kde pro každou kategorii uchováváme seznam typů.
        private Dictionary<string, List<string>> typeData;

        public TypeManagementForm()
        {
            InitializeComponent();
            LoadTypeCategories();
            LoadTypeData();
        }

        private void LoadTypeCategories()
        {
            cmbTypeCategory.Items.Clear();
            cmbTypeCategory.Items.Add("RoleVObjednavce");
            cmbTypeCategory.Items.Add("StavObjednavky");
            cmbTypeCategory.Items.Add("StavOsoby");
            cmbTypeCategory.Items.Add("TypRole");
            cmbTypeCategory.Items.Add("ZpusobPlatby");
            if (cmbTypeCategory.Items.Count > 0)
                cmbTypeCategory.SelectedIndex = 0;
        }

        private void LoadTypeData()
        {
            // Dummy data – v reálné aplikaci by se načítala z databáze
            typeData = new Dictionary<string, List<string>>();
            typeData["RoleVObjednavce"] = new List<string> { "customer", "guest" };
            typeData["StavObjednavky"] = new List<string> { "pending", "confirmed" };
            typeData["StavOsoby"] = new List<string> { "active", "inactive" };
            typeData["TypRole"] = new List<string> { "admin", "user" };
            typeData["ZpusobPlatby"] = new List<string> { "credit card", "cash" };

            LoadTypesForSelectedCategory();
        }

        private void LoadTypesForSelectedCategory()
        {
            string selectedCategory = cmbTypeCategory.SelectedItem.ToString();
            lstTypes.Items.Clear();
            if (typeData.ContainsKey(selectedCategory))
            {
                foreach (var type in typeData[selectedCategory])
                {
                    lstTypes.Items.Add(type);
                }
            }
        }

        private void cmbTypeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypesForSelectedCategory();
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            string newType = txtNewType.Text.Trim();
            if (string.IsNullOrEmpty(newType))
            {
                MessageBox.Show("Zadejte hodnotu typu.");
                return;
            }
            string selectedCategory = cmbTypeCategory.SelectedItem.ToString();
            if (!typeData[selectedCategory].Contains(newType))
            {
                typeData[selectedCategory].Add(newType);
                LoadTypesForSelectedCategory();
                txtNewType.Clear();
            }
            else
            {
                MessageBox.Show("Tento typ již existuje.");
            }
        }

        private void btnDeleteType_Click(object sender, EventArgs e)
        {
            if (lstTypes.SelectedItem == null)
            {
                MessageBox.Show("Vyberte typ ke smazání.");
                return;
            }
            string selectedType = lstTypes.SelectedItem.ToString();
            string selectedCategory = cmbTypeCategory.SelectedItem.ToString();
            typeData[selectedCategory].Remove(selectedType);
            LoadTypesForSelectedCategory();
        }
    }
}
