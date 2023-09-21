using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_MVC_Singleton.Model.Other
{
    public class ErrEx : Exception
    {
        public ErrEx() : base("Error en la capa de acceso a datos."){}
        public ErrEx(string message) : base(message){}
        public ErrEx(string message, Exception innerException) : base(message, innerException){}
    }
}
