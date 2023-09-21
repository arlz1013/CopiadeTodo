using DAO_MVC_Singleton.Model.Data;
using DAO_MVC_Singleton.Model.Service;
using DAO_MVC_Singleton.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_MVC_Singleton.Control
{
    public class ControlUs
    {
        private ServUs empleadoService;
        private Look vista;

        public ControlUs(ServUs empleadoService, Look vista)
        {
            this.empleadoService = empleadoService ?? throw new ArgumentNullException(nameof(empleadoService));
            this.vista = vista ?? throw new ArgumentNullException(nameof(vista));
        }

        public void ListarEmpleados()
        {
            try
            {
                List<User> empleados = empleadoService.ObtenerEmpleados();
                vista.MostrarEmpleados(empleados);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al listar empleados: " + e.Message);
            }
        }

        public void VerEmpleado(int id)
        {
            try
            {
                User empleado = empleadoService.ObtenerEmpleadoPorId(id);

                if (empleado != null)
                {
                    vista.MostrarEmpleado(empleado);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al obtener el empleado: " + e.Message);
            }
        }

        public void RegistrarEmpleado(User empleado)
        {
            try
            {
                if (empleadoService.RegistrarEmpleado(empleado))
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine("--> Registro exitoso: " + empleado.Id);
                    vista.MostrarEmpleado(empleado);
                }
                else
                {
                    Console.WriteLine("Error al registrar el empleado.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al registrar el empleado: " + e.Message);
            }
        }

        public void ActualizarEmpleado(int id, string Name, string Date, double n1, double n2, double n3)
        {
            try
            {
                User UsEx= empleadoService.ObtenerEmpleadoPorId(id);

                if (UsEx != null)
                {
                    Console.WriteLine("------------Datos originales------------");
                    Console.WriteLine(UsEx);

                    UsEx.Name = Name;
                    UsEx.Date = Date;
                    UsEx.Height = n1;
                    UsEx.Weight = n2;
                    UsEx.Square_Meter = n3;

                    if (empleadoService.ActualizarEmpleado(UsEx))
                    {
                        Console.WriteLine("--> Actualización exitosa");
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar el empleado.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al actualizar el empleado: " + e.Message);
            }
        }

        public void EliminarEmpleado(int id)
        {
            try
            {
                User empleadoAEliminar = empleadoService.ObtenerEmpleadoPorId(id);

                if (empleadoAEliminar != null)
                {
                    if (empleadoService.EliminarEmpleado(id))
                    {
                        Console.WriteLine("Empleado eliminado: " + empleadoAEliminar.Id);
                    }
                    else
                    {
                        Console.WriteLine("Error al eliminar el empleado.");
                    }
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún empleado con el ID {id}.");
                }
            }
            catch (ApplicationException e)
            {
                Console.WriteLine("Error al eliminar el empleado: " + e.Message);
            }
        }

        public void Average(int id) 
        {
            
            try
            {
                User us = empleadoService.ObtenerEmpleadoPorId(id);
                if (us != null)
                {
                    List<double> sa = empleadoService.Average(us);
                    vista.ShowAverage(sa);
                }
                else 
                {
                    Console.WriteLine($"No se encontró ningún empleado con el ID {id}.");
                }
                  
            }
            catch (ApplicationException e) 
            {
                Console.WriteLine("Error al eliminar el empleado: " + e.Message);
            }
        }
    }
}