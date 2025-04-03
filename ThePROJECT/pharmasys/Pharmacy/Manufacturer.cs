using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pharmacy
{
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");
        
        private void ShowMan()
        {
            try
            {
                Con.Open();
                string Query = "Select * from ManufacturerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ManufacturerDGV.DataSource = ds.Tables[0];
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ManufacturerTbl(ManName, ManAdd, ManPhone, ManJDate) values(@MN, @MA, @MP, @MJD)", Con);

                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", (object)ManJDate.Value.Date ?? DBNull.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Manufacturer Added");
                    Con.Close();

                    ShowMan();

                    Reset();
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        int Key = 0;
        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ManNameTb.Text = ManufacturerDGV.SelectedRows[0].Cells[1].Value.ToString();
            ManAddTb.Text = ManufacturerDGV.SelectedRows[0].Cells[2].Value.ToString();
            ManPhoneTb.Text = ManufacturerDGV.SelectedRows[0].Cells[3].Value.ToString();
            ManJDate.Text = ManufacturerDGV.SelectedRows[0].Cells[4].Value.ToString();

            if(ManNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ManufacturerDGV.SelectedRows[0].Cells[0].Value.ToString());

            }


        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Manufacturer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ManufacturerTbl where ManId = @MKey", Con);

                    cmd.Parameters.AddWithValue("@MKey", Key);
                    
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Manufacturer Deleted");
                    Con.Close();

                    ShowMan();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Reset ()
        {

            ManNameTb.Text = "";
            ManAddTb.Text = "";
            ManPhoneTb.Text = "";
            Key = 0;

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ManufacturerTbl Set ManName = @MN, ManAdd = @MA, ManPhone = @MP, ManJDate = @MJD where ManId = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", (object)ManJDate.Value.Date ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Manufacturer Updated");
                    Con.Close();

                    ShowMan();

                    Reset();    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            Medicines medForm = new Medicines();
            medForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
