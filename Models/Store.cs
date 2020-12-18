using DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Models
{
    public class Store : IReadableStore
    {
        public string Name { get; set; }
        public int BusinessEntityID { get; set; }

        public void GetStoreFromDB(string command, SqlConnection connection, object value)
        {
            SqlCommand command1 = new SqlCommand(command, connection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@Name", value);
            if(!(connection.State==ConnectionState.Open))connection.Open();
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    if (reader.IsDBNull(0)) Name = null;
                    else Name = reader.GetString(0);
                    BusinessEntityID = reader.GetInt32(1);
                }
            }
            reader.Close();
        }
        public async Task GetStoreFromDBAsync(string command, SqlConnection connection, object value)
        {
            await Task.Run(() =>
            {
                SqlCommand command1 = new SqlCommand(command, connection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@Name", value);
                if (!(connection.State == ConnectionState.Open)) connection.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(0)) Name = null;
                        else Name = reader.GetString(0);
                        BusinessEntityID = reader.GetInt32(1);
                    }
                }
                reader.Close();
            });
        }
    }
}
