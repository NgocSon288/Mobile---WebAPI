using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class ProductOptionDTO
    {
        public int ID { get; set; }

        public decimal? DelPrice { get; set; }

        public decimal? Price { get; set; }
         
        public string OptionContent { get; set; }

        public ProductOptionDTO()
        {
                
        }

        public ProductOptionDTO(ProductOption productOption)
        {
            ID = productOption.ID;
            DelPrice = productOption.DelPrice;
            Price = productOption.Price;
            OptionContent = productOption.OptionContent;
        }
    }
}