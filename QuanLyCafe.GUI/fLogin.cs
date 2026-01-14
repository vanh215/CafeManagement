using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCafe.BLL; // Để giao diện gọi được thằng Nghiệp vụ
using QuanLyCafe.DTO;
namespace QuanLyCafe.GUI
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Hiện ra bảng hỏi người dùng có chắc chắn muốn thoát không
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit(); // Lệnh tắt chương trình
            }
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true; // Nếu chọn Cancel thì hủy lệnh thoát (giữ nguyên form)
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text; // Lấy chữ người dùng nhập vào ô Tài khoản
            string passWord = txbPassWord.Text; // Lấy chữ người dùng nhập vào ô Mật khẩu

            // Gọi lớp BLL kiểm tra xem đúng không
            if (AccountBLL.Instance.Login(userName, passWord))
            {
                if (AccountBLL.Instance.Login(userName, passWord))
                {
                    // 1. Tạo form Quản lý bàn
                    fTableManager f = new fTableManager();

                    // 2. Ẩn form Đăng nhập đi (để đỡ vướng mắt)
                    this.Hide();

                    // 3. Hiện form Quản lý bàn lên
                    f.ShowDialog(); // Lệnh này sẽ giữ chương trình dừng ở đây cho đến khi tắt form f đi

                    // 4. Khi tắt form Quản lý bàn thì hiện lại form Đăng nhập (để người khác đăng nhập)
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
