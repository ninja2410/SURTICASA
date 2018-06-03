using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class f_Plantilla : Form
    {
        public f_Plantilla()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelControl2.Text = DateTime.Now.ToString();

        }

        private void f_Plantilla_Load(object sender, EventArgs e)
        {

        }
    }
}
