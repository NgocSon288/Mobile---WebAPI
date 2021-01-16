using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class PromotionDTO
    {    
        public string Content { get; set; }

        public PromotionDTO()
        {
                
        }

        public PromotionDTO(Promotion promotion)
        {
            Content = promotion.Content;
        }
    }
}