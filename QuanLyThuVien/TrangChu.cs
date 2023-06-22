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
    public partial class TrangChu : Form
    {
        string Conn = "Data Source=DESKTOP-CLBQFHC\\TAIVO;Initial Catalog=QuanLyThuVienDB;Integrated Security=True"; //Sai chỗ này 
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;

        private string _message;
        public TrangChu()
        {
            InitializeComponent(); //phương thức khởi tạo của lớp Form để khởi tạo và cấu hình các thành phần giao diện người dùng
        }

        public TrangChu(string Message) : this()
        {
            _message = Message;
            txtXinChao.Text = _message;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            DocGia();
            NhanVien();
            Muontra();
            cbMuontra();
            thongke();
            tracuusach();

        }
        public void tracuusach()
        {
            string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTimSach.DataSource = dt;
        }

        private void btnSachh_Click(object sender, EventArgs e)
        {
            Sach S = new Sach();
            S.Show();
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            LoaiSach LS = new LoaiSach();
            LS.Show();
        }

        private void btnTG_Click(object sender, EventArgs e)
        {
            TacGia TG = new TacGia();
            TG.Show();
        }

        private void btnXBB_Click(object sender, EventArgs e)
        {
            NhaXuatBann XB = new NhaXuatBann();
            XB.Show();
        }

        private void txtTimKiemm_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnTenSach.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia  where S.TenSach like N'%" + txtTimKiemm.Text + "%' order by Soluong";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnLoaiSach.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong  from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where LS.TenLoaiSach like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnTacGia.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where TG.TacGia like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnNXB.Checked)
            {
                string query = "select S.TenSach, Ls.TenLoaiSach, XB.NhaXuatBan, TG.TacGia, S.SoTrang, S.GiaBan, S.SoLuong from LoaiSach LS join Sach S on LS.MaLoai = S.MaLoai join NhaXuatBan XB on XB.MaXB = S.MaXB join TacGia TG on TG.MaTacGia = S.MaTacGia where XB.NhaXuatBan like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
        }

        private void DocGia()
        {
            string query = "select * from SinhVien";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSinhVien.DataSource = dt;
            SetControls(false);
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtNganhHoc.Clear();
            txtSDT.Clear();
            txtMaSinhVien.Focus();
            bien = 1;

            SetControls(true);
        }

        private void SetControls(bool edit)
        {
            txtMaSinhVien.Enabled = edit;
            txtHoTen.Enabled = edit;
            txtKhoa.Enabled = edit;
            txtNganhHoc.Enabled = edit;
            txtSDT.Enabled = edit;
            btnThemSV.Enabled = !edit;
            btnSuaSV.Enabled = !edit;
            btnXoaSV.Enabled = !edit;
            btnGhiSV.Enabled = edit;
            btnHuySV.Enabled = edit;
            //.Enabled = edit;
            
            cbTenSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayTra.Enabled = edit;
            cbMaSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            txtHoTen.Focus();
            bien = 2;

            SetControls(true);
            txtMaSinhVien.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvSinhVien.CurrentRow.Index;
                string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from SinhVien where MaSV = " + MaSV;
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DocGia();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xoá. Sinh này đang mượn sách", "Thông Báo");
            }
        }

        private void btnGhiSV_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "" || txtMaSinhVien.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    for (int i = 0; i < dgvSinhVien.RowCount; i++)
                    {
                        if (txtMaSinhVien.Text == dgvSinhVien.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã sinh viên. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    double x;
                    bool kt = double.TryParse(txtMaSinhVien.Text, out x);
                    if (kt == false)
                    {
                        MessageBox.Show("Vui lòng Nhập lại dưới dạng số!", "Thông báo");
                        return;
                    }
                    string query1 = "insert into SinhVien(MaSV,TenSV, NganhHoc, KhoaHoc, SoDienThoai) values('" + txtMaSinhVien.Text + "',N'" + txtHoTen.Text + "',N'" + txtNganhHoc.Text + "', N'" + txtKhoa.Text + "', N'" + txtSDT.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Thên sinh viên thành công", "Thông báo");
                }
            }
            else
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "" || txtMaSinhVien.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int row = dgvSinhVien.CurrentRow.Index;
                    string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update SinhVien set MaSV = '" + txtMaSinhVien.Text + "', TenSV = N'" + txtHoTen.Text + "', NganhHoc = N'" + txtNganhHoc.Text + "', KhoaHoc = N'" + txtKhoa.Text + "', SoDienThoai = N'" + txtSDT.Text + "' where MaSV = " + MaSV;
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Sửa sinh viên thành công", "Thông báo");
                }
            }
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void dgvSinhVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSinhVien.Text = dgvSinhVien.Rows[r].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[r].Cells[1].Value.ToString();
            txtNganhHoc.Text = dgvSinhVien.Rows[r].Cells[2].Value.ToString();
            txtKhoa.Text = dgvSinhVien.Rows[r].Cells[3].Value.ToString();
            txtSDT.Text = dgvSinhVien.Rows[r].Cells[4].Value.ToString();
        }

        private void txtTimKiemSV_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnMSV.Checked)
            {
                string query = "select * from SinhVien where MaSV like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }

            if (btnTSV.Checked)
            {
                string query = "select * from SinhVien where TenSV like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }
        }
        public void NhanVien() {
            string query = "select MaNhanVien, TenNhanVien, SoDienThoai, GioiTinh, DiaChi from NhanVien";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvQuanLy.DataSource = dt;
            
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() == "" || txtTenNV.Text.Trim() == "" || txtSoDienThoai.Text.Trim() == "" || cbGioiTinh.Text.Trim() == "" || txtDiaChi.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Thông tin nhập không được để trống", "Thông báo");
                return;
            }
            if(Convert.ToInt32(txtSoDienThoai.Text.Trim().Length) != 10 )
            {
                MessageBox.Show("Số điện thoại bắt buộc phải 10 số", "Thông Báo");
                return;
            }
            double x;
            bool kt = double.TryParse(txtSoDienThoai.Text, out x);
            if (kt == false || Convert.ToInt32(txtSoDienThoai.Text) < 0)
            {
                MessageBox.Show("Vui lòng Nhập lại dưới dạng số", "Thông báo");
                return;
            }
            if (Convert.ToInt32(txtMatKhau.Text.Trim().Length) < 6 )
            {
                MessageBox.Show("Mật khẩu phải ít nhất có 6 ký tự", "Thông Báo");
                return;
            }
            if (Convert.ToInt32(txtMatKhau.Text.Trim().Length) != Convert.ToInt32(txtMatKhau.Text.Length))
            {
                MessageBox.Show("Mật khẩu có ký tự không hợp lệ. Vui lòng nhập lại", "Thông Báo");
                return;
            }

            string query1 = "insert into NhanVien(MaNhanVien, TenNhanVien, SoDienThoai, GioiTinh, DiaChi, MatKhau) values(N'" + txtMaNV.Text + "',N'" + txtTenNV.Text + "','" + txtSoDienThoai.Text + "', N'" + cbGioiTinh.Text + "', N'" + txtDiaChi.Text + "',  N'" + txtMatKhau.Text + "')";
            mySqlCommand = new SqlCommand(query1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            NhanVien();
            MessageBox.Show("Đăng ký tài khoản thành công", "Thông Báo");

        }

        private void btnDoiPass_Click(object sender, EventArgs e)
        {
            DoiMatKhau DoiPass = new DoiMatKhau();
            DoiPass.Show();
        }

        public void Muontra()
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvMuonSach.DataSource = dt;

            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
            ttSoLuong.Enabled = false;
            ttTenTG.Enabled = false;
        }
        public void cbMuontra()
        {
            string sSql2 = "select MaSV from SinhVien";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                cbMaSV.Items.Add(dr[0].ToString());
            }
            string sSql9 = "select TenSach from Sach";
            mySqlCommand = new SqlCommand(sSql9, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(mySqlCommand);
            da9.Fill(dt9);
            foreach (DataRow dr in dt9.Rows)
            {
                cbTenSach.Items.Add(dr[0].ToString());
            }
        }

        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql1 = "select s.MaSach, s.TenSach, tg.TacGia, s.SoLuong from Sach s join TacGia tg on s.MaTacGia = tg.MaTacGia where s.TenSach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["MaSach"].ToString();
                ttTenSach.Text = dr["TenSach"].ToString();
                ttTenTG.Text = dr["TacGia"].ToString();
                ttSoLuong.Text = dr["SoLuong"].ToString();
            }
        }

        private void txtTimKiemSachMuon_KeyUp(object sender, KeyEventArgs e)
        {
            if (raMaSV.Checked)
            {
                string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where SV.MaSV like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
            if (raMaSach.Checked)
            {
                string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, S.MaSach, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where S.MaSach like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            //cb.Clear();
            //txtGhiChu.Clear();
            cbTenSach.Focus();
            bien = 5;
            SetControls(true);
            //cbNgayMuon.Enabled = false;
            cbNgayMuon.Visible = false;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            //cbSV.Focus();
            bien = 6;

            SetControls(true);
            txtMaPhieuMUon.Enabled = false;
            cbTenSach.Enabled = false;
            cbMaSV.Enabled = false;
            txtGhiChu.Enabled = false;
            cbNgayMuon.Enabled = false;
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn trả? ", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvMuonSach.CurrentRow.Index;
            string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from MuonTraSach where MaPhieuMuon = " + MaMuonTra;
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SoLuongSauTra();
            Muontra();
            MessageBox.Show("Trả sách thành công.", "Thông báo");
        }
        private void btnGhii_Click(object sender, EventArgs e)
        {
            int SoNgay;
            string sSql2 = "SELECT DATEDIFF(day, GETDATE(),'" + cbNgayTra.Value + "')";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(mySqlCommand);
            da5.Fill(dt5);
            SoNgay = Convert.ToInt32(dt5.Rows[0][0].ToString());
            if (SoNgay > 0)
            {
                if (bien == 5)
                {
                    int SoLuongSach = 0;
                    //int MaSach = Convert.ToInt32(ttMaSach.Text);
                    //MessageBox.Show(Convert.ToString(MaSach));
                    string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
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
                            string sSql8 = "select * from MuonTraSach where MaSV='" + cbMaSV.Text + "'";
                            mySqlCommand = new SqlCommand(sSql8, mySqlconnection);
                            mySqlCommand.ExecuteNonQuery();
                            DataTable dt8 = new DataTable();
                            SqlDataAdapter da8 = new SqlDataAdapter(mySqlCommand);
                            da8.Fill(dt8);
                            int count = Convert.ToInt32(dt8.Rows.Count.ToString());
                            if (count > 3)
                            {
                                MessageBox.Show("Sinh viên này đã mượn 3 cuốn, vui lòng trả sách để có thể tiếp tục mượn","Thông báo");
                                return;
                            }
                            else
                            {
                                string query2 = "insert into MuonTraSach( MaSV, MaSach, NgayMuon, NgayTra, GhiChu) values('" + cbMaSV.Text + "','" + ttMaSach.Text + "', GETDATE(),'" + cbNgayTra.Value + "',N'" + txtGhiChu.Text + "')";
                                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                                mySqlCommand.ExecuteNonQuery();
                                SoLuongSauMuon();
                                Muontra();
                                SetControls(false);
                                MessageBox.Show("Mượn sách thành công.", "Thông báo");

                            }
                        }
                    else
                    {
                        MessageBox.Show("Không có sẵn sách này", "Thông báo");
                    }
            }
                else
                {
                    int row = dgvMuonSach.CurrentRow.Index;
                    string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update MuonTraSach set NgayTra = '" + cbNgayTra.Value + "' where MaPhieuMuon = " + MaMuonTra;
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Muontra();
                    SetControls(false);
                    MessageBox.Show("Gia hạn thành công.", "Thông báo");
                }
                }
            else
            {
                MessageBox.Show("Thời gian trả không hợp lệ");
            }
        }

        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
        public void SoLuongSauMuon()// Hàm này để thay đổi số lượng sách khi mượn sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            //MessageBox.Show(Convert.ToString(MaSach));
            string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
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
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }
        public void SoLuongSauTra()// Hàm này để thay đổi số lượng sách khi trả sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            string sSql1 = "select SoLuong from Sach where MaSach ='" + ttMaSach.Text + "'";
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
                string query2 = "update Sach set SoLuong = " + SoLuong + " where MaSach = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        private void dgvMuonSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaPhieuMUon.Text = dgvMuonSach.Rows[r].Cells[0].Value.ToString();
            cbTenSach.Text = dgvMuonSach.Rows[r].Cells[4].Value.ToString();
            cbMaSV.Text = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            cbNgayMuon.Text = dgvMuonSach.Rows[r].Cells[5].Value.ToString();
            cbNgayTra.Text = dgvMuonSach.Rows[r].Cells[6].Value.ToString();
            txtGhiChu.Text = dgvMuonSach.Rows[r].Cells[7].Value.ToString();

            string sSql1 = "select s.MaSach, s.TenSach, tg.TacGia, s.SoLuong from Sach s join TacGia tg on s.MaTacGia = tg.MaTacGia where s.TenSach = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["MaSach"].ToString();
                ttTenSach.Text = dr["TenSach"].ToString();
                ttTenTG.Text = dr["TacGia"].ToString();
                ttSoLuong.Text = dr["SoLuong"].ToString();
            }

            string sSql2 = "select MaSV, TenSV from SinhVien where MaSV = '" + cbMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaSV"].ToString();
                ttTenSV.Text = dr["TenSV"].ToString();

            }
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra <= CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = false;
            lbquahan.Visible = true;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }
        public void thongke()
        {
            string query = "select MS.MaPhieuMuon, SV.MaSV, SV.TenSV, SV.SoDienThoai, S.TenSach,MS.NgayMuon,MS.NgayTra,MS.GhiChu from MuonTraSach MS join Sach S on S.MaSach = MS.MaSach join SinhVien SV on SV.MaSV = MS.MaSV where MS.NgayTra > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();


            NhanVienn();
            SinhVien();
            Sach();
            MuonTraSach();
            LoaiSach();
            TacGia();
            NhaXuatBan();



        }
        public void NhanVienn()
        {
            string sSql1 = "select count(*) from NhanVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkAdmin.Text = dt.Rows[0][0].ToString();
        }
        public void SinhVien()
        {
            string sSql1 = "select count(*) from SinhVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSinhVien.Text = dt.Rows[0][0].ToString();
        }
        public void Sach()
        {
            string sSql1 = "select count(*) from Sach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSach.Text = dt.Rows[0][0].ToString();
        }
        public void MuonTraSach()
        {
            string sSql1 = "select count(*) from MuonTraSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkMuonSach.Text = dt.Rows[0][0].ToString();
        }
        public void LoaiSach()
        {
            string sSql1 = "select count(*) from LoaiSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKLoaiSach.Text = dt.Rows[0][0].ToString();
        }
        public void TacGia()
        {
            string sSql1 = "select count(*) from TacGia";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKTacGia.Text = dt.Rows[0][0].ToString();
        }
        public void NhaXuatBan()
        {
            string sSql1 = "select count(*) from NhaXuatBan";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKNhaXB.Text = dt.Rows[0][0].ToString();
        }

        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql2 = "select MaSV, TenSV from SinhVien where MaSV = '" + cbMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["MaSV"].ToString();
                ttTenSV.Text = dr["TenSV"].ToString();

            }
        }

        private void dgvDSMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //string MaSV, msv, tsv, sdt, sdtt;
            //int r = e.RowIndex;
            //MaSV = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            //
            //string sSql2 = "select MaSV, TenSV, SoDienThoai from SinhVien where MaSV = '" + MaSV + "'";
            //mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            //da2.Fill(dt2);
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    msv = dr["MaSV"].ToString();
            //    tsv = dr["TenSV"].ToString();
            //    sdt = dr["SoDienThoai"].ToString();
            //    MessageBox.Show(sdt, "Thông Tin Sinh Viên");
            //}
            //sdtt = Convert.ToString(sdt);
            //MessageBox.Show(sdt,"Thông Tin Sinh Viên");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvQuanLy.CurrentRow.Index;
            string MaNV = dgvQuanLy.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from NhanVien where MaNhanVien = '" + MaNV + "'";
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            NhanVien();
            MessageBox.Show("Xoá thành công.", "Thông báo");

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel26_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}
