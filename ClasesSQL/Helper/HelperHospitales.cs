using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ClasesSQL.Models;

namespace ClasesSQL.Helper

#region PROCEDURES
/*
 CREATE PROCEDURE MOSTRARESPECIALIDADES
AS
	SELECT DISTINCT ESPECIALIDAD FROM DOCTOR;
GO

CREATE PROCEDURE MOSTRARHOSPITALES
AS
	SELECT NOMBRE FROM HOSPITAL;
GO
 
CREATE PROCEDURE BUSCARDOCTORES
(@ESPECIALIDAD NVARCHAR(30))
AS
	SELECT D.APELLIDO, D.DOCTOR_NO, D.SALARIO, D.ESPECIALIDAD, H.NOMBRE
	FROM DOCTOR D
	INNER JOIN HOSPITAL H
	ON D.HOSPITAL_COD = H.HOSPITAL_COD
	WHERE D.ESPECIALIDAD = @ESPECIALIDAD;

GO

CREATE PROCEDURE MODIFICARDOCTOR
(@NUMDOCTOR INT, @APELLIDO NVARCHAR(30), @HOSPITAL NVARCHAR(30), @ESPECIALIDAD NVARCHAR(30), @SALARIO INT)
AS
	DECLARE @NUMHOSPITAL INT;
	SELECT @NUMHOSPITAL = HOSPITAL_COD
	FROM HOSPITAL
	WHERE NOMBRE LIKE @HOSPITAL;

	UPDATE DOCTOR 
	SET APELLIDO = @APELLIDO, 
	ESPECIALIDAD = @ESPECIALIDAD, 
	SALARIO = @SALARIO,
	HOSPITAL_COD = @NUMHOSPITAL
	WHERE DOCTOR_NO = @NUMDOCTOR;

GO

CREATE PROCEDURE INSERTARDOCTOR
(@APELLIDO NVARCHAR(30), @SALARIO INT, @ESPECIALIDAD NVARCHAR(30), @HOSPITAL NVARCHAR(30))
AS
	DECLARE @NUMHOSPITAL INT;
	DECLARE @CODDOCTOR INT;

	SELECT @NUMHOSPITAL = HOSPITAL_COD 
	FROM HOSPITAL
	WHERE NOMBRE LIKE @HOSPITAL;

	SELECT @CODDOCTOR = MAX(DOCTOR_NO)+1
	FROM DOCTOR;
	
	INSERT INTO DOCTOR (HOSPITAL_COD, DOCTOR_NO, APELLIDO, ESPECIALIDAD, SALARIO) VALUES (@NUMHOSPITAL, @CODDOCTOR, @APELLIDO, @ESPECIALIDAD, @SALARIO);

GO

 */
#endregion

{
    public class HelperHospitales
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader reader;

        public HelperHospitales(String cadenaConexion)
        {
            this.con = new SqlConnection(cadenaConexion);
            this.com = new SqlCommand();
            this.com.Connection = this.con;
        }

        public List<String> GetEspecialidades()
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MOSTRARESPECIALIDADES";
            List<String> especialidades = new List<string>();
            this.con.Open();
            this.reader = com.ExecuteReader();
            while (this.reader.Read())
            {
                especialidades.Add(this.reader["ESPECIALIDAD"].ToString());
            }
            this.reader.Close();
            this.con.Close();

            return especialidades;
        }

        public List<String> GetHospitales()
        {
            List<String> hospitales = new List<string>();
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MOSTRARHOSPITALES";
            this.con.Open();
            this.reader = com.ExecuteReader();
            while (this.reader.Read())
            {
                hospitales.Add(this.reader["NOMBRE"].ToString());
            }
            this.reader.Close();
            this.con.Close();

            return hospitales;
        }

        public List<Doctor> BuscarDoctores(String especialidad)
        {
            List<Doctor> doctores = new List<Doctor>();
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "BUSCARDOCTORES";
            SqlParameter pamEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            this.com.Parameters.Add(pamEspecialidad);
            this.con.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                Doctor d = new Doctor();
                d.Apellido = this.reader["APELLIDO"].ToString();
                d.Salario = int.Parse(this.reader["SALARIO"].ToString());
                d.numDoctor = int.Parse(this.reader["DOCTOR_NO"].ToString());
                d.Especialidad = this.reader["ESPECIALIDAD"].ToString();
                d.Hospital = this.reader["NOMBRE"].ToString();
                doctores.Add(d);
            }
            this.com.Parameters.Clear();
            this.reader.Close();
            this.con.Close();

            return doctores;
        }

        public void ModificarDoctor(String apellido, int doctorCod, int salario, String especialidad, String hospital)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "MODIFICARDOCTOR";
            SqlParameter pamNumDoctor = new SqlParameter("@NUMDOCTOR",doctorCod);
            SqlParameter pamApellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter pamHospital = new SqlParameter("@HOSPITAL", hospital);
            SqlParameter pamEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamSalario = new SqlParameter("@SALARIO", salario);
            this.com.Parameters.Add(pamNumDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamHospital);
            this.com.Parameters.Add(pamSalario);
            this.con.Open();
            this.com.ExecuteNonQuery();
            this.con.Close();
            this.com.Parameters.Clear();
        }

        public void InsertarDoctor(String apellido, int salario, String especialidad, String hospital)
        {
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "INSERTARDOCTOR";
            SqlParameter pamApellido = new SqlParameter("@APELLIDO", apellido);
            SqlParameter pamSalario = new SqlParameter("@SALARIO", salario);
            SqlParameter pamEspecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamHospital = new SqlParameter("@HOSPITAL", hospital);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamSalario);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamHospital);
            this.con.Open();
            this.com.ExecuteNonQuery();
            this.con.Close();
            this.com.Parameters.Clear();
        }

    }
}
