using Microsoft.ReportingServices.Diagnostics.Internal;
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
using System.Xml.Linq;
using WindowsFormsApp1.DAO;
using static WindowsFormsApp1.frmInphieubyid;
using static WindowsFormsApp1.frmThemPhieuNhap;

namespace WindowsFormsApp1
{
    public partial class qlnx : Form
    {
        public qlnx()
        {
            InitializeComponent();
            LOADDATA();
            comboBox1.Text = "Phiếu Nhập";
        }
       public void LOADDATA()
        {
            string query = "SELECT PN.MaPhieuNhap, HH.TenHangHoa, CPN.DonGia, CPN.SoLuong, HH.DVT, HH.GiaNhap, PN.NgayInPhieu\r\n\r\nFROM tb_phieunhap PN\r\n\r\nINNER JOIN tb_phieunhapchitiet CPN ON PN.MaPhieuNhap = CPN.MaPhieuNhap\r\n\r\nINNER JOIN tb_hanghoa HH ON CPN.MaHangHoa = HH.MaHangHoa\r\n";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Columns.Add("Mã Phiếu Nhập", 100);
            listView1.Columns.Add("Tên HH", 100);
            listView1.Columns.Add("Đơn Giá", 100);
            listView1.Columns.Add("Số Lượng", 100);
            listView1.Columns.Add("Đơn Vị Tính", 100);
            listView1.Columns.Add("Giá Nhập", 100);
            listView1.Columns.Add("Ngày In", 100);
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
        // tạo hàm loaddaata để hiển thị danh sách phiếu xuất dựa trên hàm loaddata của phiếu nhập
        public void LOADDATA_XUAT()
        {
            string query = "SELECT PX.MaPhieuXuat, HH.TenHangHoa, CPX.DonGia, CPX.SoLuong, HH.DVT, HH.GiaNhap, PX.NgayInPhieu\r\n\r\nFROM tb_phieuxuat PX\r\n\r\nINNER JOIN tb_phieuxuatchitiet CPX ON PX.MaPhieuXuat = CPX.MaPhieuXuat\r\n\r\nINNER JOIN tb_hanghoa HH ON CPX.MaHangHoa = HH.MaHangHoa\r\n";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            listView1.Columns.Add("Mã Phiếu Xuất", 100);
            listView1.Columns.Add("Tên HH", 100);
            listView1.Columns.Add("Đơn Giá", 100);
            listView1.Columns.Add("Số Lượng", 100);
            listView1.Columns.Add("Đơn Vị Tính", 100);
            listView1.Columns.Add("Giá Xuất", 100);
            listView1.Columns.Add("Ngày Xuất", 100);
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

        private void inphieunhap_Click(object sender, EventArgs e)

        {
            frmInphieubyid frmInphieubyid = new frmInphieubyid();
            frmInphieubyid.Show();
        }
        public DataTable LayDuLieuTheoIdPhieuNhapTextBox(string idPhieuNhap)
        {
            // Gọi phương thức LayDuLieuTheoMaPhieuNhap với tham số là giá trị của TextBox
            return LayDuLieuTheoMaPhieuNhap(idPhieuNhap);
        }
        public DataTable LayDuLieuTheoMaPhieuNhap(string idPhieuNhap)
        {
            // Xây dựng truy vấn SQL kết hợp các bảng và điều kiện
            string query = @"
                SELECT CPN.MaPhieuNhap, CPN.MaHangHoa, CPN.DonGia, CPN.SoLuong,
                PN.MaDonVi, PN.IdNhanVien, PN.NgayInPhieu,
                HH.TenHangHoa, HH.SoLuong AS SoLuongHangHoa, HH.DonViTinh, HH.GiaNhap, HH.GhiChu
                FROM tb_phieunhapchitiet CPN
                INNER JOIN tb_phieunhap PN ON CPN.MaPhieuNhap = PN.MaPhieuNhap
                INNER JOIN tb_hanghoa HH ON CPN.MaHangHoa = HH.MaHangHoa
                WHERE CPN.MaPhieuNhap = @idPhieuNhap";
            // Thực hiện truy vấn và trả về kết quả
            return DataProvider.Instance.ExecuteQuery(query, new object[] { idPhieuNhap });
        }
        private void nhanvienBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceNV();
        }
        private void donviBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnce();
        }

        //lấy giá trị selectedMaDonVi từ ComboBox và sử dụng bên ngoài sự kiện Click
        bool isDataLoadedNV = false;
        private void LoadDataToComboBoxOnceNV()
        {
            if (!isDataLoadedNV)
            {
                LoadDataToComboBoxNV();

                isDataLoadedNV = true;
            }
        }
        bool isDataLoaded = false;
        private void LoadDataToComboBoxOnce()
        {
            if (!isDataLoaded)
            {
                LoadDataToComboBox();

                isDataLoaded = true;
            }
        }
        private void LoadDataToComboBox()
        {
            // Thực hiện truy vấn để lấy dữ liệu từ cơ sở dữ liệu
            string query = "SELECT MaDonVi, TenDonVi FROM tb_donvi";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Kiểm tra xem dữ liệu có được lấy thành công không
            if (data != null && data.Rows.Count > 0)
            {
                // Duyệt qua từng dòng dữ liệu và thêm vào ComboBox
                foreach (DataRow row in data.Rows)
                {
                    int maDonVi = Convert.ToInt32(row["MaDonVi"]);
                    string tenDonVi = row["TenDonVi"].ToString();

                    // Thêm mục vào ComboBox
                    donviBox.Items.Add(new DonViItem(maDonVi, tenDonVi));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu đơn vị để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadDataToComboBoxNV()
        {
            // Thực hiện truy vấn để lấy dữ liệu từ cơ sở dữ liệu
            string query = "SELECT id, TenHienThi FROM tb_taikhoan";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Kiểm tra xem dữ liệu có được lấy thành công không
            if (data != null && data.Rows.Count > 0)
            {
                // Duyệt qua từng dòng dữ liệu và thêm vào ComboBox
                foreach (DataRow row in data.Rows)
                {
                    int idNhanVien = Convert.ToInt32(row["id"]);
                    string tenNhanVien = row["TenHienThi"].ToString();

                    // Thêm mục vào ComboBox
                    nhanvienBox.Items.Add(new TenNhanVienItem(idNhanVien, tenNhanVien));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu nhân viên để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy mã đơn vị từ ComboBox
            using (SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;")) {
                connection.Open();
                DonViItem selectedDonVi = donviBox.SelectedItem as DonViItem;
                // alert khi chưa chọn đơn vị
                if (selectedDonVi == null)
                {
                    MessageBox.Show("Vui lòng chọn đơn vị trước khi thêm phiếu nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int madonvi = selectedDonVi.MaDonVi;
                // Lấy mã nhân viên từ ComboBox
                TenNhanVienItem selectedNhanVien = nhanvienBox.SelectedItem as TenNhanVienItem;
                // alert khi chưa chọn nhân viên
                if (selectedNhanVien == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên trước khi thêm phiếu nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int maNhanVien = selectedNhanVien.IdNhanVien;
                //insert vào bảng tb_phieunhap query
                string insertPhieuNhapQuery = "INSERT INTO tb_phieunhap ( MaDonVi, IdNhanVien,NgayInPhieu) VALUES (@MaDonVi, @MaNhanVien,GETDATE())\r\n";
                SqlCommand commandPhieuNhap = new SqlCommand(insertPhieuNhapQuery, connection);
                commandPhieuNhap.Parameters.AddWithValue("@MaDonVi", madonvi);
                commandPhieuNhap.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                commandPhieuNhap.ExecuteNonQuery();
                MessageBox.Show(insertPhieuNhapQuery);
                frmThemPhieuNhap frmThemPhieuNhap = new frmThemPhieuNhap();
                frmThemPhieuNhap.Show();
                connection.Close();
            };
                
            //refresh lại form qlnx sau khi thêm phiếu nhập
        }
       

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void qlnx_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Phiếu Nhập")
            {
                listView1.Clear();
                LOADDATA();
            }
            else if (comboBox1.Text == "Phiếu Xuất")
            {
                listView1.Clear();
                LOADDATA_XUAT();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Lấy mã đơn vị từ ComboBox
            DonViItem selectedDonVi = donviBox.SelectedItem as DonViItem;
            // Alert khi chưa chọn đơn vị
            if (selectedDonVi == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị trước khi in phiếu xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maDonVi = selectedDonVi.MaDonVi;

            // Lấy mã nhân viên từ ComboBox
            TenNhanVienItem selectedNhanVien = nhanvienBox.SelectedItem as TenNhanVienItem;
            // Alert khi chưa chọn nhân viên
            if (selectedNhanVien == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi t phiếu xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int maNhanVien = selectedNhanVien.IdNhanVien;

            // Thực hiện các thao tác in phiếu xuất ở đây
            // Ví dụ: Insert dữ liệu vào bảng phiếu xuất và sau đó mở form in phiếu xuất
            using (SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;"))
            {
                connection.Open();
                // Thực hiện insert vào bảng tb_phieuxuat
                string insertPhieuXuatQuery = "INSERT INTO tb_phieuxuat (MaDonVi, IdNhanVien, NgayInPhieu) VALUES (@MaDonVi, @MaNhanVien, GETDATE())";
                SqlCommand commandPhieuXuat = new SqlCommand(insertPhieuXuatQuery, connection);
                commandPhieuXuat.Parameters.AddWithValue("@MaDonVi", maDonVi);
                commandPhieuXuat.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                commandPhieuXuat.ExecuteNonQuery();

                // Hiển thị thông báo hoặc thực hiện các thao tác khác nếu cần

                // Mở form in phiếu xuất
                frmThemPhieuXuat frmThemPhieuXuat = new frmThemPhieuXuat();
                frmThemPhieuXuat.Show();

                connection.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
                
            //in phiếu xuất
            frmInphieuxuatbyid frmInphieuxuatbyid = new frmInphieuxuatbyid();
            frmInphieuxuatbyid.Show();

            }

        private void nhanvienBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

