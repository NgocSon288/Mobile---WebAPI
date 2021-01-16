using ElectronicShopMobileApp.Models;
using ElectronicShopMobileApp.Models.SQLiteModels;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Services.SQLiteServices;
using ElectronicShopMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private ObservableCollection<CartItemSQLite> cartItemSQLites;

        public ObservableCollection<CartItemSQLite> CartItemSQLites
        {
            get => cartItemSQLites;
            set
            {
                cartItemSQLites = value;
                OnPropertyChanged();

            }
        }

        private decimal priceTotal;

        public decimal PriceTotal
        {
            get => priceTotal;
            set
            {
                priceTotal = value;
                OnPropertyChanged();
            }
        }

        private bool isSelectAll;

        public bool IsSelectAll
        {
            get => isSelectAll;

            set
            {
                isSelectAll = value;
                OnPropertyChanged();
                OnOrderCommand.ChangeCanExecute();
            }
        }

        public Command OnImageClickedCommand { get; set; }

        public Command OnDeleteCartItemClickedCommand { get; set; }

        public Command OnIncreaseCountChangedClickedCommand { get; set; }

        public Command OnDecreaseCountChangedClickedCommand { get; set; }

        public Command OnChooseToBuyCheckBoxCommand { get; set; }

        public Command OnChooseToBuyAllCheckBoxCommand { get; set; }

        public Command OnOrderCommand { get; set; }

        public Command OnComeBackCommand { get; set; }

        public Command OnGoHomeCommand { get; set; }

        public CartViewModel()
        {
            LoadData();

            OnImageClickedCommand = new Command<CartItemSQLite>(OnImageClickedCommandExecute, cartItemSQLite => cartItemSQLite != null);

            OnDeleteCartItemClickedCommand = new Command<CartItemSQLite>(OnDeleteCartItemClickedCommandEexcute, cartItemSQLite => cartItemSQLite != null);

            OnIncreaseCountChangedClickedCommand = new Command<CartItemSQLite>(OnIncreaseCountChangedClickedCommandExecute, (cartItemSQLite) => cartItemSQLite != null && cartItemSQLite.Count < 1000);

            OnDecreaseCountChangedClickedCommand = new Command<CartItemSQLite>(OnDecreaseCountChangedClickedCommandExecute, (cartItemSQLite) => cartItemSQLite != null && cartItemSQLite.Count > 1);

            OnChooseToBuyCheckBoxCommand = new Command<CartItemSQLite>(OnChooseToBuyCheckBoxCommandExecute, (cartItemSQLite) => cartItemSQLite != null);

            OnChooseToBuyAllCheckBoxCommand = new Command(OnChooseToBuyAllCheckBoxCommandExecute, () => true);

            OnOrderCommand = new Command(OnOrderCommandExecute, OnOrderCommandCanExecute);

            OnComeBackCommand = new Command(OnComeBackCommandExecute, () => true);

            OnGoHomeCommand = new Command(OnGoHomeCommandExecute, () => true);
        }

        private void LoadData()
        {
            var data = CartItemSQLiteService.Instance.GetAll();
            if (data != null)
                CartItemSQLites = new ObservableCollection<CartItemSQLite>(data);
            else
                CartItemSQLites = new ObservableCollection<CartItemSQLite>();
            if(CartItemSQLites != null)
            {
                LoadAllCheck();
                LoadPriceTotal();
            }
        }

        private void LoadAllCheck()
        {
            var result = false;
            try
            {
                result = CartItemSQLites.All(item => item.IsSelected == true);
                IsSelectAll = result;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void LoadPriceTotal()
        { 
            PriceTotal = CartItemSQLites
                                    .Where(cartItemSQLite => cartItemSQLite.IsSelected)
                                    .Sum(cartItemSQLite => cartItemSQLite.Price * cartItemSQLite.Count); 
        }

        private async void OnComeBackCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnDeleteCartItemClickedCommandEexcute(CartItemSQLite cartItemSQLite)
        {
            bool isDelete = CartItemSQLiteService.Instance.Delete(cartItemSQLite);

            if (isDelete)
            {
                CartItemSQLites.Remove(cartItemSQLite);
                LoadPriceTotal();
            }

            LoadAllCheck();

            await App.Current.MainPage.DisplayAlert("Thông báo!", $"Xóa {(isDelete ? "" : "không")} thành công!", "OK");
        }

        private async void OnImageClickedCommandExecute(CartItemSQLite cartItemSQLite)
        { 
            await App.Current.MainPage.Navigation.PushAsync(new ProductDetailPage(cartItemSQLite.ID), true);
        }

        private void OnIncreaseCountChangedClickedCommandExecute(CartItemSQLite cartItemSQLite)
        {
            cartItemSQLite.Count++;
            LoadPriceTotal();
            CartItemSQLiteService.Instance.InsertOrUpdate(new CartItemSQLite(cartItemSQLite) { Count = 1});
            OnIncreaseCountChangedClickedCommand.ChangeCanExecute();
            OnDecreaseCountChangedClickedCommand.ChangeCanExecute();
        }

        private void OnDecreaseCountChangedClickedCommandExecute(CartItemSQLite cartItemSQLite)
        {
            cartItemSQLite.Count--;
            LoadPriceTotal();
            CartItemSQLiteService.Instance.InsertOrUpdate(new CartItemSQLite(cartItemSQLite) { Count = -1 });
            OnIncreaseCountChangedClickedCommand.ChangeCanExecute();
            OnDecreaseCountChangedClickedCommand.ChangeCanExecute();
        }

        private void OnChooseToBuyCheckBoxCommandExecute(CartItemSQLite cartItemSQLite)
        {
            cartItemSQLite.IsSelected = !cartItemSQLite.IsSelected;
            LoadPriceTotal();
            CartItemSQLiteService.Instance.InsertOrUpdate(new CartItemSQLite(cartItemSQLite) { Count = 0, IsSelected = cartItemSQLite.IsSelected });
            LoadAllCheck();
        }

        private void OnChooseToBuyAllCheckBoxCommandExecute()
        {
            IsSelectAll = !IsSelectAll;
            CartItemSQLiteService.Instance.UpdateState(IsSelectAll);
            foreach (var item in CartItemSQLites)
            {
                item.IsSelected = IsSelectAll;
            }
            OnOrderCommand.ChangeCanExecute();
            LoadPriceTotal();
        }

        private bool OnOrderCommandCanExecute()
        {
            return CartItemSQLites.Any(item => item.IsSelected); 
        }

        private async void OnOrderCommandExecute()
        {
            IsBusy = true;
            try
            {
                var billID = await CartItemSQLiteService.Instance.CreateBill();

                if (billID <= 0)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đặt hàng không thành công", "OK");
                }
                else
                {
                    BillInfoSQLiteService.Instance.CreateBillInfo(CartItemSQLites.Where(item=>item.IsSelected).ToList(), billID);

                    CartItemSQLiteService.Instance.DeleteAllBySelected(CartItemSQLites.Where(item => item.IsSelected).ToList());
   
                    await CartItemSQLiteService.Instance.SendEmail(billID, CartItemSQLites.Where(item => item.IsSelected).ToList());

                    CartItemSQLites = new ObservableCollection<CartItemSQLite>(CartItemSQLites.Where(item => !item.IsSelected));

                    IsSelectAll = false;
                    LoadPriceTotal();

                    IsBusy = false;
                    await App.Current.MainPage.Navigation.PushAsync(new MessagePage("Đặt hàng thành công", "Cảm ơn quý khách đã đặt hàng, chúng tôi sẽ gửi hoá đơn sau ít phút", Assets.Contains.Page.HOME_PAGE), true);
                }
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
            
        }

        private async void OnGoHomeCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new MainViewPage());
        }
    }

}
