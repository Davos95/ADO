using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesSQL.Models
{
    public class Empleado
    {
        public int NumeroEmpleado { get; set; }
        public string Apellido { get; set; }

        public int Salario { get; set; }
        public String Oficio { get; set; }
        public int Departamento { get; set; }

    }
}
