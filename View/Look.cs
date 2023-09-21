using DAO_MVC_Singleton.Model.Data;
using System;
using System.Collections.Generic;

using static System.Console;

namespace DAO_MVC_Singleton.View
{
    public class Look
    {
        public void MostrarEmpleado(User empleado)
        {
            WriteLine("Datos del Empleado:\n" + empleado.ToString());
        }

        public void MostrarEmpleados(List<User> empleados)
        {
            if (empleados.Count == 0)
            {
                WriteLine("No hay empleados para mostrar.");
                return;
            }

            WriteLine("Lista de Empleados:");
            foreach (User empleado in empleados)
            {
                WriteLine("------------");
                WriteLine(empleado.ToString());
            }
        }

        public void ShowAverage(List<double> da) 
        {
            WriteLine("Info About Id: " + da[0]);
            WriteLine("Id\tNota1\tNota2\tNota3\tPromedio");
            foreach (double i in da)
            {
                Write(i + "\t");
            }
            WriteLine("\n");
        }
    }
}
