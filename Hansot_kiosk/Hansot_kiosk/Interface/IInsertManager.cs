using Hansot_kiosk.Model;

namespace Hansot_kiosk.Interface
{
    interface IInsertManager
    {
        void InsertOrder(OrderModel orderModel);
        void InsertOrderedMenu(MenuModel menuModel, int orderIDX);
    }
}
