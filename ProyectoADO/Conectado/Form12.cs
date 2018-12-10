using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClasesSQL;
using ClasesSQL.Helper;
using ClasesSQL.Models;

namespace ProyectoADO.Conectado
{
    public partial class Form12 : Form
    {
        HelperDepartamentos helper;
        List<Departamento> departamentos;
        List<int> codigoEmpleados;
        public Form12()
        {
            InitializeComponent();
            String cadenaConexion = @"Data Source=LOCALHOST;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2018";
            this.helper = new HelperDepartamentos(cadenaConexion);
            departamentos = new List<Departamento>();
            this.codigoEmpleados = new List<int>();
            List<String> oficios = this.helper.GetOficios();
            this.departamentos = this.helper.GetDepartamentos();

            foreach(String ofi in oficios)
            {
                this.cmbOficio.Items.Add(ofi);
            }
            foreach(Departamento d in departamentos)
            {
                this.cmbDepartamento.Items.Add(d);   
                this.cmbDepartamentos.Items.Add(d);
            }

        }

        private void cmbDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = this.cmbDepartamento.SelectedIndex;
            if(indice != -1)
            {
                int deptno = this.departamentos[indice].NumeroDepartamento;
                Departamento dept = this.helper.BuscarDepartamento(deptno);
                this.lstEmpleados.Items.Clear();
                this.codigoEmpleados.Clear();
                if(dept is null)
                {
                    this.txtPersonas.Text = "0";
                    this.txtSumaSalarial.Text = "0";
                } else
                {
                    this.txtPersonas.Text = dept.Personas.ToString();
                    this.txtSumaSalarial.Text = dept.SumaSalarial.ToString();
                    foreach(Empleado emp in dept.Empleados)
                    {
                        this.lstEmpleados.Items.Add(emp.Apellido);
                        this.codigoEmpleados.Add(emp.NumeroEmpleado);
                    }
                }
            }
        }

        private void lstEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = this.lstEmpleados.SelectedIndex;
            if (indice != -1)
            {
                int empno = this.codigoEmpleados[indice];
                Empleado emp = this.helper.BuscarEmpleado(empno);
                this.txtSalario.Text = emp.Salario.ToString();
                this.cmbOficio.SelectedItem = emp.Oficio;
                //this.cmbOficio
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int indice = this.lstEmpleados.SelectedIndex;
            int empno = this.codigoEmpleados[indice];
            int salario = int.Parse(this.txtSalario.Text);
            String oficio = this.cmbOficio.SelectedItem.ToString();
            int indicedept = int.Parse(this.cmbDepartamento.SelectedItem.ToString());
            int deptno = this.departamentos[indicedept].NumeroDepartamento;
            this.helper.ModificarEmpleado(empno, salario, oficio, deptno);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int indice = this.lstEmpleados.SelectedIndex;
            int empno = this.codigoEmpleados[indice];
            this.helper.Eliminar(empno);
        }
    }
}
