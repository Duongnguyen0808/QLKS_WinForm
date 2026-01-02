using BUS;
using DTO;
using GUI.GUI_COMPONENT;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmRegister : Form
    {
        TaiKhoanBUS taiKhoan = new TaiKhoanBUS();
        NhanVienBUS nhanVien = new NhanVienBUS();
        
        public frmRegister()
        {
            InitializeComponent();
            timerOpacity.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            this.Close();
        }

        private void pnTop_MouseDown(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            mouseOffset = new Point(-mouseX, -mouseY);
        }

        private void pnTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                this.Location = mousePos;
            }
        }

        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DropShadow;
                return cp;
            }
        }
        
        private Point mouseOffset;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            
            // Validate input
            if (txtTK.Text.Trim().Length == 0)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Vui l?ng nh?p tên tài kho?n", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtTK.Focus();
                return;
            }
            
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Vui l?ng nh?p m? nhân viên", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtMaNV.Focus();
                return;
            }
            
            if (txtPass.Text.Length == 0)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Vui l?ng nh?p m?t kh?u", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtPass.Focus();
                return;
            }
            
            if (txtConfirmPass.Text.Length == 0)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Vui l?ng xác nh?n m?t kh?u", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtConfirmPass.Focus();
                return;
            }
            
            if (!txtPass.Text.Equals(txtConfirmPass.Text))
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "M?t kh?u xác nh?n không kh?p", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtConfirmPass.Focus();
                return;
            }
            
            // Check if employee exists
            NhanVienDTO nv = nhanVien.GetNV(txtMaNV.Text.Trim());
            if (nv == null || nv.MaNV == null)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "M? nhân viên không t?n t?i trong h? th?ng", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtMaNV.Focus();
                return;
            }
            
            // Check if employee already has account
            TaiKhoanDTO tkExist = taiKhoan.GetTKNV(txtMaNV.Text.Trim());
            if (tkExist != null && tkExist.TaiKhoan != null)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Nhân viên này ð? có tài kho?n", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtMaNV.Focus();
                return;
            }
            
            // Check if username already exists
            TaiKhoanDTO tkUser = taiKhoan.GetTK(txtTK.Text.Trim());
            if (tkUser != null && tkUser.TaiKhoan != null)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thông báo", "Tên tài kho?n ð? ðý?c s? d?ng", MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
                txtTK.Focus();
                return;
            }
            
            try
            {
                // Create account with default permission "0" (basic user)
                string hashedPassword = GetHash(txtPass.Text);
                taiKhoan.ThemTaiKhoan(txtTK.Text.Trim(), txtMaNV.Text.Trim(), "0", hashedPassword, "0");
                
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "Thành công", "Ðãng k? tài kho?n thành công", MessageBoxDialog.SUCCESS, MessageBoxDialog.YES, "Ðóng", "", "");
                
                this.Hide();
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBoxDialog message = new MessageBoxDialog();
                message.ShowDialog("Thông báo", "L?i", "Ðãng k? không thành công: " + ex.Message, MessageBoxDialog.ERROR, MessageBoxDialog.YES, "Ðóng", "", "");
            }
        }

        private void timerOpacity_Tick(object sender, EventArgs e)
        {
            if (Opacity >= 1)
            {
                timerOpacity.Stop();
            }
            else
            {
                Opacity += 0.03;
            }
            if (p.Y <= 278)
            {
                Location = p;
                p.Y += 2;
            }
        }
        
        Point p;
        
        private void frmRegister_Load(object sender, EventArgs e)
        {
            p = new Point(Location.X, 150);
        }
        
        public string GetHash(string matKhau)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(matKhau));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        bool checkPass = true;
        bool checkConfirm = true;
        
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            if (checkPass)
            {
                txtPass.UseSystemPasswordChar = false;
                checkPass = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
                checkPass = true;
            }
        }

        private void btnShowConfirmPass_Click(object sender, EventArgs e)
        {
            if (checkConfirm)
            {
                txtConfirmPass.UseSystemPasswordChar = false;
                checkConfirm = false;
            }
            else
            {
                txtConfirmPass.UseSystemPasswordChar = true;
                checkConfirm = true;
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            this.Close();
        }
    }
}
