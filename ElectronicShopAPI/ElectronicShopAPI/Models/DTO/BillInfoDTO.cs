using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class BillInfoDTO
    { 
        public int? Count { get; set; }

        public decimal? Total { get; set; }

        public int? ProductID { get; set; }

        public string ProductName { get; set; }

        public int? OptionProduct { get; set; } 

        public decimal? Price { get; set; }
         
        public string OptionContent { get; set; }

        public BillInfoDTO()
        {

        }

        public BillInfoDTO(BillInfo billInfo)
        { 
            Count = billInfo.Count;
            Total = billInfo.Total;
            ProductID = billInfo.ProductID;
            ProductName = billInfo.Product.DisplayName;
            OptionProduct = billInfo.OptionProduct;
            Price = billInfo.ProductOption.Price;
            OptionContent = billInfo.ProductOption.OptionContent;
        }
    }
}