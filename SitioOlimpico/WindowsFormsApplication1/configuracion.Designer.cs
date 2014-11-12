namespace WindowsFormsApplication1
{
    partial class configuracion
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bits = new System.Windows.Forms.ComboBox();
            this.paridad = new System.Windows.Forms.ComboBox();
            this.norma = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dir_foto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.plataforma = new System.Windows.Forms.Label();
            this.service = new System.Windows.Forms.Label();
            this.ip = new System.Windows.Forms.Label();
            this.puerto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ip_base_datos = new System.Windows.Forms.TextBox();
            this.modem = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Identificador de llamadas";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gold;
            this.panel1.Location = new System.Drawing.Point(18, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 3);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Modem instalado:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Puerto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Bits por segundo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Paridad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Norma:";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(30, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(278, 55);
            this.label8.TabIndex = 12;
            this.label8.Text = "Nota: No todos los modems cuentan con identificador de llamadas integrado.";
            // 
            // bits
            // 
            this.bits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bits.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bits.FormattingEnabled = true;
            this.bits.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.bits.Location = new System.Drawing.Point(142, 160);
            this.bits.Name = "bits";
            this.bits.Size = new System.Drawing.Size(165, 23);
            this.bits.TabIndex = 15;
            // 
            // paridad
            // 
            this.paridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paridad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.paridad.FormattingEnabled = true;
            this.paridad.Items.AddRange(new object[] {
            "Par",
            "Marcado",
            "Ninguno",
            "Impar",
            "Espacio"});
            this.paridad.Location = new System.Drawing.Point(142, 190);
            this.paridad.Name = "paridad";
            this.paridad.Size = new System.Drawing.Size(165, 23);
            this.paridad.TabIndex = 16;
            // 
            // norma
            // 
            this.norma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.norma.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.norma.FormattingEnabled = true;
            this.norma.Items.AddRange(new object[] {
            "AT+VCID=1",
            "AT#CLS=8#CID=1",
            "AT#CID=1 ",
            "AT%CCID=1 ",
            "AT%CCID=2 ",
            "AT#CID=2",
            "AT#CC1 ",
            "AT*ID1"});
            this.norma.Location = new System.Drawing.Point(142, 219);
            this.norma.Name = "norma";
            this.norma.Size = new System.Drawing.Size(165, 23);
            this.norma.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(355, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 15);
            this.label14.TabIndex = 20;
            this.label14.Text = "Directorio de fotografías:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(354, 91);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(168, 20);
            this.label15.TabIndex = 19;
            this.label15.Text = "Aspectos generales";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(286, 279);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 25);
            this.button3.TabIndex = 33;
            this.button3.Text = "detectar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dir_foto
            // 
            this.dir_foto.Location = new System.Drawing.Point(502, 128);
            this.dir_foto.Name = "dir_foto";
            this.dir_foto.ReadOnly = true;
            this.dir_foto.Size = new System.Drawing.Size(153, 21);
            this.dir_foto.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(356, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 15);
            this.label6.TabIndex = 37;
            this.label6.Text = "IP local:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(661, 127);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 39;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsFormsApplication1.Properties.Resources.ENCABEZADO1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(715, 61);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.GUARDAR2;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.Location = new System.Drawing.Point(428, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 90);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.CANCELAR;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(559, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 90);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(356, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 44;
            this.label9.Text = "Plataforma:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(356, 279);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 15);
            this.label10.TabIndex = 46;
            this.label10.Text = "Service Pack:";
            // 
            // plataforma
            // 
            this.plataforma.Location = new System.Drawing.Point(441, 224);
            this.plataforma.Name = "plataforma";
            this.plataforma.Size = new System.Drawing.Size(214, 47);
            this.plataforma.TabIndex = 48;
            this.plataforma.Text = "(No disponible)";
            // 
            // service
            // 
            this.service.Location = new System.Drawing.Point(441, 279);
            this.service.Name = "service";
            this.service.Size = new System.Drawing.Size(213, 25);
            this.service.TabIndex = 49;
            this.service.Text = "(No disponible)";
            // 
            // ip
            // 
            this.ip.Location = new System.Drawing.Point(499, 193);
            this.ip.Name = "ip";
            this.ip.Size = new System.Drawing.Size(156, 20);
            this.ip.TabIndex = 50;
            this.ip.Text = "(IP disponible)";
            // 
            // puerto
            // 
            this.puerto.Location = new System.Drawing.Point(142, 128);
            this.puerto.Name = "puerto";
            this.puerto.Size = new System.Drawing.Size(166, 21);
            this.puerto.TabIndex = 51;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(355, 160);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 15);
            this.label12.TabIndex = 52;
            this.label12.Text = "IP  de la base de datos";
            // 
            // ip_base_datos
            // 
            this.ip_base_datos.Location = new System.Drawing.Point(502, 160);
            this.ip_base_datos.Name = "ip_base_datos";
            this.ip_base_datos.Size = new System.Drawing.Size(153, 21);
            this.ip_base_datos.TabIndex = 53;
            // 
            // modem
            // 
            this.modem.Location = new System.Drawing.Point(143, 286);
            this.modem.Name = "modem";
            this.modem.Size = new System.Drawing.Size(137, 60);
            this.modem.TabIndex = 54;
            this.modem.Text = "(No se ha detectado un MODEM)";
            // 
            // configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(696, 443);
            this.Controls.Add(this.modem);
            this.Controls.Add(this.ip_base_datos);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.puerto);
            this.Controls.Add(this.ip);
            this.Controls.Add(this.service);
            this.Controls.Add(this.plataforma);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dir_foto);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.norma);
            this.Controls.Add(this.paridad);
            this.Controls.Add(this.bits);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "configuracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox bits;
        private System.Windows.Forms.ComboBox paridad;
        private System.Windows.Forms.ComboBox norma;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox dir_foto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label plataforma;
        private System.Windows.Forms.Label service;
        private System.Windows.Forms.Label ip;
        private System.Windows.Forms.TextBox puerto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox ip_base_datos;
        private System.Windows.Forms.Label modem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}