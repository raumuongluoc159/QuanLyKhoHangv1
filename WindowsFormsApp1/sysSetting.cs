using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class sysSetting : DevExpress.XtraEditors.XtraForm
    {
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        public sysSetting()
        {
            InitializeComponent();
            generalSet generalSet = new generalSet();
            OpenChildForm(generalSet);
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            generalSet generalSet = new generalSet();
            OpenChildForm(generalSet);
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            addDV addDV = new addDV();
            OpenChildForm(addDV);

        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }
    }
}