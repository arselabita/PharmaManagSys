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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pharmacy
{
    public partial class Sellings : Form
    {
        public Sellings()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");


        private void UpdateQty()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(MedQtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("update MedicineTbl set MedQty = @MQ where MedNum = @MKey", Con);
                
                cmd.Parameters.AddWithValue("@MQ", NewQty);
                cmd.Parameters.AddWithValue("@MKey", Key);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Medicine Updated");
                Con.Close();

                ShowMed();

               // Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl(UName, CustNum, CustName, BDate, BAmount) values(@UN, @CN, @CNa, @BD, @BA)", Con);

                cmd.Parameters.AddWithValue("@UN", SnameLbl.Text);
                cmd.Parameters.AddWithValue("@CN", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CNa", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@BA", GrdTotal);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Bill Saved");
                Con.Close();

                ShowBill();

               // Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void ShowBill()
        {
            Con.Open();
            string Query = "Select * from BillTbl where SName='" + SnameLbl.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void ShowMed()
        {
            Con.Open();
            string Query = "Select * from MedicineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicinesDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        int n =0,GrdTotal = 0;
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(MedQtyTb.Text == "" || Convert.ToInt32(MedQtyTb.Text) >Stock)
            {
                MessageBox.Show("Enter Correct Quantity");
            }
            else
            {
                int total = Convert.ToInt32(MedQtyTb.Text) * Convert.ToInt32(MedPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();

                newRow.CreateCells(BillDVG);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = MedNameTb.Text;
                newRow.Cells[2].Value = MedQtyTb.Text;
                newRow.Cells[3].Value = MedPriceTb.Text;
                newRow.Cells[4].Value = total;

                BillDVG.Rows.Add(newRow);

                GrdTotal = GrdTotal + total;

                TotalLbl.Text = "Lek " + GrdTotal;

                n++;
                UpdateQty();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

       

        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string MedName;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("CSpace Pharmacy", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID MEDICINE PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));

            foreach (DataGridViewRow row in BillDVG.Rows)
            {
                MedId = Convert.ToInt32(row.Cells["Column1"].Value);
                MedName = "" + row.Cells["Column2"].Value;
                MedPrice = Convert.ToInt32(row.Cells["Column3"].Value);
                MedQty = Convert.ToInt32(row.Cells["Column4"].Value);
                MedTot = Convert.ToInt32(row.Cells["Column5"].Value);

                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + MedPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));

                pos = pos + 20;

                e.Graphics.DrawString("Grand Total: Lek " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
                e.Graphics.DrawString("*************MCS Pharma*************", new Font("Century Gothic", 10, FontStyle.Bold),Brushes.Crimson, new Point(10, pos + 85));

                BillDVG.Rows.Clear();
                BillDVG.Refresh();

                pos = 100;
                GrdTotal = 0;
                n = 0;

            }
        }

        int Key = 0, Stock;
        int MedId, MedPrice, MedQty, MedTot;

        private void label10_Click_1(object sender, EventArgs e)
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

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void MedTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sellings sellingsForm = new Sellings();
            sellingsForm.Show();
            this.Hide();
        }

        int pos = 60;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString());
            MedPriceTb.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();

            //MedTypeCb.SelectedItem = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();

            // MedManCb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            // MedManTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();


            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }
    }
}
