using QuanLyCafe.DAL;
using QuanLyCafe.DTO;
using System.Collections.Generic;

namespace QuanLyCafe.BLL
{
    public class FoodBLL
    {
        private static FoodBLL instance;

        public static FoodBLL Instance
        {
            get { if (instance == null) instance = new FoodBLL(); return FoodBLL.instance; }
            private set { FoodBLL.instance = value; }
        }

        private FoodBLL() { }

        // Gọi hàm thêm món từ DAL
        public bool InsertFood(string name, int id, float price)
        {
            return FoodDAL.Instance.InsertFood(name, id, price);
        }
        // Hàm lấy danh sách món theo ID danh mục
        public List<Food> GetFoodByCategoryID(int id)
        {
            return FoodDAL.Instance.GetFoodByCategoryID(id);
        }
        public bool DeleteFood(int idFood)
        {
            return FoodDAL.Instance.DeleteFood(idFood);
        }
        // Hàm UpdateFood (BLL gọi xuống DAL)
        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            // Lúc này FoodDAL đã có hàm UpdateFood rồi nên dòng dưới này sẽ hết đỏ
            return FoodDAL.Instance.UpdateFood(idFood, name, idCategory, price);
        }
        public List<Food> SearchFoodByName(string name)
        {
            return FoodDAL.Instance.SearchFoodByName(name);
        }
    }

}