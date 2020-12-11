using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Models
{
    public class BusinessEntityContact: IReadableBusinessEntity
    {
        public int BusinessEntityID { get; set; }
        public int PersonID { get; set; }   
        public void GetDataFromDB(string command, SqlConnection connection, object value)
        {
            SqlCommand command1 = new SqlCommand(command, connection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@BusinessEntityID", value);
            if (!(connection.State == ConnectionState.Open)) connection.Open();
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BusinessEntityID = reader.GetInt32(0);
                    PersonID = reader.GetInt32(1);
                }
            }
            reader.Close();
        }
    }
}
