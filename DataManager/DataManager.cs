using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;
using Models;
using DataManager.Configuration;
using XmlGen;
using FileManager;

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
        ToFileModel result = new ToFileModel();

        string xmlFileName = "file.xml";


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
                        result._Person = PersonToSend;
                        result._Store = StoreToSend;
                    }

                }
            }
            catch(TransactionAbortedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void GenerateXml(string path,ToFileModel toFileModel)
        {
            try
            {             
                XmlGenerator.ConvertToXml(path, toFileModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Converting to XML failed. {ex.Message}");
            }
        }
        public void GenerateXml()
        {          
            try
            {
                xmlFileName = $"{DateTime.Now.Ticks}.xml";
                XmlGenerator.ConvertToXml(xmlFileName, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Converting to XML failed. {ex.Message}");
            }
        }
        public void TransferFile(string destination)
        {
           
            try
            {
                destination = Path.Join(destination, xmlFileName);
                FileTransfer.Transfer(xmlFileName, destination);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transfering file failed. {ex.Message}");
            }
        }     
    }
}
