using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using Hansot_kiosk.Model;
using Hansot_kiosk.Common;

namespace Hansot_kiosk.Service
{
    class MySQLManager
    {

        public void Initialize()
        {
            Debug.WriteLine("Database Initialize");

            string connectionPath = "";
            App.connection = new MySqlConnection(connectionPath);
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

        public List<Model.Menu> SelectMenu(string tableName, int columnCnt, string name)
        {
            string query = "SELECT * FROM" + " " + tableName;

            List<Model.Menu> menus = new List<Model.Menu>();


            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Model.Menu menu = new Model.Menu();
                    menu.IDX = dataReader.GetInt32(dataReader.GetOrdinal("IDX"));
                    menu.Name = dataReader["Name"].ToString();
                    menu.Price = dataReader.GetInt32(dataReader.GetOrdinal("Price"));
                    menu.Path = dataReader["Path"].ToString();
                    menu.Category = (Category)int.Parse(dataReader["Category"].ToString());

                }

                // 추가된 코드
                //if (element != null)
                //{
                //    for (int i = 0; i < element[0].Count; i++)
                //    {
                //        if (element[0][i].Contains(name))
                //        {
                //            for (int j = 0; j < element[1].Count; i++)
                //            {
                //                if (element[1][i].Contains(name))
                //                {
                //                    App.DataSearchResult = true;
                //                    break;
                //                }
                //            }
                //        }
                //        break;
                //    }
                //}

                dataReader.Close();
                this.CloseMySqlConnection();

                return menus;
            }
            else
            {
                return null;
            }
        }

        public List<string>[] SelectBarcode(string tableName, int columnCnt, int barcode)
        {
            string query = "SELECT * FROM" + " " + tableName;

            List<string>[] element = new List<string>[columnCnt];

            for (int index = 0; index < element.Length; index++)
            {
                element[index] = new List<string>();
            }

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    element[0].Add(dataReader["id"].ToString());
                    element[1].Add(dataReader["pw"].ToString());
                }

                // 추가된 코드
                if (element != null)
                {
                    for (int i = 0; i < element[0].Count; i++)
                    {
                        if (element[0][i].Contains(id))
                        {
                            for (int j = 0; j < element[1].Count; i++)
                            {
                                if (element[1][i].Contains(pw))
                                {
                                    App.DataSearchResult = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return element;
            }
            else
            {
                return null;
            }
        }
    }
}
}
