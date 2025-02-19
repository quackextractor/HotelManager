using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class OrderDao : IOrderDao
{
    private readonly SqlConnection connection;

    public OrderDao()
    {
        connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Order GetById(int id)
    {
        Order order = null;
        var sql = "SELECT * FROM [Order] WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                    order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
            }

            connection.Close();
        }

        if (order != null) order.Persons = LoadPersonsForOrder(order.Id);
        return order;
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = new List<Order>();
        var sql = "SELECT * FROM [Order]";
        using (var cmd = new SqlCommand(sql, connection))
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
                    orders.Add(order);
                }
            }

            connection.Close();
        }

        return orders;
    }

    public void Insert(Order order)
    {
        var sql = @"INSERT INTO [Order] 
                       (price_per_night, nights, order_date, checkin_date, status, paid, room_id) 
                       VALUES (@price_per_night, @nights, @order_date, @checkin_date, @status, @paid, @room_id);
                SELECT SCOPE_IDENTITY();"; // Retrieve the newly generated ID

        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@price_per_night", order.PricePerNight);
            cmd.Parameters.AddWithValue("@nights", order.Nights);
            cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
            cmd.Parameters.AddWithValue("@checkin_date", order.CheckinDate);
            cmd.Parameters.AddWithValue("@status", order.Status);
            cmd.Parameters.AddWithValue("@paid", order.Paid);
            cmd.Parameters.AddWithValue("@room_id", order.RoomId.HasValue ? order.RoomId.Value : DBNull.Value);

            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            var result = cmd.ExecuteScalar();
            if (result != null) order.Id = Convert.ToInt32(result);

            connection.Close();
        }
    }

    public void Update(Order order)
    {
        var sql = @"UPDATE [Order] SET 
                           price_per_night = @price_per_night, 
                           nights = @nights, 
                           order_date = @order_date, 
                           checkin_date = @checkin_date, 
                           status = @status, 
                           paid = @paid, 
                           room_id = @room_id 
                           WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@price_per_night", order.PricePerNight);
            cmd.Parameters.AddWithValue("@nights", order.Nights);
            cmd.Parameters.AddWithValue("@order_date", order.OrderDate);
            cmd.Parameters.AddWithValue("@checkin_date", order.CheckinDate);
            cmd.Parameters.AddWithValue("@status", order.Status);
            cmd.Parameters.AddWithValue("@paid", order.Paid);
            cmd.Parameters.AddWithValue("@room_id", order.RoomId.HasValue ? order.RoomId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@id", order.Id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM [Order] WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public IEnumerable<Order> SearchByOrderNumber(string orderNumber)
    {
        var orders = new List<Order>();
        var sql = "SELECT * FROM [Order] WHERE CONVERT(VARCHAR, id) LIKE @orderNumber";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@orderNumber", "%" + orderNumber + "%");
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
                    orders.Add(order);
                }
            }

            connection.Close();
        }

        return orders;
    }

    public IEnumerable<Order> SearchByPersonName(string personName)
    {
        var orders = new List<Order>();
        var sql = @"SELECT DISTINCT o.*
                   FROM [Order] o 
                   INNER JOIN OrderRole orl ON o.id = orl.order_id
                   INNER JOIN Person p ON orl.person_id = p.id
                   WHERE p.first_name LIKE @personName OR p.last_name LIKE @personName";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@personName", "%" + personName + "%");
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
                    orders.Add(order);
                }
            }

            connection.Close();
        }

        return orders;
    }


    public IEnumerable<Order> SearchByDate(DateTime date)
    {
        var orders = new List<Order>();
        var sql = "SELECT * FROM [Order] WHERE CAST(checkin_date AS DATE) = @date";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@date", date.Date);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
                    orders.Add(order);
                }
            }

            connection.Close();
        }

        return orders;
    }

    private List<Person> LoadPersonsForOrder(int orderId)
    {
        var persons = new List<Person>();
        var sql = @"SELECT p.* 
                   FROM Person p
                   INNER JOIN OrderRole orl ON p.id = orl.person_id
                   WHERE orl.order_id = @orderId";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@orderId", orderId);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var person = new Person
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Status = reader["status"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        LastVisitDate = reader["last_visit_date"] != DBNull.Value
                            ? Convert.ToDateTime(reader["last_visit_date"])
                            : null
                    };
                    persons.Add(person);
                }
            }

            connection.Close();
        }

        return persons;
    }

    public IEnumerable<Order> SearchByRoomNumber(string roomNumber)
    {
        var orders = new List<Order>();
        var sql = @"SELECT o.* 
                   FROM [Order] o 
                   INNER JOIN Room r ON o.room_id = r.id 
                   WHERE r.room_number LIKE @roomNumber";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@roomNumber", "%" + roomNumber + "%");
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var order = new Order
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        PricePerNight = Convert.ToDouble(reader["price_per_night"]),
                        Nights = Convert.ToInt32(reader["nights"]),
                        OrderDate = Convert.ToDateTime(reader["order_date"]),
                        CheckinDate = Convert.ToDateTime(reader["checkin_date"]),
                        Status = reader["status"].ToString(),
                        Paid = Convert.ToBoolean(reader["paid"]),
                        TotalPrice = Convert.ToDouble(reader["total_price"]),
                        RoomId = reader["room_id"] != DBNull.Value ? Convert.ToInt32(reader["room_id"]) : null
                    };
                    orders.Add(order);
                }
            }

            connection.Close();
        }

        return orders;
    }
}