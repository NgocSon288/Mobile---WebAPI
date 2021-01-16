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
using Xamarin.Forms;

namespace ElectronicShopMobileApp.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public int ID { get; set; }

        private Product product;

        public Product Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> imagesList;

        public ObservableCollection<string> ImagesList
        {
            get => imagesList;
            set
            {
                imagesList = value;
                OnPropertyChanged();
            }
        }

        private List<Comment> comments;

        public List<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }

        private ProductOption productOptionSelected;

        public ProductOption ProductOptionSelected
        {
            get => productOptionSelected; set
            {
                productOptionSelected = value;
                OnPropertyChanged();
            }
        }

        private bool isFavoriteProduct;

        public bool IsFavoriteProduct
        {
            get => isFavoriteProduct;
            set
            {
                isFavoriteProduct = value;
                OnPropertyChanged();
            }
        }

        private int count = 1;

        public int Count
        {
            get => count; set
            {
                if (value <= 0 || value > 1000)
                {
                    return;
                }
                count = value;
                
                OnPropertyChanged();
                OnIncreaseCountChangedClickedCommand.ChangeCanExecute();
                OnDecreaseCountChangedClickedCommand.ChangeCanExecute();
            }
        }

        private string imageOption;

        public string ImageOption
        {
            get => imageOption;
            set
            {
                imageOption = value;
                OnPropertyChanged();
            }
        }

        public Command OnComeBackCommand { get; set; }

        public Command OnProductOptionSelectedClickedCommand { get; set; }

        public Command OnIncreaseCountChangedClickedCommand { get; set; }

        public Command OnDecreaseCountChangedClickedCommand { get; set; }

        public Command OnAddToCartClickCommand { get; set; }

        public Command OnBuyNowClickCommand { get; set; }

        public Command OnGoToCartClickCommand { get; set; }

        public Command OnFavoriteProductToggleClickCommand { get; set; }

        public Command OnEvaluateCommentLikeToggleClickCommand { get; set; }

        public Command OnEvaluateCommentDisLikeToggleClickCommand { get; set; }

        public ProductDetailViewModel(int ID)
        {
            this.ID = ID;

            LoadData();

            OnProductOptionSelectedClickedCommand = new Command<ProductOption>(OnProductOptionSelectedClickedCommandExecute, productOption => productOption != null);

            OnIncreaseCountChangedClickedCommand = new Command(() => Count++, () => Count < 1000);

            OnDecreaseCountChangedClickedCommand = new Command(() => Count--, () => Count > 1);

            OnAddToCartClickCommand = new Command(OnAddToCartClickCommandExecute);

            OnBuyNowClickCommand = new Command(OnBuyNowClickCommandExecute);

            OnFavoriteProductToggleClickCommand = new Command(OnFavoriteProductToggleClickCommandExecute);
             
            OnComeBackCommand = new Command(OnComeBackCommandExecute, () => true);

            OnGoToCartClickCommand = new Command(OnGoToCartClickCommandExecute, () => true);

            OnEvaluateCommentLikeToggleClickCommand = new Command<Comment>(OnEvaluateCommentLikeToggleClickCommandExecute, (comment => comment != null));

            OnEvaluateCommentDisLikeToggleClickCommand = new Command<Comment>(OnEvaluateCommentDisLikeToggleClickCommandExecute, (comment => comment != null));
        }

        private async void LoadData()
        { 
            Product = await ProductService.Instance.GetAllProductByIDAsync(ID);
            Comments = await CommentService.Instance.GetAllCommentByProductID(ID);
            ProductOptionSelected = Product.ProductOptions.FirstOrDefault();
            IsFavoriteProduct = FavoriteProductSQLiteService.Instance.GetStateExists(ID);
            ImageOption = Product.Image1;
            EvaluateCommentSQLiteService.Instance.LoadCommentColorState(Comments, ID);

            ImagesList = new ObservableCollection<string>() { Product.Image1, Product.Image2, Product.Image3, Product.Image4 };
        }

        private async void OnGoToCartClickCommandExecute()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CartPage(), true);
        }

        private async void OnComeBackCommandExecute()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void OnProductOptionSelectedClickedCommandExecute(ProductOption productOption)
        {
            var indexImage = product.ProductOptions.IndexOf(product.ProductOptions.SingleOrDefault(product => product.ID == productOption.ID));
            switch (indexImage)
            {
                case 1:
                    ImageOption = product.Image1;
                    break;
                case 2:
                    ImageOption = product.Image2;
                    break;
                case 3:
                    ImageOption = product.Image3;
                    break;
                default:
                    ImageOption = product.Image4;
                    break;
            }
            var a = imageOption;
            ProductOptionSelected = productOption;
        }

        private async void OnAddToCartClickCommandExecute()
        {
            CartItemSQLite cartItemSQLite = new CartItemSQLite(Product, ProductOptionSelected, Count);

            CartItemSQLiteService.Instance.InsertOrUpdate(cartItemSQLite);
            await App.Current.MainPage.DisplayAlert("Thông báo!", "Đã thêm vào giỏ thành công!", "Ok");
        }

        private async void OnBuyNowClickCommandExecute()
        {
            CartItemSQLite cartItemSQLite = new CartItemSQLite(Product, ProductOptionSelected, Count);

            CartItemSQLiteService.Instance.InsertOrUpdate(cartItemSQLite); 
            await App.Current.MainPage.Navigation.PushAsync(new CartPage(), true);
        }

        private void OnFavoriteProductToggleClickCommandExecute()
        {
            if (FavoriteProductSQLiteService.Instance.SetStateExists(ID))
            {
                IsFavoriteProduct = !IsFavoriteProduct;
            }
        }

        private async void OnEvaluateCommentLikeToggleClickCommandExecute(Comment comment)
        { 
            var result = EvaluateCommentSQLiteService.Instance.InsertOrDelete(comment.ID, true, ID);

            switch(result)
            {
                case 1:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, true, true);
                    comment.LikeNumber++;
                    comment.IsLike = true;
                    break;
                case 3:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, true, true);
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, false, false);
                    comment.LikeNumber++;
                    comment.IsLike = true;
                    comment.DisLikeNumber--;
                    break;
                case 5:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, false, true);
                    comment.LikeNumber--;
                    comment.IsLike = null;
                    break;
            } 
        }

        private async void OnEvaluateCommentDisLikeToggleClickCommandExecute(Comment comment)
        { 
            var result = EvaluateCommentSQLiteService.Instance.InsertOrDelete(comment.ID, false, ID);
            switch (result)
            {
                case 2:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, true, false);
                    comment.DisLikeNumber++;
                    comment.IsLike = false;
                    break;
                case 4:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, false, false);  
                    comment.DisLikeNumber--;
                    comment.IsLike = null;
                    break;
                case 6:
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, true, false);
                    await CommentService.Instance.IncreaseOrDecreaseLikeOrDisLikePath(comment.ID, false, true);
                    comment.DisLikeNumber++;
                    comment.IsLike = false;
                    comment.LikeNumber--;
                    break;
            }
        }
    }
}
