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
    
    public partial class Form02EliminarEnfermos : Form
    {
        String cadenaConexion;
        SqlConnection cnx;
        SqlCommand com;
        SqlDataReader reader;
        public Form02EliminarEnfermos()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            this.cnx = new SqlConnection(this.cadenaConexion);
            this.com = new SqlCommand();
            lecturaEnfermos();
        }
        private void lecturaEnfermos()
        {
            this.lstInscripciones.Items.Clear();

            //Nada mas arrancar quiero pintar las inscripciones de los enfermos
            String sql = "SELECT INSCRIPCION FROM ENFERMO";

            //El proceso siempre será ENTRAR -> SALIR

            //Configuramos el objeto con la conexion a utilizar
            this.com.Connection = this.cnx;
            //Tipo de consulta
            this.com.CommandType = CommandType.Text;
            //La propia consulta
            this.com.CommandText = sql;

            this.cnx.Open();
            //Ejecutamos comando y leemos registros
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                this.lstInscripciones.Items.Add(this.reader["Inscripcion"]);
            }
            this.reader.Close();
            this.cnx.Close();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String sql = "DELETE FROM ENFERMO WHERE INSCRIPCION =" + this.txtInscripcion.Text;
            this.com.Connection = this.cnx;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cnx.Open();
            int eliminados = this.com.ExecuteNonQuery(); //Devuelve cuantas filas han sido afectadas
            this.cnx.Close();
            
            MessageBox.Show("Enfermos eliminados: " + eliminados);
            lecturaEnfermos();
        }

        private void lstInscripciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.lstInscripciones.SelectedIndex != -1)
            {
                this.txtInscripcion.Text = this.lstInscripciones.SelectedItem.ToString();
            }
        }
    }
}
