using Microsoft.VisualBasic;
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

namespace Pharmacy
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCust();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void ShowCust()
        {
            try
            {
                Con.Open();
                string Query = "Select * from CustomerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                CustomersDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
      
        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Reset()
        {
            CustNameTb.Text = "";
            CustPhoneTb.Text = "";
            CustGenCb.SelectedIndex = 0;
            CustAddTb.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustPhoneTb.Text == "" || CustGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName, CustPhone, CustAdd, CustDOB, CustGen) values(@CN, @CP, @CA, @CD, @CG)", Con);

                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery(); 

                    MessageBox.Show("Customer Added");
                    Con.Close();

                    ShowCust();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustAddTb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustDOB.Text = CustomersDGV.SelectedRows[0].Cells[4].Value.ToString();
            CustGenCb.SelectedItem = CustomersDGV.SelectedRows[0].Cells[5].Value.ToString();

            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Customer");
            }
            else
            {
                try 
                { 
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustNum=@CKey", Con);

                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Customer Deleted");
                    Con.Close();

                    ShowCust();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustPhoneTb.Text == "" || CustGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update into CustomerTbl set CustName = @CN, CustPhone = @CP, CustAdd = @CA, CustDOB = @CD, CustGen = @CG where CustNum=@CKey", Con);
                    
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CD", CustDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Customer Updated");
                    Con.Close();

                    ShowCust();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            Medicines medForm = new Medicines();
            medForm.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Customers custForm = new Customers();
            custForm.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Sellers sellForm = new Sellers();
            sellForm.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Sellings sellingsForm = new Sellings();
            sellingsForm.Show();
            this.Hide();
        }
    }
}
