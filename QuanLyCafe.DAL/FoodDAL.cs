using QuanLyCafe.DTO;
using System.Collections.Generic;
using System.Data;

namespace QuanLyCafe.DAL
{
    public class FoodDAL
    {
        private static FoodDAL instance;

        public static FoodDAL Instance
        {
            get { if (instance == null) instance = new FoodDAL(); return FoodDAL.instance; }
            private set { FoodDAL.instance = value; }
        }

        private FoodDAL() { }

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "SELECT * FROM Food";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();

            // Lệnh SQL: Chọn món ăn có idCategory bằng id truyền vào
            string query = "select * from Food where idCategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        // Hàm thêm món ăn mới
        public bool InsertFood(string name, int id, float price)
        {
            // Viết câu lệnh SQL chèn dữ liệu
            string query = string.Format("INSERT dbo.Food ( name, idCategory, price ) VALUES  ( N'{0}', {1}, {2})", name, id, price);

            // Thực thi lệnh (trả về số dòng thành công)
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0; 
        }
        // 1. Hàm Sửa món ăn (Update)
        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, idCategory, price, idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        // 2. Hàm Xóa món ăn (Delete)
        public bool DeleteFood(int idFood)
        {
            // Lưu ý: Nếu món đã bán thì không xóa được (do ràng buộc dữ liệu). 
            // Tạm thời dùng lệnh xóa đơn giản:
            string query = string.Format("Delete Food where id = {0}", idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        // Hàm tìm kiếm món ăn theo tên (gần đúng)
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = string.Format("SELECT * FROM dbo.Food WHERE name LIKE N'%{0}%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
    }
}