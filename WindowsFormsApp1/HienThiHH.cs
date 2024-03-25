using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public partial class HienThiHH : DevExpress.XtraEditors.XtraForm
    {
        public HienThiHH()
        {
            InitializeComponent();
            LOADDATA();
        }
        void LOADDATA()
        {
            string query = "SELECT MaHangHoa,TenHangHoa,SoLuong,DVT,NhaCungCap,XuatXu,GiaNhap,GhiChu,NgayNhap FROM dbo.tb_hanghoa";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Columns.Add("Mã Hàng", 100);
            listView1.Columns.Add("Tên Hàng", 100);
            listView1.Columns.Add("Tồn kho", 100);
            listView1.Columns.Add("Đơn vị tính", 100);
            listView1.Columns.Add("Nhà cung cấp", 100);
            listView1.Columns.Add("Xuất xứ", 100);
            listView1.Columns.Add("Giá sản phẩm", 100);
            listView1.Columns.Add("Ghi chú", 100);
            listView1.Columns.Add("Ngày nhập", 100);

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
            string id = idTB.Text;
            string name = nameTB.Text;
            string soluong = slTB.Text;
            string ncc = nccTB.Text;
            string dvt = dvtTB.Text;
            string xuatxu = xxTB.Text;
            string price = priceTB.Text;
            string ghichu = noteTB.Text;
            string ngaynhap = "";
            string query = "INSERT INTO dbo.tb_HANGHOA (";
            List<string> updateColumns = new List<string>();
            List<string> columnNames = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách
            if (!string.IsNullOrEmpty(id))
            {
                updateColumns.Add($"N'{id}'");
                columnNames.Add("MaHangHoa");
            }
            if (!string.IsNullOrEmpty(name))
            {
                updateColumns.Add($"N'{name}'");
                columnNames.Add("TenHangHoa");
            }
            //add  TenDVT to the list by MaDVT from database
            if (!string.IsNullOrEmpty(dvt))
            {
                updateColumns.Add($"N'{dvt}'");
                columnNames.Add("DVT");
            }
            if (!string.IsNullOrEmpty(soluong))
            {
                updateColumns.Add($"{soluong}");
                columnNames.Add("SoLuong");
            }

            //add  NhaCungCap to the list by MaNhaCungCap from database
            if (!string.IsNullOrEmpty(ncc))
            {
                updateColumns.Add($"N'{ncc}'");
                columnNames.Add("NhaCungCap");
            }
            if (!string.IsNullOrEmpty(xuatxu))
            {
                updateColumns.Add($"N'{xuatxu}'");
                columnNames.Add("XuatXu");
            }
            if (!string.IsNullOrEmpty(price))
            {
                updateColumns.Add($"{price}");
                columnNames.Add("GiaNhap");
            }
            if (!string.IsNullOrEmpty(ghichu))
            {
                updateColumns.Add($"N'{ghichu}'");
                columnNames.Add("GhiChu");
            }

            if (string.IsNullOrEmpty(ngaynhap))
            {
                ngaynhap = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                updateColumns.Add($"N'{ngaynhap}'");
                columnNames.Add("NgayNhap");
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
        //Edit items event handler by clicking on the edit button use id to update items that not affect data that doesn't need to be updated
        private void editBT_Click(object sender, EventArgs e)
        {
            string name = nameTB.Text;
            string soluong = slTB.Text;
            string id = idTB.Text;
            string ncc = nccTB.Text;
            string dvt = dvtTB.Text;
            string xuatxu = xxTB.Text;
            string price = priceTB.Text;
            string ghichu = noteTB.Text;
            string ngaynhap = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string query = "UPDATE dbo.tb_HANGHOA SET ";
            List<string> updateColumns = new List<string>();

            // Kiểm tra và thêm các cột cần cập nhật vào danh sách
            if (name != "")
                updateColumns.Add($"TenHangHoa = N'{name}'");
            if (soluong != "")
                updateColumns.Add($"SoLuong = {soluong}");
            if (ncc != "")
                updateColumns.Add($"NhaCungCap = N'{ncc}'");
            if (xuatxu != "")
                updateColumns.Add($"XuatXu = N'{xuatxu}'");
            if (dvt != "")
                updateColumns.Add($"XuatXu = N'{dvt}'");
            if (price != "")
                updateColumns.Add($"GiaNhap = {price}");
            if (ghichu != "")
                updateColumns.Add($"GhiChu = N'{ghichu}'");
            if (ngaynhap != "")
                updateColumns.Add($"NgayNhap = N'{ngaynhap}'");

            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
            if (updateColumns.Count > 0)
            {
                string updateSet = string.Join(",", updateColumns);
                query += updateSet + $" WHERE MaHangHoa = N'{id}'";
            }
            else
            {
                // Không có giá trị nào cần cập nhật, do đó không thực hiện truy vấn
                // (hoặc bạn có thể thực hiện một hành động khác tùy ý ở đây)
                // Ví dụ:
                throw new Exception("Không có giá trị nào được cập nhật.");
            }
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

       
        //Delete items event handler by clicking on the delete button use id to delete individual items without affecting data that doesn't need to be deleted
        private void deleteBT_Click(object sender, EventArgs e)
        {
            string id = idTB.Text;
            string query = string.Format("DELETE FROM dbo.tb_HANGHOA WHERE MaHangHoa = N'{0}'", id);
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
        private bool IsItemExists(System.Windows.Forms.ComboBox comboBox, string item)
        {
            foreach (var existingItem in comboBox.Items)
            {
                if (existingItem.ToString() == item)
                {
                    return true;
                }
            }
            return false;
        }
        void AddUniqueItemToComboBox(System.Windows.Forms.ComboBox comboBox, string item)
        {
            if (!IsItemExists(comboBox, item))
            {
                comboBox.Items.Add(item);
            }
        }
        private void comboBox1_Click(object sender, EventArgs e)
        {
            AddUniqueItemToComboBox(comboBox1, "Mã Hàng Hóa");
            AddUniqueItemToComboBox(comboBox1, "Tên Hàng Hóa");
            AddUniqueItemToComboBox(comboBox1, "Xuất Xứ");
            AddUniqueItemToComboBox(comboBox1, "Nhà Cung Cấp");
            AddUniqueItemToComboBox(comboBox1, "Ngày Nhập");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            object selectedItem = comboBox1.SelectedItem;
            string selectedText = selectedItem.ToString();
            string query = "";
            if (selectedText == "Mã Hàng Hóa")
            {
                query = string.Format("SELECT MaHangHoa,TenHangHoa,SoLuong,DVT,NhaCungCap,XuatXu,GiaNhap,GhiChu,NgayNhap FROM dbo.tb_hanghoa Where MaHangHoa like N'{0}'", input);
                MessageBox.Show(query);
            }
            else if (selectedText == "Tên Hàng Hóa") 
            {
                query = string.Format("SELECT TenHangHoa,MaHangHoa,SoLuong,DVT,NhaCungCap,XuatXu,GiaNhap,GhiChu,NgayNhap FROM dbo.tb_hanghoa Where TenHangHoa like N'{0}'", input);
                MessageBox.Show(query);
            }
            else if (selectedText == "Xuất Xứ")
            { 
                query = string.Format("SELECT XuatXu,MaHangHoa,TenHangHoa,SoLuong,DVT,NhaCungCap,GiaNhap,GhiChu,NgayNhap FROM dbo.tb_hanghoa Where XuatXu like N'{0}'", input);
                MessageBox.Show(query);
            }
            else if (selectedText == "Nhà Cung Cấp")
            {
                query = string.Format("SELECT NhaCungCap,MaHangHoa,TenHangHoa,SoLuong,DVT,XuatXu,GiaNhap,GhiChu,NgayNhap FROM dbo.tb_hanghoa Where NhaCungCap like N'{0}'", input);
                MessageBox.Show(query);
            }
            else if (selectedText == "Ngày Nhập")
            {
                query = string.Format("SELECT NgayNhap,MaHangHoa,TenHangHoa,SoLuong,DVT,NhaCungCap,XuatXu,GiaNhap,GhiChu FROM dbo.tb_hanghoa Where NgayNhap like N'{0}'", input);
                MessageBox.Show(query);
            }
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Clear();
            listView1.Columns.Add("Mã Hàng", 100);
            listView1.Columns.Add("Tên Hàng", 100);
            listView1.Columns.Add("Tồn kho", 100);
            listView1.Columns.Add("Đơn vị tính", 100);
            listView1.Columns.Add("Nhà cung cấp", 100);
            listView1.Columns.Add("Xuất xứ", 100);
            listView1.Columns.Add("Giá sản phẩm", 100);
            listView1.Columns.Add("Ghi chú", 100);
            listView1.Columns.Add("Ngày nhập", 100);
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
    }
    }
    
