using System;
using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Views;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        private string title;

        public string tTtle
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        private Assets.Contains.Page OptionPage;

        public Command OnGoBackHomeCommand { get; set; }

        public MessageViewModel(string title, string message, Assets.Contains.Page optionPage= Assets.Contains.Page.LOGIN_PAGE)
        {
            Title = title;
            Message = message;
            OptionPage = optionPage;

            OnGoBackHomeCommand = new Command(OnGoBackHomeCommandExecute, () => true);
        }

        private async void OnGoBackHomeCommandExecute()
        {
            switch (OptionPage)
            {
                case Assets.Contains.Page.LOGIN_PAGE:
                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage(), true);
                    break;
                case Assets.Contains.Page.HOME_PAGE:
                    await App.Current.MainPage.Navigation.PushAsync(new MainViewPage(), true);
                    break;
            }
        }
    }
}
