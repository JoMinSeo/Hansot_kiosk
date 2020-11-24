using Hansot_kiosk.Model;
using Hansot_kiosk.Service;
using System.Collections.Generic;

namespace Hansot_kiosk.Manager
{
    public class UserManager
    {
        public UserModel compareName(string tbValue)
        {
            UserModel user = null;

            foreach(UserModel u in App.Users)
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
