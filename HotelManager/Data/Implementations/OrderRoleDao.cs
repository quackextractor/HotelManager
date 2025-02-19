using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class OrderRoleDao : IOrderRoleDao
{
    private readonly SqlConnection _connection;

    public OrderRoleDao()
    {
        _connection = SqlConnectionSingleton.Instance.Connection;
    }

    public OrderRole GetById(int id)
    {
        OrderRole orderRole = null;
        const string sql = "SELECT * FROM OrderRole WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                    orderRole = new OrderRole
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PersonId = Convert.ToInt32(reader["person_id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        Role = reader["role"].ToString()
                    };
            }

            _connection.Close();
        }

        return orderRole;
    }

    public IEnumerable<OrderRole> GetAll()
    {
        var roles = new List<OrderRole>();
        const string sql = "SELECT * FROM OrderRole";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var orderRole = new OrderRole
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PersonId = Convert.ToInt32(reader["person_id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        Role = reader["role"].ToString()
                    };
                    roles.Add(orderRole);
                }
            }

            _connection.Close();
        }

        return roles;
    }

    public void Insert(OrderRole orderRole)
    {
        const string sql = "INSERT INTO OrderRole (person_id, order_id, role) VALUES (@person_id, @order_id, @role)";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@person_id", orderRole.PersonId);
            cmd.Parameters.AddWithValue("@order_id", orderRole.OrderId);
            cmd.Parameters.AddWithValue("@role", orderRole.Role);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Update(OrderRole orderRole)
    {
        const string sql =
            "UPDATE OrderRole SET person_id = @person_id, order_id = @order_id, role = @role WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@person_id", orderRole.PersonId);
            cmd.Parameters.AddWithValue("@order_id", orderRole.OrderId);
            cmd.Parameters.AddWithValue("@role", orderRole.Role);
            cmd.Parameters.AddWithValue("@id", orderRole.Id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM OrderRole WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void DeleteByOrderId(int orderId)
    {
        const string sql = "DELETE FROM OrderRole WHERE order_id = @orderId";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@orderId", orderId);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public IEnumerable<OrderRole> GetByOrderId(int orderId)
    {
        var roles = new List<OrderRole>();
        const string sql = "SELECT * FROM OrderRole WHERE order_id = @orderId";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@orderId", orderId);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var orderRole = new OrderRole
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PersonId = Convert.ToInt32(reader["person_id"]),
                        OrderId = Convert.ToInt32(reader["order_id"]),
                        Role = reader["role"].ToString()
                    };
                    roles.Add(orderRole);
                }
            }

            _connection.Close();
        }

        return roles;
    }
}