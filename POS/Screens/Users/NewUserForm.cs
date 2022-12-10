using POS.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Screens.Users
{
    public partial class NewUserForm : Form
    {
        POSEntities db = new POSEntities();
        string imagePath= "";
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtUser.Text !="" && txtPassword.Text !="")
            {
                User u = new User
                {
                    UserName = txtUser.Text,
                    Password = txtPassword.Text,
                    Image = imagePath
                };
                db.Users.Add(u);
                db.SaveChanges();
                MessageBox.Show("Saved Successfully!");

                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + $"\\Images\\Users\\{u.Id}.jpg";
                    File.Copy(imagePath, newPath);
                    u.Image = newPath;
                    db.SaveChanges();
                }
                this.Close();
                Thread thread = new Thread(openForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();


            }
            else
            {
                MessageBox.Show("Please Enter Data Correct!");
            }

        }

        private void openForm()
        {
            Application.Run(new MainForm());
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


    }
}
