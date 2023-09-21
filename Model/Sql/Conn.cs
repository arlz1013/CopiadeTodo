using System;
using MySql.Data.MySqlClient;


namespace DAO_MVC_Singleton.Model.Sql
{
    public sealed class Conn : IDisposable
    {
        private static readonly string ConnectionString = "server=127.0.0.1;user=test;password=;database=testing;";
        private MySqlConnection _conn;

        private static Conn _instance = new Conn();
        public static Conn Instance => _instance;

        private bool _disposed = false;

        private Conn()
        {
            _conn = new MySqlConnection(ConnectionString);
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (_conn.State != System.Data.ConnectionState.Open)
                {
                    _conn.Open();
                    Console.WriteLine("Conectado");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
                throw;
            }

            return _conn;
        }

        public void CerrarConexion()
        {
            if (_conn.State != System.Data.ConnectionState.Closed)
            {
                _conn.Close();
                Console.WriteLine("Conexión cerrada");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_conn != null)
                    {
                        CerrarConexion();
                        _conn.Dispose();
                    }
                }

                _disposed = true;
            }
        }
    }
}
