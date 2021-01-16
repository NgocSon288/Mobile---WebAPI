using ElectronicShopAPI.Models.DTO;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ElectronicShopAPI.Models.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        ElectronicDBContext db = new ElectronicDBContext();

        public async Task<List<ProductDTO>> GetAllProduct()
        {
            return (await db.Products
                                .ToListAsync())
                                .Select(product => new ProductDTO(product))
                                .ToList();
              
        }

        public async Task<List<ProductDTO>> GetAllProductByCategoryID(int categoryID)
        {
            return (await db.Products
                                .Where(product => product.CategoryID == categoryID)
                                .ToListAsync())
                                .Select(product => new ProductDTO(product))
                                .ToList();
        }

        public async Task<List<ProductDTO>> GetAllProductMostSearched()
        {
            return (await db.Products
                                .OrderByDescending(product => product.ViewNumber)
                                .Take(10)
                                .ToListAsync())
                                .Select(product => new ProductDTO(product))
                                .ToList();
        }

        public async Task<List<ProductDTO>> GetAllProductMostDiscount()
        {
            return (await db.Products
                                .OrderByDescending(product => product.Discount)
                                .Take(10)
                                .ToListAsync())
                                .Select(product => new ProductDTO(product))
                                .ToList();
        }

        public async Task<ProductDTO> GetAllProductByID(int ID)
        {
            var MyProduct = await db.Products.SingleOrDefaultAsync(product => product.ID == ID);

            MyProduct.ViewNumber++;
            await db.SaveChangesAsync();

            return MyProduct == null ? null : new ProductDTO(MyProduct);
        }

        public async Task<int?> IncreaseProductViewByID(int ID)
        {
            var myProduct = await db.Products.SingleOrDefaultAsync(product => product.ID == ID);

            myProduct.ViewNumber++;

            await db.SaveChangesAsync();

            return myProduct.ViewNumber;
            
        }
    }
}