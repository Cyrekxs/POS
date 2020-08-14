using POS_Library.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class frm_product_entry : Form
    {
        Product _product = new Product();
        public frm_product_entry()
        {
            InitializeComponent();
        }

        public frm_product_entry(Product p)
        {
            InitializeComponent();
            _product = p;
            txtProductCode.Text = p.Code;
            txtProductDetails.Text = p.Details;
            txtWholeSale.Text = p.WholeSalePrice.ToString("n");
            txtRetail.Text = p.RetailPrice.ToString("n");
        }

    
        private void button1_Click(object sender, EventArgs e)
        {
            Product product = new Product()
            {
                ProductID = _product.ProductID,
                Code = txtProductCode.Text,
                Details = txtProductDetails.Text,
                WholeSalePrice = Convert.ToDouble(txtWholeSale.Text),
                RetailPrice = Convert.ToDouble(txtRetail.Text)
            };

            var result = Product.InsertUpdateProduct(product);
            if (result == true)
                MessageBox.Show("Product saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Product saving failed!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
