using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models.SQLiteModels;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Services.SQLiteServices;
using ElectronicShopMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{  
    public class LoginViewModel : BaseViewModel 
    {
        private string userName;

        public string UserName
        {
            get => userName; set
            {
                userName = value;
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        private string passWord;

        public string PassWord
        {
            get => passWord; set
            {
                passWord = value; 
                OnPropertyChanged();
                LoginCommand.ChangeCanExecute();
            }
        }

        private bool isRememberPassword;

        public bool IsRememberPassword
        {
            get => isRememberPassword; set
            {
                isRememberPassword = value;
                OnPropertyChanged();
            }
        } 

        public Command LoginCommand { get; }

        public Command OnForgetPasswordClickCommand { get; set; }

        public Command OnRememberPasswordClickCommand { get; set; }

        public Command OnRegisterAccountCommand { get; set; }

        public LoginViewModel()
        { 
            LoginCommand = new Command(ExecuteLoginCommand, OnLoginCommandCanExecute);

            OnForgetPasswordClickCommand = new Command(OnForgetPasswordClickCommandExecute, ()=>true);

            OnRememberPasswordClickCommand = new Command(OnRememberPasswordClickCommandExecute, () => true);

            OnRegisterAccountCommand = new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new RegisterAccountPage(),true));
             
            Load();
        }

        private bool OnLoginCommandCanExecute()
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrEmpty(PassWord) && !string.IsNullOrWhiteSpace(PassWord))
                return true;
            return false;            
        }

        private void Load()
        {
            var customer = CustomerTempSQLiteService.Instance.Get();

            if(customer != null)
            { 
                UserName = customer.UserName;
                PassWord = customer.PassWord;
                IsRememberPassword = true;
            }
            else
            {
                IsRememberPassword = false;
            } 
        }

        private async void ExecuteLoginCommand()
        {
            IsBusy = true;
            try
            {
                var customer = await LoginService.Instance.LoginAsync(UserName, PassWord);
                if (customer != null)
                {
                    if (CustomerSQLiteService.Instance.Find(customer.ID) == null)
                    {
                        CustomerSQLiteService.Instance.Insert(new CustomerSQLite(customer));
                    }
                    else
                    {
                        CustomerSQLiteService.Instance.Update(new CustomerSQLite(customer));
                    }

                    Const.CurrentCustomerID = customer.ID;
                    if (IsRememberPassword)
                    {
                        CustomerTempSQLiteService.Instance.Insert(customer);
                    }
                    else
                    {
                        CustomerTempSQLiteService.Instance.Delete();
                    }

                    await App.Current.MainPage.Navigation.PushAsync(new MainViewPage(), true);
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Thông báo!", "Tài khoản không hợp lệ", "OK");
                }
            }
            catch (Exception ex)
            {
    
            }

            IsBusy = false;
        }

        private async void OnForgetPasswordClickCommandExecute()
        {
            IsBusy = true;
            try
            {
                var customer = await LoginService.Instance.ForgetPasswordAsync(UserName);

                IsBusy = false;
                if (customer == null)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo!", "Vui lòng kiểm tra lại mật khẩu", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Đổi mật khẩu thành công!", "Vui lòng kiểm tra lại Email", "OK");
                }
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
        }

        private void OnRememberPasswordClickCommandExecute()
        {
            IsRememberPassword = !IsRememberPassword;
        }
    }
}
