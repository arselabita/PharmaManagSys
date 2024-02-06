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
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
            ShowMed();
            GetManufacturer();
        }

     
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void ShowMed()
        {
            try
            {
                Con.Open();
                string Query = "Select * from MedicineTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                MedicinesDGV.DataSource = ds.Tables[0];
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


        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Reset()
        {
            MedManTb.Text = "";
            MedNameTb.Text = "";
            MedPriceTb.Text = "";
            MedQtyTb.Text = "";
            MedTypeCb.SelectedIndex = 0;
            Key = 0;
        }

        private void GetManufacturer()
        {
            Con.Open();

            SqlCommand cmd = new SqlCommand("Select ManId from ManufacturerTbl", Con);

            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader(); 
            
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);

            MedManCb.ValueMember = "ManId";
            MedManCb.DataSource = dt;

            Con.Close();
        }

        private void GetManName()
        {
            Con.Open();

            string Query = "Select * from ManufacturerTbl where ManId = '" + MedManCb.SelectedValue.ToString() + "'";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                MedManTb.Text = dr["ManName"].ToString();
            }

            Con.Close();
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedTypeCb.SelectedIndex== -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MedicineTbl (MedName, MedType, MedQty, MedPrice, MedManId, MedManufact) values(@MN, @MT, @MQ, @MP, @MMI, @MM)", Con);

                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MM", MedManTb.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Medicine Added");
                    Con.Close();

                    ShowMed();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void MedManCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManName();
        }

        int Key = 0;
        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            MedTypeCb.SelectedItem = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            MedQtyTb.Text = MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString();
            MedPriceTb.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
            MedManCb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            MedManTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();



            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select the Medicine");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from MedicineTbl where MedNum=@MKey", Con);

                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Medicine Deleted");
                    Con.Close();

                    ShowMed();

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
            if (MedNameTb.Text == "" || MedPriceTb.Text == "" || MedQtyTb.Text == "" || MedTypeCb.SelectedIndex == -1 || MedManTb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update MedicineTbl set MedName = @MN, MedType = @MT, MedQty = @MQ, MedPrice = @MP, MedManId = @MMI, MedManufact = @MM where MedNum =@MKey", Con);
                    
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MMI", MedManCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MM", MedManTb.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Medicine Updated");
                    Con.Close();
                    
                    ShowMed();

                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {
            Medicines medForm = new Medicines();
            medForm.Show();
            //this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Customers custForm = new Customers();
            custForm.Show();
            //this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Sellers sellForm = new Sellers();
            sellForm.Show();
            //this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Sellings sellingsForm = new Sellings();
            sellingsForm.Show();
            // this.Hide();
        }
    }





}
