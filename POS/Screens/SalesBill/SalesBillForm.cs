using POS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Screens.SalesBill
{

    public partial class SalesBillForm : Form
    {
        POSEntities db = new POSEntities();
        List<Product> products;
        public SalesBillForm()
        {
            InitializeComponent();
           comboBox1.DataSource = db.Customers.Where(x => x.IsActive == true).ToList();
           comboBox1.SelectedValue = "Id";
           comboBox1.SelectedText = "Name";
            products =db.Products.ToList();
            imageList1.ImageSize = new Size(80, 80);
            lblUser.Text = "Hi: "+Userss.Name ;

        }

        private void SalesBillForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < products.Count(); i++)
            {
                if (products[i].Image != null)
                {
                    imageList1.Images.Add(Image.FromFile(products[i].Image));
                }
                else
                {
                    Bitmap bitmap = new Bitmap(80, 80);
                    imageList1.Images.Add(bitmap);
                }

                ListViewItem item = new ListViewItem();
                item.Text = products[i].Name;
                item.ImageIndex = i;
                item.Tag = products[i];
                listView1.Items.Add(item);
            }


        }

        void CalculateTotal()
        {
            try
            {
                decimal total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += decimal.Parse(dataGridView1.Rows[i].Cells["totalprice"].Value.ToString());

                }
                lblTotal.Text = total.ToString();
                decimal disc = decimal.Parse(txtDiscount.Text);
                lblTotalAfterDis.Text = (total - disc).ToString();
            }
            catch { }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {

                var product = (Product)listView1.SelectedItems[0].Tag;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == product.Id.ToString())
                    {
                        dataGridView1.Rows[i].Cells["quantity"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) + 1;
                        dataGridView1.Rows[i].Cells["totalprice"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) * decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString());
                        CalculateTotal();
                        return;
                    }
                }
                dataGridView1.Rows.Add(product.Id, product.Name, product.Price,1, product.Price * 1, Image.FromFile(product.Image));
                CalculateTotal();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            POS.DB.SalesBill sale = new POS.DB.SalesBill()
            {
                Date = (DateTime)dateTimePicker1.Value.Date,
                Discount = decimal.Parse(txtDiscount.Text),
                Total = decimal.Parse(lblTotal.Text),
                TotalAfterDiscount = decimal.Parse(lblTotalAfterDis.Text),
                Notes = txtNote.Text,
                CustomerId = int.Parse(comboBox1.SelectedValue.ToString()),
                UserId = Userss.id

            };
            List<SalesBillDetail> list = new List<SalesBillDetail>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                list.Add(new SalesBillDetail
                {
                    ProductId = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                    Quantity = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()),
                    Price = decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString()),
                    TotalPrice = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) * decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString()),
                });

            }    
                sale.SalesBillDetails = list;
                db.SalesBills.Add(sale);
                db.SaveChanges(); 
                MessageBox.Show("Saved Successfully! " + sale.Id);

        }
    }
}
