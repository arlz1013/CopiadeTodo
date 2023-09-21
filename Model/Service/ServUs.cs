using DAO_MVC_Singleton.Model.Data;
using DAO_MVC_Singleton.Model.Other;
using System;
using System.Collections.Generic;

namespace DAO_MVC_Singleton.Model.Service
{
    public class ServUs
    {
        private IntelUss dao;

        public ServUs(IntelUss dao)
        {
            this.dao = dao ?? throw new ArgumentNullException(nameof(dao));
        }

        public bool RegistrarEmpleado(User empleado)
        {
            try
            {
                return dao.RegistrarEmpleado(empleado);
            }
            catch (ErrEx e)
            {
                Console.WriteLine("Error al registrar el empleado: " + e.Message);
                return false;
            }
        }

        public bool ActualizarEmpleado(User empleado)
        {
            try
            {
                return dao.ActualizarEmpleado(empleado);
            }
            catch (ErrEx e)
            {
                Console.WriteLine("Error al actualizar el empleado: " + e.Message);
                return false;
            }
        }

        public bool EliminarEmpleado(int id)
        {
            try
            {
                return dao.EliminarEmpleado(id);
            }
            catch (ErrEx e)
            {
                Console.WriteLine("Error al eliminar el empleado: " + e.Message);
                return false;
            }
        }

        public List<User> ObtenerEmpleados()
        {
            try
            {
                return dao.ObtenerEmpleados();
            }
            catch (ErrEx e)
            {
                Console.WriteLine("Error al obtener los empleados: " + e.Message);
                return new List<User>();
            }
        }

        public User ObtenerEmpleadoPorId(int id)
        {
            try
            {
                return dao.ObtenerEmpleadoPorId(id);
            }
            catch (ErrEx e)
            {
                Console.WriteLine("Error al obtener el empleado por ID: " + e.Message);
                return null;
            }
        }

        public List<double> Average(User us)
        {
            double Ave = (us.Height * us.Weight) * us.Square_Meter;
            return new List<double> { us.Id, us.Height, us.Weight, us.Square_Meter, Ave };
        }
    }
}
