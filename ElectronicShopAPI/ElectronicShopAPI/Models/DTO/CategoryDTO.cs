using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class CategoryDTO
    {  
        public string DisplayName { get; set; }

        public string Image { get; set; }

        public int ID { get; set; }

        public CategoryDTO()
        {

        }

        public CategoryDTO(Category category)
        { 
            DisplayName = category.DisplayName;
            Image = Const.CategoryImagePath + category.Image;
            ID = category.ID;
        }
    }
}