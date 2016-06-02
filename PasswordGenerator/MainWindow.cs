// *
// * ******** COPS v6 Emulator - Open Source ********
// * Copyright (C) 2015 Jean-Philippe Boivin
// *
// * Please read the WARNING, DISCLAIMER and PATENTS
// * sections in the LICENSE file.
// *

using System;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace COServer.Tools.PasswordGenerator
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mBtnGenerate_Click(object sender, EventArgs e)
        {
            Byte[] pwd_data = Program.Encoding.GetBytes(mTxtPassword.Text);
            Byte[] acc_data = Program.Encoding.GetBytes(mTxtAccount.Text);
            Byte[] salt = null;
            Byte[] hash = null;

            using (var sha256 = SHA256.Create())
                salt = sha256.ComputeHash(acc_data, 0, acc_data.Length);

            using (var PBKDF2 = new Rfc2898DeriveBytes(pwd_data, salt, 100000))
                hash = PBKDF2.GetBytes(32); // 256 bits

            String password = "";
            for (Int32 i = 0, len = hash.Length; i < len; ++i)
                password += Convert.ToString(hash[i], 16).PadLeft(2, '0');

            mTxtSecurePassword.Text = password;
        }
    }
}
