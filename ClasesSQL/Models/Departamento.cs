using ClasesSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesSQL
{
   public class Departamento
    {
        public int NumeroDepartamento { get; set; }
        public String Nombre { get; set; }
        public int Personas { get; set; }
        public int SumaSalarial { get; set; }
        public List<Empleado> Empleados { get; set; }
    }
}
