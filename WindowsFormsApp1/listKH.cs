using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public partial class listKH : Form
    {
        public listKH()
        {
            InitializeComponent();
            LOADDATA();
        }
        void LOADDATA()
        {
            string query = "SELECT * FROM dbo.tb_khachhang";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Columns.Add("Mã Khách Hàng", 100);
            listView1.Columns.Add("Tên Khách Hàng", 100);
            listView1.Columns.Add("Địa Chỉ", 100);
            listView1.Columns.Add("Số Điện Thoại", 100);
            listView1.Columns.Add("Email", 100);
            listView1.Columns.Add("Fax", 100);
            listView1.Columns.Add("Ghi Chú", 100);
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

        private void listKH_Load(object sender, EventArgs e)
        {

        }

        private void addBT_Click(object sender, EventArgs e)
        {
            string id = idTB.Text;
            string name = nameTB.Text;
            string address = AddressTB.Text;
            string phone = PhoneTB.Text;
            string email = EmailTB.Text;
            string fax = FaxTB.Text;
            string note = TBnote.Text;
            bool containsNonDigit = id.Any(c => !char.IsDigit(c));
            string query = "INSERT INTO dbo.tb_khachhang (";
            List<string> updateColumns = new List<string>();
            List<string> columnNames = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách
            //id có kiểu dữ liệu int, kiểm tra trước khi thêm vào bảng
            if (!string.IsNullOrEmpty(id) && !containsNonDigit)
            {
                updateColumns.Add($"N'{id}'");
                columnNames.Add("MaKhach");
            }
            else
            {
                MessageBox.Show("ID không hợp lệ");
            }



            if (!string.IsNullOrEmpty(name))
            {
                updateColumns.Add($"N'{name}'");
                columnNames.Add("TenKhach");
            }
            if (!string.IsNullOrEmpty(address))
            {
                updateColumns.Add($"N'{address}'");
                columnNames.Add("DiaChi");
            }
            if (!string.IsNullOrEmpty(phone))
            {
                updateColumns.Add($"N'{phone}'");
                columnNames.Add("SoDienThoai");
            }
            if (!string.IsNullOrEmpty(email))
            {
                updateColumns.Add($"N'{email}'");
                columnNames.Add("Email");
            }
            if (!string.IsNullOrEmpty(fax))
            {
                updateColumns.Add($"N'{fax}'");
                columnNames.Add("Fax");
            }
            if (!string.IsNullOrEmpty(note))
            {
                updateColumns.Add($"N'{note}'");
                columnNames.Add("GhiChu");
            }

            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
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
            string id = idTB.Text;
            string name = nameTB.Text;
            string address = AddressTB.Text;
            string phone = PhoneTB.Text;
            string email = EmailTB.Text;
            string fax = FaxTB.Text;
            string note = TBnote.Text;
            string query = "UPDATE dbo.tb_khachhang SET ";
            List<string> updateColumns = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách
            if (!string.IsNullOrEmpty(name))
            { 
                updateColumns.Add($"TenKhach = N'{name}'");
            }
            if (!string.IsNullOrEmpty(address))
            {
                updateColumns.Add($"DiaChi = N'{address}'");
            }
                if (!string.IsNullOrEmpty(phone))
            {
                updateColumns.Add($"SoDienThoai = N'{phone}'");
            }
                if (!string.IsNullOrEmpty(email))
            {
                updateColumns.Add($"Email = N'{email}'");
            }
                if (!string.IsNullOrEmpty(fax))
            {
                updateColumns.Add($"Fax = N'{fax}'");
            }
                if (!string.IsNullOrEmpty(note))
            {
                updateColumns.Add($"GhiChu = N'{note}'");
            }

            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn

            /*if (updateColumns.Count > 0)
            {
                string updateSet = string.Join(",", updateColumns);
                query += updateSet + $" WHERE MaDV = N'{id}'";
            }
            else
            {
                // Không có giá trị nào cần cập nhật, do đó không thực hiện truy vấn
                // (hoặc bạn có thể thực hiện một hành động khác tùy ý ở đây)
                // Ví dụ:
                MessageBox.Show("Không có giá trị nào được sửa.");
                query = "";
            }*/
            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
            if (updateColumns.Count > 0)
            {
                string updateSet = string.Join(",", updateColumns);
                query += updateSet;
                //while if ID is not null then + $" WHERE ID = N'{id}'", else not + $" WHERE ID = N'{id}'"
                while (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("ID không được để trống");
                    break;
                }
                query += $" WHERE MaKhach = N'{id}'";
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
            string id = idTB.Text;
            string query = string.Format("DELETE FROM dbo.tb_khachhang WHERE MaKhach = N'{0}'", id);
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
    

