using POS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Screens.Products
{
    public partial class ProductForm : Form
    {
        POSEntities db =new POSEntities();
        string imagePath ="";
        public ProductForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text !="" && txtPrice.Text !="" && txtCode.Text !="" || comboBox1.SelectedValue != null) 
            {
                int qty;
                decimal price;
                int.TryParse(txtQuan.Text, out qty);
                decimal.TryParse(txtPrice.Text, out price);
                Product product = new Product
                {
                    Name = txtName.Text,
                    Code = txtCode.Text,
                    Notes = txtNote.Text,
                    CategoryId = int.Parse(comboBox1.SelectedValue.ToString()),
                    Image = imagePath,
                    Quantity = qty,
                    Price = price
                };
                db.Products.Add(product);
                db.SaveChanges();
                MessageBox.Show("Saved Successfully!");

                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + $"\\Images\\Products\\{product.Id}.jpg";
                    File.Copy(imagePath, newPath);
                    product.Image = newPath;
                    db.SaveChanges();
                }

               
            }
            else
            {
                MessageBox.Show("Invalid Input!");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    pictureBox1.ImageLocation = dialog.FileName;
                }

        }

        private void label11_Click(object sender, EventArgs e)
        {
            ProductListForm productListForm = new ProductListForm();
            productListForm.Show();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sMSDataSet.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.sMSDataSet.Category);

        }
    }
}
