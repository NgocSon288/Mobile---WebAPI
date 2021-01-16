using System;
using System.Collections.Generic;
using ElectronicShopMobileApp.Models;
using Xamarin.Forms;

namespace ElectronicShopMobileApp.Controls
{
    public class CustomFlexLayout:FlexLayout
    {
        public List<ProductOption> ProductOptions { get; set; }

        public CustomFlexLayout()
        {

        }
    }
}
