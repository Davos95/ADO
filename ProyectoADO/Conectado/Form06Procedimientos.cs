using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

#region Procedimientos almacenados
/*
CREATE PROCEDURE MOSTRARENFERMOS
AS

    SELECT* FROM ENFERMO
GO

CREATE PROCEDURE ELIMINARENFERMO
(@INSCRIPCION INT)
AS
    DELETE FROM ENFERMO WHERE INSCRIPCION = @INSCRIPCION;
GO
*/
#endregion

namespace ProyectoADO.Conectado
{
    public partial class Form06Procedimientos : Form
    {
        String cadenaConexion;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        List<int> inscripciones;
        public Form06Procedimientos()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            cn = new SqlConnection(cadenaConexion);
            this.com = new SqlCommand();
            this.inscripciones = new List<int>();
            CargarEnfermos();  
        }
        private void CargarEnfermos()
        {
            this.inscripciones.Clear();
            this.lstEnfermos.Items.Clear();
            //Nuestro SQL es el nombre del procedimiento
            String sql = "MOSTRARENFERMOS";
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                this.inscripciones.Add(int.Parse(this.reader["INSCRIPCION"].ToString()));
                this.lstEnfermos.Items.Add(this.reader["APELLIDO"]);
            }
            this.reader.Close();
            this.cn.Close();
        }

        private void btnEliminarEnfermo_Click(object sender, EventArgs e)
        {
            String sql = "ELIMINARENFERMO";
            int inscripcion = this.inscripciones[this.lstEnfermos.SelectedIndex];
            SqlParameter pamInsc = new SqlParameter("@INSCRIPCION", inscripcion);
            this.com.Parameters.Add(pamInsc);
            this.com.Connection = this.cn;
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();
            this.CargarEnfermos();
        }

        private void lstEnfermos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.lstEnfermos.SelectedIndex != -1)
            {
                this.txtEnfermo.Text = this.lstEnfermos.SelectedItem.ToString();
            }
        }
    }
}
