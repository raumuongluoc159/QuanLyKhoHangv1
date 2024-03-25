using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Text;
            /*

            string connectionSTR = "Data Source=DESKTOP-AI8MUAJ\\DEVICE1;Initial Catalog=QuanLyKhoHang;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionSTR);
            string query = "SELECT * FROM dbo.tb_TAIKHOAN WHERE Username = @UserName AND Password = @PassWord AND Type = 1;";
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", username);
            command.Parameters.AddWithValue("@PassWord", password);
            connection.Close();
            
           

            string query = "SELECT [UserName], [PassWord] FROM dbo.tb_TAIKHOAN WHERE type = 1;";
            List<User> users = new List<User>();
            
            using (DataTable data = DataProvider.Instance.ExecuteQuery(query))
            {
                foreach (DataRow row in data.Rows)
                {
                    // Lấy giá trị của cột username và password từ mỗi dòng
                    string username = row["username"].ToString();
                    string password = row["password"].ToString();

                    // Tạo một đối tượng User và thêm vào danh sách
                    users.Add(new User { Username = username, Password = password });

                }
            }
            bool UserCheck = false;
                foreach (var user in users)
                {
                if(txtUser.Text == user.Username && txtPass.Text == user.Password)
                {
                     UserCheck = true;
                }
                
            }
                if(UserCheck == true)
            {
                Form2 form2 = new Form2();
                form2.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }
             class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
             */ //old login
            //lession login
            if (login(username, password))
            {
                string USERname = txtUser.Text;
                string PASSword = txtPass.Text;
                // Tạo một đối tượng của form 2

                // Gán giá trị người dùng cho thuộc tính của form 2

                Form2 loginForm = new Form2();
                loginForm.Username = tenTaiKhoan(USERname, PASSword);
                loginForm.Password = PASSword;
                loginForm.Type = PhanLoaiTaiKhoan(USERname, PASSword);
                loginForm.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }

        }
        bool login(string userName,string passWord)
        {
            return AccountDAO.Instance.login(userName,passWord);
        }
        string PhanLoaiTaiKhoan(string username, string password)
        {
            return PhanLoaiTK.Instance.PhanLoaiTaiKhoan(username, password);
        }
        string tenTaiKhoan(string username, string password)
        {
            return PhanLoaiTK.Instance.tenTaiKhoan(username, password);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }



        private void label4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên");

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Reg reg = new Reg();
            reg.Show();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else if (checkBox1.Checked == false)
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát Chương Trình ?", "Thông Báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtUser.Focus();
        }
    }
}
