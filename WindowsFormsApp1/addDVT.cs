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
    public partial class addDVT : Form
    {
        public addDVT()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //thêm đơn vị tính vào bảng DVT
            string query = "INSERT INTO tb_DVT VALUES ( N'" + tenDVT.Text + "')";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            if (result > 0)
            {
                MessageBox.Show("Thêm đơn vị tính thành công");
            }
            else
            {
                MessageBox.Show("Thêm đơn vị tính thất bại");
            }

        }
    }
}
