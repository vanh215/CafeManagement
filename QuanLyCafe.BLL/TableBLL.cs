using QuanLyCafe.DAL;
using QuanLyCafe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.BLL
{
    public class TableBLL
    {
        // 1. Singleton Pattern
        private static TableBLL instance;

        public static TableBLL Instance
        {
            get { if (instance == null) instance = new TableBLL(); return TableBLL.instance; }
            private set { TableBLL.instance = value; }
        }

        private TableBLL() { }

        // 2. Hàm lấy danh sách bàn (Gọi từ DAL lên)
        public List<Table> LoadTableList()
        {
            return TableDAL.Instance.LoadTableList();
        }
        public bool InsertTable(string name)
        {
            return TableDAL.Instance.InsertTable(name);
        }

        public bool UpdateTable(string name, int id)
        {
            return TableDAL.Instance.UpdateTable(name, id);
        }

        public bool DeleteTable(int id)
        {
            return TableDAL.Instance.DeleteTable(id);
        }
        
        public List<Table> GetListTable()
        {
            return TableDAL.Instance.GetListTable();
        }
    }
}