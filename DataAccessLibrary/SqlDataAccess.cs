using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


namespace DataAccessLibrary
{
    public class SqlDataAccess
    {
        public SqlDataAccess()
        {
            
        }

        void Connect()
        {
            var conStr = "";
            using (var connection = new SqlConnection(conStr))
            {
                var sql = "";
                var customers = connection.Query<BlazorApp1.Flight>(sql)
            }
        }
    }

    
}