using Microsoft.AspNetCore.Hosting;
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
        private string GetParameterValue(string ParamName)
        {
            if (configuration[ParamName] != null)
                return configuration[ParamName];
            else
                return "";
        }

    }
}
