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
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyThuVien
{
    public partial class Login : Form
    {
        string Conn = "Data Source=DESKTOP-CLBQFHC\\TAIVO; Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string TenNV;
        public Login()
        {
            InitializeComponent(); //khởi tạo các thành phần giao diện người dùng
        }
        private void Login_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            string sSql = "select * from NhanVien where MaNhanVien='" + txtTaiKhoan.Text + "' and MatKhau='" + txtMatKhau.Text + "'";
            mySqlCommand = new SqlCommand(sSql, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            int count = Convert.ToInt32(dt.Rows.Count.ToString());
            if (count == 0)
            {
                MessageBox.Show("Tai khoan hoac mat khau khong dung");
            }
            else
            {                
                string sSql1 = "select TenNhanVien from NhanVien where MaNhanVien = '" + txtTaiKhoan.Text + "'";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
                da1.Fill(dt1);
                foreach (DataRow dr in dt1.Rows)
                {
                    TenNV = dr["TenNhanVien"].ToString();
                }
                this.Hide();
                TrangChu Home = new TrangChu(TenNV);
                Home.Show();
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
