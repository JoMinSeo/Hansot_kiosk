using Hansot_kiosk.Model;
using Hansot_kiosk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hansot_kiosk.Manager
{
    public class UserManager
    {
        List<UserModel> userList = new List<UserModel>();
        MySQLManager mySQLManager = new MySQLManager();

        public UserManager()
        {
            userList = mySQLManager.selectUser();
        }
        
        public UserModel compareName(string tbValue)
        {
            UserModel user = null;

            foreach(UserModel u in userList)
            {
                if (u.Name.Equals(tbValue))
                {
                    user = u;
                }else if (u.Value.Equals(tbValue))
                {
                    user = u;
                }
            }
            return user;
        }
    }
}
