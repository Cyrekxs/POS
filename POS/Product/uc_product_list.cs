using POS_Library.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace POS
{
    public partial class uc_product_list : UserControl
    {
        List<Product> products = new List<Product>();
        int SelectedRow = 0;
        public uc_product_list()
        {
            InitializeComponent();

            DisplayProductAsync();
        }

        private async void DisplayProductAsync()
        {
            products = await Product.GetProductsAsync();

            dataGridView1.Rows.Clear();
            foreach (var item in products)
            {
                dataGridView1.Rows.Add(item.ProductID, item.Code, item.Details, item.WholeSalePrice.ToString("n"), item.RetailPrice.ToString("n"), 
                    item.DateUpdated.ToString("MM-dd-yyyy"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_product_entry frm = new frm_product_entry();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            DisplayProductAsync();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == clmData.Index)
            {
                SelectedRow = e.RowIndex;

                System.Drawing.Rectangle cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                System.Drawing.Point ptLoc = new System.Drawing.Point(cellRect.Left - 100, cellRect.Bottom + 25);
                cm_actions.Show(this, ptLoc);
            }
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = Convert.ToInt16(dataGridView1.Rows[SelectedRow].Cells[0].Value);
            Product p = Product.GetProduct(products, ProductID);
            frm_product_entry frm = new frm_product_entry(p);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            DisplayProductAsync();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = Convert.ToInt16(dataGridView1.Rows[SelectedRow].Cells[0].Value);

            if (MessageBox.Show("Are you sure you want to delete this product?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Product.DeleteProduct(ProductID);
                DisplayProductAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DisplayProductAsync();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
