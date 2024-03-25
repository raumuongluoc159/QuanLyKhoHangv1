using DATALAYER;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KHOHANG
{
    public partial class frmConnectDB : DevExpress.XtraEditors.XtraForm
    {
        public frmConnectDB()
        {
            InitializeComponent();
        }
        SqlConnection GetConnection(string server,string username,string pass,string database) {
            return new SqlConnection("Data source=" + server + "; Initial Catalog="+database+";User ID="+username
                +"Password="+pass+";"); 
        }

        private void frmConnectDB_Load(object sender, EventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = GetConnection(txtServer.Text, txtUsername.Text, txtPassword.Text, cboDatabase.Text);
            try
            {
                sqlConnection.Open();
                MessageBox.Show("Kết nối thành công.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { 
            MessageBox.Show("Kết nối thất bại.","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string enCryptServ = Encryptor.Encrypt(txtServer.Text,"qwertyuiop",true);
            string enCryptPass = Encryptor.Encrypt(txtPassword.Text, "qwertyuiop", true);
            string enCryptData = Encryptor.Encrypt(cboDatabase.Text, "qwertyuiop", true);
            string enCryptUser = Encryptor.Encrypt(txtUsername.Text, "qwertyuiop", true);
            connect cn = new connect(enCryptData, enCryptUser, enCryptPass, enCryptData);
            cn.SaveFile();
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboDatabase_Click(object sender, EventArgs e)
        {
            cboDatabase.Items.Clear();
            try
            {
                string Conn = "server=" + txtServer.Text + ";User Id =" + txtUsername.Text + ";pwd" + txtPassword.Text + ";";
                SqlConnection con = new SqlConnection(Conn);
                con.Open();
                string sql = "select name form sys.database WHERE name NOT IN('master','tempdb','model','msdb')";
                SqlCommand cmd = new SqlCommand(sql, con);
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cboDatabase.Items.Add(dr[0].ToString());
                }

            }
            catch(Exception ex) {
                MessageBox.Show("Lỗi: "+ex.Message,"Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}