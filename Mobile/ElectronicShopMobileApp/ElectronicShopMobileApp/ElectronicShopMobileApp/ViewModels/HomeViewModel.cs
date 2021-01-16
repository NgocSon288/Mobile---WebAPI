using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Advertisement> advertisements;

        public ObservableCollection<Advertisement> Advertisements
        {
            get => advertisements;
            set
            {
                advertisements = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> categories;

        public ObservableCollection<Category> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> productsMostSearched;

        public ObservableCollection<Product> ProductsMostSearched
        {
            get => productsMostSearched;
            set
            {
                productsMostSearched = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> productsMostDiscount;

        public ObservableCollection<Product> ProductsMostDiscount
        {
            get => productsMostDiscount;
            set
            {
                productsMostDiscount = value;
                OnPropertyChanged();
            }
        }

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

        public Command OnCategoryClickedCommand { get; set; }

        public Command OnProductClickedCommand { get; set; }

        public Command OnCompletedSearchProductCommand { get;set; }

        public HomeViewModel()
        {
            LoadData();

            OnCategoryClickedCommand = new Command<Category>(OnCategoryClickedCommandExecute, categories => categories != null);

            OnProductClickedCommand = new Command<Product>(OnProductClickedCommandExecute, product => product != null);

            OnCompletedSearchProductCommand = new Command(OnCompletedSearchProductCommandExecute, ()=>true);
        }
         
        private async void LoadData()
        {
            Advertisements = new ObservableCollection<Advertisement>(await HomeService.Instance.GetAllAdvertisementsAsync());
            Categories = new ObservableCollection<Category>(await HomeService.Instance.GetAllCategoryAsync());
            ProductsMostSearched = new ObservableCollection<Product>(await HomeService.Instance.GetAllProductMostSearchedAsync());
            ProductsMostDiscount = new ObservableCollection<Product>(await HomeService.Instance.GetAllProductMostDiscountAsync());
        }

        private async void OnCategoryClickedCommandExecute(Category category)
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductsPage(category.ID, null), true); 
        }

        private async void OnProductClickedCommandExecute(Product product)
        { 
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(product.ID), true);
        }

        private async void OnCompletedSearchProductCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ProductsPage(-1, TextSearch), true);
            TextSearch = "";
        }
    }
}
