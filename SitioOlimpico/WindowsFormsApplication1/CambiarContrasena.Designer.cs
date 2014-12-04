namespace WindowsFormsApplication1
{
    partial class CambiarContrasena
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
            this.label1 = new System.Windows.Forms.Label();
            this.actual = new System.Windows.Forms.TextBox();
            this.nueva1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nueva2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contraseña actual:";
            // 
            // actual
            // 
            this.actual.Location = new System.Drawing.Point(170, 19);
            this.actual.Margin = new System.Windows.Forms.Padding(4);
            this.actual.Name = "actual";
            this.actual.PasswordChar = '*';
            this.actual.Size = new System.Drawing.Size(186, 21);
            this.actual.TabIndex = 1;
            this.actual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.actual_MouseClick);
            this.actual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.actual_KeyPress);
            // 
            // nueva1
            // 
            this.nueva1.Location = new System.Drawing.Point(170, 49);
            this.nueva1.Margin = new System.Windows.Forms.Padding(4);
            this.nueva1.Name = "nueva1";
            this.nueva1.PasswordChar = '*';
            this.nueva1.Size = new System.Drawing.Size(186, 21);
            this.nueva1.TabIndex = 3;
            this.nueva1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nueva1_MouseClick);
            this.nueva1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nueva1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña nueva:";
            // 
            // nueva2
            // 
            this.nueva2.Location = new System.Drawing.Point(170, 79);
            this.nueva2.Margin = new System.Windows.Forms.Padding(4);
            this.nueva2.Name = "nueva2";
            this.nueva2.PasswordChar = '*';
            this.nueva2.Size = new System.Drawing.Size(186, 21);
            this.nueva2.TabIndex = 5;
            this.nueva2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nueva2_MouseClick);
            this.nueva2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nueva2_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Repite nueva contraseña:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 127);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cambiar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(70, 127);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CambiarContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 183);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nueva2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nueva1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.actual);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CambiarContrasena";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar contraseña";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox actual;
        private System.Windows.Forms.TextBox nueva1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nueva2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}