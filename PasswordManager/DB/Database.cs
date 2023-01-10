using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace PasswordManager.DB
{
    internal class Database
    {
        private SQLiteConnection conn1 = new SQLiteConnection("Data Source=manager.sqlite;Version=3;");
        private SQLiteConnection master = new SQLiteConnection("Data Source=master.sqlite;Version=3;");
        public void __init__()
        {
            try
            {
                CreateDatabaseAndTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected internal void CreateDatabaseAndTables()
        {
            string cmd = "CREATE TABLE IF NOT EXISTS users (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, username VARCHAR NOT NULL, " +
                "password TEXT NOT NULL, hasSetMP INTEGER NOT NULL, created_at DATETIME DEFAULT CURRENT_TIMESTAMP)";
            string cmd2 = "CREATE TABLE IF NOT EXISTS manager (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, user_id INTEGER NOT NULL, " +
                "password TEXT NOT NULL, created_at DATETIME DEFAULT CURRENT_TIMESTAMP)";
            if (File.Exists("manager.sqlite"))
            {
                
                CreateTables(cmd, conn1);
                CreateTables(cmd2, conn1);
            }
            else
            {
                SQLiteConnection.CreateFile("manager.sqlite");
                CreateTables(cmd, conn1);
                CreateTables(cmd2, conn1);
                MessageBox.Show("Successfully generated the files on the first launch.");
            }
        }

        protected internal void CreateTables(string query, SQLiteConnection conn)
        {
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        protected internal bool CreateMasterDatabase()
        {
            string cmd = "CREATE TABLE IF NOT EXISTS master_table (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, password TEXT NOT NULL, " +
               "created_at DATETIME DEFAULT CURRENT_TIMESTAMP)";
        
            if (File.Exists("master.sqlite"))
            {
                CreateTables(cmd, master);
                return true;
            }
            else
            {
                SQLiteConnection.CreateFile("master.sqlite");
                if(File.Exists("master.sqlite"))
                {
                    CreateTables(cmd, master);
                    return true;
                }
                return false;
            }
            return false;
        }

        public SQLiteConnection GetMaster()
        {
            return master;
        }

        public SQLiteConnection StandardConn()
        {
            return conn1;
        }

        /*
         * 
         * Database Transaction
         * 
         */

        /*** SELECT ***/
        public void SelectAllWhere(Dictionary<string, string> data, string table, string boolean)
        {
            try
            {
                string tablesAndData = "";
                int count = data.Count;
                foreach(KeyValuePair<string, string> kvp in data)
                {
                    tablesAndData += String.Format("{0} = {1}", kvp.Key, kvp.Value);
                    if (count-- != 1) tablesAndData += boolean + " ";
                }

                //MessageBox.Show("Count: "+count+"\nData: "+tablesAndData);

                SQLiteConnection conn = this.StandardConn();
                SQLiteCommand cmd = new SQLiteCommand(String.Format("SELECT * FROM {0} WHERE {1}", table, tablesAndData), conn);

                SQLiteDataReader rd = cmd.ExecuteReader();

                while (rd.Read()) { }
            }
            catch(Exception e )
            {
                MessageBox.Show(e.Message); 
            }
        }

      

        
    }
}
