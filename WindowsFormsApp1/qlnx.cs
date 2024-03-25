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

namespace WindowsFormsApp1
{
    public partial class qlnx : Form
    {
        public qlnx()
        {
            InitializeComponent();
            LOADDATA();
        }
        void LOADDATA()
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

        private void inphieunhap_Click(object sender, EventArgs e)

        {
            // Lấy giá trị từ TextBox idphieunhap.Text
            string idPhieuNhap = dateTimePicker1.Text;


            // Gọi phương thức để lấy dữ liệu từ database dựa trên MaPhieuNhap
            DataTable data = LayDuLieuTheoIdPhieuNhapTextBox(idPhieuNhap);

            PhieuNhap r = new PhieuNhap();
            r.SetDataSource(data);
            frmInbaocao f = new frmInbaocao();
            f.crystalReportViewer1.ReportSource = r;
            f.ShowDialog();


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
                WHERE CPN.MaPhieuNhap = @idPhieuNhap"
;
            MessageBox.Show(query);
            // Thực hiện truy vấn và trả về kết quả
            return DataProvider.Instance.ExecuteQuery(query, new object[] { idPhieuNhap });
        }
        private int selectedMaDonVi = 0;
        
        //lấy giá trị selectedMaDonVi từ ComboBox và sử dụng bên ngoài sự kiện Click



        private void button1_Click(object sender, EventArgs e)
        {
            //tao phieu nhap moi
            int soLg = Int32.Parse(soLuong.Text);
            TenNhanVienItem tenNhanVienItem = (TenNhanVienItem)nhanvienBox.SelectedItem;
            int idNVien = tenNhanVienItem.IdNhanVien;
            HangHoaItem selectedHangHoa = (HangHoaItem)hanghoaBox.SelectedItem;
            int mahanghoa = selectedHangHoa.MaHangHoa;
            decimal thanhtien = selectedHangHoa.GiaNhap * soLg;
            DVTItems selectedDVT = (DVTItems)dvtBox.SelectedItem;
            int DVT = selectedDVT.MaDVT;
            DonViItem selectedDonVi = (DonViItem)donviBox.SelectedItem;
            int selectedMaDonVi = selectedDonVi.MaDonVi;
            decimal giaNhap = selectedHangHoa.GiaNhap;
            /*/them phieu vao database
            string query = "INSERT INTO dbo.tb_phieunhap (";
            List<string> updateColumns = new List<string>();
            List<string> columnNames = new List<string>();

            if (!string.IsNullOrEmpty(id))
            {
                updateColumns.Add($"N'{id}'");
                columnNames.Add("idNhanVien");
            }
            if (string.IsNullOrEmpty(ngayIn) && !string.IsNullOrEmpty(id))
            {
                ngayIn = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                updateColumns.Add($"N'{ngayIn}'");
                columnNames.Add("NgayNhap");
            }
            if (!string.IsNullOrEmpty(soL))
            {
                updateColumns.Add($"{soL}");
                columnNames.Add("SoLuong");
            }
            if (!string.IsNullOrEmpty(donViTinh))
            {
                updateColumns.Add($"N'{donViTinh}'");
                columnNames.Add("DVT");
            }

            if (!string.IsNullOrEmpty(giaNhap))
            {
                updateColumns.Add($"N'{giaNhap}'");
                columnNames.Add("DonGia");
            }



            // Nếu không có cột nào được cập nhật, không thực hiện truy vấn
            if (updateColumns.Count > 0)
            {
                string columnNamesStr = string.Join(", ", columnNames);
                string updateValues = string.Join(", ", updateColumns);
                query += $"{columnNamesStr}) VALUES ({updateValues})";
                MessageBox.Show(query);
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
            }*/
            //THêm phiếu nhập vào database
             void InsertDataToDatabase(int maHangHoa,int madonvi, int maNhanVien, decimal giaTien, int soLuong, int donViTinh)
            {
                // Tạo kết nối mới
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-AI8MUAJ\\DEVICE1;Initial Catalog=QuanLyKhoHang;Integrated Security=True"))
                {
                    // Mở kết nối
                    connection.Open();

                    // Bắt đầu giao dịch
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Câu lệnh SQL INSERT cho bảng Phiếu Nhập
                        string insertPhieuNhapQuery = "INSERT INTO tb_phieunhap (MaDonVi,IdNhanVien, NgayInPhieu) VALUES (@madonvi, @MaNhanVien, GETDATE()); SELECT SCOPE_IDENTITY();";

                        // Tạo đối tượng SqlCommand và gán kết nối và giao dịch
                        SqlCommand commandPhieuNhap = new SqlCommand(insertPhieuNhapQuery, connection, transaction);
                        commandPhieuNhap.Parameters.AddWithValue("@MaDonVi", madonvi);
                        commandPhieuNhap.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                        // Thực thi câu lệnh và lấy mã phiếu nhập mới được sinh ra
                        int maPhieuNhap = Convert.ToInt32(commandPhieuNhap.ExecuteScalar());

                        // Câu lệnh SQL INSERT cho bảng Phiếu Nhập Chi Tiết
                        string insertPhieuNhapChiTietQuery = "INSERT INTO tb_phieunhapchitiet (MaPhieuNhap, MaHangHoa, DonGia, SoLuong, DonViTinh) VALUES (@MaPhieuNhap, @MaHangHoa, @DonGia, @SoLuong, @DonViTinh)";

                        // Tạo đối tượng SqlCommand và gán kết nối và giao dịch
                        SqlCommand commandPhieuNhapChiTiet = new SqlCommand(insertPhieuNhapChiTietQuery, connection, transaction);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@MaHangHoa", maHangHoa);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@DonGia", giaTien);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                        commandPhieuNhapChiTiet.Parameters.AddWithValue("@DonViTinh", donViTinh);

                        // Thực thi câu lệnh INSERT cho bảng Phiếu Nhập Chi Tiết
                        commandPhieuNhapChiTiet.ExecuteNonQuery();

                        // Commit giao dịch nếu mọi thứ thành công
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
            InsertDataToDatabase(mahanghoa, selectedMaDonVi, idNVien, thanhtien, soLg, DVT);

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

        public class  DVTItems
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

        private bool isDataLoaded = false;
        private void LoadDataToComboBoxOnce()
        {
            if (!isDataLoaded)
            {
                LoadDataToComboBox();

                isDataLoaded = true;
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
        private void hanghoaBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceHH();
        }
        private void donviBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnce();
        }
        private void nhanvienBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceNV();
        }
        private bool isDataLoadedNV = false;
        private void LoadDataToComboBoxOnceNV()
        {
            if (!isDataLoadedNV)
            {
                LoadDataToComboBoxNV();

                isDataLoadedNV = true;
            }
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
                   hanghoaBox.Items.Add(new HangHoaItem(maHangHoa, tenHangHoa,giaNhap));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu đơn vị để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}

