using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShopMobileApp.Models
{
    public class Product
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string Discount { get; set; }

        public string DisplayName { get; set; }

        public int ID { get; set; }

        public string Image1 { get; set; }

        public string Image2 { get; set; }

        public string Image3 { get; set; }
        
        public string Image4 { get; set; }

        public bool IsFreeShip { get; set; }

        public bool IsInstallment { get; set; }

        public bool IsNew { get; set; }

        public int KeyWordID { get; set; }

        public string KeyWordName { get; set; }

        public List<ProductDetail> ProductDetails { get; set; }

        public List<ProductOption> ProductOptions { get; set; }

        public List<Promotion> Promotions { get; set; }

        public float Rating { get; set; }

        public int ViewNumber { get; set; }
    }
}
