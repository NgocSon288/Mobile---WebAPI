using System;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Services.SQLiteServices;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class ModifyProfileViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get => userName;
            private set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        private string passWord;

        public string PassWord
        {
            get => passWord;
            set
            {
                passWord = value;
                OnPropertyChanged();
                OnUpdatePasswordClickCommand.ChangeCanExecute();
            }
        }

        private string newPassWord;

        public string NewPassWord
        {
            get => newPassWord;
            set
            {
                newPassWord = value;
                OnPropertyChanged();
                OnUpdatePasswordClickCommand.ChangeCanExecute();
            }
        }

        private string confirmNewPassWord;

        public string ConfirmNewPassWord
        {
            get => confirmNewPassWord;
            set
            {
                confirmNewPassWord = value;
                OnPropertyChanged();
                OnUpdatePasswordClickCommand.ChangeCanExecute();
            }
        }

        public Command OnUpdatePasswordClickCommand { get; set; }

        public ModifyProfileViewModel()
        {
            var customer = CustomerSQLiteService.Instance.Find(Const.CurrentCustomerID);

            UserName = customer.Username;

            OnUpdatePasswordClickCommand = new Command(OnUpdatePasswordClickCommandExecute, OnUpdatePasswordClickCommandCanExecute);
        }

        private async void OnUpdatePasswordClickCommandExecute()
        {
            IsBusy = true;
            try
            {
                var customer = await LoginService.Instance.ChangePasswordAsync(PassWord, NewPassWord);

                if (customer == null)
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Thông báo!", "Vui lòng kiểm tra lại mật khẩu", "OK");
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Thông báo!", "Cập nhật tài khoản thành công\nVui lòng kiểm tra lại Email. ", "OK");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                return;
            }
            IsBusy = false;
        }

        private bool OnUpdatePasswordClickCommandCanExecute()
        {
            // Update Password and and Condition PassWord.Length >= 8
             if(!string.IsNullOrWhiteSpace(PassWord)
                && !string.IsNullOrWhiteSpace(NewPassWord) && NewPassWord.Length >=8 
                && !string.IsNullOrWhiteSpace(ConfirmNewPassWord) && ConfirmNewPassWord.Length >=8
                && NewPassWord == ConfirmNewPassWord)
            {
                return true;
            }

            return false;
        }

    }
}
