using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public partial class addDV : DevExpress.XtraEditors.XtraForm
    {
        public addDV()
        {
            InitializeComponent();
            addDV_Load();
        }
        //Hiển thị dữ liệu bảng tb_DonVi lên datagridview
        private void addDV_Load()
        {
            string query = "SELECT * FROM tb_DonVi";
            dataGridView1.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tendv = tendvTB.Text;
            string SDT = sdtTB.Text;
            string diachi = dcTB.Text;
            string fax = faxTB.Text;
            string email = mailTB.Text;
            // Kiểm tra dữ liệu nhập vào có rỗng không
            if (tendv == "" || SDT == "" || diachi == "" || email == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                string query = "INSERT INTO tb_donvi VALUES (N'" + tendv + "','" + diachi + "',N'" + SDT + "','" + email + "','" + fax + "','" + " "+ "','" + "1" + "')";
                DataProvider.Instance.ExecuteQuery(query);
                addDV_Load();
                comboBox1.Items.Clear();
                LoadDataToComboBoxOnceDV();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //sửa thông tin đơn vị thông qua mã đơn vị đã chọn ở combobox
            string madv = comboBox1.SelectedItem.ToString();
            string tendv = tendvTB.Text;
            string SDT = sdtTB.Text;
            string diachi = dcTB.Text;
            string fax = faxTB.Text;
            string email = mailTB.Text;
            // Kiểm tra dữ liệu nhập vào có rỗng không, nếu rỗng giữ nguyên thông tin cũ
            if (tendv == "" || SDT == "" || diachi == "" || email == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                string query = "UPDATE tb_donvi SET TenDonVi = N'" + tendv + "', DiaChi = '" + diachi + "', SoDienThoai = '" + SDT + "', Email = '" + email + "', Fax = '" + fax + "' WHERE MaDonVi = '" + madv + "'";
                DataProvider.Instance.ExecuteQuery(query);
                addDV_Load();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Xóa thông tin đơn vị thông qua mã đơn vị đã chọn ở combobox
            string madv = comboBox1.SelectedItem.ToString();
            string query = "DELETE FROM tb_donvi WHERE MaDonVi = '" + madv + "'";
            DataProvider.Instance.ExecuteQuery(query);
            addDV_Load();
            //clear các textbox và combobox
            tendvTB.Text = "";
            sdtTB.Text = "";
            dcTB.Text = "";
            faxTB.Text = "";
            mailTB.Text = "";
            comboBox1.Items.Clear();
            //load lại combobox
            LoadDataToComboBoxOnceDV();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }
        
        public class DonVi
        {
            public string MaDonVi { get; set; }
            public string TenDonVi { get; set; }
            public string DiaChi { get; set; }
            public string SDT { get; set; }
            public string Email { get; set; }
            public string Fax { get; set; }
            public DonVi(string madonvi, string tendonvi, string diachi, string sdt, string email, string fax)
            {
                MaDonVi = madonvi;
                TenDonVi = tendonvi;
                DiaChi = diachi;
                SDT = sdt;
                Email = email;
                Fax = fax;
            }
            public override string ToString()
            {
                return MaDonVi;
            }

        }
        private bool isDataLoadedDV = false;
        private void LoadDataToComboBoxOnceDV()
        {
            if (!isDataLoadedDV)
            {
                LoadDatatoComboBox();
                isDataLoadedDV= true;
            }
        }
        private void LoadDatatoComboBox()
        {
            List<DonVi> list = new List<DonVi>();
            string query = "SELECT * FROM tb_donvi";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DonVi dv = new DonVi(item["MaDonVi"].ToString(), item["TenDonVi"].ToString(), item["DiaChi"].ToString(), item["SoDienThoai"].ToString(), item["Email"].ToString(), item["Fax"].ToString());
                list.Add(dv);
            }
            comboBox1.DataSource = list;
            comboBox1.DisplayMember = "MaDonVi";
        }
        
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            LoadDataToComboBoxOnceDV();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //Lấy thông tin từ combobox rồi hiển thị lên các textbox
            DonVi dv = comboBox1.SelectedItem as DonVi;
            if (dv != null)
            {
                tendvTB.Text = dv.TenDonVi;
                sdtTB.Text = dv.SDT;
                dcTB.Text = dv.DiaChi;
                faxTB.Text = dv.Fax;
                mailTB.Text = dv.Email;
            }

        }
    }
}