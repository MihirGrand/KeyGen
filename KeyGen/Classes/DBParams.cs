using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyGen.Classes
{
    public static class DBParams
    {
        static String ConnString { get; set; }
        static String DBName { get; set; }
        static String CollName { get; set; }

        public static void SetParams(String connString, String dbName, String collName)
        {
            ConnString = connString;
            DBName = dbName;
            CollName = collName;
        }

        public static String GetConnString()
        {
            return ConnString;
        }

        public static String GetDBName()
        {
            return DBName;
        }

        public static String GetCollName()
        {
            return CollName;
        }
    }
}
