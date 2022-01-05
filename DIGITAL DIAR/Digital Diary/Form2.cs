using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Digital_Diary
{
    public partial class From2 : Form
    {
        public From2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=MHJ\MSSQLSERVER01;Initial Catalog=ConnectionDB;Integrated Security=True");
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
               
                
               
                string qry = "insert into EventDescriptions(Event,Date,Importance,Photo) values('"+txtEventBox+"',@Date,@Importance,@Photo)";
                SqlCommand cmd = new SqlCommand(qry,con);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker.Text);
                cmd.Parameters.AddWithValue("@Importance", importanceComboBox.Text);
                cmd.Parameters.AddWithValue("@Photo", SavePhoto());
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Save Successfully");
                GetValue();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void GetValue()
        {
            DataTable dt = new DataTable();
            con.Open();
            string qry = "Select * From EventDescriptions";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            con.Close();
            dataGridView1.DataSource = dt;


        }


       /* private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=MHJ\MSSQLSERVER01;Initial Catalog=ConnectionDb;Integrated Security=True");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * From EventDescriptions ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }*/

       

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                
                con.Open();
                string qry = "Delete from EventDescriptions where id='"+textBoxid.Text+"'";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Delete Successfully","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetValue();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void From2_Load(object sender, EventArgs e)
        {
            GetValue();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBoxid.Text = row.Cells[0].Value.ToString();
                txtEventBox.Text= row.Cells[1].Value.ToString();
                dateTimePicker.Text  = row.Cells[2].Value.ToString();
                importanceComboBox.Text = row.Cells[3].Value.ToString();
                pictureBox1.Text = row.Cells[4].Value.ToString();

            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string qry = "UPDATE EventDescriptions SET Event=@Event,Date=@Date,Importance=@importance,@Photo where id='" + textBoxid + "'";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@Event", txtEventBox.Text);
                cmd.Parameters.AddWithValue("@Date", dateTimePicker.Text);
                cmd.Parameters.AddWithValue("@importance", importanceComboBox.Text);
                cmd.Parameters.AddWithValue("@Photo", pictureBox1.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Update Successfully ", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select Photo";
            open.Filter = "Image File(*.png;*jpeg;*jpg;*bmp)|*.png;*jpeg;*jpg;*bmp";
            open.ShowDialog();
            if(open.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
            }

        }
    }
}












