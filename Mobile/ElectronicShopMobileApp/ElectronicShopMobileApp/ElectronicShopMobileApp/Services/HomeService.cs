using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services
{
    public class HomeService
    {
        private static HomeService instance;

        public static HomeService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomeService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async Task<List<Advertisement>> GetAllAdvertisementsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllAdvertisementPath));

                    var advertisementList = JsonConvert.DeserializeObject<List<Advertisement>>(dataString);

                    return advertisementList;
                }
                catch (Exception)
                { 
                    return null;
                }
            }
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllCategoryPath));

                    var CategoryList = JsonConvert.DeserializeObject<List<Category>>(dataString);

                    return CategoryList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<List<Product>> GetAllProductMostSearchedAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllProductMostSearchedPath));

                    var productList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    return productList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<List<Product>> GetAllProductMostDiscountAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.GetAllProductMostDiscountPath));

                    var productList = JsonConvert.DeserializeObject<List<Product>>(dataString);

                    return productList;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
