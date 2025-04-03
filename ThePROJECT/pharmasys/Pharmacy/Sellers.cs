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
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
            ShowSeller();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void ShowSeller()
        {
            try
            {
                Con.Open();
                string Query = "Select * from SellerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                SellersDGV.DataSource = ds.Tables[0];
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

        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddTb.Text = "";
            SPasswordTb.Text = "";
            SGenCb.SelectedIndex = 0;
            Key = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SGenCb.SelectedIndex == -1 || SAddTb.Text == "")
            { 
                MessageBox.Show("Missing Information");

            } 
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SellerTbl(SName, SDOB, SPhone, SAdd, AGen, SPass) values(@SN, @SD, @SP,  @SA, @SG, @SPA)", Con);

                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Seller Added");
                    Con.Close();

                    ShowSeller();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void SellersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SNameTb.Text = SellersDGV.SelectedRows[0].Cells[1].Value.ToString();
            SDOB.Text = SellersDGV.SelectedRows[0].Cells[2].Value.ToString();
            SPhoneTb.Text = SellersDGV.SelectedRows[0].Cells[3].Value.ToString();
            SAddTb.Text = SellersDGV.SelectedRows[0].Cells[4].Value.ToString();
            SGenCb.SelectedItem = SellersDGV.SelectedRows[0].Cells[5].Value.ToString();
            SPasswordTb.Text = SellersDGV.SelectedRows[0].Cells[6].Value.ToString();



            if (SNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SellersDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Seller");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from SellerTbl where SNum=@SKey", Con);

                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Seller Deleted");
                    Con.Close();

                    ShowSeller();

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
            if (SNameTb.Text == "" || SPhoneTb.Text == "" || SPasswordTb.Text == "" || SGenCb.SelectedIndex == -1 || SAddTb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update SellerTbl set SName = @SN, SDOB = @SD, SPhone = @SP, SAdd = @SA, AGen = @SG, SPass = @SPA where SNum=@SKey", Con);

                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SD", SDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SPA", SPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Seller Updated");
                    Con.Close();

                    ShowSeller();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
