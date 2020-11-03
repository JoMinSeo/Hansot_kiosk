using Hansot_kiosk.Manager;
using MySql.Data.MySqlClient;
using System.Windows;
using UIManager;

namespace Hansot_kiosk
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        public static UIStateManager uIStateManager = new UIStateManager();
        public static OrderManager orderManager = new OrderManager();
        public static MySqlConnection connection;
        public static bool DataSaveResult = false;
        public static bool DataSearchResult = false;
    }
}
