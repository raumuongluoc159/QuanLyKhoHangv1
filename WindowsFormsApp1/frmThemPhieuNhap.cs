using DevExpress.XtraEditors;
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
using WindowsFormsApp1.DAO;
using static WindowsFormsApp1.qlnx;

namespace WindowsFormsApp1
{
    public partial class frmThemPhieuNhap : DevExpress.XtraEditors.XtraForm
    {
        public frmThemPhieuNhap()
        {

            InitializeComponent();
            LOADDATA();
            
        }
        void LOADDATA()
        {
            using(SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AI8MUAJ\\DEVICE1;Initial Catalog=QuanLyKhoHang;Integrated Security=True"))
            {
                connection.Open();

                string getMaPhieuNhap = "SELECT TOP 1 MaPhieuNhap FROM tb_phieunhap ORDER BY MaPhieuNhap DESC";
                // Tạo đối tượng SqlCommand và gán kết nối và giao dịch

                SqlCommand addPhieuNhap = new SqlCommand(getMaPhieuNhap, connection);

                // Thực thi câu lệnh và lấy mã phiếu nhập mới được sinh ra
                int maPhieuNhap = Convert.ToInt32(addPhieuNhap.ExecuteScalar());
                string query = "SELECT PN.MaPhieuNhap, HH.TenHangHoa, CPN.DonGia, CPN.SoLuong, HH.DVT, HH.GiaNhap, PN.NgayInPhieu\r\n\r\nFROM tb_phieunhap PN\r\n\r\nINNER JOIN tb_phieunhapchitiet CPN ON PN.MaPhieuNhap = CPN.MaPhieuNhap\r\n\r\nINNER JOIN tb_hanghoa HH ON CPN.MaHangHoa = HH.MaHangHoa\r\n Where PN.MaPhieuNhap = "+maPhieuNhap+"";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                listView1.Columns.Clear();
                listView1.Columns.Add("Mã Phiếu Nhập", 100);
                listView1.Columns.Add("Tên HH", 100);
                listView1.Columns.Add("Đơn Giá", 100);
                listView1.Columns.Add("Số Lượng", 100);
                listView1.Columns.Add("Đơn Vị Tính", 100);
                listView1.Columns.Add("Giá Nhập", 100);
                listView1.Columns.Add("Ngày In", 100);
                listView1.View = View.Details;
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader[0].ToString());
                    for (int j = 1; j < reader.FieldCount; j++)
                    {
                        item.SubItems.Add(reader[j].ToString());
                    }
                    listView1.Items.Add(item);
                }
                connection.Close();
            }
        }


        private void frmThemPhieuNhap_Load(object sender, EventArgs e)
        {

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;");
            connection.Open();
            string getMaPhieuNhap = "SELECT TOP 1 MaPhieuNhap FROM tb_phieunhap ORDER BY MaPhieuNhap DESC";
            // Tạo đối tượng SqlCommand và gán kết nối và giao dịch

            SqlCommand addPhieuNhap = new SqlCommand(getMaPhieuNhap, connection);

            // Thực thi câu lệnh và lấy mã phiếu nhập mới được sinh ra
            int maPhieuNhap = Convert.ToInt32(addPhieuNhap.ExecuteScalar());
            //Delete phieu nhap where MaPhieuNhap = maPhieuNhap
            string deletePhieuNhapChiTiet = "DELETE FROM tb_phieunhapchitiet WHERE MaPhieuNhap = " + maPhieuNhap + "";
            SqlCommand command1 = new SqlCommand(deletePhieuNhapChiTiet, connection);
            string deletePhieuNhap = "DELETE FROM tb_phieunhap WHERE MaPhieuNhap = "+maPhieuNhap+"";
            SqlCommand command = new SqlCommand(deletePhieuNhap, connection);
            // If chitietphieunhap with maPhieuNhap exists, delete it.Else, show message box "Không có phiếu nhập để xóa"
            if (command1.ExecuteNonQuery() > 0)
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Hủy thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Không có phiếu nhập để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            //Delete phieu nhap chi tiet where MaPhieuNhap = maPhieuNhap
            connection.Close();


        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //tao phieu nhap moi
            int soLg = Int32.Parse(soLuong.Text);
            HangHoaItem selectedHangHoa = (HangHoaItem)hanghoaBox.SelectedItem;
            int mahanghoa = selectedHangHoa.MaHangHoa;
            decimal thanhtien = selectedHangHoa.GiaNhap * soLg;
            DVTItems selectedDVT = (DVTItems)dvtBox.SelectedItem;
            int DVT = selectedDVT.MaDVT;
            decimal giaNhap = selectedHangHoa.GiaNhap;
            
            void InsertDataToDatabase(int maHangHoa, decimal giaTien, int soLuong, int donViTinh)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;"))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string getMaPhieuNhap = "SELECT TOP 1 MaPhieuNhap FROM tb_phieunhap ORDER BY MaPhieuNhap DESC";


                        SqlCommand addPhieuNhap = new SqlCommand(getMaPhieuNhap, connection, transaction);
                        int maPhieuNhap = Convert.ToInt32(addPhieuNhap.ExecuteScalar());
                        string insertPhieuNhapChiTietQuery = "INSERT INTO tb_phieunhapchitiet (MaPhieuNhap, MaHangHoa, DonGia, SoLuong, DonViTinh) VALUES (@MaPhieuNhap, @MaHangHoa, @DonGia, @SoLuong, @DonViTinh)";

                        SqlCommand commandPhieuNhapChiTiet = new SqlCommand(insertPhieuNhapChiTietQuery, connection, transaction);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@MaHangHoa", maHangHoa);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@DonGia", giaTien);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@DonViTinh", donViTinh);

                        commandPhieuNhapChiTiet.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // Nếu có lỗi xảy ra, rollback giao dịch
                        transaction.Rollback();
                        MessageBox.Show("Thêm dữ liệu thất bại. Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            InsertDataToDatabase(mahanghoa, thanhtien, soLg, DVT);
            listView1.Items.Clear();
            LOADDATA();
        }
   public class DonViItem
        {
            public int MaDonVi { get; set; }
            public string TenDonVi { get; set; }

            public DonViItem(int maDonVi, string tenDonVi)
            {
                MaDonVi = maDonVi;
                TenDonVi = tenDonVi;
            }

            public override string ToString()
            {
                return TenDonVi;
            }
        }
        public class HangHoaItem
        {
            public int MaHangHoa { get; set; }
            public string TenHangHoa { get; set; }
            public decimal GiaNhap { get; set; }

            public HangHoaItem(int maHangHoa, string tenHangHoa, decimal giaNhap)
            {
                MaHangHoa = maHangHoa;
                TenHangHoa = tenHangHoa;
                GiaNhap = giaNhap;
            }

            public override string ToString()
            {
                return TenHangHoa;
            }
        }
        public class TenNhanVienItem
        {
            public int IdNhanVien { get; set; }
            public string TenNhanVien { get; set; }

            public TenNhanVienItem(int idNhanVien, string tenNhanVien)
            {
                IdNhanVien = idNhanVien;
                TenNhanVien = tenNhanVien;
            }

            public override string ToString()
            {
                return TenNhanVien;
            }
        }

        public class DVTItems
        {
            public int MaDVT { get; set; }
            public string TenDVT { get; set; }
            public DVTItems(int maDVT, string tenDVT)
            {
                MaDVT = maDVT;
                TenDVT = tenDVT;
            }
            public override string ToString()
            {
                return TenDVT;
            }
        }

        private bool isDataLoadedHH = false;
        private void LoadDataToComboBoxOnceHH()
        {
            if (!isDataLoadedHH)
            {
                ComboBoxHH();

                isDataLoadedHH = true;
            }
        }
        private bool isDataLoadedTable = false;
        private void LoadDataToTableOnce()
        {
            if (!isDataLoadedTable)
            {
                LoadDataTable();

                isDataLoadedTable = true;
            }
        }
        private void LoadDataTable() {
            listView1.Columns.Add("Tên HH", 100);
            listView1.Columns.Add("Đơn Giá", 100);
            listView1.Columns.Add("Số Lượng", 100);
            listView1.Columns.Add("Đơn Vị Tính", 100);
            listView1.Columns.Add("Giá Nhập", 100);
        }
        private void hanghoaBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceHH();
        }
        
        private bool isDataLoadedDVT = false;
        private void LoadDataToComboBoxOnceDVT()
        {
            if (!isDataLoadedDVT)
            {
                LoadDataToComboBoxDVT();

                isDataLoadedDVT = true;
            }
        }
        private void dvtBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceDVT();
        }
        private void LoadDataToComboBoxDVT()
        {
            // Thực hiện truy vấn để lấy dữ liệu từ cơ sở dữ liệu
            string query = "SELECT maDVT, TenDVT FROM tb_DVT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Kiểm tra xem dữ liệu có được lấy thành công không
            if (data != null && data.Rows.Count > 0)
            {
                // Duyệt qua từng dòng dữ liệu và thêm vào ComboBox
                foreach (DataRow row in data.Rows)
                {
                    string dvt = row["TenDVT"].ToString();
                    int maDVT = Convert.ToInt32(row["MaDVT"]);

                    // Thêm mục vào ComboBox
                    dvtBox.Items.Add(new DVTItems(maDVT, dvt));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu đơn vị để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
       
        private void ComboBoxHH()
        {
            // Thực hiện truy vấn để lấy dữ liệu từ cơ sở dữ liệu
            string query = "SELECT MaHangHoa, TenHangHoa, GiaNhap FROM tb_hanghoa";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Kiểm tra xem dữ liệu có được lấy thành công không
            if (data != null && data.Rows.Count > 0)
            {
                // Duyệt qua từng dòng dữ liệu và thêm vào ComboBox
                foreach (DataRow row in data.Rows)
                {
                    int maHangHoa = Convert.ToInt32(row["MaHangHoa"]);
                    string tenHangHoa = row["TenHangHoa"].ToString();
                    decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);

                    // Thêm mục vào ComboBox
                    hanghoaBox.Items.Add(new HangHoaItem(maHangHoa, tenHangHoa, giaNhap));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu đơn vị để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            // alert if items in listview1 is empty
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Close();
            qlnx qlnx = new qlnx();
            qlnx.LOADDATA();
        }

        
        private void hanghoaBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
