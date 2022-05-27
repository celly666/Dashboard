using _Dashboard.Models;
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

namespace _Dashboard
{
    public partial class Form1 : Form
    {
        //Fields
        private Dashboard model;
        private Button currentButton;
        //Constructor
        public Form1()
        {
            InitializeComponent();
            //Default - Last 7 days
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();
            SetDateMenuButtonUI(btnLast7Days);
            model = new Dashboard();
            LoadData();
        }
        //Private methods
        private void LoadData()
        {
            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lbNumOrders.Text = model.NumOrders.ToString();
                lbTotalRevenue.Text = "$" + model.TotalRevenue.ToString();
                lbTotalProfit.Text = "$" + model.TotalProfit.ToString();
                lbNumCustomers.Text = model.NumCustomers.ToString();
                lbNumSuppliers.Text = model.NumSuppliers.ToString();
                lbNumProducts.Text = model.NumProducts.ToString();
                chartGrossRevenue.DataSource = model.GrossRevenueList;
                chartGrossRevenue.Series[0].XValueMember = "Date";
                chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
                chartGrossRevenue.DataBind();
                chartTopProducts.DataSource = model.TopProductsList;
                chartTopProducts.Series[0].XValueMember = "Key";
                chartTopProducts.Series[0].YValueMembers = "Value";
                chartTopProducts.DataBind();
                dgvUnderstock.DataSource = model.UnderstockList;
                dgvUnderstock.Columns[0].HeaderText = "Item";
                dgvUnderstock.Columns[1].HeaderText = "Units";
                Console.WriteLine("Loaded view :)");
            }
            else Console.WriteLine("View not loaded, same query");
        }
        private void SetDateMenuButtonUI(object button)
        {
            var btn = (Button)button;
            //Highlight button
            btn.BackColor = btnLast30Days.FlatAppearance.BorderColor;
            btn.ForeColor = Color.White;
            //Unhighlight button
            if( currentButton != null && currentButton != btn)
            {
                currentButton.BackColor = this.BackColor;
                currentButton.ForeColor = Color.FromArgb(124, 141, 181);
            }
            currentButton = btn; // Set current button
        }
        private void DisableCustomDates()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }
        //Event methods

        private void btnThisMonth_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
            SetDateMenuButtonUI(sender);
        }

        private void btnLast30Days_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
            SetDateMenuButtonUI(sender);
        }

        private void btnLast7Days_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
            SetDateMenuButtonUI(sender);
        }

        private void btnToday_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
            SetDateMenuButtonUI(sender);
        }

        private void BtnCustomDate_Click_1(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
            SetDateMenuButtonUI(sender);
        }

        private void btnOkCustomDate_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvUnderstock.Columns[1].Width = 50;
        }
    }
}
