using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;
using Models;
using DataManager.Configuration;
namespace DataManager
{
    public class DataManager
    {
        public DataManager(DataManagerConfigModel config)
        {
            this.config = config;
        }
        public DataManager(DataManagerConfigurationManager DMCmanager, string path)
        {
            this.config = DMCmanager.LoadConfig(path);
        }


        DataManagerConfigModel config;
        Person PersonToSend { get; set; }
        Store StoreToSend { get; set; }
        BusinessEntityContact BusinessEntityContact { get; set; }


        public void Extract(string request)
        {   
            PersonToSend = new Person();
            StoreToSend = new Store();
            BusinessEntityContact = new BusinessEntityContact();
            int BEID;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(config.ConnectionString))
                    {
                        StoreToSend.GetStoreFromDB(config.StoreProcedure, connection, request);
                        BEID = StoreToSend.BusinessEntityID;
                        BusinessEntityContact.GetDataFromDB(config.EntityContactProcedure, connection, BEID);
                        BEID--;
                        PersonToSend.GetPersonNames(config.PersonNamesProcedure, connection, BEID);
                        PersonToSend.GetPersonEmail(config.PersonEmailProcedure, connection, BEID);
                        PersonToSend.GetPersonPhone(config.PersonPhoneProcedure, connection, BEID);
                        Console.WriteLine(PersonToSend.FirstName);
                        Console.WriteLine(PersonToSend.LastName);
                        Console.WriteLine(PersonToSend.PhoneNumber);
                        Console.WriteLine(PersonToSend.EmailAddress);
                    }

                }
            }
            catch(TransactionAbortedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //DURING TEST!!!
        public void GetSomeShit(string connectString,string commandText)
        {
            try
            {
                using (TransactionScope scope=new TransactionScope())
                {
                    using (SqlConnection connection = new SqlConnection(connectString))
                    {                        
                        connection.Open();                     
                        SqlCommand command1 = new SqlCommand(commandText, connection);
                        command1.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command1.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                object obj1 = reader.GetValue(0);
                                object obj2 = reader.GetValue(1);
                                object obj3 = reader.GetValue(2);
                                Console.WriteLine("{0} \t{1} \t{2}", obj1, obj2, obj3);
                            }                    
                        }
                    }

                }

            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
