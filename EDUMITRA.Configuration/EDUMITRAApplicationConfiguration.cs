﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EDUMITRA.Configuration
{
    public class EDUMITRAApplicationConfiguration
    {
        private static readonly EDUMITRAApplicationConfiguration instance = new EDUMITRAApplicationConfiguration();
        private static IHostingEnvironment _HostingEnvironment = null;
        private static IConfigurationRoot configuration = null;

        private EDUMITRAApplicationConfiguration()
        {
        }

        static EDUMITRAApplicationConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            if (File.Exists(Directory.GetCurrentDirectory() + "\\" + "appsettings.development.json"))
                builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
            else
                builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            configuration = builder.Build();
        }


        public static EDUMITRAApplicationConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        public string FIADB
        {
            get
            {
                return configuration.GetConnectionString("FIADB");
            }
        }
        public string FileDownloadPath
        {
            get
            {
                return configuration["FileDownloadPath"];
            }
        }
        public string AuditingDB
        {
            get
            {
                return configuration.GetConnectionString("AuditingDB");
            }
        }
        public string LoggingDB
        {
            get
            {
                return configuration.GetConnectionString("LoggingDB");
            }
        }
        public string FIAAPIUrl
        {
            get
            {
                return configuration["FIAAPIUrl"];
            }
        }

        public string FIABillBayDB
        {
            get
            {
                return configuration.GetConnectionString("FIABillBayDB");
            }
        }

        public string FileUploadPath
        {
            get
            {
                return configuration["FileUploadPath"];
            }
        }


        public string APIToken
        {
            get
            {
                return configuration["APIToken"];
            }
        }

        public string RblPayoutBaseUrl
        {
            get
            {
                return configuration["RblBankPayout:BaseUrl"];
            }
        }
        public string RblPayoutusername
        {
            get
            {
                return configuration["RblBankPayout:username"];
            }
        }
        public string RblPayoutPass
        {
            get
            {
                return configuration["RblBankPayout:pass"];
            }
        }
        public string RblPayoutclientId
        {
            get
            {
                return configuration["RblBankPayout:client_id"];
            }
        }
        public string RblPayoutclientSecrat
        {
            get
            {
                return configuration["RblBankPayout:client_secret"];
            }
        }
        public string RblPayoutCORPID
        {
            get
            {
                return configuration["RblBankPayout:CORPID"];
            }
        }

        public string certisslpass
        {
            get
            {
                return configuration["CertSslPass"];
            }
        }
        public string certisslName
        {
            get
            {
                return configuration["CertSslName"];
            }
        }

        public string RblAccountNo
        {
            get
            {
                return configuration["RblBankPayout:AccountNo"];
            }
        }
        public string RblAccountName
        {
            get
            {
                return configuration["RblBankPayout:AccountName"];
            }
        }
        public string RblPayoutIfsccode
        {
            get
            {
                return configuration["RblBankPayout:DebitIfsc"];
            }
        }
        public string RblPayoutMobile
        {
            get
            {
                return configuration["RblBankPayout:DebitMobile"];
            }
        }

        private string GetParameterValue(string ParamName)
        {
            if (configuration[ParamName] != null)
                return configuration[ParamName];
            else
                return "";
        }

    }
}
