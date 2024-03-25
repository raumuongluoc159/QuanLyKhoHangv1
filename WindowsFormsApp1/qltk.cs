using DevExpress.XtraEditors.Filtering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.DAO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp1
{
    public partial class qltk : Form
    {
        public qltk()
        {
            InitializeComponent();
            LOADDATA();
        }
        void LOADDATA()
        {
            string query = "SELECT * FROM dbo.tb_TAIKHOAN";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Columns.Add("ID Tài Khoản", 100);
            listView1.Columns.Add("Tên Hiển Thị", 100);
            listView1.Columns.Add("Username", 100);
            listView1.Columns.Add("Password", 100);
            listView1.Columns.Add("Type", 100);
            listView1.View = View.Details;
            for (int i = 0; i < result.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(result.Rows[i][0].ToString());
                for (int j = 1; j < result.Columns.Count; j++)
                {
                    item.SubItems.Add(result.Rows[i][j].ToString());
                }
                listView1.Items.Add(item);
            }
        }

        private void addBT_Click(object sender, EventArgs e)
        {
            string TenHienThi = nameTB.Text;
            string LoaiTaiKhoan = typeTB.Text;
            string pass = passTB.Text;
            string username = usernameTB.Text;
            string query = "INSERT INTO dbo.tb_TAIKHOAN (";
            List<string> updateColumns = new List<string>();
            List<string> columnNames = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách

            if (!string.IsNullOrEmpty(TenHienThi))
            {
                updateColumns.Add($"N'{TenHienThi}'");
                columnNames.Add("TenHienThi");
            }
            if (!string.IsNullOrEmpty(LoaiTaiKhoan))
            {
                updateColumns.Add($"N'{LoaiTaiKhoan}'");
                columnNames.Add("LoaiTaiKhoan");
            }
            if (!string.IsNullOrEmpty(pass))
            {
                updateColumns.Add($"N'{pass}'");
                columnNames.Add("PassWord");
            }
            if (!string.IsNullOrEmpty(username))
            {
                updateColumns.Add($"N'{username}'");
                columnNames.Add("UserName");
            }

            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
            // Lỗi sql
            if (updateColumns.Count > 0)
            {
                string columnNamesStr = string.Join(", ", columnNames);
                string updateValues = string.Join(", ", updateColumns);
                query += $"{columnNamesStr}) VALUES ({updateValues})";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                if (result > 0)
                {
                    MessageBox.Show("Thêm thành công");
                    listView1.Clear();
                    LOADDATA();
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = "";
                        }
                    }
                }
            }
            else
            {
                // Không có giá trị nào cần cập nhật, do đó không thực hiện truy vấn
                // (hoặc bạn có thể thực hiện một hành động khác tùy ý ở đây)
                // Ví dụ:
                MessageBox.Show("Không có giá trị nào được thêm.");
            }
        }

        private void editBT_Click(object sender, EventArgs e)
        {
            string id = ID.Text;
            string TenHienThi = nameTB.Text;
            string LoaiTaiKhoan = typeTB.Text;
            string pass = passTB.Text;
            string username = usernameTB.Text;
            string query = "UPDATE dbo.tb_TAIKHOAN SET ";
            List<string> updateColumns = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách
            if (!string.IsNullOrEmpty(TenHienThi))
            {
                updateColumns.Add($"TenHienThi = N'{TenHienThi}'");
            }
            if (!string.IsNullOrEmpty(LoaiTaiKhoan))
            {
                updateColumns.Add($"LoaiTaiKhoan = N'{LoaiTaiKhoan}'");
            }
            if (!string.IsNullOrEmpty(pass))
            {
                updateColumns.Add($"PassWord = N'{pass}'");
            }
            if (!string.IsNullOrEmpty(username))
            {
                updateColumns.Add($"UserName = N'{username}'");
            }
            
            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
            if (updateColumns.Count > 0)
            {
                string updateSet = string.Join(",", updateColumns);
                query += updateSet;
                //while if ID is not null then + $" WHERE ID = N'{id}'", else not + $" WHERE ID = N'{id}'"
               while(string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("ID không được để trống");
                    break;
                }
               query += $" WHERE ID = N'{id}'";

                int result = DataProvider.Instance.ExecuteNonQuery(query);

                if (result > 0)
                {
                    MessageBox.Show("Sửa thành công");
                    listView1.Clear();
                    LOADDATA();
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sửa thất bại");
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = "";
                        }
                    }
                }
            }
            else
            {
                // Không có giá trị nào cần cập nhật, do đó không thực hiện truy vấn
                // (hoặc bạn có thể thực hiện một hành động khác tùy ý ở đây)
                // Ví dụ:
                MessageBox.Show("Không có giá trị nào được sửa.");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = ID.Text;
            string query = string.Format("DELETE FROM dbo.tb_TAIKHOAN WHERE ID = N'{0}'", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            if (result > 0)
            {
                MessageBox.Show("Xóa thành công");
                listView1.Clear();
                LOADDATA();
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = "";
                    }
                }
            }
        }
    }


}
