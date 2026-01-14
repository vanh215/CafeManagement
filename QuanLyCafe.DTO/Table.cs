using System;
using System.Collections.Generic;
using System.Data; // Thư viện để xử lý dòng dữ liệu
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DTO
{
    public class Table
    {
        // Khai báo các thuộc tính giống hệt trong SQL
        private int iD;
        private string name;
        private string status;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }

        // Hàm dựng 1: Tự tạo bàn bằng tay
        public Table(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        // Hàm dựng 2: Tự động lấy dữ liệu từ dòng trong SQL đổ vào
        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
    }
}