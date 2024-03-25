using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using static WindowsFormsApp1.Form1;

namespace WindowsFormsApp1
{
    public partial class Form2 : DevExpress.XtraEditors.XtraForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public Form2()
        {
            InitializeComponent();

        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if(currentFormChild != null) {
            currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel=false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Form2_Load(object sender, EventArgs e) {
            username.Text = Username;
            loaiTK.Text = Type;
            //if loaiTK = 0 => admin, loaiTK = 1 => staff, loaiTK = 2 => stock_manager,loaiTK = 3 => boss
            if(Type == "admin")
            {
                loaiTK.Text = "Admin";
                qlkhBT.Visible = true; // - false
                qlspBT.Visible = true; //false - false 
                qltkBT.Visible = true;
                reportBT.Visible = true;
                qlhdBT.Visible = true;
            }
            else if (Type == "staff")
            {
                loaiTK.Text = "Nhân viên";
                qlkhBT.Visible = true;
                qlspBT.Visible = true;
                qltkBT.Enabled = false;
                reportBT.Enabled = false;
                qlhdBT.Enabled = false;
            }
            else if (Type == "keeper")
            {
                loaiTK.Text = "Thủ kho";
                qltkBT.Enabled = false;
            }
            else if (Type == "accountant")
            {
                loaiTK.Text = "Kế toán";
                qltkBT.Enabled = false;
               
            }
            else if (Type == "boss")
            {
                loaiTK.Text = "Giám đốc";
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        { 
                Application.Exit();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void navBarItem24_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenChildForm(new HienThiHH());

        }
        private void dskhItem_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            OpenChildForm(new listKH());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new listKH());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HienThiHH());
        }

        private void qltkBT_Click(object sender, EventArgs e)
        {
            OpenChildForm(new qltk());
        }

        private void reportBT_Click(object sender, EventArgs e)
        {

        }

        private void qlhdBT_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void qlnx_Click(object sender, EventArgs e)
        {
            OpenChildForm(new qlnx());
        }
    }
    }
