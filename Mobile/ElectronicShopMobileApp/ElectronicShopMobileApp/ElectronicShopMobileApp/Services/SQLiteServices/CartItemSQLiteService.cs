using ElectronicShopMobileApp.Assets.Contains;
using ElectronicShopMobileApp.Models.SQLiteModels;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicShopMobileApp.Services.SQLiteServices
{
    public class CartItemSQLiteService
    {
        private static CartItemSQLiteService instance;

        public static CartItemSQLiteService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartItemSQLiteService();
                }
                return instance;
            }
            private set => instance = value;
        }

        public List<CartItemSQLite> GetAll()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<CartItemSQLite>().Where(cartItemSQLite => cartItemSQLite.CustomerID == Const.CurrentCustomerID).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CartItemSQLite Find(int ID)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    return connection.Table<CartItemSQLite>().FirstOrDefault(CartItemSQLite => CartItemSQLite.ID == ID && CartItemSQLite.CustomerID == Const.CurrentCustomerID);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool InsertOrUpdate(CartItemSQLite cartItemSQLite)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var item = Find(cartItemSQLite.ID);
                    if (item == null)
                    {
                        connection.Insert(cartItemSQLite);
                    }
                    else
                    {
                        item.Count += cartItemSQLite.Count;
                        item.DelPrice = cartItemSQLite.DelPrice;
                        item.Price = cartItemSQLite.Price;
                        item.OptionContent = cartItemSQLite.OptionContent;
                        item.IsSelected = cartItemSQLite.IsSelected;

                        connection.Update(item);
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateState(bool isSelectAll)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    var itemList = GetAll();
                    foreach (var item in itemList)
                    {
                        item.IsSelected = isSelectAll;
                    }

                    connection.UpdateAll(itemList);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public bool Delete(CartItemSQLite CartItemSQLite)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.Delete(CartItemSQLite);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAll()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                {
                    connection.DeleteAll<CartItemSQLite>();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAllBySelected(List<CartItemSQLite> list)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Const.FolderPath, Const.SQLiteDBContextPath)))
                { 
                    foreach (var item in list)
                    {
                        var b = connection.Delete(item);
                    } 
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> CreateBill()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var dataString = await client.GetStringAsync(Const.ConverToPathWithParameter(Const.CreateBillPath, new object[] { Const.CurrentCustomerID }));

                    var billID = JsonConvert.DeserializeObject<int>(dataString);

                    return billID > 0 ? billID : -1;

                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        public async Task SendEmail(int billID, List<CartItemSQLite> billInfos)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var customer = CustomerSQLiteService.Instance.Find(Const.CurrentCustomerID);
                    var body = "";

                    body += "<hr/>";
                    body += "Xin chào <b>" + customer.DisplayName + "</b>,<br/><br/>";
                    body += "<b style='color: rgb(2, 172, 234);'>Electronic Shop</b> gửi đến quý khách hóa đơn điện tử cho đơn hàng #" + billID + ". Quý khách vui lòng kiểm tra hóa đơn điện tử.<br/><br/>";

                    body += "<h1 style='color: rgb(2, 172, 234); text-align: center; font-size: 40px;'>HÓA ĐƠN ELECTRONIC SHOP</h1>";
                    body += "<b style='color: rgb(2, 172, 234);'>THÔNG TIN ĐƠN HÀNG #" + billID + "</b> (" + DateTime.Now + ")<br/>";
                    body += "<hr/>";
                    body += $@"
                        <table style='width: 100%; border:1px solid rgb(2, 172, 234); border-collapse: separate; border-spacing: 0;'>
                            <tr style='background-color: rgb(2, 172, 234); color: white;'>
                                <th style='text-align: left;'>THÔNG TIN THANH TOÁN</th>
                                <th style='text-align: left;'>ĐỊA CHỈ GIAO HÀNG</th>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        {customer.DisplayName} <br/>
                                        {customer.Email} <br/>
                                        {customer.PhoneNumber} <br/>
                                    </div>                                            
                                </td>
                                <td>
                                    <div>
                                        {customer.DisplayName} <br/>
                                        {customer.Address} <br/>
                                        {customer.PhoneNumber} <br/>
                                    </div>  
                                </td>
                            </tr>
                        </table><br/><br/><br/>";

                    body += "<b style='color: rgb(2, 172, 234);'>CHI TIẾT ĐƠN HÀNG";
                    body += "<hr/>";
                    body += @"
                        <table style='width: 100%; border:1px solid rgb(2, 172, 234); border-collapse: separate; border-spacing: 0;'>
                            <tr style='background-color: rgb(2, 172, 234); color: white;'>
                                <th style='text-align: left;'>SẢN PHẨM</th> 
                                <th style='text-align: left;'>ĐƠN GIÁ</th> 
                                <th style='text-align: left;'>SỐ LƯỢNG</th>  
                                <th style='text-align: left;'>TỔNG TẠM</th> 
                            </tr>";

                    foreach (var item in billInfos)
                    {
                        body += $@"
                            <tr style='margin-top:10px; border-bottom: 1px solid black;'>
                                <td style='text-align: left; color: black;'>{item.DisplayName}</td>
                                <td style='text-align: left; color: black;'>{Convert.ToDecimal(item.Price).ToString("#,##")} đ</td>
                                <td style='text-align: left; color: black;'>{item.Count}</td> 
                                <td style='text-align: left; color: black;'>{Convert.ToDecimal(item.Price * item.Count).ToString("#,##")} đ</td>
                            </tr>";
                    }

                    body += $@"
                    <tr style='border-top: 1px solid rgb(2, 172, 234);'>
                        <td colspan='3'><h4>Tổng giá trị đơn hàng</h4></td>                    
                        <td>{Convert.ToDecimal(billInfos.Sum(billinfo => billinfo.Price * billinfo.Count)).ToString("#,##")}đ</td>                    
                    </tr>";
                    body += "</table> <br/><br/>";

                    body += "<span style='color: black;'>Thanks and Regards,<span><br/><br/>";
                    body += "<span style='color: black;'>-----------------------------</span><br/>";
                    body += "<span style='color: black;'><b>Huynh Ngoc Son - Leader Team</b></span><br/>";
                    body += "<span style='color: black;'><b>Mobile:</b> (84) 35 9521 032</span><br/>";
                    body += "<span style='color: black;'><b>Facebook: </b></span><a href=" + "https://www.facebook.com/HuynhNgocSon123" + ">https://www.facebook.com/HuynhNgocSon123<a/>";
                    body += "<hr/>";

                    Const.SendMail(customer.Email, "Export Bill - Electronic Shop", body);

                }
                catch (Exception)
                {
                    return;
                }
            }
        }
    }
}
