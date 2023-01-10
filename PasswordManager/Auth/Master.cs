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

// For Registry
using Microsoft.Win32;
using PasswordManager.DB;

namespace PasswordManager.Auth
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();

            GenerateMasterPassword();
            Database db = new Database();
            db.__init__();
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

        private void btnMasterSubmit_Click(object sender, EventArgs e)
        {
            var _txtMaster = txtMaster.Text;
            var _txtConfirm = txtConfirm.Text;
            var _txtMasterLen = txtMaster.TextLength;
            var _txtConfirmLen = txtConfirm.TextLength;
            int flag = 0;

            if(_txtMasterLen < 30 || _txtConfirmLen < 30)
                MessageBox.Show("Master Password length must greater than 39 characters", "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            
            else if(_txtConfirm != _txtMaster)
                MessageBox.Show("Master Password do not match", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            else
            {
                saveToRegistry(_txtMaster);
            }
        }


        private void saveToRegistry(string mp)
        {
            string keyName = @"SOFTWARE\BrightChase\PasswordManager";
            string valueName = "mp";
            string valueData = mp;

            RegistryKey key = Registry.CurrentUser.CreateSubKey(keyName);
            key.SetValue(valueName, valueData);
            key.Close();
        }
    }
}
