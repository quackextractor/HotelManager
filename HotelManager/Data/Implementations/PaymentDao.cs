using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class PaymentDao : IPaymentDao
{
    private readonly SqlConnection _connection;

    public PaymentDao()
    {
        _connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Payment GetById(int id)
    {
        Payment payment = null;
        const string sql = "SELECT * FROM Payment WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
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

            _connection.Close();
        }

        return payment;
    }

    public IEnumerable<Payment> GetAll()
    {
        var payments = new List<Payment>();
        const string sql = "SELECT * FROM Payment";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
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

            _connection.Close();
        }

        return payments;
    }
    
    public IEnumerable<Payment> GetByOrderId(int orderId)
    {
        var payments = new List<Payment>();
        const string sql = "SELECT * FROM Payment WHERE order_id = @order_id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@order_id", orderId);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
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
            _connection.Close();
        }
        return payments;
    }
    
    public void Insert(Payment payment)
    {
        const string sql = @"INSERT INTO Payment 
                           (order_id, amount, payment_date, payment_method, note) 
                           VALUES (@order_id, @amount, @payment_date, @payment_method, @note)";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
            cmd.Parameters.AddWithValue("@amount", payment.Amount);
            cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
            cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);
            cmd.Parameters.AddWithValue("@note", payment.Note ?? (object)DBNull.Value);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Update(Payment payment)
    {
        const string sql = @"UPDATE Payment SET 
                           order_id = @order_id, 
                           amount = @amount, 
                           payment_date = @payment_date, 
                           payment_method = @payment_method, 
                           note = @note 
                           WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@order_id", payment.OrderId);
            cmd.Parameters.AddWithValue("@amount", payment.Amount);
            cmd.Parameters.AddWithValue("@payment_date", payment.PaymentDate);
            cmd.Parameters.AddWithValue("@payment_method", payment.PaymentMethod);
            cmd.Parameters.AddWithValue("@note", payment.Note ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", payment.Id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Payment WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }
}