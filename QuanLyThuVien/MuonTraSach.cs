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
    public partial class MuonTraSach : Form
    {
        string Conn = "Data Source=DESKTOP-CLBQFHC\\TAIVO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public MuonTraSach()
        {
            InitializeComponent();
        }

        private void MuonTraSach_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            SetControls(false);
            DisPhieuMuon();
            loadcombobox();
            DisMuonSach();
        }
        private void DisPhieuMuon()
        {
            string query = "select * from PhieuMuon";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvPhieuMuon.DataSource = dt;

        }
        private void loadcombobox()
        {
            string sSql1 = "select MaSV from SinhVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cbSV.Items.Add(dr[0].ToString());
            }

            string sSql2 = "select MaSach from Sach";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                cbMaSach.Items.Add(dr1[0].ToString());
            }

            string sSql3 = "select MaPhieuMuon from PhieuMuon";
            mySqlCommand = new SqlCommand(sSql3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(mySqlCommand);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                cbMaPhieuMuon.Items.Add(dr3[0].ToString());
            }
        }

        private void dgvPhieuMuon_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaPhieu.Text = dgvPhieuMuon.Rows[r].Cells[0].Value.ToString();
            cbSV.Text = dgvPhieuMuon.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvPhieuMuon.Rows[r].Cells[2].Value.ToString();
        }
        private void SetControls(bool edit)
        {
            txtMaPhieu.Enabled = edit;
            cbSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;

            cbMaPhieuMuon.Enabled = edit;
            cbMaSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayHenTra.Enabled = edit;
            txtGhichuu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            cbNgayMuon.Visible = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Clear();
            txtGhiChu.Clear();
            txtMaPhieu.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            cbSV.Focus();
            bien = 2;

            SetControls(true);
            txtMaPhieu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvPhieuMuon.CurrentRow.Index;
                string MaPhieuMuon = dgvPhieuMuon.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from PhieuMuon where MaPhieuMuon = '" + MaPhieuMuon + "'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DisPhieuMuon();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá. Sinh viên chưa trả hêt sách", "Thông Báo");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaPhieu.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    for (int i = 0; i < dgvPhieuMuon.RowCount; i++)
                    {
                        if (txtMaPhieu.Text == dgvPhieuMuon.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã phiếu mượn. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    
                    string query1 = "insert into PhieuMuon(MaPhieuMuon, MaSV, GhiChu) values(N'" + txtMaPhieu.Text + "','" + cbSV.Text + "',N'" + txtGhiChu.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DisPhieuMuon();
                }
            }
            else
            {
                if (txtMaPhieu.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int row = dgvPhieuMuon.CurrentRow.Index;
                    string MaPhieu = dgvPhieuMuon.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update PhieuMuon set MaSV = '" + cbSV.Text + "', GhiChu = N'" + txtGhiChu.Text + "' where MaPhieuMuon = '" + MaPhieu +"'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DisPhieuMuon();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        public void DisMuonSach()
        {
            string query = "select * from MuonTraSach";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvMuonTra.DataSource = dt;
        }

        private void dgvMuonTra_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            cbMaPhieuMuon.Text = dgvMuonTra.Rows[r].Cells[1].Value.ToString();
            cbMaSach.Text = dgvMuonTra.Rows[r].Cells[2].Value.ToString();
            cbNgayMuon.Text = dgvMuonTra.Rows[r].Cells[3].Value.ToString();
            cbNgayHenTra.Text = dgvMuonTra.Rows[r].Cells[4].Value.ToString();
            txtGhichuu.Text = dgvMuonTra.Rows[r].Cells[5].Value.ToString();
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            //cb.Clear();
            //txtGhiChu.Clear();
            cbMaPhieuMuon.Focus();
            bien = 5;
            SetControls(true);
            //cbNgayMuon.Enabled = false;
            cbNgayMuon.Visible = false;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            //cbSV.Focus();
            bien = 2;

            SetControls(true);
            cbMaPhieuMuon.Enabled = false;
            cbMaSach.Enabled = false;
            cbNgayMuon.Enabled = false;

        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn trả? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvMuonTra.CurrentRow.Index;
            string MaMuonTra = dgvMuonTra.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from MuonTraSach where MaMuonTra = " + MaMuonTra ;
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SoLuongSauTra();
            DisMuonSach();           
            MessageBox.Show("Trả sách thành công.", "Thông báo");
        }

        private void btnGhii_Click(object sender, EventArgs e)
        {
            if (bien == 5)
            {

                int SoLuongSach = 0;
                int MaSach = Convert.ToInt32(cbMaSach.Text);
                //MessageBox.Show(Convert.ToString(MaSach));
                string sSql1 = "select SoLuong from Sach where MaSach =" + MaSach + "";
                mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                }

                if (SoLuongSach > 0)
                {
                    int SoNgay;
                    string sSql2 = "SELECT DATEDIFF(day, GETDATE(),'" + cbNgayHenTra.Value + "')";
                    mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DataTable dt5 = new DataTable();
                    SqlDataAdapter da5 = new SqlDataAdapter(mySqlCommand);
                    da5.Fill(dt5);
                    SoNgay = Convert.ToInt32(dt5.Rows[0][0].ToString());

                    if (SoNgay > 0)
                    {

                        string query2 = "insert into MuonTraSach(MaPhieuMuon, MaSach, NgayMuon, NgayTra, GhiChu) values(N'" + cbMaPhieuMuon.Text + "','" + cbMaSach.Text + "', GETDATE(),'" + cbNgayHenTra.Value + "','" + txtGhichuu.Text + "')";
                        mySqlCommand = new SqlCommand(query2, mySqlconnection);
                        mySqlCommand.ExecuteNonQuery();
                        SoLuongSauMuon();
                        DisMuonSach();
                        SetControls(false);        
                        MessageBox.Show("Mượn sách thành công.", "Thông báo");
                    }
                    else
                    {
                        MessageBox.Show("Thời gian mượn trả không hợp lệ");
                    }
                }
                else
                {
                    MessageBox.Show("Không có sẵn sách này", "Thông báo");
                }



                //string query1 = "insert into MuonTraSach(MaPhieuMuon, MaSach, NgayMuon, NgayTra, GhiChu) values(N'" + cbMaPhieuMuon.Text + "','" + cbMaSach.Text + "', GETDATE(),'" + cbNgayHenTra.Value + "','" + txtGhichuu.Text + "')";
                //mySqlCommand = new SqlCommand(query1, mySqlconnection);
                //mySqlCommand.ExecuteNonQuery();
                //DisMuonSach();
                //SetControls(false);
                //MessageBox.Show("Mượn sách thành công.", "Thông báo");
            }
            else
            {
                int row = dgvMuonTra.CurrentRow.Index;
                string MaMuonTra = dgvMuonTra.Rows[row].Cells[0].Value.ToString();
                string query2 = "update MuonTraSach set MaPhieuMuon = '" + cbMaPhieuMuon.Text + "', MaSach = '" + cbMaSach.Text + "', NgayMuon = '" + cbNgayMuon.Value + "', NgayTra = '" + cbNgayHenTra.Value + "', GhiChu = N'" + txtGhichuu.Text + "' where MaMuonTra = " + MaMuonTra;
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DisMuonSach();
                SetControls(false);
                MessageBox.Show("Gia hạn thành công.", "Thông báo");
            }
        }

        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        public void SoLuongSauMuon()// Hàm này để thay đổi số lượng sách khi mượn sách.
        {
            int MaSach = Convert.ToInt32(cbMaSach.Text);
            //MessageBox.Show(Convert.ToString(MaSach));
            string sSql1 = "select SoLuong from Sach where MaSach =" + MaSach + "";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                int SoLuong = SoLuongSach - 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = " + MaSach + "";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }
        public void SoLuongSauTra()// Hàm này để thay đổi số lượng sách khi trả sách.
        {
            int MaSach = Convert.ToInt32(cbMaSach.Text);
            string sSql1 = "select SoLuong from Sach where MaSach =" + MaSach + "";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["SoLuong"].ToString());
                int SoLuong = SoLuongSach + 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = " + MaSach + "";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
