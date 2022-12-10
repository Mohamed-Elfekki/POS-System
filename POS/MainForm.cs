using POS.DB;
using POS.Screens.Customers;
using POS.Screens.Products;
using POS.Screens.SalesBill;
using POS.Screens.Suppliers;
using POS.Screens.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUserForm newUserForm = new NewUserForm();
            newUserForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void addProductToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ProductForm p = new ProductForm();
            p.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierManagForm supplierManagForm = new SupplierManagForm();
            supplierManagForm.Show();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            ProductListForm productListForm = new ProductListForm();
            productListForm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            CustomerManageForm customerManageForm = new CustomerManageForm();
            customerManageForm.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ProductListForm productListForm = new ProductListForm();
            productListForm.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CustomerManageForm customerManageForm = new CustomerManageForm();
            customerManageForm.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SupplierManagForm supplierManagForm = new SupplierManagForm();
            supplierManagForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SalesBillForm s = new SalesBillForm();
            s.Show();
;        }
    }
}
