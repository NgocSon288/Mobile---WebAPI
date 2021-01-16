using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services
{
    public class ProductService
    {
        private static ProductService instance;

        public static ProductService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Product>> GetAllProductByTextSearchAsync(string textSearch)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllProductPath));

                    var productList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    return productList.Where(product=>product.KeyWordName.Contains(textSearch)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<List<Product>> GetAllProductByCategoryIDAsync(int categoryID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllProductByCategoryIDPath, new object[] { categoryID }));

                    var productList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    return productList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<Product> GetAllProductByIDAsync(int ID)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllProductByIDPath, new object[] { ID }));

                    var product = JsonConvert.DeserializeObject<Product>(dataString);

                    return product;
                }
                catch (Exception)
                {
                    return null;
                }
            } 
        }
    }
}
