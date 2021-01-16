using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ElectronicShopMobileApp.Models
{
    public class Comment : INotifyPropertyChanged
    {
        public string AvatarCustomer { get; set; }
        
        public string CustomerName { get; set; }
        
        public string Description { get; set; }

        private int disLikeNumber;

        public int DisLikeNumber
        {
            get => disLikeNumber;
            set
            {
                disLikeNumber = value;
                OnPropertyChanged();
            }
        }

        public int ID { get; set; }

        private int likeNumber;

        public int LikeNumber
        {
            get => likeNumber;
            set
            {
                likeNumber = value;
                OnPropertyChanged();
            }
        }

        public string Reason { get; set; }
        
        public int StarNumber { get; set; }

        private bool? isLike;

        public bool? IsLike
        {
            get => isLike;
            set
            {
                isLike = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
