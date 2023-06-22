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
    public partial class NhaXuatBann : Form
    {
        string Conn = "Data Source=DESKTOP-CLBQFHC\\TAIVO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public NhaXuatBann()
        {
            InitializeComponent();
        }

        private void NhaXuatBann_Load(object sender, EventArgs e)
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
        private void Display()
        {
            string query = "select * from NhaXuatBan";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvNXB.DataSource = dt;
        }
        private void SetControls(bool edit)
        {
            txtMaXB.Enabled = edit;
            txtTenXB.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void dgvNXB_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaXB.Text = dgvNXB.Rows[r].Cells[0].Value.ToString();
            txtTenXB.Text = dgvNXB.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvNXB.Rows[r].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaXB.Clear();
            txtTenXB.Clear();
            txtGhiChu.Clear();
            txtMaXB.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenXB.Focus();
            bien = 2;
            SetControls(true);
            txtMaXB.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvNXB.CurrentRow.Index;
                string Ma = dgvNXB.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from NhaXuatBan where MaXB = '" + Ma + "'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                Display();
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
                if (txtMaXB.Text == "" || txtTenXB.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    for (int i = 0; i < dgvNXB.RowCount; i++)
                    {
                        if (txtMaXB.Text == dgvNXB.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã nhà xuất bản. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    string query1 = "insert into NhaXuatBan(MaXB, NhaXuatBan, GhiChu) values(N'" + txtMaXB.Text + "',N'" + txtTenXB.Text + "',N'" + txtGhiChu.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
            }
            else
            {
                if (txtMaXB.Text == "" || txtTenXB.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int row = dgvNXB.CurrentRow.Index;
                    string Ma = dgvNXB.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update NhaXuatBan set NhaXuatBan = N'" + txtTenXB.Text + "', GhiChu = N'" + txtGhiChu.Text + "' where MaXB ='" + Ma + "'";
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
            string query = "select * from NhaXuatBan where NhaXuatBan like N'%" + txtTimKiem.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvNXB.DataSource = dt;
        }
    }
}
