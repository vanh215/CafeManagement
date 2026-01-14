using QuanLyCafe.DAL;
using QuanLyCafe.DTO;
using System.Collections.Generic;

namespace QuanLyCafe.BLL
{
    public class MenuBLL
    {
        private static MenuBLL instance;

        public static MenuBLL Instance
        {
            get { if (instance == null) instance = new MenuBLL(); return MenuBLL.instance; }
            private set { MenuBLL.instance = value; }
        }

        private MenuBLL() { }

        // Hàm này gọi sang bên DAL (Thủ kho) để lấy thực đơn về
        public List<Menu> GetListMenuByTable(int id)
        {
            return MenuDAL.Instance.GetListMenuByTable(id);
        }
    }
}