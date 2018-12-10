using ClasesSQL.Helper;
using ClasesSQL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoADO.Conectado
{
    public partial class Form13Doctores : Form
    {
        HelperHospitales helper;
        
        List<Doctor> doctores;
        public Form13Doctores()
        {
            InitializeComponent();
            String cadenaConexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2018";
            helper = new HelperHospitales(cadenaConexion);
            doctores = new List<Doctor>();
            mostrarEspecialidades();
            cargarHospitales();
        }

        private void mostrarEspecialidades()
        {
            List<String> especialidades = new List<string>();
            especialidades = helper.GetEspecialidades();
            foreach(String e in especialidades)
            {
                this.lstEspecialidades.Items.Add(e);
                this.cmbEspecialidad.Items.Add(e);
            }
        }
        private void cargarHospitales() {
            List<String> hospitales = new List<string>();
            hospitales = helper.GetHospitales();
            foreach (String h in hospitales)
            {
                this.cmbHospital.Items.Add(h);
            }
        }
        private void cargarDoctores()
        {
            doctores.Clear();
            this.lstDoctores.Items.Clear();
            foreach (String esp in this.lstEspecialidades.SelectedItems)
            {
                List<Doctor> doctoresTemp = new List<Doctor>();
                doctoresTemp = helper.BuscarDoctores(esp);
                foreach (Doctor d in doctoresTemp)
                {
                    this.doctores.Add(d);
                    this.lstDoctores.Items.Add(d.Apellido);
                }
            }
            
        }
        private void lstEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = this.lstEspecialidades.SelectedIndex;
            if(indice != -1)
            {
                cargarDoctores();   
            } else
            {
                doctores.Clear();
                this.lstDoctores.Items.Clear();
            }
        }
        
        private void lstDoctores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = this.lstDoctores.SelectedIndex;
            if(indice != -1)
            {
                this.txtSalario.Text = this.doctores[indice].Salario.ToString();
                this.txtApellido.Text = this.doctores[indice].Apellido.ToString();
                this.cmbHospital.SelectedItem = this.doctores[indice].Hospital;
                this.cmbEspecialidad.SelectedItem = this.doctores[indice].Especialidad;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int indice = this.lstDoctores.SelectedIndex;
            if(indice != -1)
            {
                String apellido = this.txtApellido.Text;
                int cod = this.doctores[indice].numDoctor;
                int salario = int.Parse(this.txtSalario.Text);
                String especialidad = this.cmbEspecialidad.Text;
                String hospital = this.cmbHospital.Text;
                helper.ModificarDoctor(apellido, cod, salario, especialidad, hospital);
                cargarDoctores();
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if(this.txtApellido.Text != "" && this.txtSalario.Text != "" && this.cmbEspecialidad.SelectedIndex != -1 && this.cmbHospital.SelectedIndex != -1)
            {
                helper.InsertarDoctor(this.txtApellido.Text, int.Parse(this.txtSalario.Text), this.cmbEspecialidad.Text, this.cmbHospital.Text);
                cargarDoctores();
            }
        }
    }
}
