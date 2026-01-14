using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Thư viện quan trọng để nói chuyện với SQL
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCafe.DAL
{
    public class DataProvider
    {
        // 1. Singleton Pattern: Đảm bảo cả chương trình chỉ có duy nhất 1 "người đưa thư" (Instance)
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // 2. Chuỗi kết nối (Địa chỉ nhà SQL của bạn)
        // Mình đã lấy đúng tên máy trong ảnh cũ của bạn điền vào đây rồi
        private string connectionSTR = "Data Source=LAPTOP-HPNUBJ8K\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        // 3. Hàm lấy dữ liệu (Dùng cho câu lệnh SELECT) -> Trả về bảng dữ liệu
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        // 4. Hàm thay đổi dữ liệu (Dùng cho INSERT, UPDATE, DELETE) -> Trả về số dòng thành công
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        // 5. Hàm đếm (Dùng cho câu lệnh SELECT COUNT(*)) -> Trả về giá trị đầu tiên
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}