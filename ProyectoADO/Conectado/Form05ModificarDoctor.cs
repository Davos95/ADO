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

namespace ProyectoADO.Conectado
{
    public partial class Form05ModificarDoctor : Form
    {
        String cadenaConexion;
        SqlConnection cnx;
        SqlCommand com;
        SqlDataReader reader;
        public Form05ModificarDoctor()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            this.cnx = new SqlConnection(this.cadenaConexion);
            this.com = new SqlCommand();
            this.com.CommandType = CommandType.Text;
            cargarDoctores();
            
        }
        private void cargarDoctores()
        {
            this.lstDoctores.Items.Clear();
            String sql = "SELECT APELLIDO FROM DOCTOR";
            this.com.Connection = this.cnx;
            this.com.CommandText = sql;
            this.cnx.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                this.lstDoctores.Items.Add(this.reader["APELLIDO"].ToString());
            }
            this.reader.Close();
            this.cnx.Close();
        }

        private void lstDoctores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.lstDoctores.SelectedIndex != -1)
            {
                String sql = "SELECT * FROM DOCTOR WHERE " + "APELLIDO = @APELLIDO";
                
                String apellido = this.lstDoctores.SelectedItem.ToString();
                //Creamos el objeto parameter
                SqlParameter parape = new SqlParameter();
                //Nombre del paramtero
                parape.ParameterName = "@APELLIDO";
                parape.Value = apellido;
                //Tipo de dato NET
                parape.DbType = DbType.String;
                //Los parametros, por defecto, son IN
                parape.Direction = ParameterDirection.Input;
                //Añadimos el paramtro a la coleccion del Command
                this.com.Parameters.Add(parape);
                this.com.Connection = this.cnx;
                this.com.CommandText = sql;
                this.cnx.Open();
                this.reader = this.com.ExecuteReader();
                //En teoria solamente tenemos una fila leemos dicha fila
                this.reader.Read();
                this.txtApellido.Text = this.reader["APELLIDO"].ToString();
                this.txtEspecialidad.Text = this.reader["ESPECIALIDAD"].ToString();
                this.txtSalario.Text = this.reader["SALARIO"].ToString();
                //Liberamos recursos
                this.com.Parameters.Clear();
                this.reader.Close();
                this.cnx.Close();
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            String sql = "UPDATE DOCTOR SET APELLIDO = @NEWAPE, ESPECIALIDAD = @ESPE, SALARIO = @SAL WHERE APELLIDO = @OLDAPE";
            String oldApe = this.lstDoctores.SelectedItem.ToString();
            String newApe = this.txtApellido.Text;
            String espe = this.txtEspecialidad.Text;
            String sal = this.txtSalario.Text;
            //Creamos los parametros, solamente con Name y Value
            SqlParameter pamOld = new SqlParameter("@OLDAPE", oldApe);
            SqlParameter pamnew = new SqlParameter("@NEWAPE", newApe);
            SqlParameter pamespe = new SqlParameter("@ESPE", espe);
            SqlParameter pamsal = new SqlParameter("@SAL", sal);
            //Añadimos los parametros a la coleccion
            this.com.Parameters.Add(pamOld);
            this.com.Parameters.Add(pamnew);
            this.com.Parameters.Add(pamespe);
            this.com.Parameters.Add(pamsal);
            this.com.Connection = this.cnx;
            this.com.CommandText = sql;
            this.cnx.Open();
            int afectados = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cnx.Close();
            
            this.cargarDoctores();
            this.txtApellido.Text = "";
            this.txtEspecialidad.Text = "";
            this.txtSalario.Text = "";

        }
    }
}
