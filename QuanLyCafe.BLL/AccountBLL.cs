using QuanLyCafe.DAL; // Để gọi được thằng DAL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyCafe.BLL
{
    public class AccountBLL
    {
        // 1. Singleton Pattern (Giống hệt bên DAL)
        private static AccountBLL instance;

        public static AccountBLL Instance
        {
            get { if (instance == null) instance = new AccountBLL(); return instance; }
            private set { instance = value; }
        }

        private AccountBLL() { }

        // 2. Hàm xử lý đăng nhập
        public bool Login(string userName, string passWord)
        {
            // Gọi xuống lớp DAL để kiểm tra
            return AccountDAL.Instance.Login(userName, passWord);
        }
        // (Nhớ đảm bảo class này đã có hàm GetListAccount gọi từ DAL nhé)

        public bool InsertAccount(string name, string displayName, int type)
        {
            return AccountDAL.Instance.InsertAccount(name, displayName, type);
        }

        public bool UpdateAccount(string name, string displayName, int type)
        {
            return AccountDAL.Instance.UpdateAccount(name, displayName, type);
        }

        public bool DeleteAccount(string name)
        {
            return AccountDAL.Instance.DeleteAccount(name);
        }

        public bool ResetPassword(string name)
        {
            return AccountDAL.Instance.ResetPassword(name);
        }
        public DataTable GetListAccount()
        {
            return AccountDAL.Instance.GetListAccount();
        }
    }
}