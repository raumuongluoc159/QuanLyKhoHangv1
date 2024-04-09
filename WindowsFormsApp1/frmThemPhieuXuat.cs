using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public partial class frmThemPhieuXuat : DevExpress.XtraEditors.XtraForm
    {
        public frmThemPhieuXuat()
        {
            InitializeComponent();
            LOADDATA();
        }

        void LOADDATA()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;"))
            {
                connection.Open();

                string getMaPhieuXuat = "SELECT TOP 1 MaPhieuXuat FROM tb_phieuxuat ORDER BY MaPhieuXuat DESC";
                SqlCommand addPhieuXuat = new SqlCommand(getMaPhieuXuat, connection);
                int maPhieuXuat = Convert.ToInt32(addPhieuXuat.ExecuteScalar());
                string query = "SELECT PX.MaPhieuXuat, HH.TenHangHoa, CPX.DonGia, CPX.SoLuong, HH.DVT, HH.GiaNhap, PX.NgayInPhieu\r\n\r\nFROM tb_phieuxuat PX\r\n\r\nINNER JOIN tb_phieuxuatchitiet CPX ON PX.MaPhieuXuat = CPX.MaPhieuXuat\r\n\r\nINNER JOIN tb_hanghoa HH ON CPX.MaHangHoa = HH.MaHangHoa\r\n Where PX.MaPhieuXuat = " + maPhieuXuat + "";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                listView1.Columns.Clear();
                listView1.Columns.Add("Mã Phiếu Xuất", 100);
                listView1.Columns.Add("Tên HH", 100);
                listView1.Columns.Add("Đơn Giá", 100);
                listView1.Columns.Add("Số Lượng", 100);
                listView1.Columns.Add("Đơn Vị Tính", 100);
                listView1.Columns.Add("Giá Xuất", 100);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;");
            connection.Open();
            string getMaPhieuXuat = "SELECT TOP 1 MaPhieuXuat FROM tb_phieuxuat ORDER BY MaPhieuXuat DESC";
            SqlCommand addPhieuXuat = new SqlCommand(getMaPhieuXuat, connection);
            int maPhieuXuat = Convert.ToInt32(addPhieuXuat.ExecuteScalar());

            string deletePhieuXuatChiTiet = "DELETE FROM tb_phieuxuatchitiet WHERE MaPhieuXuat = " + maPhieuXuat + "";
            SqlCommand command1 = new SqlCommand(deletePhieuXuatChiTiet, connection);
            string deletePhieuXuat = "DELETE FROM tb_phieuxuat WHERE MaPhieuXuat = " + maPhieuXuat + "";
            SqlCommand command = new SqlCommand(deletePhieuXuat, connection);

            if (command1.ExecuteNonQuery() > 0)
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Hủy thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Không có phiếu xuất để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            connection.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //alert if txtsoLuong is empty
            if (txtsoLuong.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int soLg = Int32.Parse(txtsoLuong.Text);
            HangHoaItem selectedHangHoa = (HangHoaItem)hanghoaBox.SelectedItem;
            int mahanghoa = selectedHangHoa.MaHangHoa;
            decimal thanhtien = selectedHangHoa.GiaNhap * soLg;
            DVTItems selectedDVT = (DVTItems)dvtBox.SelectedItem;
            int DVT = selectedDVT.MaDVT;
            decimal GiaNhap = selectedHangHoa.GiaNhap;

            void InsertDataToDatabase(int maHangHoa, decimal giaTien, int soLuong, int donViTinh)
            {
                using (SqlConnection connection = new SqlConnection("Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;"))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string getMaPhieuXuat = "SELECT TOP 1 MaPhieuXuat FROM tb_phieuxuat ORDER BY MaPhieuXuat DESC";
                        SqlCommand addPhieuXuat = new SqlCommand(getMaPhieuXuat, connection, transaction);
                        int maPhieuXuat = Convert.ToInt32(addPhieuXuat.ExecuteScalar());

                        string insertPhieuXuatChiTietQuery = "INSERT INTO tb_phieuxuatchitiet (MaPhieuXuat, MaHangHoa, DonGia, SoLuong, DonViTinh) VALUES (@MaPhieuXuat, @MaHangHoa, @DonGia, @SoLuong, @DonViTinh)";
                        SqlCommand commandPhieuXuatChiTiet = new SqlCommand(insertPhieuXuatChiTietQuery, connection, transaction);
                        commandPhieuXuatChiTiet.Parameters.AddWithValue("@MaPhieuXuat", maPhieuXuat);
                        commandPhieuXuatChiTiet.Parameters.AddWithValue("@MaHangHoa", maHangHoa);
                        commandPhieuXuatChiTiet.Parameters.AddWithValue("@DonGia", giaTien);
                        commandPhieuXuatChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
                        commandPhieuXuatChiTiet.Parameters.AddWithValue("@DonViTinh", donViTinh);

                        commandPhieuXuatChiTiet.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Thêm dữ liệu thất bại. Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            InsertDataToDatabase(mahanghoa, thanhtien, soLg, DVT);
            listView1.Items.Clear();
            LOADDATA();
        }

        private void frmThemPhieuXuat_FormClosing(object sender, FormClosingEventArgs e)
        {
            string query = "DELETE FROM tb_chitietphieuxuat WHERE MaPhieuXuat = (SELECT TOP 1 MaPhieuXuat FROM tb_phieuxuat ORDER BY MaPhieuXuat DESC);DELETE FROM tb_phieuxuat WHERE MaPhieuXuat = (SELECT TOP 1 MaPhieuXuat FROM tb_phieuxuat ORDER BY MaPhieuXuat DESC)";

            if (listView1.Items.Count == 0)
            {
                DataProvider.Instance.ExecuteNonQuery(query);
            }
            else
            {
                DialogResult = MessageBox.Show("Dữ liệu sẽ bị xóa khi chưa được lưu !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult == DialogResult.OK)
                {
                    DataProvider.Instance.ExecuteNonQuery(query);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void hanghoaBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceHH();
        }
        bool isDataLoadedHH = false;
        private void LoadDataToComboBoxOnceHH()
        {
            if (!isDataLoadedHH)
            {
                ComboBoxHH();

                isDataLoadedHH = true;
            }
        }

        private void ComboBoxHH()
        {
            string query = "SELECT MaHangHoa, TenHangHoa, GiaNhap FROM tb_hanghoa";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    int maHangHoa = Convert.ToInt32(row["MaHangHoa"]);
                    string tenHangHoa = row["TenHangHoa"].ToString();
                    decimal giaNhap = Convert.ToDecimal(row["GiaNhap"]);

                    hanghoaBox.Items.Add(new HangHoaItem(maHangHoa, tenHangHoa, giaNhap));
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu hàng hóa để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool isDataLoadedDVT = false;

        private void dvtBox_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceDVT();
        }

        private void LoadDataToComboBoxOnceDVT()
        {
            if (!isDataLoadedDVT)
            {
                LoadDataToComboBoxDVT();

                isDataLoadedDVT = true;
            }
        }

        private void LoadDataToComboBoxDVT()
        {
            string query = "SELECT maDVT, TenDVT FROM tb_DVT";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    string dvt = row["TenDVT"].ToString();
                    int maDVT = Convert.ToInt32(row["MaDVT"]);

                    dvtBox.Items.Add(new DVTItems(maDVT, dvt));
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
            // Thêm mã xử lý tại đây nếu cần thiết
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
    }
}
