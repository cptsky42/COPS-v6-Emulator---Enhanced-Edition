namespace COServer.Tools.PasswordGenerator
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mLblAccount = new System.Windows.Forms.Label();
            this.mLblPassword = new System.Windows.Forms.Label();
            this.mTxtAccount = new System.Windows.Forms.TextBox();
            this.mTxtPassword = new System.Windows.Forms.TextBox();
            this.mLblSecurePassword = new System.Windows.Forms.Label();
            this.mBtnGenerate = new System.Windows.Forms.Button();
            this.mTxtSecurePassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mLblAccount
            // 
            this.mLblAccount.AutoSize = true;
            this.mLblAccount.Location = new System.Drawing.Point(12, 16);
            this.mLblAccount.Name = "mLblAccount";
            this.mLblAccount.Size = new System.Drawing.Size(53, 13);
            this.mLblAccount.TabIndex = 0;
            this.mLblAccount.Text = "Account :";
            // 
            // mLblPassword
            // 
            this.mLblPassword.AutoSize = true;
            this.mLblPassword.Location = new System.Drawing.Point(12, 42);
            this.mLblPassword.Name = "mLblPassword";
            this.mLblPassword.Size = new System.Drawing.Size(59, 13);
            this.mLblPassword.TabIndex = 1;
            this.mLblPassword.Text = "Password :";
            // 
            // mTxtAccount
            // 
            this.mTxtAccount.Location = new System.Drawing.Point(115, 13);
            this.mTxtAccount.MaxLength = 15;
            this.mTxtAccount.Name = "mTxtAccount";
            this.mTxtAccount.Size = new System.Drawing.Size(410, 20);
            this.mTxtAccount.TabIndex = 2;
            // 
            // mTxtPassword
            // 
            this.mTxtPassword.Location = new System.Drawing.Point(115, 39);
            this.mTxtPassword.MaxLength = 15;
            this.mTxtPassword.Name = "mTxtPassword";
            this.mTxtPassword.PasswordChar = '*';
            this.mTxtPassword.Size = new System.Drawing.Size(410, 20);
            this.mTxtPassword.TabIndex = 3;
            // 
            // mLblSecurePassword
            // 
            this.mLblSecurePassword.AutoSize = true;
            this.mLblSecurePassword.Location = new System.Drawing.Point(12, 68);
            this.mLblSecurePassword.Name = "mLblSecurePassword";
            this.mLblSecurePassword.Size = new System.Drawing.Size(96, 13);
            this.mLblSecurePassword.TabIndex = 4;
            this.mLblSecurePassword.Text = "Secure Password :";
            // 
            // mBtnGenerate
            // 
            this.mBtnGenerate.Location = new System.Drawing.Point(242, 93);
            this.mBtnGenerate.Name = "mBtnGenerate";
            this.mBtnGenerate.Size = new System.Drawing.Size(75, 23);
            this.mBtnGenerate.TabIndex = 5;
            this.mBtnGenerate.Text = "Generate";
            this.mBtnGenerate.UseVisualStyleBackColor = true;
            this.mBtnGenerate.Click += new System.EventHandler(this.mBtnGenerate_Click);
            // 
            // mTxtSecurePassword
            // 
            this.mTxtSecurePassword.Location = new System.Drawing.Point(115, 65);
            this.mTxtSecurePassword.Name = "mTxtSecurePassword";
            this.mTxtSecurePassword.ReadOnly = true;
            this.mTxtSecurePassword.Size = new System.Drawing.Size(410, 20);
            this.mTxtSecurePassword.TabIndex = 6;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 128);
            this.Controls.Add(this.mTxtSecurePassword);
            this.Controls.Add(this.mBtnGenerate);
            this.Controls.Add(this.mLblSecurePassword);
            this.Controls.Add(this.mTxtPassword);
            this.Controls.Add(this.mTxtAccount);
            this.Controls.Add(this.mLblPassword);
            this.Controls.Add(this.mLblAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Password Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLblAccount;
        private System.Windows.Forms.Label mLblPassword;
        private System.Windows.Forms.TextBox mTxtAccount;
        private System.Windows.Forms.TextBox mTxtPassword;
        private System.Windows.Forms.Label mLblSecurePassword;
        private System.Windows.Forms.Button mBtnGenerate;
        private System.Windows.Forms.TextBox mTxtSecurePassword;
    }
}

