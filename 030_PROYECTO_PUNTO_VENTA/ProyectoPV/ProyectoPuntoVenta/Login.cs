using ProyectoPuntoVenta.Logica;
using ProyectoPuntoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace ProyectoPuntoVenta
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/RK1tRH");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ouo.io/VRgLgZ");
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtdocumento.Focus();
        }

        public static string GetMotherBoardID()
        {
            string mbInfo = String.Empty;
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());

            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    //mbInfo = String.Format("{0,-25}{1}", propData.Name, Convert.ToString(propData.Value));
                    mbInfo = Convert.ToString(propData.Value);
            }

            return mbInfo;
        }

        public static Boolean DemoSistema()
        {
        Boolean Vencio = false;
        //Año - Mes - Dia
        int año = 2022;
        int mes = 06;
        int dia = 08;
        DateTime date_1 = new DateTime(año, mes, dia);
        DateTime date_2 = DateTime.Now.Date;

        //Valores negativos es que aun falta del período de prueba, valores positivos indican que ya acabo
        TimeSpan Diff_dates = date_2.Subtract(date_1);
            //Si Days es mayor que 0, el periodo de prueba acabo.
            int diasrest = 0;
            if (int.Parse(Diff_dates.Days.ToString()) > 0)
            {
                diasrest = Diff_dates.Days;
                MessageBox.Show("Obtenega su licencia, escribeme a mi correo: maxwellchaconnica@gmail.com, el demo a expírado.");
                Vencio = true;
            }
            if (int.Parse(Diff_dates.Days.ToString()) < 0)
            {
                diasrest = Diff_dates.Days * -1;
                MessageBox.Show("Período de evaluación activo, quedan: " + diasrest + " restantes.");
                Vencio = false;
            }

            return Vencio;
       
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            Boolean HabilitarDemo = false;
            Boolean DemoExpiro;
            if (HabilitarDemo == true)
            {
                DemoExpiro = DemoSistema();
                if (DemoExpiro == true)
                {
                    //Error
                    MessageBox.Show("Error de Demo Expirado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (DemoExpiro == false)
                {
                    Persona oPersona = PersonaLogica.Instancia.Listar().Where(p => p.Documento == txtdocumento.Text && p.Clave == txtclave.Text && p.oTipoPersona.IdTipoPersona != 3).FirstOrDefault();
                    if (oPersona != null)
                    {
                        Form1 frm = new Form1(oPersona);
                        frm.Show();
                        this.Hide();
                        frm.FormClosing += Frm_Closing;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (HabilitarDemo == false) //No es un demo, va con licencia, se ejecuta este bloque.
            {
                String IDCPU = "0";
                //if (true) //Habilitar para desarrollo 
                if (String.Equals(IDCPU, GetMotherBoardID())) //Habilitar para hacer instalador y desactivar la linea anterior.
                {

                    Persona oPersona = PersonaLogica.Instancia.Listar().Where(p => p.Documento == txtdocumento.Text && p.Clave == txtclave.Text && p.oTipoPersona.IdTipoPersona != 3).FirstOrDefault();
                    if (oPersona != null)
                    {
                        Form1 frm = new Form1(oPersona);
                        frm.Show();
                        this.Hide();
                        frm.FormClosing += Frm_Closing;
                    }
                    else
                    {
                        MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    //Funciona el programa
                    MessageBox.Show("La Licencia comprada solo aplica para el equipo con ID:" + IDCPU + "No para el Equipo con ID:" + GetMotherBoardID() + ", debe adquirir una licencia para este PC valida. Contáctame a mi correo: maxwellchaconnica@gmail.com para adquirir una nueva licencia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }         

        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            txtdocumento.Text = "";
            txtclave.Text = "";
            txtdocumento.Focus();
            this.Show();
        }
    }
}
