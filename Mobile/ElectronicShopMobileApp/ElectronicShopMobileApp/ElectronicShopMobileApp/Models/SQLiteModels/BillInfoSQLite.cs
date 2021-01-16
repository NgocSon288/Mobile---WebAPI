using System;
using SQLite;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class BillInfoSQLite
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Image { get; set; }

        public int ProductOptionID { get; set; }

        public string OptionContent { get; set; }

        public decimal DelPrice { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public int CustomerID { get; set; } 

        public BillInfoSQLite()
        {

        }

        public BillInfoSQLite(CartItemSQLite cartItemSQLite)
        {
            ID = cartItemSQLite.ID;
            DisplayName = cartItemSQLite.DisplayName;
            Image = cartItemSQLite.Image;
            ProductOptionID = cartItemSQLite.ProductOptionID;
            OptionContent = cartItemSQLite.OptionContent;
            DelPrice = cartItemSQLite.DelPrice;
            Price = cartItemSQLite.Price;
            Count = cartItemSQLite.Count; 
        }
    }
}

