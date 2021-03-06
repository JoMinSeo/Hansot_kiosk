﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Hansot_kiosk.Model;
using Hansot_kiosk.Common;
using System.Windows;
using Hansot_kiosk.Interface;

namespace Hansot_kiosk.Service
{
    public class MySQLManager : IInsertManager
    {
        private MySqlConnection connection;
        public MySQLManager()
        {
            App.InitDeleGate += init;
        }

        private void init()
        {
            string connectionPath = "Server = 10.80.163.155; Database=kiosk; " +
            "Uid=root;Pwd=y28645506;Charset=utf8";
            this.connection = new MySqlConnection(connectionPath);

            App.Menus = this.SelectAllMenus();
            App.Orders = this.SelectAllOrders();
            App.Users = this.selectAllUsers();
            App.OrderedMenus = this.SelectAllOrderedMenus();
        }

        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command;
        }

        public bool OpenMySqlConnection()
        {
            try
            {
                connection.Open();
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
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void ExecuteNonQuery(string command)
        {
            if (OpenMySqlConnection() == true)
            {
                MySqlCommand sqlCommand = new MySqlCommand(command, connection);

                if (sqlCommand.ExecuteNonQuery() == 1)
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
        public List<OrderedMenuModel> SelectAllOrderedMenus()
        {
            string query = "SELECT * FROM orderedmenu";

            List<OrderedMenuModel> orderedMenus = new List<OrderedMenuModel>();

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    OrderedMenuModel orderedMenu = new OrderedMenuModel();
                    orderedMenu.IDX = dataReader.GetInt32("IDX");
                    orderedMenu.OrderIdx = dataReader.GetInt32("order_IDX");
                    orderedMenu.MenuIDX = dataReader.GetInt32("menu_IDX");
                    orderedMenu.Amount = dataReader.GetInt32("amount");

                    orderedMenus.Add(orderedMenu);
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return orderedMenus;
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

            if (this.OpenMySqlConnection() == true)
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
                    order.User_IDX = dataReader.GetInt32("User_IDX");
                    order.Seat_IDX = dataReader.GetInt32("Seat_IDX");
                    order.IsCard = Convert.ToBoolean(dataReader.GetInt32("isCard"));
                    order.OrderedTime = Convert.ToDateTime(dataReader["OrderedTime"].ToString());
                    order.TotalPrice = dataReader.GetInt32("TotalPrice");

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

        public void InsertOrder(OrderModel orderModel)
        {
            string command = string.Format("INSERT INTO kiosk.order (IDX, User_IDX, Seat_IDX, isCard, OrderedTime, TotalPrice) " +
                "VALUES ( {0}, {1}, {2}, {3}, {4}, {5} )",
                orderModel.IDX, orderModel.User_IDX, orderModel.Seat_IDX,
                orderModel.IsCard == true ? 1 : 0, "'" + orderModel.OrderedTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'", orderModel.TotalPrice);
            ExecuteNonQuery(command);
        }

        public void InsertOrderedMenu(MenuModel menuModel, int orderIDX)
        {
            string command = string.Format("INSERT INTO kiosk.orderedmenu (Order_IDX, menu_IDX, amount) " +
                            "VALUES ( {0}, {1}, {2} )", orderIDX, menuModel.IDX, menuModel.Amount);
            ExecuteNonQuery(command);
        }

        public void UpdateDiscountedPer(int idx, int discountedPer)
        {
            string command = string.Format("UPDATE kiosk.menu SET DiscountedPer = " +
                            "{0} WHERE IDX = {1}", discountedPer, idx);
            ExecuteNonQuery(command);
        }
    }
}

