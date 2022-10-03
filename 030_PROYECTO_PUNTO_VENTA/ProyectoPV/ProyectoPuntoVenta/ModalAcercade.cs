using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPuntoVenta
{
    public partial class ModalAcercade : Form
    {
        public ModalAcercade()
        {
            InitializeComponent();
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

        private void ModalAcercade_Load(object sender, EventArgs e)
        {
            try
            {
                label6.Text = GetMotherBoardID();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Obtención de Licencia:"+ex.Message.ToString());
            }
        }
    }
}
