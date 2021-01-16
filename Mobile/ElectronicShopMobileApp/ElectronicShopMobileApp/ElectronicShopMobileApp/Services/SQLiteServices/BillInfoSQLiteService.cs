using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Models.SQLiteModels;
using SQLite;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class BillInfoSQLiteService
    {
        private static BillInfoSQLiteService instance;

        public static BillInfoSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public async void CreateBillInfo(List<CartItemSQLite> list, int billID)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                { 
                    foreach (var item in list)
                    {
                        await client.GetStringAsync(Const.ConverToPathWithParameter(Const.CreateBillInfoPath, new object[] { item.Count, item.ID, item.ProductOptionID, billID }));
                    } 
                }
                 
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.InsertAll(list.Select(billInfo=>new BillInfoSQLite(billInfo)));
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
