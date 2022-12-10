using POS.DB;
using POS.Screens.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Form1 : Form
    {
        POSEntities db = new POSEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = db.Users.FirstOrDefault(x=>x.UserName ==txtUser.Text && x.Password == txtPass.Text);
            if (txtUser.Text !="" || txtPass.Text !="")
            {
                if (result != null)
                {
                    this.Close();
                    Thread thread = new Thread(openForm);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();

                    Userss.Name = result.UserName;
                    Userss.id = result.Id;
                }
                else
                {
                    MessageBox.Show("Invalid Input!");
                    txtUser.Text = "";
                    txtPass.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Invalid Input!");
            }


        }
        void openForm()
        {
            Application.Run(new MainForm());
        }
        void openNewForm()
        {
            Application.Run(new NewUserForm());
        }




        private void label4_Click(object sender, EventArgs e)
        {
            NewUserForm f =new NewUserForm();
            f.Show();
            this.Close();
            Thread thread = new Thread(openNewForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

    }
    static class Userss
    {
        static public string Name { set; get; }
        static public int id { set; get; }
    }
}
