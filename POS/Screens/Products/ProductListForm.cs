using POS.DB;
using POS.Screens.Users;
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
    public partial class ProductListForm : Form
    {
        POSEntities db = new POSEntities();
        int _id = 0;
        Product result;
        private string imagePath;

        public ProductListForm()
        {
            InitializeComponent();
           dataGridView1.DataSource = db.Products.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "")
            {
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtCode.Text).ToList();

            }
            else
            dataGridView1.DataSource = db.Products.Where(x => x.Code == txtCode.Text || x.Name.Contains(txtName.Text)).ToList();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                result = db.Products.SingleOrDefault(x => x.Id == _id);
                txtUpCode.Text = result.Code;
                txtUpName.Text = result.Name;
                txtPrice.Text = result.Price.ToString();
                txtQuan.Text = result.Quantity.ToString();
                txtNote.Text = result.Notes;
                pictureBox1.ImageLocation = result.Image;
                comboBox1.SelectedValue = result.CategoryId;
            }
            catch { }
            
        }

        private void btnUpSave_Click(object sender, EventArgs e)
        {
            result.Name = txtUpName.Text;
            result.Code = txtUpCode.Text;
            result.Price = decimal.Parse(txtPrice.Text);
            result.Quantity = int.Parse(txtQuan.Text);
            result.Notes = txtNote.Text;
            result.CategoryId = int.Parse(comboBox1.SelectedValue.ToString());
            
            if (imagePath != "")
            {
                string newPath = Environment.CurrentDirectory + $"\\Images\\Products\\{result.Id}.jpg";
                File.Copy(imagePath, newPath,true);
                result.Image = newPath;
            }
            db.SaveChanges();
            MessageBox.Show("item Updated Successfully!");
            dataGridView1.DataSource = db.Products.ToList();
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Warning","Delete order!",MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Products.Find(_id);
                db.Products.Remove(result);
                db.SaveChanges();
                MessageBox.Show("Deleted Successfully!");
                dataGridView1.DataSource = db.Products.ToList();
            }

        }

        private void label13_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
        }

        private void ProductListForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sMSDataSet1.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.sMSDataSet1.Category);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catid = int.Parse(comboBox2.SelectedValue.ToString());
            dataGridView1.DataSource =db.Products.Where(x => x.CategoryId == catid).ToList();
        }
    }
}
