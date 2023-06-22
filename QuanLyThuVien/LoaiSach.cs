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

namespace QuanLyThuVien
{
    public partial class LoaiSach : Form
    {
        string Conn = "Data Source=DESKTOP-CLBQFHC\\TAIVO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public LoaiSach()
        {
            InitializeComponent();
        }

        private void LoaiSach_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            SetControls(false);
            Display();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLoai.Clear();
            txtLoaiSach.Clear();
            txtGhiChu.Clear();
            txtMaLoai.Focus();
            bien = 1;
            SetControls(true);
        }

        private void dgvLoaiSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaLoai.Text = dgvLoaiSach.Rows[r].Cells[0].Value.ToString();
            txtLoaiSach.Text = dgvLoaiSach.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvLoaiSach.Rows[r].Cells[2].Value.ToString();
        }
        private void Display()
        {
            string query = "select * from LoaiSach";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvLoaiSach.DataSource = dt;
        }
        private void SetControls(bool edit)
        {
            txtMaLoai.Enabled = edit;
            txtLoaiSach.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtLoaiSach.Focus();
            bien = 2;
            SetControls(true);
            txtMaLoai.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvLoaiSach.CurrentRow.Index;
                string Ma = dgvLoaiSach.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from LoaiSach where MaLoai = '" + Ma + "'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                Display();
                MessageBox.Show("Xoá thành công", "Thông báo");
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá.", "Thông Báo");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaLoai.Text == "" || txtLoaiSach.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    for (int i = 0; i < dgvLoaiSach.RowCount; i++)
                    {
                        if (txtMaLoai.Text == dgvLoaiSach.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã Loại. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    string query1 = "insert into LoaiSach(MaLoai, TenLoaiSach, GhiChu) values(N'" + txtMaLoai.Text + "',N'" + txtLoaiSach.Text + "',N'" + txtGhiChu.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
            }
            else
            {
                if (txtMaLoai.Text == "" || txtLoaiSach.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int row = dgvLoaiSach.CurrentRow.Index;
                    string Ma = dgvLoaiSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update LoaiSach set TenLoaiSach = N'" + txtLoaiSach.Text + "', GhiChu = N'" + txtGhiChu.Text + "' where MaLoai ='" + Ma + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Sửa thành công", "Thông báo");
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            string query = "select * from LoaiSach where TenLoaiSach like N'%" + txtTimKiem.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvLoaiSach.DataSource = dt;
        }
    }
}
