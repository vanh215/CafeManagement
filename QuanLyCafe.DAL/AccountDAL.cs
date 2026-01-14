using System;
using System.Collections.Generic;
using System.Data; // Thư viện chứa DataTable
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DAL
{
    public class AccountDAL
    {
        // 1. Tạo Singleton (Để duy nhất 1 người được vào kho lấy dữ liệu tài khoản)
        private static AccountDAL instance;

        public static AccountDAL Instance
        {
            get { if (instance == null) instance = new AccountDAL(); return instance; }
            private set { instance = value; }
        }

        private AccountDAL() { }

        // 2. Hàm kiểm tra đăng nhập (Trả về True nếu đúng, False nếu sai)
        public bool Login(string userName, string passWord)
        {
            // Viết câu truy vấn SQL: Chọn tất cả từ bảng Account nếu Tên và Mật khẩu trùng khớp
            // Lưu ý: Đây là cách viết cơ bản, bài sau mình sẽ hướng dẫn dùng @Parameter để chống hack SQL Injection sau nhé.
            string query = "SELECT * FROM dbo.Account WHERE UserName = N'" + userName + "' AND PassWord = N'" + passWord + "'";

            // Nhờ thằng DataProvider chạy câu lệnh này và lấy kết quả về
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            // Nếu kết quả trả về có số dòng > 0 (nghĩa là tìm thấy tài khoản) -> Thành công
            return result.Rows.Count > 0;
        }
        // 1. Thêm tài khoản
        public bool InsertAccount(string name, string displayName, int type)
        {
            string query = string.Format("INSERT dbo.Account (UserName, DisplayName, Type) VALUES (N'{0}', N'{1}', {2})", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 2. Sửa tài khoản (Chỉ sửa tên hiển thị và loại, không sửa tên đăng nhập)
        public bool UpdateAccount(string name, string displayName, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayName, type);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 3. Xóa tài khoản
        public bool DeleteAccount(string name)
        {
            string query = string.Format("Delete dbo.Account where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 4. Đặt lại mật khẩu (Về mặc định là '0')
        public bool ResetPassword(string name)
        {
            string query = string.Format("update dbo.Account set Password = N'0' where UserName = N'{0}'", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public DataTable GetListAccount()
        {
            // Lấy 3 cột cần thiết: Tên đăng nhập, Tên hiển thị, Loại
            return DataProvider.Instance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account");
        }
    }
}