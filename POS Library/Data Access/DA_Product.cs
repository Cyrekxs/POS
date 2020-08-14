using POS_Library.Helper;
using POS_Library.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace POS_Library.Data_Access
{
    class DA_Product
    {
        public static bool SetProduct(Product product)
        {
            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("EXECUTE sp_set_product @ProductID,@Code,@Details,@WholeSalePrice,@RetailPrice", conn))
                {
                    comm.Parameters.AddWithValue("@ProductID", product.ProductID);
                    comm.Parameters.AddWithValue("@Code", product.Code);
                    comm.Parameters.AddWithValue("@Details", product.Details);
                    comm.Parameters.AddWithValue("@WholeSalePrice", product.WholeSalePrice);
                    comm.Parameters.AddWithValue("@RetailPrice", product.RetailPrice);
                    return Convert.ToBoolean(comm.ExecuteNonQuery());
                }
            }
        }

        public static async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                await conn.OpenAsync();
                using (SqlCommand comm = new SqlCommand("SELECT * FROM fn_get_products() ORDER BY Code ASC", conn))
                {
                    using (SqlDataReader reader = await comm.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Product p = new Product()
                            {
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                Code = Convert.ToString(reader["Code"]),
                                Details = Convert.ToString(reader["Details"]),
                                WholeSalePrice = Convert.ToDouble(reader["WholeSalePrice"]),
                                RetailPrice = Convert.ToDouble(reader["RetailPrice"]),
                                DateCreated = Convert.ToDateTime(reader["DateCreated"]),
                                DateUpdated = Convert.ToDateTime(reader["DateUpdated"])
                            };
                            products.Add(p);
                        }
                    }
                }
            }

            return products;
        }

        public static bool DeleteProduct(int ProductID)
        {
            using (SqlConnection conn = new SqlConnection(Connection.SQLConnection))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand("EXECUTE sp_delete_product @ProductID", conn))
                {
                    comm.Parameters.AddWithValue("@ProductID", ProductID);
                    return Convert.ToBoolean(comm.ExecuteNonQuery());
                }
            }
        }
    }
}
