using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCafe.BLL;
using QuanLyCafe.DAL;

namespace QuanLyCafe.GUI
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            dtgvFood.DataSource = foodList; // Gán nguồn dữ liệu
            LoadListFood(); // Tải danh sách món ăn
            LoadCategoryIntoCombobox(cbFoodCategory); // <--- BẠN CHÈN DÒNG NÀY VÀO ĐÂY
            AddFoodBinding(); // Kích hoạt binding
            dtgvCategory.DataSource = categoryList; // Gắn bảng vào dây trung gian
            LoadListCategory();                     // Tải dữ liệu
            AddCategoryBinding();
            dtgvTable.DataSource = tableList;
            LoadListTable();
            AddTableBinding();
            dtgvAccount.DataSource = accountList;
            LoadAccount();
            AddAccountBinding();
            LoadDateTime(); 
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        void AddFoodBinding()
        {
            // 1. Xóa binding cũ
            txtFoodName.DataBindings.Clear();
            txtFoodID.DataBindings.Clear();
            nmFoodPrice.DataBindings.Clear();
            cbFoodCategory.DataBindings.Clear();

            // 2. Thêm binding mới (LƯU Ý: Dùng foodList thay vì dtgvFood.DataSource)
            txtFoodName.DataBindings.Add(new Binding("Text", foodList, "Name", true, DataSourceUpdateMode.Never));
            txtFoodID.DataBindings.Add(new Binding("Text", foodList, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", foodList, "Price", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("SelectedValue", foodList, "CategoryID", true, DataSourceUpdateMode.Never));
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillBLL.Instance.GetBillListByDate(checkIn, checkOut);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAL.Instance.GetListFood();
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAL.Instance.GetListCategory();
            cb.DisplayMember = "Name"; // Hiển thị Tên danh mục
            cb.ValueMember = "ID";     // Nhưng giá trị thực là ID
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ các ô nhập
            string name = txtFoodName.Text;
            int categoryID = (int)cbFoodCategory.SelectedValue; // Lấy ID của danh mục đang chọn
            float price = (float)nmFoodPrice.Value;

            // 2. Kiểm tra tên món có bị trống không
            if (name == "")
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!");
                return;
            }

            // 3. Gọi BLL để thêm món
            if (FoodBLL.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood(); // Tải lại danh sách để thấy món mới
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu
            string name = txtFoodName.Text;
            int categoryID = (int)cbFoodCategory.SelectedValue;
            float price = (float)nmFoodPrice.Value;

            // Kiểm tra ID
            if (txtFoodID.Text == "")
            {
                MessageBox.Show("Hãy chọn món cần sửa!");
                return;
            }
            int id = Convert.ToInt32(txtFoodID.Text);

            // Gọi lệnh Sửa
            if (FoodBLL.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa thành công!");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa!");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            // Kiểm tra ID
            if (txtFoodID.Text == "")
            {
                MessageBox.Show("Hãy chọn món cần xóa!");
                return;
            }
            int id = Convert.ToInt32(txtFoodID.Text);

            // Hỏi chắc chắn chưa
            if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                // Gọi lệnh Xóa
                if (FoodBLL.Instance.DeleteFood(id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadListFood();
                }
                else
                {
                    MessageBox.Show("Không xóa được (Có thể món này dính líu đến hóa đơn cũ)!");
                }
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = FoodBLL.Instance.SearchFoodByName(txtSearchFoodName.Text);
        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryBLL.Instance.GetListCategory();
        }

        void AddCategoryBinding()
        {
            // Nhớ kiểm tra kỹ tên TextBox và Label bên tab Danh mục nhé!
            // Giả sử ô nhập tên là txtCategoryName, ô ID là txtCategoryID

            txtCategoryName.DataBindings.Clear();
            txtCategoryID.DataBindings.Clear();

            txtCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text; // Lấy tên từ ô nhập
            if (CategoryBLL.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm thành công!");
                LoadListCategory(); // Load lại bảng
            }
            else MessageBox.Show("Lỗi khi thêm!");
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            int id = Convert.ToInt32(txtCategoryID.Text); // Lấy ID

            if (CategoryBLL.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa thành công!");
                LoadListCategory();
            }
            else MessageBox.Show("Lỗi khi sửa!");
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);

            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (CategoryBLL.Instance.DeleteCategory(id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadListCategory();
                }
                else MessageBox.Show("Không xóa được (Có thể do danh mục này đang có món ăn)!");
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        void LoadListTable()
        {
            tableList.DataSource = TableBLL.Instance.GetListTable(); 
        }

        void AddTableBinding()
        {
            txtTableID.DataBindings.Clear();
            txtTableName.DataBindings.Clear();

            txtTableID.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if (TableBLL.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công");
                LoadListTable();
            }
            else MessageBox.Show("Lỗi khi thêm bàn");
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            int id = Convert.ToInt32(txtTableID.Text);

            if (TableBLL.Instance.UpdateTable(name, id))
            {
                MessageBox.Show("Sửa bàn thành công");
                LoadListTable();
            }
            else MessageBox.Show("Lỗi khi sửa bàn");
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTableID.Text);

            if (MessageBox.Show("Bạn có thật sự muốn xóa bàn này?", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (TableBLL.Instance.DeleteTable(id))
                {
                    MessageBox.Show("Xóa thành công");
                    LoadListTable();
                }
                else MessageBox.Show("Xóa thất bại (Bàn đang có người hoặc lỗi hệ thống)");
            }
        }

      
        void LoadAccount()
        {
            accountList.DataSource = AccountBLL.Instance.GetListAccount();
        }

        void AddAccountBinding()
        {
            txtUserName.DataBindings.Clear();
            txtDisplayName.DataBindings.Clear();
            nmAccountType.DataBindings.Clear();

            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        // XEM
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        // THÊM
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            if (AccountBLL.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
                LoadAccount();
            }
            else MessageBox.Show("Thêm thất bại");
        }

        // SỬA
        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nmAccountType.Value;

            if (AccountBLL.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadAccount();
            }
            else MessageBox.Show("Cập nhật thất bại");
        }

        // XÓA
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            // Không cho xóa chính mình (tài khoản đang đăng nhập) - code này nâng cao, làm sau cũng được
            // if (loginAccount.UserName.Equals(userName)) { MessageBox.Show("Đừng xóa chính mình chứ!"); return;}

            if (MessageBox.Show("Bạn có thật sự muốn xóa tài khoản này?", "Cảnh báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (AccountBLL.Instance.DeleteAccount(userName))
                {
                    MessageBox.Show("Xóa thành công");
                    LoadAccount();
                }
                else MessageBox.Show("Xóa thất bại");
            }
        }

        // ĐẶT LẠI MẬT KHẨU (Nút mới)
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;

            if (AccountBLL.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else MessageBox.Show("Đặt lại mật khẩu thất bại");
        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void LoadDateTime()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
    }
}
