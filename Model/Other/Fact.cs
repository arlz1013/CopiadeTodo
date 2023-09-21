using DAO_MVC_Singleton.Model._Dao;
using DAO_MVC_Singleton.Model.Data;
using DAO_MVC_Singleton.Model.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DAO_MVC_Singleton.Model.Other
{
    internal class Fact
    {
        public static IntelUss CrearEmpleadoDAO()
        {
            using (MySqlConnection connection = Conn.Instance.AbrirConexion())
            {
                return new DAO_Sql_US(connection);
            }
        }
    }
}

