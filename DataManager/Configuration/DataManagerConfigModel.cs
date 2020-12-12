using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager.Configuration
{
    public class DataManagerConfigModel //Конфигурация DataManager
    {
        public string ConnectionString { get; set; }
        public string StoreProcedure { get; set; }
        public string EntityContactProcedure { get; set; }
        public string PersonNamesProcedure{get;set;}
        public string PersonEmailProcedure { get; set; }
        public string PersonPhoneProcedure { get; set; }
    }
}
