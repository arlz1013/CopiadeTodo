using DAO_MVC_Singleton.Model.Data;
using DAO_MVC_Singleton.Model.Other;
using DAO_MVC_Singleton.Model.Service;
using DAO_MVC_Singleton.View;
using System;
using System.Collections.Generic;
using System.Data;

using static System.Console;

namespace DAO_MVC_Singleton.Control
{
    public class ControlAll
    {


        private IntelUss dao;
        private Look vista;
        private ServUs empleadoService;
        private ControlUs controller;
        private string action;
        private int id;

        public ControlAll()
        {
            dao = Fact.CrearEmpleadoDAO();
            vista = new Look();
            empleadoService = new ServUs(dao);
            controller = new ControlUs(empleadoService, vista);
            while (Exe() == true)
            {
                ReadKey();
                Clear();
            }
        }

        private bool Exe()
        {
            string action;
            int id;
            WriteLine("MENU DE OPCIONES:");
            WriteLine("|I| Insert");
            WriteLine("|U| Update");
            WriteLine("|D| Delete");
            WriteLine("|L| List");
            WriteLine("|A| Average");
            WriteLine("Exit");
            action = ReadLine()?.ToUpper();

            if (!string.IsNullOrEmpty(action))
            {
                try
                {
                    switch (action)
                    {
                        case "L":
                            ListUs();
                            break;
                        case "I":
                            InsertUs();
                            break;
                        case "U":
                            UpdateUs();
                            break;
                        case "D":
                            DeleteUs();
                            break;
                        case "S":
                            return false;
                        case "H":
                            MostrarAyuda();
                            break;
                        case "A":
                            Average();
                            break;
                        default:
                            WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                            break;
                    }
                }
                catch (ErrEx e)
                {
                    WriteLine("Error: " + e.Message);
                }
            }
            return true;
        }
        private static void MostrarAyuda()
        {
            WriteLine("Opciones disponibles:");
            WriteLine("[L]istar: Muestra la lista de empleados.");
            WriteLine("[R]egistrar: Registra un nuevo empleado.");
            WriteLine("[A]ctualizar: Actualiza un empleado existente.");
            WriteLine("[E]liminar: Elimina un empleado.");
            WriteLine("[C]alcular Nomina.");
            WriteLine("[S]alir: Sale del programa.");
            WriteLine("[H]elp: Muestra esta ayuda.");
        }

        private static User InputEmpleado()
        {
            string name = InputString("Insert Nombre");
            string date = InputDateNow();
            double n1 = InputDouble("Insert Alto");
            double n2 = InputDouble("Insert Ancho");
            double n3 = InputDouble("Insert Valor x m^2");

            return new User(name, n1, n2, n3, date);
        }

        private static int InputId()
        {
            int id;
            while (true)
            {
                try
                {
                    WriteLine("Ingrese un valor entero para el ID del empleado: ");
                    if (int.TryParse(Console.ReadLine(), out id))
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    WriteLine("Error de formato de número");
                }
            }
            return id;
        }

        private static string InputString(string message)
        {
            string s;
            while (true)
            {
                WriteLine(message);
                s = ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(s) && s.Length >= 2)
                {
                    break;
                }
                else
                {
                    WriteLine("La longitud de la cadena debe ser >= 2");
                }
            }
            return s;
        }

        private static double InputDouble(string message)
        {
            double value;
            while (true)
            {
                try
                {
                    WriteLine(message);
                    if (double.TryParse(ReadLine(), out value))
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Error de formato de número");
                    }
                }
                catch (FormatException)
                {
                    WriteLine("Error de formato de número");
                }
            }
            return value;
        }
        private static string InputDateNow()
        {
            DateTime Fecha = DateTime.Now;
            string FormatoFechaSql = Fecha.ToString("yyyy-MM-dd HH:mm:ss");
            WriteLine(FormatoFechaSql);
            return FormatoFechaSql;
        }

        private void ListUs() 
        {
            controller.ListarEmpleados();
        }
        private void InsertUs() 
        {
            User nuevoEmpleado = InputEmpleado();
            controller.RegistrarEmpleado(nuevoEmpleado);
        }
        private void UpdateUs()
        {
            id = InputId();
            WriteLine("-------------------");
            controller.VerEmpleado(id);
            WriteLine("------------------------");
            WriteLine("Ingrese los nuevos datos");
            WriteLine("------------------------");
            string name = InputString("Ingrese el nombre ");
            string date = InputDateNow();
            double n1 = InputDouble("Ingrese el Alto ");
            double n2 = InputDouble("Ingrese el Ancho ");
            double n3 = InputDouble("Ingrese el valor por metro cuadrado ");
            controller.ActualizarEmpleado(id, name, date, n1, n2, n3);
            controller.VerEmpleado(id);
        }
        private void DeleteUs()
        {
            id = InputId();
            controller.EliminarEmpleado(id);
        }
        private void Average() 
        {
            id = InputId();
            controller.Average(id);
        }
    }
}
