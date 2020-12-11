using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace Models
{
    public class Person:IReadablePerson
    {
        public string FirstName{get;set;}
        public string LastName{get;set;}    
        public string EmailAddress { get; set; }    
        public string PhoneNumber { get; set; }
        public void GetPersonNames(string command, SqlConnection connection, int value)
        {
            SqlCommand command1 = new SqlCommand(command, connection);
            command1.Parameters.AddWithValue("@BusinessEntityID", value);
            command1.CommandType = CommandType.StoredProcedure;
            if (!(connection.State == ConnectionState.Open)) connection.Open();
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    if (reader.IsDBNull(0)) FirstName = null;
                    else FirstName = reader.GetString(0);
                    
                    if (reader.IsDBNull(1))LastName = null;
                    else LastName = reader.GetString(1);               
                }
            }
            reader.Close();
        }
        public void GetPersonEmail(string command, SqlConnection connection, int value)
        {
            SqlCommand command1 = new SqlCommand(command, connection);
            command1.Parameters.AddWithValue("@BusinessEntityID", value);
            command1.CommandType = CommandType.StoredProcedure;
            if (!(connection.State == ConnectionState.Open)) connection.Open();
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    if (reader.IsDBNull(0)) EmailAddress = null;
                    else EmailAddress = reader.GetString(0);
                }
            }
            reader.Close();
        }
        public void GetPersonPhone(string command, SqlConnection connection,int value)
        {
            SqlCommand command1 = new SqlCommand(command, connection);
            command1.Parameters.AddWithValue("@BusinessEntityID", value);
            command1.CommandType = CommandType.StoredProcedure;
            if (!(connection.State == ConnectionState.Open)) connection.Open();
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    if (reader.IsDBNull(0)) PhoneNumber = null;
                    else PhoneNumber = reader.GetString(0);
                }
            }
            reader.Close();
        }
    }
}
