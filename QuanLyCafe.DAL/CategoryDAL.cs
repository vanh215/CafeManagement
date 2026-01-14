using QuanLyCafe.DTO;
using System.Collections.Generic;
using System.Data;

namespace QuanLyCafe.DAL
{
    public class CategoryDAL
    {
        private static CategoryDAL instance;

        public static CategoryDAL Instance
        {
            get { if (instance == null) instance = new CategoryDAL(); return CategoryDAL.instance; }
            private set { CategoryDAL.instance = value; }
        }

        private CategoryDAL() { }

        // Hàm lấy danh sách tất cả danh mục
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "SELECT * FROM FoodCategory"; // Lưu ý: Tên bảng trong SQL thường là FoodCategory

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }

            return list;
        }
        // 1. Thêm danh mục
        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT dbo.FoodCategory (name) VALUES (N'{0}')", name);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 2. Sửa danh mục
        public bool UpdateCategory(string name, int id)
        {
            string query = string.Format("UPDATE dbo.FoodCategory SET name = N'{0}' WHERE id = {1}", name, id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 3. Xóa danh mục
        public bool DeleteCategory(int id)
        {
            // Lưu ý: Nếu xóa danh mục thì các món ăn trong danh mục đó sẽ bị lỗi.
            // Tạm thời mình xóa các món trong danh mục đó trước (nếu muốn kỹ hơn), hoặc chỉ xóa danh mục rỗng.
            // Ở đây viết lệnh xóa đơn giản:
            string query = string.Format("DELETE dbo.FoodCategory WHERE id = {0}", id);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}