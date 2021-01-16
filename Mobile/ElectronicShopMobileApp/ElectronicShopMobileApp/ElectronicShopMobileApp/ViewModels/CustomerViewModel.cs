using ElectronicShopMobileApp.Assets.Contains;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private CustomerSQLite customerSQLite;

        public CustomerSQLite CustomerSQLite
        {
            get => customerSQLite;
            set
            {
                customerSQLite = value;
                OnPropertyChanged();
            }
        }

        private ImageSource source;

        public ImageSource Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        private AvatarCustomerSQLite avatarCustomerSQLite;

        public AvatarCustomerSQLite AvatarCustomerSQLite
        {
            get => avatarCustomerSQLite;
            set
            {
                avatarCustomerSQLite = value;
                Source = ImageSource.FromStream(() => Const.BytesToStream(AvatarCustomerSQLite.Avatar));
                OnPropertyChanged();
            }
        }

        public Command OnLogoutClickCommand { get; set; }

        public Command OnChangePasswordClickCommand { get; set; }

        public Command OnChangeAvatarCustomerCommand { get; set; }

        public CustomerViewModel()
        {
            LoadData();

            OnLogoutClickCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new LoginPage(), true), () => true);

            OnChangePasswordClickCommand = new Command(OnChangePasswordClickCommandExecute, () => true);

            OnChangeAvatarCustomerCommand = new Command(OnChangeAvatarCustomerCommandExecute, () => true);
        }

        private void LoadData()
        {
            CustomerSQLite = CustomerSQLiteService.Instance.Find(Const.CurrentCustomerID);

            if(CustomerSQLite.IsRegister)
            {
                AvatarCustomerSQLite = AvatarCustomerSQLiteService.Instance.Get();

                if(AvatarCustomerSQLite == null)
                {
                    AvatarCustomerSQLite = new AvatarCustomerSQLite();
                    Source = null;
                    AvatarCustomerSQLiteService.Instance.InsertOrUpdate(AvatarCustomerSQLite);
                }
            }
            else
            { 
                AvatarCustomerSQLite = new AvatarCustomerSQLite();
                Source = null;
            }
        }

        private async void OnChangePasswordClickCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ModifyProfilePage(), true);
        }

        private async void OnChangeAvatarCustomerCommandExecute()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Vui lòng chọn một tấm ảnh"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                AvatarCustomerSQLite.Avatar = Const.GetImageBytes(stream);

                AvatarCustomerSQLiteService.Instance.InsertOrUpdate(AvatarCustomerSQLite);

                if (!CustomerSQLite.IsRegister)
                {
                    await LoginService.Instance.UpdateAvatar();
                    CustomerSQLite.IsRegister = true;
                }

                Source = ImageSource.FromStream(() => Const.BytesToStream(AvatarCustomerSQLite.Avatar));
            } 
        }
    }
}
