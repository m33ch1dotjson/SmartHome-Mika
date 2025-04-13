using Microsoft.Data.SqlClient;
using SmartHome_Mika.Models;
using System.Reflection.PortableExecutable;

namespace SmartHome_Mika.Models
{
    public class Database
    {
        private readonly string connectionString = "Server=DESKTOP-K01F946\\MSSQLSERVER01;Database=SmartHome;Trusted_Connection=True;TrustServerCertificate=True;";


        public void ChangeIsOn(int id, string DeviceType)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            string tableName = DeviceType;

            string query = $"UPDATE {tableName} SET IsOn = ~IsOn WHERE Id = @id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public SmartDevice GetDeviceById(int id, string deviceType)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            string tableName = deviceType;

            string query = $"SELECT * FROM {tableName} WHERE Id = @id";
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader.GetString(reader.GetOrdinal("Name"));
                bool isOn = reader.GetBoolean(reader.GetOrdinal("IsOn"));
                int room = reader.GetInt32(reader.GetOrdinal("Room"));

                if (deviceType == "SmartLamp")
                {
                    return new SmartLamp(id, (Room)room, name)
                    {
                        IsOn = isOn
                    };
                }
                else if (deviceType == "SmartFridge")
                {
                    int temperature = reader.GetInt32(reader.GetOrdinal("Temperature"));
                    bool freezeMode = reader.GetBoolean(reader.GetOrdinal("FreezeMode"));

                    return new SmartFridge(id, (Room)room, name, temperature)
                    {
                        IsOn = isOn,
                        FreezeMode = freezeMode
                    };
                }
            }

            return null;
        }

        public List<SmartDevice> SearchDevicesByName(string zoekterm)
        {
            List<SmartDevice> results = new();

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string queryLamp = "SELECT * FROM SmartLamp WHERE Name LIKE @term";
            using SqlCommand cmd = new(queryLamp, conn);
            cmd.Parameters.AddWithValue("@term", "%" + zoekterm + "%");

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new SmartLamp(
                    reader.GetInt32(0),
                    (Room)reader.GetInt32(3),
                    reader.GetString(1)
                )
                {
                    IsOn = reader.GetBoolean(2)
                });
            }

            return results;
        }

        public void AddDevice(SmartDevice device, string deviceType)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = "";
            SqlCommand cmd = new();

            if (deviceType == "SmartLamp")
            {
                query = "INSERT INTO SmartLamp (Name, IsOn, Room) VALUES (@name, 0, @room)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", device.Name);
                cmd.Parameters.AddWithValue("@room", (int)device.Room);
            }
            else if (deviceType == "SmartFridge")
            {
                query = "INSERT INTO SmartFridge (Name, IsOn, Room, Temperature, FreezeMode) VALUES (@name, 0, @room, 5, 0)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", device.Name);
                cmd.Parameters.AddWithValue("@room", (int)device.Room);
            }

            cmd.ExecuteNonQuery();
        }


    }
}
