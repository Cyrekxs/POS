using POS_Library.Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Library.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string Details { get; set; }
        public double WholeSalePrice { get; set; }
        public double RetailPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public static bool InsertUpdateProduct(Product product)
        {
            return DA_Product.SetProduct(product);
        }

        public static bool DeleteProduct(int ProductID)
        {
            return DA_Product.DeleteProduct(ProductID);
        }

        public static async Task<List<Product>> GetProductsAsync()
        {
            return await DA_Product.GetProductsAsync();
        }

        public static Product GetProduct(List<Product> products, int ProductID)
        {
            return (from r in products
                    where r.ProductID == ProductID
                    select r).FirstOrDefault();
        }
    }
}
