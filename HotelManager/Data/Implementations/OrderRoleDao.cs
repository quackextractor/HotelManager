using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class OrderRoleDao : IOrderRoleDao
{
    private readonly SqlConnection connection;

    public OrderRoleDao()
    {
        connection = SqlConnectionSingleton.Instance.Connection;
    }

    public OrderRole GetById(int id)
    {
        OrderRole orderRole = null;
        var sql = "SELECT * FROM OrderRole WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
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

            connection.Close();
        }

        return orderRole;
    }

    public IEnumerable<OrderRole> GetAll()
    {
        List<OrderRole> roles = new List<OrderRole>();
        var sql = "SELECT * FROM OrderRole";
        using (var cmd = new SqlCommand(sql, connection))
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
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

            connection.Close();
        }

        return roles;
    }

    public void Insert(OrderRole orderRole)
    {
        var sql = "INSERT INTO OrderRole (person_id, order_id, role) VALUES (@person_id, @order_id, @role)";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@person_id", orderRole.PersonId);
            cmd.Parameters.AddWithValue("@order_id", orderRole.OrderId);
            cmd.Parameters.AddWithValue("@role", orderRole.Role);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Update(OrderRole orderRole)
    {
        var sql = "UPDATE OrderRole SET person_id = @person_id, order_id = @order_id, role = @role WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@person_id", orderRole.PersonId);
            cmd.Parameters.AddWithValue("@order_id", orderRole.OrderId);
            cmd.Parameters.AddWithValue("@role", orderRole.Role);
            cmd.Parameters.AddWithValue("@id", orderRole.Id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM OrderRole WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void DeleteByOrderId(int orderId)
    {
        var sql = "DELETE FROM OrderRole WHERE order_id = @orderId";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@orderId", orderId);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}