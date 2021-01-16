using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ElectronicShopMobileApp.Assets.Contains;
using SQLite;

namespace ElectronicShopMobileApp.Models.SQLiteModels
{
    public class CartItemSQLite : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int ID { get; set; }

        public string DisplayName { get; set; }

        public string Image { get; set; }

        public int ProductOptionID { get; set; }

        public string OptionContent { get; set; }

        public decimal DelPrice { get; set; }

        public decimal Price { get; set; }

        private int count;

        public int Count
        {
            get => count; set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        public int CustomerID { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(); 
            }
        }

        public CartItemSQLite()
        {

        }

        public CartItemSQLite(Product product, ProductOption productOption, int count)
        {
            ID = product.ID;
            DisplayName = product.DisplayName;
            Image = product.Image1;
            ProductOptionID = productOption.ID;
            OptionContent = productOption.OptionContent;
            DelPrice = productOption.DelPrice;
            Price = productOption.Price;
            Count = count;
            CustomerID = Const.CurrentCustomerID;
            IsSelected = false;
        }

        public CartItemSQLite(CartItemSQLite cartItemSQLite)
        {
            ID = cartItemSQLite.ID;
            DisplayName = cartItemSQLite.DisplayName;
            Image = cartItemSQLite.Image;
            ProductOptionID = cartItemSQLite.ProductOptionID;
            OptionContent = cartItemSQLite.OptionContent;
            DelPrice = cartItemSQLite.DelPrice;
            Price = cartItemSQLite.Price;
            Count = cartItemSQLite.Count;
            IsSelected = cartItemSQLite.IsSelected;
        }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
