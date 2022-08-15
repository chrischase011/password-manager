using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PasswordManager.Auth
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();

            GenerateMasterPassword();
        }

      private void GenerateMasterPassword(int _n = 30)
      {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890@!#.%^";
            char[] _chars = chars.ToCharArray();

            string result = "";
            Random rand = new Random();
            for (int i = 0; i < _n; i++)
            {
                result+= _chars[rand.Next(_chars.Length)];
            }

            txtMaster.Text = result;
            txtConfirm.Text = result;

      }

        private void checkShow_CheckedChanged(object sender, EventArgs e)
        {
            if(checkShow.Checked)
            {
                txtMaster.UseSystemPasswordChar = false;
                txtConfirm.UseSystemPasswordChar = false;
            }
            else
            {
                txtMaster.UseSystemPasswordChar = true;
                txtConfirm.UseSystemPasswordChar = true;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateMasterPassword();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string textMaster = txtMaster.Text;

            Clipboard.SetText(textMaster);
        }

        private void Master_Load(object sender, EventArgs e)
        {
            if(!File.Exists("manager.sqlite"))
            {
                DB.Database db = new DB.Database();

                if (!db.CreateMasterDatabase())
                {
                    MessageBox.Show("Unexpected error occurred on creating database");
                }
            }
            
        }
    }
}
