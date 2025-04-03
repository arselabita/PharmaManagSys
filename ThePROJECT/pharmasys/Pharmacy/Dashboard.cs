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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMed();
            CountSellers();
            CountCust();
            LoadDashboardData();

          
        }

        private void LoadDashboardData()
        {
            CountMed();
            CountSellers();
            CountCust();
        }

      
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Arsela Bita\Documents\PharmacyDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void CountMed()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from MedicineTbl", Con);
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            MedNum.Text = dt.Rows[0][0].ToString();
            Con.Close();    

        }
        private void CountSellers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from SellerTbl", Con); 
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellerLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }
        private void CountCust()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from CustomerTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();

        }

        /*private void CustomersBtn_Click(object sender, EventArgs e)
        {
            Customers custForm = new Customers();
            custForm.Show();
            //this.Hide();
        }
        private void ManufacturerBtn_Click(object sender, EventArgs e)
        {
            Manufacturer manForm = new Manufacturer();
            manForm.Show();
            //this.Hide();
        }


        private void MedicinesBtn_Click(object sender, EventArgs e)
        {
            Medicines medForm = new Medicines();
            medForm.Show();
            //this.Hide();
        }

        private void SellersBtn_Click(object sender, EventArgs e)
        {
            Sellers sellForm = new Sellers();
            sellForm.Show();
            //this.Hide();
        }

        private void SellingsBtn_Click(object sender, EventArgs e)
        {
            Sellings sellingsForm = new Sellings();
            sellingsForm.Show();
           // this.Hide();
        }*/

        /// <summary>
        /// 
        /// 
        ///
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
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
