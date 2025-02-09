using System;
using System.IO;
using System.Windows.Forms;

namespace HotelManager.UI
{
    public partial class ConfigLoaderForm : Form
    {
        public ConfigLoaderForm()
        {
            InitializeComponent();
            // Povolení drag & drop
            this.AllowDrop = true;
            this.DragEnter += ConfigLoaderForm_DragEnter;
            this.DragDrop += ConfigLoaderForm_DragDrop;
        }

        private void ConfigLoaderForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void ConfigLoaderForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                txtFilePath.Text = files[0];
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Config files (*.config;*.xml)|*.config;*.xml|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Soubor nebyl nalezen.");
                return;
            }
            try
            {
                // Pro demonstraci jen načteme obsah souboru
                string configContent = File.ReadAllText(filePath);
                // Zde by následovalo zpracování konfigurace (načtení databázových objektů apod.)
                MessageBox.Show("Konfigurace byla úspěšně načtena.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při načítání konfigurace: " + ex.Message);
            }
        }
    }
}
