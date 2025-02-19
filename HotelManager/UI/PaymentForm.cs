using System;
using System.Windows.Forms;
using HotelManager.Data.Implementations;
using HotelManager.Domain;
using System.ComponentModel;

namespace HotelManager.UI
{
    public partial class PaymentForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Payment Payment { get; private set; }
        private readonly int _orderId;
        private readonly Payment _existingPayment;

        public PaymentForm(int orderId, Payment existingPayment = null)
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _orderId = orderId;
            _existingPayment = existingPayment;
            LoadPaymentMethods();
            if (_existingPayment != null)
                PopulateFields();
        }

        private void LoadPaymentMethods()
        {
            cmbPaymentMethod.Items.AddRange(new object[] { "Cash", "Credit Card", "Debit Card", "Bank Transfer" });
            cmbPaymentMethod.SelectedIndex = 0;
        }

        private void PopulateFields()
        {
            txtAmount.Text = _existingPayment.Amount.ToString();
            dtpPaymentDate.Value = _existingPayment.PaymentDate;
            cmbPaymentMethod.SelectedItem = _existingPayment.PaymentMethod;
            txtNote.Text = _existingPayment.Note;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtAmount.Text, out double amount))
            {
                MessageBox.Show("Zadejte prosím platnou částku.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Vyberte způsob platby.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("Částka musí být kladná.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Payment = new Payment
            {
                OrderId = _orderId,
                Amount = amount,
                PaymentDate = dtpPaymentDate.Value,
                PaymentMethod = cmbPaymentMethod.SelectedItem.ToString(),
                Note = txtNote.Text
            };

            if (_existingPayment != null)
                Payment.Id = _existingPayment.Id;

            try
            {
                var paymentDao = new PaymentDao();
                if (_existingPayment == null)
                    paymentDao.Insert(Payment);
                else
                    paymentDao.Update(Payment);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při ukládání platby: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}