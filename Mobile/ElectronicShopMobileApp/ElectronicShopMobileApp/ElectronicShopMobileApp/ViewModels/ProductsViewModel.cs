using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using ElectronicShopMobileApp.Views;
using System.Linq;

namespace ElectronicShopMobileApp.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private int categoryID;

        private string textSearch;

        public string TextSearch
        {
            get => textSearch;
            set
            {
                textSearch = value;
                OnPropertyChanged();
            }
        }

        private List<Product> ProductsStore;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products; set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        private decimal minPrice;

        public decimal MinPrice
        {
            get => minPrice;
            set
            {
                if (value < MaxPrice)
                {
                    minPrice = value;
                } 
                OnPropertyChanged();
            }
        }

        private decimal maxPrice;

        public decimal MaxPrice
        {
            get => maxPrice;
            set
            {
                if(value > MinPrice)
                {
                    maxPrice = value;
                }

                OnPropertyChanged();
            }
        }

        private bool? isNew;

        public bool? IsNew
        {
            get => isNew;
            set
            {
                isNew = value;
                OnPropertyChanged();
            }
        }

        private bool? isInstallment;

        public bool? IsInstallment
        {
            get => isInstallment;
            set
            {
                isInstallment = value;
                OnPropertyChanged();
            }
        }

        private bool isFiveStar;

        public bool IsFiveStar
        {
            get => isFiveStar;
            set
            {
                LoadStarNumber();
                isFiveStar = value; 
                OnPropertyChanged();
            }
        }

        private bool  isFourStar;

        public bool IsFourStar
        {
            get => isFourStar;
            set
            {
                LoadStarNumber();
                isFourStar = value;
                OnPropertyChanged();
            }
        }

        private bool isThreeStar;

        public bool IsThreeStar
        {
            get => isThreeStar;
            set
            {
                LoadStarNumber();
                isThreeStar = value;
                OnPropertyChanged();
            }
        }

        private bool? isDiscount;

        public bool? IsDiscount
        {
            get => isDiscount;
            set
            { 
                isDiscount = value;
                OnPropertyChanged();
            }
        }

        private bool? isFreeship;

        public bool? IsFreeship
        {
            get => isFreeship;
            set
            {
                isFreeship = value;
                OnPropertyChanged();
            }
        }

        public Command OnComeBackCommand { get; set; }

        public Command OnProductClickedCommand { get; set; }

        public Command OnNewProductFilterCommand { get; set; }

        public Command OnInstallmentFilterCommand { get; set; }

        public Command OnStarNunberFilterCommand { get; set; }

        public Command OnDiscountFilterCommand { get; set; }

        public Command OnFreeshipFilterCommand { get; set; }

        public Command OnResetFilterCommand { get; set; }

        public Command OnFilterCommand { get; set; }

        public Command OnCompletedSearchProductCommand { get; set; }


        public ProductsViewModel(int categoryID, string textSearch=null)
        {
            this.categoryID = categoryID;
            this.textSearch = textSearch;
            LoadData();
        }

        private async void LoadData()
        {
            OnProductClickedCommand = new Command<Product>(OnProductClickedCommandExecute, product => product != null);

            OnNewProductFilterCommand = new Command<string>(OnNewProductFilterCommandExecute, key => key != null);

            OnInstallmentFilterCommand = new Command(OnInstallmentFilterCommandExecute, () => true);

            OnStarNunberFilterCommand = new Command<string>(OnStarNunberFilterCommandExecurte, Key => Key != null);

            OnDiscountFilterCommand = new Command(OnDiscountFilterCommandExecute, () => true);

            OnFreeshipFilterCommand = new Command(OnFreeshipFilterCommandExecute, () => true);

            OnResetFilterCommand = new Command(OnResetFilterCommandExecute, () => true);

            OnFilterCommand = new Command(OnFilterCommandExecute, () => true);

            OnComeBackCommand = new Command(OnComeBackCommandExecute, () => true);

            OnCompletedSearchProductCommand = new Command(OnCompletedSearchProductCommandExecute, () => true);
             
            LoadDataFilter();

            if(textSearch==null)
            {
                ProductsStore = await ProductService.Instance.GetAllProductByCategoryIDAsync(categoryID);
            }
            else
            {
                ProductsStore = await ProductService.Instance.GetAllProductByTextSearchAsync(textSearch);
            }
            Products = new ObservableCollection<Product>(ProductsStore); 
        }

        private void LoadDataFilter()
        {
            MinPrice = 0;
            MaxPrice = 2000000;
            IsNew = null;
            IsInstallment = null;
            IsDiscount = null;
            IsFreeship = null;
            LoadStarNumber();
        }

        private void LoadStarNumber()
        {
            isFiveStar = isFourStar = isThreeStar = false;
            OnPropertyChanged(nameof(IsFiveStar));
            OnPropertyChanged(nameof(IsFourStar));
            OnPropertyChanged(nameof(IsThreeStar));
        }

        private async void OnComeBackCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnProductClickedCommandExecute(Product product)
        { 
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        private void OnNewProductFilterCommandExecute(string key)
        { 
            var value = key == "new";

            if(value != IsNew)
            {
                IsNew = value;
            }
            else
            {
                IsNew = null;
            }
        }

        private void OnInstallmentFilterCommandExecute()
        {
            if(IsInstallment != true)
            {
                IsInstallment = true;
            }
            else
            {
                IsInstallment = false;
            } 
        }

        private void OnStarNunberFilterCommandExecurte(string key)
        {
            switch (key)
            {
                case "5":
                    IsFiveStar = !IsFiveStar;
                    break;
                case "4":
                    IsFourStar = !IsFourStar;
                    break;
                case "3": 
                    IsThreeStar = !IsThreeStar;
                    break;
            }
        }

        private void OnDiscountFilterCommandExecute()
        { 
            if (IsDiscount != true)
            {
                IsDiscount = true;
            }
            else
            {
                IsDiscount = false;
            }
        }

        private void OnFreeshipFilterCommandExecute()
        { 
            if (IsFreeship != true)
            {
                IsFreeship = true;
            }
            else
            {
                IsFreeship = false;
            }
        }

        private void OnResetFilterCommandExecute()
        {
            LoadDataFilter();
        }

        private void OnFilterCommandExecute()
        {
            var query = ProductsStore
                .Where(product => product.ProductOptions[0].Price >= MinPrice && product.ProductOptions[0].Price <= MaxPrice)
                .Where(product => IsNew == null ? true : (IsNew == true ? product.IsNew == true : true))
                .Where(product => IsInstallment == null ? true : (IsInstallment == true ? product.IsInstallment == true : true))
                .Where(product =>
                {
                    if (IsThreeStar)
                        return product.Rating >= 3;
                    if (IsFourStar)
                        return product.Rating >= 4;
                    if (IsFiveStar)
                        return product.Rating >= 5;
                    return true;
                })
                .Where(product =>
                {
                    if (IsDiscount == null)
                    {
                        return true;
                    }
                    if (IsDiscount == true)
                    {
                        return Convert.ToDouble(product.Discount) > 0;
                    }
                    return true;
                })
                .Where(product => IsFreeship == null ? true : (IsFreeship == true ? product.IsFreeShip == true : true));

            Products = new ObservableCollection<Product>(query);
        }

        private async void OnCompletedSearchProductCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductsPage(-1, TextSearch), true);
            TextSearch = "";
        }
    }
}
