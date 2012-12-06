﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace GrouponDesktop.DataAccess
{
    public class DBManager
    {
        private SqlConnection connection;
        public SqlConnection Connection
        {
            
            get { return connection; }
        }

        public DBManager()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch
            {
                throw new Exception("Error al querer abrir la DB");
            }
        }
        public void closeConnection()
        {
            connection.Close();
        }

        String connectionString = AdministradorConfiguracion.obtenerValorDB("dataSource");
    }

 


}
