using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using static WindowsFormsApp1.frmqlhoadon;

namespace WindowsFormsApp1
{
    public partial class frmqlhoadon : DevExpress.XtraEditors.XtraForm
    {
        public frmqlhoadon()
        {
            InitializeComponent();
        }


        void LoadDatatoGridView()
        {
            string query = "SELECT a.MaHangHoa, b.TenHangHoa, a.SoLuong, b.GiaNhap, a.GiamGia,a.ThanhTien FROM tb_chitiethoadon AS a, tb_hanghoa AS b WHERE a.MaHoaDon = N'" + txtHD.Text + "' AND a.MaHangHoa = b.MaHangHoa;";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            dgvHoaDon.DataSource = data;
            dgvHoaDon.Columns[0].HeaderText = "Mã hàng";
            dgvHoaDon.Columns[1].HeaderText = "Tên hàng";
            dgvHoaDon.Columns[2].HeaderText = "Số lượng";
            dgvHoaDon.Columns[3].HeaderText = "Đơn giá";
            dgvHoaDon.Columns[4].HeaderText = "Giảm giá %";
            dgvHoaDon.Columns[5].HeaderText = "Thành tiền";
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        void LoadInfoHoaDon()
        {
            string query = "SELECT * FROM tb_hoadon WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                txtHD.Text = row["MaHoaDon"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtSDT.Text = row["SDT"].ToString();
                txtTongTien.Text = row["TongTien"].ToString();
                txtGiamGia.Text = row["GiamGia"].ToString();
                nhanvienBox.Text = row["NhanVien"].ToString();
                khachhangBox.Text = row["KhachHang"].ToString();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu hóa đơn để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        private void frmqlhoadon_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            btnIn.Enabled = true;
            txtHD.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            if (txtHD.Text != "")
            {
                LoadInfoHoaDon();
                btnHuy.Enabled = true;
                btnIn.Enabled = true;
            }
            LoadDatatoGridView();
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
        public class TenKhacHangItem
        {
            public int MaKhach { get; set; }
            public string TenKhach { get; set; }

            public TenKhacHangItem(int idKhachHang, string tenKhachHang)
            {
                MaKhach = idKhachHang;
                TenKhach = tenKhachHang;
            }

            public override string ToString()
            {
                return TenKhach;
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
        private void LoadDatatoComboBoxKH()
        {
            string query = "SELECT MaKhach, TenKhach FROM tb_khachhang";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    int idKhachHang = Convert.ToInt32(row["MaKhach"]);
                    string tenKhachHang = row["TenKhach"].ToString();
                    khachhangBox.Items.Add(new TenKhacHangItem(idKhachHang, tenKhachHang));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool isDataLoadedKH = false;
        private void LoadDataToComboBoxOnceKH()
        {
            if (!isDataLoadedKH)
            {
                LoadDatatoComboBoxKH();
                isDataLoadedKH = true;
            }
        }
        private void khachhangBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceKH();
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
        private void hanghoaBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceHH();
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
        private void reloadDataToComboBoxHH()
        {
            hoadonBox.Items.Clear();
            LoadDatatoComboBoxHD();
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
        private void nhanvienBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceNV();
        }

        private void ResetValues()
        {
            //clear all textboxes
            // Kiểm tra và đặt giá trị cho các textbox
            txtHD.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtDonGia.Text = "";
            txtThanhTien.Text = "";
            txtTongTien.Text = "0";
            txtGiamGia.Text = "0";

            // Kiểm tra và đặt giá trị cho các combobox
            if (nhanvienBox.Items.Count > 0)
                nhanvienBox.SelectedIndex = 0;
            if (khachhangBox.Items.Count > 0)
                khachhangBox.SelectedIndex = 0;
            if (hanghoaBox.Items.Count > 0)
                hanghoaBox.SelectedIndex = 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //tao ma hoa don dung ham CreateKey
            btnHuy.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtHD.Text = CreateKey("HD");
            string query = "INSERT INTO tb_hoadon(MaHoaDon, NgayBan) VALUES (N'" + txtHD.Text + "', GETDATE())";

            DataProvider.Instance.ExecuteNonQuery(query);
            LoadDatatoGridView();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            //Them san pham vao hoa don chi tiet va hoa don chinh 
            if (txtHD.Text == "")
            {
                MessageBox.Show("Bạn chưa tạo hóa đơn mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (hanghoaBox.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hàng hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtGiamGia.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giảm giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "SELECT * FROM tb_hanghoa WHERE MaHangHoa = N'" + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);
                decimal thanhTien = Convert.ToDecimal(txtSoLuong.Text) * giaNhap * (1 - Convert.ToDecimal(txtGiamGia.Text) / 100);
                query = "INSERT INTO tb_chitiethoadon (MaHoaDon, MaHangHoa, SoLuong, DonGia, GiamGia, ThanhTien) VALUES (N'" + txtHD.Text + "', " + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa + ", " + txtSoLuong.Text + ", " + txtDonGia.Text + ", " + txtGiamGia.Text + ", " + thanhTien + ")";

                DataProvider.Instance.ExecuteNonQuery(query);
                query = "UPDATE tb_hoadon SET TongTien = TongTien + " + thanhTien + " WHERE MaHoaDon = N'" + txtHD.Text + "'";
                DataProvider.Instance.ExecuteNonQuery(query);
                LoadDatatoGridView();
                txtTongTien.Text = (Convert.ToDecimal(txtTongTien.Text) + thanhTien).ToString();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu hàng hóa để thêm vào hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void khachhangBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT DiaChi, SoDienThoai FROM tb_khachhang WHERE MaKhach = " + ((TenKhacHangItem)khachhangBox.SelectedItem).MaKhach;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtSDT.Text = row["SoDienThoai"].ToString();
            }
        }

        private void hanghoaBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string query = "SELECT GiaNhap FROM tb_hanghoa WHERE MaHangHoa = " + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                txtDonGia.Text = row["GiaNhap"].ToString();
            }
            // tinh thanh tien

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "" && txtGiamGia.Text != "")
            {
                txtThanhTien.Text = (Convert.ToDecimal(txtSoLuong.Text) * Convert.ToDecimal(txtDonGia.Text) * (1 - (Convert.ToDecimal(txtGiamGia.Text) / 100))).ToString();
            }
            //kiểm tra số lượng nhập vào xem còn hay không và thông báo số lượng hàng hóa còn lại trong kho
            string query = "SELECT SoLuong FROM tb_hanghoa WHERE MaHangHoa = " + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0 && txtSoLuong != null)
            {
                DataRow row = data.Rows[0];
                if (Convert.ToInt32(txtSoLuong.Text) > Convert.ToInt32(row["SoLuong"]) && txtSoLuong.Text != null)
                {
                    MessageBox.Show("Số lượng hàng hóa trong kho không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSoLuong.Text = row["SoLuong"].ToString();
                }
            }



        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "" && txtGiamGia.Text != "")
            {
                if (!string.IsNullOrEmpty(txtSoLuong.Text) && !string.IsNullOrEmpty(txtGiamGia.Text))
                {
                    decimal soLuong, donGia;
                    if (decimal.TryParse(txtSoLuong.Text, out soLuong) &&
                        decimal.TryParse(txtDonGia.Text, out donGia))
                    {
                        decimal giamGia;
                        if (decimal.TryParse(txtGiamGia.Text, out giamGia))
                        {
                            txtThanhTien.Text = (soLuong * donGia * (1 - giamGia / 100)).ToString();
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //luu hoa don vao tb_hoadon
            if (txtHD.Text == "")
            {
                MessageBox.Show("Bạn chưa tạo hóa đơn mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // update table hoadon(MaHoaDon,MaNhanVien,NgayBan,MaKhachHang,TongTien)eturn;
            string query = "UPDATE tb_hoadon SET MaNhanVien = " + ((TenNhanVienItem)nhanvienBox.SelectedItem).IdNhanVien + ", MaKhachHang = " + ((TenKhacHangItem)khachhangBox.SelectedItem).MaKhach + ", TongTien = " + txtTongTien.Text + " WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            btnHuy.Enabled = true;
            btnIn.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //Huy hoa don hien tai
            if (txtHD.Text == "")
            {
                MessageBox.Show("Bạn chưa tạo hóa đơn mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "DELETE FROM tb_chitiethoadon WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            query = "DELETE FROM tb_hoadon WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            //message box success or fail
            MessageBox.Show("Hủy hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetValues();
            LoadDatatoGridView();
            btnHuy.Enabled = false;
            btnIn.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;

        }
        //tạo đối tượng items hóa đơn
        public class HoaDonItem
        {
            public string MaHoaDon { get; set; }
            public int maNhanVien { get; set; }
            public string NgayBan { get; set; }
            public int maKhachHang { get; set; }
            public decimal TongTien { get; set; }

            public HoaDonItem(string maHoaDon, int manhanVien, string ngayBan, int makhachHang, decimal tongTien)
            {
                MaHoaDon = maHoaDon;
                NgayBan = ngayBan;
                maNhanVien = manhanVien;
                maKhachHang = makhachHang;
                TongTien = tongTien;
            }
            public override string ToString()
            {
                return MaHoaDon;
            }

        }
        private bool isDataLoadedHD = false;
        private void LoadDataToComboBoxOnceHD()
        {
            if (!isDataLoadedHD)
            {
                LoadDatatoComboBoxHD();
                isDataLoadedHD = true;
            }
        }
        private void LoadDatatoComboBoxHD()
        {
            // Thực hiện truy vấn để lấy dữ liệu từ cơ sở dữ liệu
            string query = "SELECT MaHoaDon, MaNhanVien, NgayBan, MaKhachHang, TongTien FROM tb_hoadon";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Kiểm tra xem dữ liệu có được lấy thành công không
            if (data != null && data.Rows.Count > 0)
            {
                // Duyệt qua từng dòng dữ liệu và thêm vào ComboBox
                foreach (DataRow row in data.Rows)
                {
                    string maHoaDon = (row["MaHoaDon"]).ToString();
                    //Nếu giá trị khác null thì lấy giá trị, ngược lại lấy giá trị mặc định
                    int maNhanVien = row["MaNhanVien"] != DBNull.Value ? Convert.ToInt32(row["MaNhanVien"]) : 0;
                    //Nếu giá trị khác null thì lấy giá trị, ngược lại lấy giá trị mặc định
                    string ngayBan = row["NgayBan"] != DBNull.Value ? row["NgayBan"].ToString() : "";
                    //Nếu giá trị khác null thì lấy giá trị, ngược lại lấy giá trị mặc định
                    int maKhachHang = row["MaKhachHang"] != DBNull.Value ? Convert.ToInt32(row["MaKhachHang"]) : 0;
                    //Nếu giá trị khác null thì lấy giá trị, ngược lại lấy giá trị mặc định
                    decimal TongTien = row["TongTien"] != DBNull.Value ? Convert.ToDecimal(row["TongTien"]) : 0;

                    // Thêm mục vào ComboBox
                    hoadonBox.Items.Add(new HoaDonItem(maHoaDon, maNhanVien, ngayBan, maKhachHang, TongTien));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu đơn vị để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void hoadonBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceHD();
        }
        void loadHoaDon()
        {
            // Lấy giá trị của MaHoaDon từ ComboBox
            string maHoaDon = ((HoaDonItem)hoadonBox.SelectedItem).MaHoaDon;

            //Hiển thị thông tin hóa đơn được chọn từ ComboBox và thông tin các sản phẩm trong hóa đơn đó
            string query = @"SELECT h.MaHoaDon, hh.TenHangHoa AS TenHangHoa, c.SoLuong, c.DonGia, c.GiamGia, c.ThanhTien, h.NgayBan 
                     FROM tb_hoadon h 
                     JOIN tb_chitiethoadon c ON h.MaHoaDon = c.MaHoaDon 
                     JOIN tb_hanghoa hh ON c.MaHangHoa = hh.MaHangHoa 
                     WHERE h.MaHoaDon = '" + maHoaDon + "';";

            // Tạo mảng tham số


            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                //show data to datagridview
                dgvHoaDon.DataSource = data;
                dgvHoaDon.Columns[0].HeaderText = "Mã Hóa Đơn";
                dgvHoaDon.Columns[1].HeaderText = "Tên hàng Hóa";
                dgvHoaDon.Columns[2].HeaderText = "Số lượng";
                dgvHoaDon.Columns[3].HeaderText = "Đơn giá";
                dgvHoaDon.Columns[4].HeaderText = "Giảm giá %";
                dgvHoaDon.Columns[5].HeaderText = "Thành tiền";
                dgvHoaDon.Columns[6].HeaderText = "Ngày Bán";
                dgvHoaDon.AllowUserToAddRows = false;
                dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;

            }
            else
            {
                MessageBox.Show("Không có dữ liệu hóa đơn để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void btnShowHD_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            loadHoaDon();
            //load vào các textbox thông tin hóa đơn
            txtHD.Text = ((HoaDonItem)hoadonBox.SelectedItem).MaHoaDon;
            txtTongTien.Text = ((HoaDonItem)hoadonBox.SelectedItem).TongTien.ToString();
            txtDiaChi.Text = ((HoaDonItem)hoadonBox.SelectedItem).NgayBan;
            txtSDT.Text = ((HoaDonItem)hoadonBox.SelectedItem).maKhachHang.ToString();
            nhanvienBox.Text = ((HoaDonItem)hoadonBox.SelectedItem).maNhanVien.ToString();
            khachhangBox.Text = ((HoaDonItem)hoadonBox.SelectedItem).maKhachHang.ToString();

        }
        public class HoaDonService
        {
            public DataTable LayHoaDon(string maHoaDon)
            {
                string query = @"SELECT hd.MaHoaDon, tk.TenHienThi , kh.TenKhach , kh.DiaChi , kh.SoDienThoai, hh.TenHangHoa, cthd.SoLuong, cthd.DonGia, cthd.GiamGia, cthd.ThanhTien, hd.TongTien, hd.NgayBan
                     FROM tb_hoadon hd
                     INNER JOIN tb_taikhoan tk ON hd.MaNhanVien = tk.id
                     INNER JOIN tb_khachhang kh ON hd.MaKhachHang = kh.MaKhach
                     INNER JOIN tb_chitiethoadon cthd ON hd.MaHoaDon = cthd.MaHoaDon
                     INNER JOIN tb_hanghoa hh ON cthd.MaHangHoa = hh.MaHangHoa
                     WHERE hd.MaHoaDon =  '" + maHoaDon+"'";


                return DataProvider.Instance.ExecuteQuery(query);
            }
        }
        public class HoaDon
        {
            public string MaHoaDon { get; set; }
            public string TenHienThi { get; set; }
            public string TenKhachHang { get; set; }
            public decimal TongTien { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
            public string TenHangHoa { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public decimal GiamGia { get; set; }
            public decimal ThanhTien { get; set; }
            public DateTime NgayBan { get; set; }
        }
        private void btnIn_Click(object sender, EventArgs e) {
            string maHoaDon = ((HoaDonItem)hoadonBox.SelectedItem).MaHoaDon;
            if(maHoaDon == "")
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HoaDonService service = new HoaDonService();
            DataTable dataTable = service.LayHoaDon(maHoaDon);
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            inHoaDon inHoaDon = new inHoaDon();
            inHoaDon.SetDataSource(dataTable);
            frmInbaocao frm = new frmInbaocao();
            frm.crystalReportViewer1.ReportSource = inHoaDon;
            frm.ShowDialog();



        }


        private void button1_Click(object sender, EventArgs e)
        {
            //sửa hóa đơn
            if (txtHD.Text == "")
            {
                MessageBox.Show("Bạn chưa tạo hóa đơn mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (hanghoaBox.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn hàng hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtGiamGia.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giảm giá.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string query = "SELECT * FROM tb_hanghoa WHERE MaHangHoa = N'" + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data != null && data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);
                decimal thanhTien = Convert.ToDecimal(txtSoLuong.Text) * giaNhap * (1 - Convert.ToDecimal(txtGiamGia.Text) / 100);
                query = "UPDATE tb_chitiethoadon SET SoLuong = " + txtSoLuong.Text + ", DonGia = " + txtDonGia.Text + ", GiamGia = " + txtGiamGia.Text + ", ThanhTien = " + thanhTien + " WHERE MaHoaDon = N'" + txtHD.Text + "' AND MaHangHoa = " + ((HangHoaItem)hanghoaBox.SelectedItem).MaHangHoa;
                DataProvider.Instance.ExecuteNonQuery(query);
                query = "UPDATE tb_hoadon SET TongTien = TongTien + " + thanhTien + " WHERE MaHoaDon = N'" + txtHD.Text + "'";
                DataProvider.Instance.ExecuteNonQuery(query);
                LoadDatatoGridView();
                txtTongTien.Text = (Convert.ToDecimal(txtTongTien.Text) + thanhTien).ToString();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu hàng hóa để thêm vào hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dgvHoaDon_MouseClick(object sender, MouseEventArgs e)
        {
            //Tạo context menu strip để xóa sản phẩm trong hóa đơn
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Xóa sản phẩm");
            contextMenuStrip.Items.Add(toolStripMenuItem);
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            dgvHoaDon.ContextMenuStrip = contextMenuStrip;
        }
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Xóa sản phẩm trong hóa đơn
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                string maHoaDon = dgvHoaDon.SelectedRows[0].Cells[0].Value.ToString();
                string tenHangHoa = dgvHoaDon.SelectedRows[0].Cells[1].Value.ToString();
                //lay ma hang hoa bang ten hang hoa
                string query = "DELETE FROM tb_chitiethoadon WHERE MaHoaDon = N'" + maHoaDon + "' AND MaHangHoa = (SELECT MaHangHoa FROM tb_hanghoa WHERE TenHangHoa = N'" + tenHangHoa + "')";
                DataProvider.Instance.ExecuteNonQuery(query);
                query = "UPDATE tb_hoadon SET TongTien = TongTien - " + Convert.ToDecimal(dgvHoaDon.SelectedRows[0].Cells[5].Value) + " WHERE MaHoaDon = N'" + maHoaDon + "'";
                DataProvider.Instance.ExecuteNonQuery(query);
                LoadDatatoGridView();
                //Lấy tổng tiền từ database và hiển thị lên textbox
                query = "SELECT TongTien FROM tb_hoadon WHERE MaHoaDon = N'" + maHoaDon + "'";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                DataRow row = data.Rows[0];
                txtTongTien.Text = row["TongTien"].ToString();
                txtTongTien.Text = (Convert.ToDecimal(txtTongTien.Text) - Convert.ToDecimal(dgvHoaDon.SelectedRows[0].Cells[5].Value)).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            //Xóa hóa đơn
            if (txtHD.Text == "")
            {
                MessageBox.Show("Bạn chưa tạo hóa đơn mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            string query = "DELETE FROM tb_chitiethoadon WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            query = "DELETE FROM tb_hoadon WHERE MaHoaDon = N'" + txtHD.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
            //message box success or fail
            MessageBox.Show("Xóa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetValues();
            LoadDatatoGridView();
            hoadonBox.SelectedItem = null;
            reloadDataToComboBoxHH();
            txtHD.Text ="";
            btnHuy.Enabled = false;
            btnIn.Enabled = true;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;

        }

        private void hoadonBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void khachhangBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

