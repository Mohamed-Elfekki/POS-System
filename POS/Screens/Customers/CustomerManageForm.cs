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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace POS.Screens.Customers
{
    public partial class CustomerManageForm : Form
    {
        POSEntities db = new POSEntities();
        int _id = 0;
        Customer result;
        string imagePath="";
        public CustomerManageForm()
        {
            InitializeComponent();
            IsActiveBox.Checked = false;
           dataGridView1.DataSource = db.Customers.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtNameSrch.Text == "")
            {
                dataGridView1.DataSource = db.Customers.Where(x => x.Phone.Contains(txtPhoneSrch.Text)).ToList();

            }
            else
                dataGridView1.DataSource = db.Customers.Where(x=> x.Name.Contains(txtNameSrch.Text) && x.Phone.Contains(txtPhoneSrch.Text)).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           var r = MessageBox.Show("Are you Sure about Adding New Customer!", "New Customer!", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                if (txtName.Text != "" && txtPhone.Text != "")
                {

                    Customer c = new Customer
                    {
                        Name = txtName.Text,
                        Phone = txtPhone.Text,
                        Notes = txtNote.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        Company = txtcomp.Text,
                        IsActive = IsActiveBox.Checked

                    };
                    db.Customers.Add(c);
                    db.SaveChanges();
                    MessageBox.Show("Saved Successfully!");

                    if (imagePath != "")
                    {
                        string newPath = Environment.CurrentDirectory + $"\\Images\\Customers\\{c.Id}.jpg";
                        File.Copy(imagePath, newPath);
                        c.Image = newPath;
                        db.SaveChanges();
                    }

                    txtAddress.Text = "";
                    txtcomp.Text = "";
                    txtEmail.Text = "";
                    txtName.Text = "";
                    txtNote.Text = "";
                    txtPhone.Text = "";
                    pictureBox1.ImageLocation = "";
                    IsActiveBox.Checked = false;
                    dataGridView1.DataSource = db.Customers.ToList();

                }
                else
                {
                    MessageBox.Show("Invalid Input!");
                }
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                result = db.Customers.SingleOrDefault(x => x.Id == _id);

                txtAddress.Text = result.Address;
                txtcomp.Text =result.Company;
                txtEmail.Text = result.Email;
                txtName.Text = result.Name;
                txtPhone.Text = result.Phone;
                txtNote.Text = result.Notes;
                if (result.IsActive ==null)
                {
                    result.IsActive = false;
                }
                IsActiveBox.Checked = (bool)result.IsActive;
                pictureBox1.ImageLocation = result.Image;
            }
            catch { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Warning", "Delete Customer!", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                var result = db.Customers.Find(_id);
                db.Customers.Remove(result);
                db.SaveChanges();
                MessageBox.Show("Deleted Successfully!");
                dataGridView1.DataSource = db.Customers.ToList();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            result.Address = txtAddress.Text ;
            result.Company = txtcomp.Text;
            result.Email = txtEmail.Text;
            result.Name = txtName.Text;
            result.Phone = txtPhone.Text;
            result.Notes = txtNote.Text;
            result.IsActive = IsActiveBox.Checked;

            if (imagePath != "")
            {
                string newPath = Environment.CurrentDirectory + $"\\Images\\Customers\\{result.Id}.jpg";
                File.Copy(imagePath, newPath, true);
                result.Image = newPath;
            }
            db.SaveChanges();
            MessageBox.Show("Customer Updated Successfully!");
            dataGridView1.DataSource = db.Customers.ToList();
        }
    }
}
