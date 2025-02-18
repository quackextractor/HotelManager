using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class RoomDao : IRoomDao
{
    private readonly SqlConnection connection;

    public RoomDao()
    {
        connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Room GetById(int id)
    {
        Room room = null;
        var sql = "SELECT * FROM Room WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                    room = new Room
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        RoomNumber = reader["room_number"].ToString(),
                        RoomType = reader["room_type"].ToString(),
                        Capacity = Convert.ToInt32(reader["capacity"]),
                        Price = Convert.ToDouble(reader["price"])
                    };
            }

            connection.Close();
        }

        return room;
    }

    public IEnumerable<Room> GetAll()
    {
        List<Room> rooms = new List<Room>();
        var sql = "SELECT * FROM Room";
        using (var cmd = new SqlCommand(sql, connection))
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var room = new Room
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        RoomNumber = reader["room_number"].ToString(),
                        RoomType = reader["room_type"].ToString(),
                        Capacity = Convert.ToInt32(reader["capacity"]),
                        Price = Convert.ToDouble(reader["price"])
                    };
                    rooms.Add(room);
                }
            }

            connection.Close();
        }

        return rooms;
    }

    public void Insert(Room room)
    {
        var sql = @"INSERT INTO Room (room_number, room_type, capacity, price) 
                VALUES (@room_number, @room_type, @capacity, @price);
                SELECT SCOPE_IDENTITY();";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
            cmd.Parameters.AddWithValue("@room_type", room.RoomType);
            cmd.Parameters.AddWithValue("@capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@price", room.Price);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                room.Id = Convert.ToInt32(result);
            }
            connection.Close();
        }
    }

    public void Update(Room room)
    {
        var sql =
            "UPDATE Room SET room_number = @room_number, room_type = @room_type, capacity = @capacity, price = @price WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
            cmd.Parameters.AddWithValue("@room_type", room.RoomType);
            cmd.Parameters.AddWithValue("@capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@price", room.Price);
            cmd.Parameters.AddWithValue("@id", room.Id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM Room WHERE id = @id";
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