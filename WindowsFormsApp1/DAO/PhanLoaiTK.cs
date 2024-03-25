using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO
{
    public class PhanLoaiTK
    {
        //take Type of account in tb_TAIKHOAN
        
        private static PhanLoaiTK instance;
        public static PhanLoaiTK Instance
        {
            get { if (instance == null) instance = new PhanLoaiTK(); return instance; }
            private set { instance = value; }
        }
        private PhanLoaiTK() { }
        public string PhanLoaiTaiKhoan(string username, string password)
        {
            string query = "SELECT LoaiTaiKhoan FROM tb_taikhoan WHERE Username ='" + username +"'"+ "AND Password ='" + password +"'";
            string result = DataProvider.Instance.ExecuteScalar(query).ToString();
            return result;

        }
        public string tenTaiKhoan(string username, string password)
        {
            string query = "SELECT TenHienTHi FROM tb_taikhoan WHERE Username ='" + username +"'"+ "AND Password ='" + password +"'";
            string name = DataProvider.Instance.ExecuteScalar(query).ToString();
            return name;

        }
        // Ở đây sẽ lấy type 0 là admin, 1 là nhân viên, 2 là thủ kho, 3 là kế toán, 4 là giám đốc





    }
}
