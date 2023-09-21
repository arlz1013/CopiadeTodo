using System.Collections.Generic;
using System;

namespace DAO_MVC_Singleton.Model.Data
{
    public interface IntelUss
    {
        bool RegistrarEmpleado(User empleado);
        List<User> ObtenerEmpleados();
        bool ActualizarEmpleado(User empleado);
        bool EliminarEmpleado(int id);
        User ObtenerEmpleadoPorId(int id);
    }
}