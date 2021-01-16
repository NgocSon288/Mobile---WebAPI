using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class ProductDetailDTO
    { 
         
        public string ElementLabel { get; set; }
         
        public string ElementDescription { get; set; }

        public ProductDetailDTO()
        {

        }

        public ProductDetailDTO(ProductDetail productDetail)
        { 
            ElementLabel = productDetail.ElementLabel;
            ElementDescription = productDetail.ElementDescription;
        }
    }
}