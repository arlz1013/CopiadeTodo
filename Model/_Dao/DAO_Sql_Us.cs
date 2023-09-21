using DAO_MVC_Singleton.Model.Data;
using DAO_MVC_Singleton.Model.Other;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace DAO_MVC_Singleton.Model._Dao
{
    public class DAO_Sql_US : IntelUss
    {
        private const string INSERT_QUERY = "INSERT INTO Alumnos (Name, Date, height, weight , Square_Meter) values (@a, @b , @c ,@d , @e);";
        private const string SELECT_ALL_QUERY = "SELECT * FROM Alumnos ORDER BY id";
        private const string UPDATE_QUERY = "UPDATE Alumnos SET Name = @a, Date = @b, height = @c, weight = @d, Square_Meter = @e WHERE id = @f";
        private const string DELETE_QUERY = "DELETE FROM Alumnos WHERE id=@id";
        private const string SELECT_BY_ID_QUERY = "SELECT * FROM Alumnos WHERE id=@id";

        private readonly MySqlConnection _connection;

        public DAO_Sql_US(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public bool ActualizarEmpleado(User us)
        {
            bool actualizado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(UPDATE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@a", us.Name);
                    cmd.Parameters.AddWithValue("@b", us.Date);
                    cmd.Parameters.AddWithValue("@c", us.Height);
                    cmd.Parameters.AddWithValue("@d", us.Weight);
                    cmd.Parameters.AddWithValue("@e", us.Square_Meter);
                    cmd.Parameters.AddWithValue("@f", us.Id);
                    cmd.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (Exception ex)
            {
                throw new ErrEx("Error al actualizar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return actualizado;
        }

        public bool EliminarEmpleado(int id)
        {
            bool eliminado = false;

            try
            {
                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(DELETE_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (Exception ex)
            {
                throw new ErrEx("Error al eliminar el empleado", ex);
            }
            finally
            {
                _connection.Close();
            }

            return eliminado;
        }

        public User ObtenerEmpleadoPorId(int id)
        {
            User empleado = null;

            try
            {

                ProveState();

                using (MySqlCommand cmd = new MySqlCommand(SELECT_BY_ID_QUERY, _connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            empleado = CrearEmpleadoDesdeDataReader(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ErrEx("Error al obtener el empleado por ID", ex);
            }
            finally
            {
                _connection.Close();
            }

            return empleado;
        }

        public List<User> ObtenerEmpleados()
        {
            using (MySqlCommand cmd = new MySqlCommand(SELECT_ALL_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    List<User> uss = new List<User>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User empleado = CrearEmpleadoDesdeDataReader(reader);
                            uss.Add(empleado);
                        }
                    }

                    return uss;
                }
                catch (MySqlException ex)
                {
                    throw new ErrEx("Error al obtener los empleados", ex);
                }
            }
        }

        private User CrearEmpleadoDesdeDataReader(MySqlDataReader reader)
        {
            int ide = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
            string nm = reader.GetString("Name");
            string dt = reader.GetString("Date");
            double hg = reader.GetDouble("height");
            double wg = reader.GetDouble("weight");
            double m2 = reader.GetDouble("Square_Meter");
            return new User(ide, nm, hg, wg, m2, dt);

        }

        public bool RegistrarEmpleado(User empleado)
        {
            using (MySqlCommand cmd = new MySqlCommand(INSERT_QUERY, _connection))
            {
                try
                {
                    ProveState();

                    cmd.Parameters.AddWithValue("@a", empleado.Name);
                    cmd.Parameters.AddWithValue("@b", empleado.Date);
                    cmd.Parameters.AddWithValue("@c", empleado.Height);
                    cmd.Parameters.AddWithValue("@d", empleado.Weight);
                    cmd.Parameters.AddWithValue("@e", empleado.Square_Meter);

                    cmd.ExecuteNonQuery();

                    empleado.Id = (int)cmd.LastInsertedId;

                    return true;
                }
                catch (MySqlException ex)
                {
                    throw new ErrEx("Error al registrar el empleado", ex);
                }
            }
        }
        private void ProveState()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}

