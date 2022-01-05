using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digital_Diary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=MHJ\MSSQLSERVER01;Initial Catalog=msdb;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
         if(userBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("Enter Username");
                userBox1.Focus();
            }
            else if (passBox2.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter Password");
                passBox2.Focus();
            }
            try
            {
                con.Open();
                string qry = "SELECT Username,Password From logins where Username='" + userBox1.Text.Trim() + "'AND password='"+ passBox2.Text.Trim()+"'";
                SqlCommand cmd = new SqlCommand(qry, con);
               SqlDataReader rdr =cmd.ExecuteReader();
                    //con.Close();
                if(rdr.Read()==true)
                {
                    this.Hide();
                    From2 sd = new From2();
                    sd.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("Username or Passworrd are Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                passBox2.UseSystemPasswordChar = false;
            }
            else if(checkBox1.Checked == false)
            {
                passBox2.UseSystemPasswordChar = true;
            }
        }

        
    }

}
