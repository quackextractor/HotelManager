using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class RoomDao : IRoomDao
{
    private readonly SqlConnection _connection;

    public RoomDao()
    {
        _connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Room GetById(int id)
    {
        Room room = null;
        const string sql = "SELECT * FROM Room WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
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

            _connection.Close();
        }

        return room;
    }

    public IEnumerable<Room> GetAll()
    {
        var rooms = new List<Room>();
        const string sql = "SELECT * FROM Room";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
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

            _connection.Close();
        }

        return rooms;
    }

    public void Insert(Room room)
    {
        const string sql = @"INSERT INTO Room (room_number, room_type, capacity, price) 
                VALUES (@room_number, @room_type, @capacity, @price);
                SELECT SCOPE_IDENTITY();";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
            cmd.Parameters.AddWithValue("@room_type", room.RoomType);
            cmd.Parameters.AddWithValue("@capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@price", room.Price);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) room.Id = Convert.ToInt32(result);
            _connection.Close();
        }
    }

    public void Update(Room room)
    {
        const string sql =
            "UPDATE Room SET room_number = @room_number, room_type = @room_type, capacity = @capacity, price = @price WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
            cmd.Parameters.AddWithValue("@room_type", room.RoomType);
            cmd.Parameters.AddWithValue("@capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@price", room.Price);
            cmd.Parameters.AddWithValue("@id", room.Id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Room WHERE id = @id";
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