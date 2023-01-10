using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DB.Interfaces
{
    internal interface IUser
    {
        bool IsMasterSet();
        void GetUserData();
    }
}
