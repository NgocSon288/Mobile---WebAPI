using System;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Views;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class ComfirmEmailViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get => userName;
            set
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
            }
        }

        private string fullName;

        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string address;

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private byte[] avatarRegister;

        public byte[] AvatarRegister
        {
            get => avatarRegister;
            set
            {
                avatarRegister = value;
                OnPropertyChanged();
            }
        }

        private string OTPEmail { get; set; }

        private string oTP;

        public string OTP
        {
            get => oTP;
            set
            {
                oTP = value;
                OnPropertyChanged();
            }
        }

        private string txt1;

        public string Txt1
        {
            get => txt1;
            set
            {
                txt1 = value;
                if (value.Length >= 2)
                {
                    txt1 = value.Substring(1, 1);
                }
                else
                {
                    txt1 = value;
                }

                LoadOTP();
                OnPropertyChanged();
                OnComfirmEmailCommand.ChangeCanExecute();
            }
        }

        private string txt2;

        public string Txt2
        {
            get => txt2;
            set
            {
                if (value.Length >= 2)
                {
                    txt2 = value.Substring(1, 1);
                }
                else
                {
                    txt2 = value;
                }

                LoadOTP();
                OnPropertyChanged();
                OnComfirmEmailCommand.ChangeCanExecute();
            }
        }

        private string txt3;

        public string Txt3
        {
            get => txt3;
            set
            {
                if (value.Length >= 2)
                {
                    txt3 = value.Substring(1, 1);
                }
                else
                {
                    txt3 = value;
                }

                LoadOTP();
                OnPropertyChanged();
                OnComfirmEmailCommand.ChangeCanExecute();
            }
        }

        private string txt4;

        public string Txt4
        {
            get => txt4;
            set
            {
                 if (value.Length >= 2)
                {
                    txt4= value.Substring(1, 1);
                }
                else
                {
                    txt4 = value;
                }

                LoadOTP();
                OnPropertyChanged();
                OnComfirmEmailCommand.ChangeCanExecute();
            }
        }

        public Command OnComfirmEmailCommand { get; set; }

        public Command OnReLoadOTPCommand { get; set; }

        public Command OnComeBackCommand { get; set; }
         
        public ComfirmEmailViewModel(string userName, string passWord, string fullName, string phoneNumber, string email, string address, byte[] avatarRegister)
        {  
            UserName = userName; 
            PassWord = passWord; 
            FullName = fullName; 
            PhoneNumber = phoneNumber; 
            Email = email; 
            Address = address; 
            AvatarRegister = avatarRegister; 

            Load();

            OnComfirmEmailCommand = new Command(OnComfirmEmailCommandExecute, () => !IsBusy);

            OnReLoadOTPCommand = new Command(OnReLoadOTPCommandExecute, () => !IsBusy);

            OnComeBackCommand = new Command(OnComeBackCommandExecute, () => true);
        }

        private async void Load()
        {
            OTPEmail = await LoginService.Instance.ConfirmOTPEmail(fullName, email);
        }
         

        private void LoadOTP()
        {
            OTP = Txt1 + Txt2 + Txt3 + Txt4;
        }

        private async void OnComfirmEmailCommandExecute()
        {
            IsBusy = true;
            OnReLoadOTPCommand.ChangeCanExecute();
            
            if(OTP == OTPEmail)
            {
                IsBusy = false;
                OnReLoadOTPCommand.ChangeCanExecute();

                await LoginService.Instance.Register(UserName, PassWord, FullName, PhoneNumber, Email, Address, AvatarRegister);

                await App.Current.MainPage.Navigation.PushAsync(new MessagePage("Thành công", "Bạn đã đăng ký thành công tài khoản"), true);
            }
            else
            {
                IsBusy = false;
                OnReLoadOTPCommand.ChangeCanExecute();
                await App.Current.MainPage.DisplayAlert("NOT OK", "NOT OK", "NOT OK");
            }

            IsBusy = false;
            OnReLoadOTPCommand.ChangeCanExecute();
        }

        private async void OnReLoadOTPCommandExecute()
        {
            IsBusy = true;
            OnComfirmEmailCommand.ChangeCanExecute();
            OTPEmail = await LoginService.Instance.ConfirmOTPEmail(fullName, email);
            IsBusy = false;
            OnComfirmEmailCommand.ChangeCanExecute();

            await App.Current.MainPage.DisplayAlert("Thông báo!", "Vui lòng kiểm tra lại Email, chúng tôi đã cấp lại mã OTP cho bạn", "OK");
        } 

        private async void OnComeBackCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
