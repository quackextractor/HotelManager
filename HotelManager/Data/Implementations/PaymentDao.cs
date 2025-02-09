using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class PaymentDao : IPaymentDao
{
    private readonly SqlConnection connection;

    public PaymentDao()
    {
        connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Payment GetById(int id)
    {
        Payment payment = null;
        var sql = "SELECT * FROM Payment WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                    payment = new Payment
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        Amount = Convert.ToDouble(reader["amount"]),
                        PaymentDate = Convert.ToDateTime(reader["payment_date"]),
                        PaymentMethod = reader["payment_method"].ToString(),
                        Note = reader["note"].ToString()
                    };
            }

            connection.Close();
        }

        return payment;
    }

    public IEnumerable<Payment> GetAll()
    {
        List<Payment> payments = new List<Payment>();
        var sql = "SELECT * FROM Payment";
        using (var cmd = new SqlCommand(sql, connection))
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var payment = new Payment
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        Amount = Convert.ToDouble(reader["amount"]),
                        PaymentDate = Convert.ToDateTime(reader["payment_date"]),
                        PaymentMethod = reader["payment_method"].ToString(),
                        Note = reader["note"].ToString()
                    };
                    payments.Add(payment);
                }
            }

            connection.Close();
        }

        return payments;
    }

    public void Insert(Payment payment)
    {
        var sql = @"INSERT INTO Payment 
                           (order_id, amount, payment_date, payment_method, note) 
                           VALUES (@order_id, @amount, @payment_date, @payment_method, @note)";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
            cmd.Parameters.AddWithValue("@amount", payment.Amount);
            cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
            cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);
            cmd.Parameters.AddWithValue("@note", payment.Note ?? (object)DBNull.Value);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Update(Payment payment)
    {
        var sql = @"UPDATE Payment SET 
                           order_id = @order_id, 
                           amount = @amount, 
                           payment_date = @payment_date, 
                           payment_method = @payment_method, 
                           note = @note 
                           WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
            cmd.Parameters.AddWithValue("@amount", payment.Amount);
            cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
            cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);
            cmd.Parameters.AddWithValue("@note", payment.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", payment.Id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM Payment WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}