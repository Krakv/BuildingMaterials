namespace BuildingMaterials
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            password = new TextBox();
            loginLabel = new Label();
            passwordLabel = new Label();
            mainLabel = new Label();
            enterBtn = new Button();
            login = new ComboBox();
            SuspendLayout();
            // 
            // password
            // 
            password.Font = new Font("Segoe UI", 14F);
            password.Location = new Point(321, 284);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(373, 39);
            password.TabIndex = 1;
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Segoe UI", 14F);
            loginLabel.Location = new Point(91, 223);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(224, 32);
            loginLabel.TabIndex = 2;
            loginLabel.Text = "Имя пользователя:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 14F);
            passwordLabel.Location = new Point(214, 287);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(101, 32);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Пароль:";
            // 
            // mainLabel
            // 
            mainLabel.AutoSize = true;
            mainLabel.Font = new Font("Segoe UI", 32F);
            mainLabel.Location = new Point(335, 62);
            mainLabel.Name = "mainLabel";
            mainLabel.Size = new Size(148, 72);
            mainLabel.TabIndex = 4;
            mainLabel.Text = "Вход";
            // 
            // enterBtn
            // 
            enterBtn.Font = new Font("Segoe UI", 16F);
            enterBtn.Location = new Point(599, 373);
            enterBtn.Name = "enterBtn";
            enterBtn.Size = new Size(189, 65);
            enterBtn.TabIndex = 5;
            enterBtn.Text = "Войти";
            enterBtn.UseVisualStyleBackColor = true;
            enterBtn.Click += enterBtn_Click;
            // 
            // login
            // 
            login.Font = new Font("Segoe UI", 14F);
            login.FormattingEnabled = true;
            login.Items.AddRange(new object[] { "customer", "manager" });
            login.Location = new Point(321, 229);
            login.Name = "login";
            login.Size = new Size(373, 39);
            login.TabIndex = 6;
            login.SelectedIndexChanged += login_SelectedIndexChanged;
            login.SelectionChangeCommitted += login_SelectionChangeCommitted;
            login.TextUpdate += login_TextUpdate;
            login.DropDownClosed += login_DropDownClosed;
            login.MouseLeave += login_MouseLeave;
            login.Validating += login_Validating;
            login.Validated += login_Validated;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(login);
            Controls.Add(enterBtn);
            Controls.Add(mainLabel);
            Controls.Add(passwordLabel);
            Controls.Add(loginLabel);
            Controls.Add(password);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Вход";
            Deactivate += Form1_Deactivate;
            VisibleChanged += Form1_VisibleChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox login1;
        private TextBox password;
        private Label loginLabel;
        private Label passwordLabel;
        private Label mainLabel;
        private Button enterBtn;
        private ComboBox login;
    }
}
