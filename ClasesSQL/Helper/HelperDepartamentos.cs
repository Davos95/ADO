using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ClasesSQL.Models;

#region PROCEDURES
/*
 CREATE PROCEDURE MOSTRAROFICIOS
AS
	SELECT DISTINCT OFICIO FROM EMP;
GO

CREATE PROCEDURE MOSTRARDEPARTAMENTOS
AS
	SELECT DNOMBRE, DEPT_NO FROM DEPT;
GO
*/
#endregion
namespace ClasesSQL.Helper
{
    public class HelperDepartamentos
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader reader;
        
        public HelperDepartamentos(String cadenaConexion)
        {
            con = new SqlConnection(cadenaConexion);
            com = new SqlCommand();
            this.com.Connection = this.con;
        }
        public List<String> GetOficios()
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MOSTRAROFICIOS";
            this.con.Open();
            this.reader = this.com.ExecuteReader();
            List<String> oficios = new List<string>();
            while (this.reader.Read())
            {
                String ofi = this.reader["OFICIO"].ToString();
                oficios.Add(ofi);
            }
            this.reader.Close();
            this.con.Close();
            return oficios;
        }
        public List<Departamento> GetDepartamentos()
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MOSTRARDEPARTAMENTOS";
            this.con.Open();
            this.reader = this.com.ExecuteReader();
            List<Departamento> departamentos = new List<Departamento>();
            while (this.reader.Read())
            {
                Departamento d = new Departamento();
                int deptNo = int.Parse(this.reader["DEPT_NO"].ToString());
                String nombre = this.reader["DNOMBRE"].ToString();
                d.NumeroDepartamento = deptNo;
                d.Nombre = nombre;
                departamentos.Add(d);
            }
            this.reader.Close();
            this.con.Close();
            return departamentos;
        }
        public Departamento BuscarDepartamento(int deptno)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "BUSCARDEPARTAMENTO";

            SqlParameter pamdept = new SqlParameter("@DEPTNO", deptno);
            this.com.Parameters.Add(pamdept);

            SqlParameter pamperson = new SqlParameter("@PERSONAS", 0);
            pamperson.Direction = System.Data.ParameterDirection.Output;
            this.com.Parameters.Add(pamperson);

            SqlParameter pamsum = new SqlParameter("@SUMA", 0);
            pamsum.Direction = System.Data.ParameterDirection.Output;
            this.com.Parameters.Add(pamsum);

            this.con.Open();
            this.reader = this.com.ExecuteReader();
            Departamento dept = new Departamento();
            if (this.reader.HasRows)
            {
                dept.Empleados = new List<Models.Empleado>();
                while (this.reader.Read())
                {
                    Empleado emp = new Empleado();
                    emp.NumeroEmpleado = int.Parse(this.reader["EMP_NO"].ToString());
                    emp.Apellido = this.reader["APELLIDO"].ToString();
                    dept.Empleados.Add(emp);
                }
                this.reader.Close();
                dept.Personas = int.Parse(pamperson.Value.ToString());
                dept.SumaSalarial = int.Parse(pamsum.Value.ToString());
                this.com.Parameters.Clear();
                this.con.Close();
                return dept;
            } else
            {
                this.con.Close();
                this.com.Parameters.Clear();
                return null;
            }
            
        }

        public Empleado BuscarEmpleado(int empno)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "BUSCAREMPLEADO";
            SqlParameter pamempno = new SqlParameter("@EMPNO", empno);
            this.com.Parameters.Add(pamempno);
            this.con.Open();
            this.reader = this.com.ExecuteReader();
            Empleado emp = new Empleado();
            emp.Salario = int.Parse(this.reader["SALARIO"].ToString());
            emp.Oficio = this.reader["OFICIO"].ToString();
            emp.Departamento = int.Parse(this.reader["DEPT_NO"].ToString());
            this.reader.Close();
            this.con.Close();
            return emp;
        }

        public void ModificarEmpleado(int empno, int salario, String oficio, int numdept)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MODIFICAREMPLEADO";
            SqlParameter pamsal = new SqlParameter("@SALARIO",salario);
        }

        public void Eliminar(int empno)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "ELIMINAREMPLEADO";
            SqlParameter pamempno = new SqlParameter("@EMPNO", empno);
            this.com.Parameters.Add(pamempno);
            this.con.Open();
            this.com.ExecuteNonQuery();
            this.con.Close();
            this.com.Parameters.Clear();
        }
    }
}
