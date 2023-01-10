using PasswordManager.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager.DB
{
    internal class User : Database, IUser
    {
        private int ID;
        private string username;


        SQLiteConnection conn; 
        public bool IsMasterSet()
        {
            conn = StandardConn();

            try
            {

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            return false; 
        }

        public void GetUserData()
        {

        }
    }
}
