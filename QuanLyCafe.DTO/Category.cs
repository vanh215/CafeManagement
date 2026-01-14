using System;
using System.Data;
using System.Linq;

namespace QuanLyCafe.DTO
{
    public class Category
    {
        // Khai báo thuộc tính (giống hệt cột trong bảng FoodCategory bên SQL)
        private int iD;
        private string name;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }

        // Hàm dựng 1: Tự tạo bằng tay
        public Category(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        // Hàm dựng 2: Tự động lấy dữ liệu từ dòng SQL đổ vào
        public Category(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
        }
    }
}