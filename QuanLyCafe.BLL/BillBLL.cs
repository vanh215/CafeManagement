using QuanLyCafe.DAL;
using QuanLyCafe.DTO;
using System.Data; 
using System.Collections.Generic;
using System;

namespace QuanLyCafe.BLL
{
    public class BillBLL
    {
        private static BillBLL instance;

        public static BillBLL Instance
        {
            get { if (instance == null) instance = new BillBLL(); return BillBLL.instance; }
            private set { BillBLL.instance = value; }
        }

        private BillBLL() { }

        // 1. Lấy ID hóa đơn chưa thanh toán theo bàn
        public int GetUncheckBillIDByTableID(int id)
        {
            return BillDAL.Instance.GetUncheckBillIDByTableID(id);
        }

        // 2. Thêm hóa đơn mới
        public void InsertBill(int id)
        {
            BillDAL.Instance.InsertBill(id);
        }

        // 3. Thanh toán
        public void CheckOut(int id, int discount, float totalPrice)
        {
            BillDAL.Instance.CheckOut(id, discount, totalPrice);
        }

        // 4. Lấy danh sách hóa đơn theo ngày (Dùng cho nút Thống kê)
        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return BillDAL.Instance.GetBillListByDate(checkIn, checkOut);
        }

    }
}