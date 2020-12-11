using System;
using System.Data.SqlClient;

namespace DataAccess
{
    public interface IReadableStore
    {
        void GetStoreFromDB(string command, SqlConnection connection, object value);
    }
    public interface IReadablePerson
    {
         void GetPersonNames(string command, SqlConnection connection, int value);
        void GetPersonEmail(string command, SqlConnection connection, int value);
        void GetPersonPhone(string command, SqlConnection connection, int value);     
    }
    public interface IReadableBusinessEntity
    {
        void GetDataFromDB(string command, SqlConnection connection, object value);
    }
    
    
}
