using QuanLyCafe.DTO;
using System.Data;

namespace QuanLyCafe.DAL
{
    public class BillInfoDAL
    {
        private static BillInfoDAL instance;

        public static BillInfoDAL Instance
        {
            get { if (instance == null) instance = new BillInfoDAL(); return BillInfoDAL.instance; }
            private set { BillInfoDAL.instance = value; }
        }

        private BillInfoDAL() { }

        // Hàm thêm món vào chi tiết hóa đơn
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
    }
}