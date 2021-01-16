using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Drawing;
using ElectronicShopMobileApp.Assets.Contains;
using System.Threading;
using ElectronicShopMobileApp.Services;
using ElectronicShopMobileApp.Views;
using System.Text;

namespace ElectronicShopMobileApp.ViewModels
{
    public class RegisterAccountViewModel : BaseViewModel
    {
        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
            }
        }

        private string confirmPassWord;

        public string ConfirmPassWord
        {
            get => confirmPassWord;
            set
            {
                confirmPassWord = value;
                OnPropertyChanged();
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
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
                OnCanCreateAccountCommand.ChangeCanExecute();
            }
        }  

        public Command OnPickImageCommand { get; set; }

        public Command OnCanCreateAccountCommand { get; set; }

        public Command OnComeBackCommand { get; set; }

        public RegisterAccountViewModel()
        { 
            OnPickImageCommand = new Command(OnPickImageCommandExecute, () => true);

            OnCanCreateAccountCommand = new Command(OnCanCreateAccountCommandExecite, OnCanCreateAccountCommandCanExecute); 

            OnComeBackCommand = new Command(OnComeBackCommandExecute, () => true);

            Source = null;
            AvatarRegister = null;
        }

        private bool OnCanCreateAccountCommandCanExecute()
        {
            if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrWhiteSpace(PassWord) && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrWhiteSpace(ConfirmPassWord) && !string.IsNullOrEmpty(UserName)
                && PassWord == ConfirmPassWord
                && !string.IsNullOrWhiteSpace(FullName) && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrEmpty(UserName)
                && Source != null
                && AvatarRegister !=null)
                return true;
            return false;
        } 

        private async void OnComeBackCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnPickImageCommandExecute()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "Vui lòng chọn một tấm ảnh"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                AvatarRegister = Const.GetImageBytes(stream);

                Source = ImageSource.FromStream(() => Const.BytesToStream(AvatarRegister));
            }
        }       

        private async void OnCanCreateAccountCommandExecite()
        {
            IsBusy = true;
            try
            {
                var result = await LoginService.Instance.CanRegister(UserName, Email); 

                switch (result)
                { 
                    case -2:
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Email không hợp lệ!", "OK");
                        break;
                    case -1:
                        IsBusy = false;
                        await App.Current.MainPage.DisplayAlert("Thông báo", "Tài khoản đã có người sử dụng!", "OK");
                        break;
                    case 1:
                        IsBusy = false; 
                        await App.Current.MainPage.Navigation.PushAsync(new ComfirmEmailPage(UserName, PassWord, FullName, PhoneNumber, Email, Address, AvatarRegister), true);
                        break;
                }
                 
            }
            catch (Exception ex)
            { 
                return;
            }
            IsBusy = false;
        } 
    }
}
