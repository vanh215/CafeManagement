using QuanLyCafe.DAL;
using QuanLyCafe.DTO;
using System.Collections.Generic;

namespace QuanLyCafe.BLL
{
    public class CategoryBLL
    {
        private static CategoryBLL instance;

        public static CategoryBLL Instance
        {
            get { if (instance == null) instance = new CategoryBLL(); return CategoryBLL.instance; }
            private set { CategoryBLL.instance = value; }
        }

        private CategoryBLL() { }

        public List<Category> GetListCategory()
        {
            return CategoryDAL.Instance.GetListCategory();
        }
        public bool InsertCategory(string name)
        {
            return CategoryDAL.Instance.InsertCategory(name);
        }

        public bool UpdateCategory(string name, int id)
        {
            return CategoryDAL.Instance.UpdateCategory(name, id);
        }

        public bool DeleteCategory(int id)
        {
            return CategoryDAL.Instance.DeleteCategory(id);
        }
    }
}