﻿using System.Configuration;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace Project
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;


            string dbname = appConfig["RDS_DB_NAME"];
            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];



            return $"Data Source={hostname};Initial Catalog={dbname};User ID={username};Password={password};";
        }
    }
}