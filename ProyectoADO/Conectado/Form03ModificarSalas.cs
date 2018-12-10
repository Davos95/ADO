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
    public partial class Form03ModificarSalas : Form
    {
        String cadenaConexion;
        SqlConnection cnx;
        SqlCommand com;
        SqlDataReader reader;
        public Form03ModificarSalas()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            this.cnx = new SqlConnection(cadenaConexion);
            this.com = new SqlCommand();


            lecturaHospital();
        }

        private void lecturaHospital()
        {
            this.com.Connection = this.cnx;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "SELECT NOMBRE FROM HOSPITAL";
            this.cnx.Open();
            this.reader = this.com.ExecuteReader();
            while(reader.Read())
            {
                this.lstHospital.Items.Add(reader["NOMBRE"]);
            }
            this.reader.Close();
            this.cnx.Close();
        }
        private void lecturaSalas()
        {
            this.lstSala.Items.Clear();
            this.com.Connection = this.cnx;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "SELECT DISTINCT NOMBRE FROM SALA WHERE HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE LIKE '"+this.lstHospital.SelectedItem.ToString()+"')";            

            this.cnx.Open();
            this.reader = this.com.ExecuteReader();
            while (reader.Read())
            {
                this.lstSala.Items.Add(reader["NOMBRE"]);
            }
            this.reader.Close();
            this.cnx.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(this.lstSala.SelectedIndex == -1 || this.txtNombre.Text.Trim(' ') == "")
            {
                return;
            }

            this.com.Connection = this.cnx;
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = "UPDATE SALA SET NOMBRE = '" + this.txtNombre.Text + "' WHERE NOMBRE LIKE '" + this.lstSala.SelectedItem.ToString() + "' AND HOSPITAL_COD = (SELECT HOSPITAL_COD FROM HOSPITAL WHERE NOMBRE LIKE '" + this.lstHospital.SelectedItem.ToString() + "')";
            this.cnx.Open();
            int numero = this.com.ExecuteNonQuery();
            MessageBox.Show("Numero de filas modificadas: " + numero.ToString());
            this.cnx.Close();
            lecturaSalas();
        }

        private void lstHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.lstHospital.SelectedIndex != -1)
            {
                lecturaSalas();
            }
            
        }
    }
}
