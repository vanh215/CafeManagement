using QuanLyCafe.DAL;
using QuanLyCafe.DTO;

namespace QuanLyCafe.BLL
{
    public class BillInfoBLL
    {
        private static BillInfoBLL instance;

        public static BillInfoBLL Instance
        {
            get { if (instance == null) instance = new BillInfoBLL(); return BillInfoBLL.instance; }
            private set { BillInfoBLL.instance = value; }
        }

        private BillInfoBLL() { }

        // Gọi xuống DAL để thêm món ăn
        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            BillInfoDAL.Instance.InsertBillInfo(idBill, idFood, count);
        }
    }
}