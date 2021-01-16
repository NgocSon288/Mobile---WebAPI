using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class DiscountCodeDTO
    {
        public string Code { get; set; }

        public int? Discount { get; set; }

        public DiscountCodeDTO()
        {

        }

        public DiscountCodeDTO(DiscountCode discountCode)
        {
            Code = discountCode.Code;
            Discount = discountCode.Discount;
        }
    }
}