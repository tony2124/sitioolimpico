namespace WindowsFormsApplication1
{
    partial class cliente
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.nombre_cliente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ref_cliente = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.calle_cliente = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.colonia_cliente = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.num_tel_1_cliente = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.unidad_servicio = new System.Windows.Forms.TextBox();
            this.descripcion_servicio = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.editar_btn_cliente = new System.Windows.Forms.Button();
            this.cancelar_btn_cliente = new System.Windows.Forms.Button();
            this.guardar_btn_cliente = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.nombre_cliente);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(78, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(522, 64);
            this.panel1.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(505, 22);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 20);
            this.label25.TabIndex = 7;
            this.label25.Text = "*";
            // 
            // nombre_cliente
            // 
            this.nombre_cliente.BackColor = System.Drawing.Color.Ivory;
            this.nombre_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.nombre_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombre_cliente.Location = new System.Drawing.Point(78, 20);
            this.nombre_cliente.Name = "nombre_cliente";
            this.nombre_cliente.Size = new System.Drawing.Size(426, 24);
            this.nombre_cliente.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nombre (s): ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(120, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos Personales";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(8, 441);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 20);
            this.label27.TabIndex = 10;
            this.label27.Text = "*";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(22, 442);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(91, 13);
            this.label28.TabIndex = 11;
            this.label28.Text = "Campo obligatorio";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Location = new System.Drawing.Point(24, 234);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(306, 187);
            this.panel6.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Khaki;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(20, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "Dirección y Contacto";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.ref_cliente);
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.calle_cliente);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.colonia_cliente);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Location = new System.Drawing.Point(10, 15);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(284, 159);
            this.panel5.TabIndex = 15;
            // 
            // ref_cliente
            // 
            this.ref_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.ref_cliente.Location = new System.Drawing.Point(76, 81);
            this.ref_cliente.Multiline = true;
            this.ref_cliente.Name = "ref_cliente";
            this.ref_cliente.Size = new System.Drawing.Size(200, 67);
            this.ref_cliente.TabIndex = 5;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(3, 83);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 13);
            this.label24.TabIndex = 13;
            this.label24.Text = "Referencias:";
            // 
            // calle_cliente
            // 
            this.calle_cliente.BackColor = System.Drawing.Color.Ivory;
            this.calle_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.calle_cliente.Location = new System.Drawing.Point(50, 56);
            this.calle_cliente.Name = "calle_cliente";
            this.calle_cliente.Size = new System.Drawing.Size(226, 20);
            this.calle_cliente.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 60);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Calle:";
            // 
            // colonia_cliente
            // 
            this.colonia_cliente.BackColor = System.Drawing.Color.Ivory;
            this.colonia_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.colonia_cliente.Location = new System.Drawing.Point(50, 32);
            this.colonia_cliente.Name = "colonia_cliente";
            this.colonia_cliente.Size = new System.Drawing.Size(226, 20);
            this.colonia_cliente.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Colonia:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Teléfono :";
            // 
            // num_tel_1_cliente
            // 
            this.num_tel_1_cliente.BackColor = System.Drawing.Color.Ivory;
            this.num_tel_1_cliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.num_tel_1_cliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_tel_1_cliente.Location = new System.Drawing.Point(85, 10);
            this.num_tel_1_cliente.Name = "num_tel_1_cliente";
            this.num_tel_1_cliente.Size = new System.Drawing.Size(419, 38);
            this.num_tel_1_cliente.TabIndex = 2;
            this.num_tel_1_cliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.num_tel_1_cliente_KeyPress);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.unidad_servicio);
            this.panel7.Controls.Add(this.descripcion_servicio);
            this.panel7.Controls.Add(this.label22);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Location = new System.Drawing.Point(348, 273);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(311, 126);
            this.panel7.TabIndex = 4;
            // 
            // unidad_servicio
            // 
            this.unidad_servicio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.unidad_servicio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.unidad_servicio.Location = new System.Drawing.Point(86, 20);
            this.unidad_servicio.Name = "unidad_servicio";
            this.unidad_servicio.Size = new System.Drawing.Size(207, 20);
            this.unidad_servicio.TabIndex = 6;
            this.unidad_servicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.unidad_servicio_KeyPress);
            // 
            // descripcion_servicio
            // 
            this.descripcion_servicio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.descripcion_servicio.Location = new System.Drawing.Point(86, 46);
            this.descripcion_servicio.Multiline = true;
            this.descripcion_servicio.Name = "descripcion_servicio";
            this.descripcion_servicio.Size = new System.Drawing.Size(207, 62);
            this.descripcion_servicio.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(4, 65);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(78, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Descripción:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Unidad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Khaki;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(355, 267);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Servicios";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.num_tel_1_cliente);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(78, 157);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(522, 63);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(506, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "*";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::WindowsFormsApplication1.Properties.Resources.CLIENTE_ENCABEZADO;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(679, 60);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // editar_btn_cliente
            // 
            this.editar_btn_cliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.editar_btn_cliente.Image = global::WindowsFormsApplication1.Properties.Resources.MODIFICAR;
            this.editar_btn_cliente.Location = new System.Drawing.Point(168, 427);
            this.editar_btn_cliente.Name = "editar_btn_cliente";
            this.editar_btn_cliente.Size = new System.Drawing.Size(118, 92);
            this.editar_btn_cliente.TabIndex = 19;
            this.editar_btn_cliente.UseVisualStyleBackColor = true;
            this.editar_btn_cliente.Click += new System.EventHandler(this.editar_btn_cliente_Click);
            // 
            // cancelar_btn_cliente
            // 
            this.cancelar_btn_cliente.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.CANCELAR;
            this.cancelar_btn_cliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cancelar_btn_cliente.Location = new System.Drawing.Point(420, 427);
            this.cancelar_btn_cliente.Name = "cancelar_btn_cliente";
            this.cancelar_btn_cliente.Size = new System.Drawing.Size(121, 92);
            this.cancelar_btn_cliente.TabIndex = 21;
            this.cancelar_btn_cliente.UseVisualStyleBackColor = true;
            this.cancelar_btn_cliente.Click += new System.EventHandler(this.cancelar_btn_personal_Click);
            // 
            // guardar_btn_cliente
            // 
            this.guardar_btn_cliente.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.GUARDAR2;
            this.guardar_btn_cliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.guardar_btn_cliente.Location = new System.Drawing.Point(292, 427);
            this.guardar_btn_cliente.Name = "guardar_btn_cliente";
            this.guardar_btn_cliente.Size = new System.Drawing.Size(122, 92);
            this.guardar_btn_cliente.TabIndex = 20;
            this.guardar_btn_cliente.UseVisualStyleBackColor = true;
            this.guardar_btn_cliente.Click += new System.EventHandler(this.guardar_btn_personal_Click);
            // 
            // cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(672, 531);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.editar_btn_cliente);
            this.Controls.Add(this.cancelar_btn_cliente);
            this.Controls.Add(this.guardar_btn_cliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "cliente";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox nombre_cliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button guardar_btn_cliente;
        private System.Windows.Forms.Button editar_btn_cliente;
        private System.Windows.Forms.Button cancelar_btn_cliente;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox ref_cliente;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox calle_cliente;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox colonia_cliente;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox num_tel_1_cliente;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox descripcion_servicio;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox unidad_servicio;
    }
}