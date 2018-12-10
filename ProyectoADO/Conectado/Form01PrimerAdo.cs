using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Espacio de nombres de SQL
using System.Data.SqlClient;


namespace ProyectoADO.Conectado
{
    public partial class Form01PrimerAdo : Form
    {
        //Declaramos la cadena de conexion
        String cadenaConexion;
        //Declaramos variables de acceso a datos
        //Conexion
        SqlConnection cnx;
        SqlCommand com;
        SqlDataReader reader;

        public Form01PrimerAdo()
        {
            InitializeComponent();
            this.cadenaConexion = "Data Source = LOCALHOST; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA; Password = 'MCSD2018'";
            this.cnx = new SqlConnection();
            this.com = new SqlCommand();
            //CREAMOS EL EVENTO StateChanged PARA VISUALIZAR EL CAMBIO DE ESTADO DE LA CONEXION
            this.cnx.StateChange += cnx_StateChange;

            //La cadena de conexion se establece al instanciar un SQLConnection
            this.cnx.ConnectionString = this.cadenaConexion;
        }

        private void cnx_StateChange(object sender, StateChangeEventArgs e)
        {
            //En este evento entra cada vez que cambiamos el estado de OPEN a CLOSE y viceversa
            this.lblEstado.Text = "Conexion pasado de " + e.OriginalState + " a " + e.CurrentState;

        }

        private void btnConectar_Click(object sender, EventArgs e)  
        {
            //Siempre que trabajemos con acceso a datos debemos incluir control de errores
            try
            {
                //Comprobar el estado de la conexion con STATE
                if (cnx.State == ConnectionState.Closed)
                {
                    //Abrimos la conexion
                    this.cnx.Open();
                }
                
            }
            catch (SqlException ex) 
            {
                MessageBox.Show("Error de conexion: " + ex.Message);
            }            
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            //Cerramos la conexion
            this.cnx.Close();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            //Indicamos al comando la conexion a utilizar
            this.com.Connection = this.cnx;
            //Indicamos el tipo de consulta a realizar
            this.com.CommandType = CommandType.Text;
            //Creamos la consulta a realizar
            this.com.CommandText = "SELECT * FROM EMP";

            //Ejecutamos el comando
            //Al ser una consulta SELECT devuelve registros (cursor)
            if(this.cnx.State == ConnectionState.Closed)
            {
                return;
            }
            this.reader = this.com.ExecuteReader();

            //Pintamos la primera columna de la consulta con GetName
            this.lstColumnas.Items.Add(this.reader.GetName(0));

            //Para recuperar DATOS necesitamos que lea registros

            this.reader.Read(); //Con esto pasa a la primera fila, funciona igual que un cursor

            //Las columnas tienen un indice de inicio (0) y un final que es FieldCount
            for (int i = 1; i < this.reader.FieldCount; i++)
            {
                this.lstColumnas.Items.Add(this.reader.GetName(i));
            }
            //Las filas se leen con READ() que devuelve un BOOL
            //Debemos ller todos los registros con WHILE
            while (this.reader.Read())
            {
                //Recogemos el primer apellido con GetString
                String apellido = this.reader.GetString(1);
                this.lstApellidos.Items.Add(apellido);
                //Recuperamos el Salario (COLUMNA 5)
                //Con GetValue te devuelve el valor con tipo Object por lo que podemos hacerle un Casting o un ToString()
                String salario = this.reader.GetValue(5).ToString();

                //Esta segunda opción deja poner el nombre de la columna y obtiene igualmente el valor con tipo Object
                String salarioOpcion2 = this.reader["SALARIO"].ToString();
                this.lstSalarios.Items.Add(salarioOpcion2);
            }

            //Cerramos el reader una vez que hayamos terminado de leer los datos
            this.reader.Close();
        }
    }
}
