using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public partial class frmInphieuxuatbyid : DevExpress.XtraEditors.XtraForm
    {
        public frmInphieuxuatbyid()
        {
            InitializeComponent();
        }

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                XtraMessageBox.Show("Bạn chưa chọn mã phiếu xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }
            return true;
        }

        private bool isLoad = false;

        private void IDphieuxuat()
        {
            string query = "select MaPhieuXuat from tb_phieuxuat";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "MaPhieuXuat";
        }

        private void isLoadid()
        {
            if (!isLoad)
            {
                IDphieuxuat();
                isLoad = true;
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            isLoadid();
        }

        public class PhieuXuat
        {
            public string MaPhieuXuat { get; set; }
            public string TenNhanVien { get; set; }
            public DateTime NgayIn { get; set; }
            public string TenDonVi { get; set; }
            public string MaHangHoa { get; set; }
            public string TenHangHoa { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public decimal ThanhTien { get; set; }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            CrystalReport2 cr = new CrystalReport2();
            PhieuXuatService service = new PhieuXuatService();
            DataTable dataTable = service.LayPhieuXuat(comboBox1.Text);
            if (dataTable.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có dữ liệu để in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CheckData();
            cr.SetDataSource(dataTable);
            frmInbaocao frm = new frmInbaocao();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.ShowDialog();
        }

        public class PhieuXuatService
        {
            public DataTable LayPhieuXuat(string maPhieuXuat)
            {
                string connectionString = "Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;";
                string query = @"
                SELECT PX.MaPhieuXuat, NV.TenHienThi, PX.NgayInPhieu, DV.TenDonVi,
                 HH.MaHangHoa, HH.TenHangHoa, CTPX.SoLuong, CTPX.DonGia, CTPX.DonGia
                FROM tb_phieuxuat PX
                INNER JOIN tb_taikhoan NV ON PX.IdNhanVien = NV.id
                INNER JOIN tb_donvi DV ON PX.MaDonVi = DV.MaDonVi
                INNER JOIN tb_phieuxuatchitiet CTPX ON PX.MaPhieuXuat = CTPX.MaPhieuXuat
                INNER JOIN tb_hanghoa HH ON CTPX.MaHangHoa = HH.MaHangHoa
                WHERE PX.MaPhieuXuat = @MaPhieuXuat";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaPhieuXuat", maPhieuXuat);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
