using EDUMITRA.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDUMITRA.Database
{
    public class EDUMITRADatabase : BaseDatabase, IEDUMITRADatabase

    {
        private string _connectionString;
        public override string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        _connectionString = EDUMITRAApplicationConfiguration.Instance.FIADB;
                    }
                    catch
                    {
                        throw;
                    }
                }
                return _connectionString;
            }
        }
    }
}
