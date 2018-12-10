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
    public partial class Form04BuscarEmpleado : Form
    {
        String cadenaConexion;
        SqlConnection cnx;
        SqlCommand com;
        SqlDataReader reader;
        public Form04BuscarEmpleado()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            this.cnx = new SqlConnection(cadenaConexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cnx;
            this.com.CommandType = CommandType.Text;
            listarEmpleado();
        }
        private void listarEmpleado()
        {
            this.lstEmpleados.Items.Clear();
            this.com.CommandText = "SELECT APELLIDO, OFICIO FROM EMP WHERE APELLIDO LIKE '%" + this.txtApellido.Text + "%'";
            this.cnx.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                this.lstEmpleados.Items.Add(this.reader["APELLIDO"] + " - " + this.reader["OFICIO"]);
            }
            this.reader.Close();
            this.cnx.Close();
        }
        private void txtApellido_TextChanged(object sender, EventArgs e)
        {
            listarEmpleado();
        }
    }
}
