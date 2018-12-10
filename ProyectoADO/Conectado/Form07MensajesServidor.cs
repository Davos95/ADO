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

#region Procedimientos Almacenados
/*
CREATE PROCEDURE MOSTRARDEPT
AS

    SELECT* FROM DEPT;
GO

CREATE PROCEDURE BUSCARDEPT
(@NUMERO INT)
AS
	SELECT * FROM DEPT WHERE DEPT_NO = @NUMERO;
GO

CREATE PROCEDURE INSERTARDEPT
(@NUMERO INT, @NOMBRE NVARCHAR(30), @LOCALIDAD NVARCHAR(30))
AS
    DECLARE @EXISTE INT

    SELECT @EXISTE = COUNT(*) FROM DEPT WHERE DEPT_NO = @NUMERO;
IF(@EXISTE = 0)

    BEGIN
        INSERT INTO DEPT VALUES(@NUMERO, @NOMBRE, @LOCALIDAD)

    END
    ELSE

    BEGIN
        PRINT 'ERROR'
	END

GO

CREATE PROCEDURE MODIFICARDEPT
(@NUMERO INT, @NOMBRE NVARCHAR(30), @LOCALIDAD NVARCHAR(30))
AS
    UPDATE DEPT SET DNOMBRE = @NOMBRE, LOC = @LOCALIDAD WHERE DEPT_NO = @NUMERO;
GO

CREATE PROCEDURE ELIMINARDEPT
(@NUMERO INT)
AS
    DELETE FROM DEPT WHERE DEPT_NO = @NUMERO;
GO
*/
#endregion

namespace ProyectoADO.Conectado
{
    public partial class Form07MensajesServidor : Form
    {
        String cadenaConexion;
        SqlConnection con;
        SqlCommand com;
        SqlDataReader reader;
        List<int> departamentos;
        public Form07MensajesServidor()
        {
            InitializeComponent();
            cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            con = new SqlConnection(cadenaConexion);
            com = new SqlCommand();
            this.con.InfoMessage += CapturarMensaje;
            departamentos = new List<int>();
            cargarDepartamentos();
        }

        private void CapturarMensaje(object sender, SqlInfoMessageEventArgs e)
        {
            this.lblMensajes.Text = e.Message;
        }

        private void cargarDepartamentos()
        {
            
            this.departamentos.Clear();
            this.lstDepartamentos.Items.Clear();

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "MOSTRARDEPT";
            this.com.Connection = this.con;
            this.con.Open();
            this.reader = com.ExecuteReader();
            while (this.reader.Read())
            {
                this.departamentos.Add(int.Parse(this.reader["DEPT_NO"].ToString()));
                this.lstDepartamentos.Items.Add(this.reader["DNOMBRE"] + " - " + this.reader["LOC"]);
            }
            this.reader.Close();
            this.con.Close();
        }
        private void limpiarTextbox()
        {
            
            foreach (Control txtbox in this.Controls)
            {
                if(txtbox is TextBox)
                {
                    txtbox.Text = "";
                }
            }
        }

        private void lstDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.lstDepartamentos.SelectedIndex != -1)
            {
                this.com.CommandType = CommandType.StoredProcedure;
                this.com.CommandText = "BUSCARDEPT";
                SqlParameter paramNum = new SqlParameter("@NUMERO", this.departamentos[this.lstDepartamentos.SelectedIndex]);
                this.com.Parameters.Add(paramNum);
                this.com.Connection = this.con;
                this.con.Open();
                this.reader = this.com.ExecuteReader();
                this.reader.Read();
                this.txtNumero.Text = this.reader["DEPT_NO"].ToString();
                this.txtNombre.Text = this.reader["DNOMBRE"].ToString();
                this.txtLocalidad.Text = this.reader["LOC"].ToString();
                this.com.Parameters.Clear();
                this.reader.Close();
                this.con.Close();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            this.lblMensajes.Text = "";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "INSERTARDEPT";
            SqlParameter paramNum = new SqlParameter("@NUMERO", this.txtNumero.Text);
            SqlParameter paramNom = new SqlParameter("@NOMBRE", this.txtNombre.Text);
            SqlParameter paramLoc = new SqlParameter("@LOCALIDAD", this.txtLocalidad.Text);
            this.com.Parameters.Add(paramLoc);
            this.com.Parameters.Add(paramNom);
            this.com.Parameters.Add(paramNum);
            this.com.Connection = this.con;
            this.con.Open();
            int num = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.con.Close();
            this.cargarDepartamentos();
            limpiarTextbox();
            lblRegistros.Text = "Registros: " + num;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            this.lblMensajes.Text = "";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "MODIFICARDEPT";
            SqlParameter paramNum = new SqlParameter("@NUMERO", this.departamentos[this.lstDepartamentos.SelectedIndex]);
            SqlParameter paramNom = new SqlParameter("@NOMBRE", this.txtNombre.Text);
            SqlParameter paramLoc = new SqlParameter("@LOCALIDAD", this.txtLocalidad.Text);
            this.com.Parameters.Add(paramLoc);
            this.com.Parameters.Add(paramNom);
            this.com.Parameters.Add(paramNum);
            this.com.Connection = this.con;
            this.con.Open();
            int num = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.con.Close();
            this.cargarDepartamentos();
            limpiarTextbox();
            lblRegistros.Text = "Registros: " + num;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.lblMensajes.Text = "";
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "ELIMINARDEPT";
            SqlParameter paramNum = new SqlParameter("@NUMERO", this.departamentos[this.lstDepartamentos.SelectedIndex]);
            this.com.Parameters.Add(paramNum);
            this.com.Connection = this.con;
            this.con.Open();
            int num = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.con.Close();
            this.cargarDepartamentos();
            limpiarTextbox();
            lblRegistros.Text = "Registros: " + num;
        }
    }
}
