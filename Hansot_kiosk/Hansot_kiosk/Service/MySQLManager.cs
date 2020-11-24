using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Hansot_kiosk.Model;
using Hansot_kiosk.Common;
using System.Windows;
using System.Collections.ObjectModel;

namespace Hansot_kiosk.Service
{
    public class MySQLManager
    {
        public MySQLManager()
        {
            Init();
        }
        public void Init()
        {
            string connectionPath = "Server = localhost; Database=kiosk; " +
            "Uid=root;Pwd=y28645506;Charset=utf8";
            App.connection = new MySqlConnection(connectionPath);

            App.Menus = this.SelectAllMenus();
            App.Orders = new ObservableCollection<OrderModel>(this.SelectAllOrders());
            App.Users = new ObservableCollection<UserModel>(this.selectAllUsers());
        }
        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, App.connection);
            return command;
        }

        public bool OpenMySqlConnection()
        {
            try
            {
                App.connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Debug.WriteLine("Unable to Connect to Server");
                        break;
                    case 1045:
                        Debug.WriteLine("Please check your ID or PassWord");
                        break;
                }
                return false;
            }
        }

        public bool CloseMySqlConnection()
        {
            try
            {
                App.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void MySqlQueryExecuter(string userQuery)
        {
            string query = userQuery;

            if (OpenMySqlConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, App.connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    Debug.WriteLine("값 저장 성공");
                    App.DataSaveResult = true;
                }
                else
                {
                    Debug.WriteLine("값 저장 실패");
                    App.DataSaveResult = false;
                }

                CloseMySqlConnection();
            }
        }

        public List<MenuModel> SelectAllMenus()
        {
            string query = "SELECT * FROM menu";

            List<MenuModel> menus = new List<MenuModel>();


            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    MenuModel menu = new MenuModel();
                    menu.IDX = dataReader.GetInt32("IDX");
                    menu.Name = dataReader["Name"].ToString();
                    menu.Price = dataReader.GetInt32("Price");
                    menu.Path = dataReader["Path"].ToString();
                    menu.Category = (ECategory)dataReader.GetInt32("Category");
                    menu.DiscountedPer = dataReader.GetInt32("DiscountedPer");

                    menus.Add(menu);
                }

                dataReader.Close();
                this.CloseMySqlConnection();
                return menus;
            }
            else
            {
                MessageBox.Show("메뉴를 불러오는것에 실패하였습니다.");
                return null;
            }
        }

        public List<UserModel> selectAllUsers()
        {
            string query = "SELECT * FROM kiosk.user";
            List<UserModel> users = new List<UserModel>();

            if(this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    UserModel user = new UserModel();
                    user.IDX = dataReader.GetInt32("IDX");
                    user.Name = dataReader["Name"].ToString();
                    user.Value = dataReader["Value"].ToString();

                    users.Add(user);
                }


                dataReader.Close();
                this.CloseMySqlConnection();
                return users;
            }
            else
            {
                MessageBox.Show("유저를 불러오는데 실패하였습니다.");
                return null;
            }
        }
        public List<OrderModel> SelectAllOrders()
        {
            string query = "SELECT * FROM kiosk.order";

            List<OrderModel> orders = new List<OrderModel>();


            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    OrderModel order = new OrderModel();
                    order.IDX = dataReader.GetInt32("IDX");
                    order.User_IDX = dataReader.GetInt32(dataReader.GetOrdinal("User_IDX"));
                    order.Seat_IDX = dataReader.GetInt32(dataReader.GetOrdinal("Seat_IDX"));
                    order.IsCard = Convert.ToBoolean(dataReader.GetInt32(dataReader.GetOrdinal("isCard")));
                    order.OrderedTime = Convert.ToDateTime(dataReader["OrderedTime"].ToString());

                    orders.Add(order);
                }

                dataReader.Close();
                this.CloseMySqlConnection();
                return orders;
            }
            else
            {
                MessageBox.Show("메뉴를 불러오는것에 실패하였습니다.");
                return null;
            }
        }


        public DateTime selectLastOrderDate(int tableIdx)
        {
            DateTime lastOrderDate = default(DateTime);

            string sSeatIdx = "'" + tableIdx + "'";

            string query = "SELECT OrderedTime FROM kiosk.order WHERE Seat_IDX = " + sSeatIdx +
                " ORDER BY OrderedTime DESC LIMIT 1";

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    lastOrderDate = Convert.ToDateTime(dataReader["OrderedTime"].ToString());
                }


                dataReader.Close();
                this.CloseMySqlConnection();
                return lastOrderDate;
            }
            else
            {
                MessageBox.Show("테이블의 마지막 시간을 불러오는데 실패하였습니다.");
                return lastOrderDate;
            }
        }

        public string createOrderCommand(OrderModel orderModel)
        {
            string command = string.Format("INSERT INTO order (User_IDX, Seat_IDX, isCard, OrderedTime, TotalPrice) VALUES ( {0}, {1}, {2}, {3}, {4} )", orderModel.User_IDX, orderModel.Seat_IDX, orderModel.IsCard, orderModel.OrderedTime, orderModel.TotalPrice);
            return command;
        }
    }
}

