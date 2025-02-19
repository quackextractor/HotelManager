using System.ComponentModel;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     Represents the form for creating or updating a payment.
/// </summary>
public partial class PaymentForm : Form
{
    private readonly Payment _existingPayment;
    private readonly int _orderId;

    /// <summary>
    ///     Initializes a new instance of the PaymentForm class.
    /// </summary>
    /// <param name="orderId">The ID of the order associated with the payment.</param>
    /// <param name="existingPayment">An existing payment to update, or null to create a new payment.</param>
    public PaymentForm(int orderId, Payment existingPayment = null)
    {
        InitializeComponent();
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        _orderId = orderId;
        _existingPayment = existingPayment;
        LoadPaymentMethods();
        PopulateFields();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    private Payment Payment { get; set; }

    /// <summary>
    ///     Loads available payment methods into the combo box.
    /// </summary>
    private void LoadPaymentMethods()
    {
        cmbPaymentMethod.Items.AddRange("Cash", "Credit Card", "Debit Card", "Bank Transfer");
        cmbPaymentMethod.SelectedIndex = 0;
    }

    /// <summary>
    ///     Populates the form fields with data from an existing payment.
    /// </summary>
    private void PopulateFields()
    {
        txtAmount.Text = _existingPayment.Amount.ToString();
        dtpPaymentDate.Value = _existingPayment.PaymentDate;
        cmbPaymentMethod.SelectedItem = _existingPayment.PaymentMethod;
        txtNote.Text = _existingPayment.Note;
    }

    /// <summary>
    ///     Handles the Cancel button click event. Closes the form without saving.
    /// </summary>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    ///     Handles the Save button click event. Validates input, saves the payment, and closes the form.
    /// </summary>
    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!double.TryParse(txtAmount.Text, out var amount))
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

        Payment.Id = _existingPayment.Id;

        try
        {
            var paymentDao = new PaymentDao();
            paymentDao.Update(Payment);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání platby: " + ex.Message, "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}