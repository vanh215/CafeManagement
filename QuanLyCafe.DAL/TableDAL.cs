using QuanLyCafe.DTO; // Nhớ dùng DTO
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DAL
{
    public class TableDAL
    {
        private static TableDAL instance;

        public static TableDAL Instance
        {
            get { if (instance == null) instance = new TableDAL(); return TableDAL.instance; }
            private set { TableDAL.instance = value; }
        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;

        private TableDAL() { }

        // Hàm lấy danh sách bàn trả về dạng List
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            // Lấy tất cả dữ liệu từ bảng TableFood
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList"); // Chút nữa mình sẽ tạo cái Store Procedure này bên SQL

            // Chuyển từng dòng dữ liệu thành object Table và thêm vào List
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
        // 1. Thêm bàn mới (Mặc định trạng thái là Trống)
        public bool InsertTable(string name)
        {
            // Mặc định thêm bàn mới thì status là 'Trống'
            string query = string.Format("INSERT dbo.TableFood (name, status) VALUES (N'{0}', N'Trống')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 2. Sửa tên bàn
        public bool UpdateTable(string name, int id)
        {
            string query = string.Format("UPDATE dbo.TableFood SET name = N'{0}' WHERE id = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 3. Xóa bàn
        public bool DeleteTable(int id)
        {
            // Lưu ý: Chỉ xóa được bàn trống, bàn đang có người ngồi xóa sẽ lỗi hoặc mất dữ liệu
            string query = string.Format("DELETE dbo.TableFood WHERE id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        // Hàm lấy danh sách tất cả bàn ăn
        public List<Table> GetListTable()
        {
            List<Table> list = new List<Table>();

            // Lấy tất cả dữ liệu từ bảng TableFood
            string query = "SELECT * FROM dbo.TableFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
    }
}