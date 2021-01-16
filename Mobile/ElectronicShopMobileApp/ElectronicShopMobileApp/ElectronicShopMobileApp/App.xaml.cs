using ElectronicShopMobileApp.Models.SQLiteModels;
using ElectronicShopMobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElectronicShopMobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SQLiteDBContext db = new SQLiteDBContext();

            db.CreateDatabase();
             
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new RegisterAccountPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
