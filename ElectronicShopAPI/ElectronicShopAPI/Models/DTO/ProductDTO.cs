using ElectronicShopAPI.Assets.Contains;
using ElectronicShopAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ElectronicShopAPI.Models.DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryName { get; set; }

        public int? KeyWordID { get; set; }

        public string KeyWordName { get; set; }

        public double? Rating { get; set; }

        public int? Discount { get; set; }

        public bool? IsFreeShip { get; set; }

        public int? ViewNumber { get; set; }
         
        public string Image1 { get; set; }
         
        public string Image2 { get; set; }
         
        public string Image3 { get; set; }
         
        public string Image4 { get; set; }

        public bool? IsNew { get; set; }

        public bool? IsInstallment { get; set; }

        public string DisplayName { get; set; }
         
        public string Description { get; set; }   
         
        public List<ProductDetailDTO> ProductDetails { get; set; }
         
        public List<ProductOptionDTO> ProductOptions { get; set; }
         
        public List<PromotionDTO> Promotions { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product product)
        {
            ID = product.ID;
            CategoryID = product.CategoryID;
            CategoryName = product.Category.DisplayName;
            KeyWordID = product.KeyWordID;
            KeyWordName = product.KeyWord.Content;
            Rating = product.Rating;
            Discount = product.Discount;
            IsFreeShip = product.IsFreeShip;
            ViewNumber = product.ViewNumber;
            Image1 = ConvertToImagePath(CategoryID) + product.Image1;
            Image2 = ConvertToImagePath(CategoryID) + product.Image2;
            Image3 = ConvertToImagePath(CategoryID) + product.Image3;
            Image4 = ConvertToImagePath(CategoryID) + product.Image4;
            IsNew = product.IsNew;
            IsInstallment = product.IsInstallment;
            DisplayName = product.DisplayName;
            Description = product.Description;
            ProductDetails = product.ProductDetails
                                        .Select(productDetail=> new ProductDetailDTO(productDetail))
                                        .ToList();
            ProductOptions = product.ProductOptions
                                        .Select(productOption => new ProductOptionDTO(productOption))
                                        .ToList();
            Promotions = product.Promotions
                                        .Select(promotion => new PromotionDTO(promotion))
                                        .ToList();
        }

        string ConvertToImagePath(int? categoryID)
        {
            switch (categoryID)
            {
                case 1:
                    return Const.CellphoneImagePath;
                case 2:
                    return Const.TabletImagePath;
                case 3:
                    return Const.LaptopImagePath;
                case 4:
                    return Const.SmartWatchImagePath;
                default:
                    return Const.AccessoriesImagePath;
            }
        }
    }
}