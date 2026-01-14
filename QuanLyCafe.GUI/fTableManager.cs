// ... (Các using giữ nguyên) ...
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCafe.DTO;
using QuanLyCafe.BLL;
using System.Globalization; // Thư viện để định dạng tiền tệ (VND)
using Menu = QuanLyCafe.DTO.Menu;

namespace QuanLyCafe.GUI
{
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
        }
       

        void LoadCategory()
        {
            List<Category> listCategory = CategoryBLL.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodBLL.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
        }

        void LoadTable()
        {
            flpTable.Controls.Clear(); // Xóa bàn cũ đi trước khi load lại
            List<Table> tableList = TableBLL.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = 90, Height = 90 };
                btn.Text = item.Name + Environment.NewLine + item.Status;

                // Gắn ID của bàn vào nút (Để tý nữa bấm vào còn biết là bàn số mấy)
                btn.Tag = item;

                // Gắn sự kiện: Bấm vào nút -> Chạy hàm btn_Click
                btn.Click += btn_Click;

                if (item.Status == "Trống")
                    btn.BackColor = Color.Aqua;
                else
                    btn.BackColor = Color.LightPink;

                flpTable.Controls.Add(btn);
            }
        }

        // Hàm hiển thị hóa đơn của bàn
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();

            // 1. Lưu bàn đang chọn để tý nữa biết thanh toán cho bàn nào
            List<Table> tableList = TableBLL.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                if (item.ID == id)
                {
                    lsvBill.Tag = item;
                    break;
                }
            }

            // 2. Lấy danh sách món ăn và tính tổng tiền
            List<Menu> listBillInfo = MenuBLL.Instance.GetListMenuByTable(id);

            float totalPrice = 0; // Biến để cộng dồn tiền

            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice; // Cộng tiền từng món vào tổng

                lsvBill.Items.Add(lsvItem);
            }

            // 3. Hiển thị Tổng tiền ra cái ô TextBox (MỚI THÊM ĐOẠN NÀY)
            // Định dạng tiền Việt Nam (đ)
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c", culture);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            // 1. Lấy bàn đang được chọn (Lấy từ cái Tag mình vừa lưu ở trên)
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn trước!", "Thông báo");
                return;
            }

            // 2. Lấy thông tin từ giao diện (Món gì, số lượng bao nhiêu)
            int idBill = BillBLL.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            // 3. Xử lý thêm món
            if (idBill == -1) // Nếu bàn chưa có hóa đơn -> Tạo mới
            {
                BillBLL.Instance.InsertBill(table.ID);
                int idBillMax = BillBLL.Instance.GetUncheckBillIDByTableID(table.ID);
                BillInfoBLL.Instance.InsertBillInfo(idBillMax, foodID, count);
            }
            else // Nếu bàn có hóa đơn rồi -> Chỉ thêm món vào
            {
                BillInfoBLL.Instance.InsertBillInfo(idBill, foodID, count);
            }

            // 4. Load lại dữ liệu để cập nhật hiển thị ngay lập tức
            ShowBill(table.ID);
            LoadTable();

        }

        
 
            private void btnCheckOut_Click(object sender, EventArgs e)
            {
                // 1. Lấy bàn đang chọn
                Table table = lsvBill.Tag as Table;
                if (table == null) return;

                // 2. Lấy ID hóa đơn
                int idBill = BillBLL.Instance.GetUncheckBillIDByTableID(table.ID);
                int discount = (int)nmDiscount.Value;

                if (idBill != -1) // Nếu bàn có hóa đơn
                {
               double totalPrice = 0;
                    List<Menu> listBillInfo = MenuBLL.Instance.GetListMenuByTable(table.ID);
                    foreach (Menu item in listBillInfo)
                    {
                        totalPrice += item.TotalPrice;
                    }
                    double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
                    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vi-VN");

                    string msg = string.Format("Bạn có chắc thanh toán hóa đơn cho {0}?\n\nTổng tiền: {1}\nGiảm giá: {2}%\n\nTHÀNH TIỀN: {3}",
                        table.Name,
                        totalPrice.ToString("c", culture),
                        discount,
                        finalTotalPrice.ToString("c", culture));

                    if (MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        BillBLL.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                        ShowBill(table.ID);
                        LoadTable();
                    }
                }
            }

        
            private void adminToolStripMenuItem_Click(object sender, EventArgs e)
            {
                fAdmin f = new fAdmin();
                f.ShowDialog();
            }
        // 1. Hàm xử lý khi bấm vào nút Bàn ăn (Sửa lỗi btn_Click)
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag; // Lưu bàn vừa chọn vào Tag để lát nữa thanh toán dùng
            ShowBill(tableID); // Hiện hóa đơn của bàn đó
        }
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListByCategoryID(id); // Hàm này để tải món ăn theo danh mục (nếu bạn chưa có thì báo mình nhé)
        }
    }
}