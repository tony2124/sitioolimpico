namespace WindowsFormsApplication1
{
    partial class InicioSesion
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
            this.user = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.iniciar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(89, 21);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(152, 21);
            this.user.TabIndex = 0;
            this.user.Click += new System.EventHandler(this.user_Click);
            this.user.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.user_KeyPress);
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(89, 55);
            this.pass.Name = "pass";
            this.pass.PasswordChar = '*';
            this.pass.Size = new System.Drawing.Size(152, 21);
            this.pass.TabIndex = 1;
            this.pass.UseSystemPasswordChar = true;
            this.pass.Click += new System.EventHandler(this.pass_Click);
            this.pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pass_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contraseña:";
            // 
            // iniciar
            // 
            this.iniciar.Location = new System.Drawing.Point(69, 96);
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(128, 27);
            this.iniciar.TabIndex = 4;
            this.iniciar.Text = "Iniciar Sesión";
            this.iniciar.UseVisualStyleBackColor = true;
            this.iniciar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Base de datos";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(105, 144);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(136, 21);
            this.ip.TabIndex = 6;
            this.ip.Text = "localhost";
            this.ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ip_KeyPress);
            // 
            // InicioSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 178);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iniciar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.user);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InicioSesion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InicioSesion_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button iniciar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ip;
    }
}