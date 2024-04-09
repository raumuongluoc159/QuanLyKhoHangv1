using DevExpress.XtraEditors;
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
//using quan ly  nha xuat ban
using WindowsFormsApp1.DAO;
namespace WindowsFormsApp1
{
    public partial class frmInphieubyid : DevExpress.XtraEditors.XtraForm
    {
        public frmInphieubyid()
        {
            InitializeComponent();
        }
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                XtraMessageBox.Show("Bạn chưa chọn mã phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }
            return true;
        }
        private bool isLoad = false;
        private void IDphieunhap()
        {
            string query = "select MaPhieuNhap from tb_phieunhap";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "MaPhieuNhap";
        }
        private void isLoadid() {
            if (!isLoad)
            {
                IDphieunhap();

                isLoad = true;
            }
        }
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            isLoadid();
        }
        public class PhieuNhap
        {
            public string MaPhieuNhap { get; set; }
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
        CrystalReport1 cr = new CrystalReport1();
            PhieuNhapService service = new PhieuNhapService();
            DataTable dataTable = service.LayPhieuNhap(comboBox1.Text);
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
        public class PhieuNhapService
        {
            public DataTable LayPhieuNhap(string maPhieuNhap)
            {
                string connectionString = "Data Source=thetoan.database.windows.net;Initial Catalog=QuanLyKhoHang;User ID=thetoanictu;Password=Dtt1990vn!;Connect Timeout=60;Encrypt=True;";
                string query = @"
                SELECT PN.MaPhieuNhap, NV.TenHienThi, PN.NgayInPhieu, DV.TenDonVi,
                 HH.MaHangHoa, HH.TenHangHoa, CTPN.SoLuong, CTPN.DonGia, CTPN.DonGia
                FROM tb_phieunhap PN
                INNER JOIN tb_taikhoan NV ON PN.IdNhanVien = NV.id
                INNER JOIN tb_donvi DV ON PN.MaDonVi = DV.MaDonVi
                INNER JOIN tb_phieunhapchitiet CTPN ON PN.MaPhieuNhap = CTPN.MaPhieuNhap
                INNER JOIN tb_hanghoa HH ON CTPN.MaHangHoa = HH.MaHangHoa
                WHERE PN.MaPhieuNhap = @MaPhieuNhap";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);

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
} //commented out because it is not used
   