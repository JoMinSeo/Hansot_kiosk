using Hansot_kiosk.Manager;
using Hansot_kiosk.Model;
using Hansot_kiosk.Service;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using UIManager;

namespace Hansot_kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static List<MenuModel> Menus;
        public static List<OrderModel> Orders;
        public static List<UserModel> Users;

        public static readonly UIStateManager uIStateManager = new UIStateManager();
        public static readonly MySQLManager sQLManager = new MySQLManager();
        public static readonly UserManager userManager = new UserManager();
        public static readonly OrderManager orderManager = new OrderManager();
        public static readonly TCPManager tcpManager = new TCPManager();

        public static MySqlConnection connection;
        public static bool DataSaveResult = false;
        public static bool isLogined = false;
    }
}
