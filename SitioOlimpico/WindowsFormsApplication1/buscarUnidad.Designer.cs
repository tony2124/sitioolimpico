namespace WindowsFormsApplication1
{
    partial class buscarUnidad
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
            this.label2 = new System.Windows.Forms.Label();
            this.unidad_buscar_txt = new System.Windows.Forms.TextBox();
            this.buscar_unidad = new System.Windows.Forms.Button();
            this.cancelar_buscar_unidad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("News Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "INGRESA EL NÚMERO DE UNIDAD QUE DESEAS BUSCAR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("News Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Unidad:";
            // 
            // unidad_buscar_txt
            // 
            this.unidad_buscar_txt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.unidad_buscar_txt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.unidad_buscar_txt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.unidad_buscar_txt.Font = new System.Drawing.Font("News Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unidad_buscar_txt.Location = new System.Drawing.Point(137, 43);
            this.unidad_buscar_txt.Name = "unidad_buscar_txt";
            this.unidad_buscar_txt.Size = new System.Drawing.Size(408, 31);
            this.unidad_buscar_txt.TabIndex = 2;
            // 
            // buscar_unidad
            // 
            this.buscar_unidad.Location = new System.Drawing.Point(201, 80);
            this.buscar_unidad.Name = "buscar_unidad";
            this.buscar_unidad.Size = new System.Drawing.Size(75, 44);
            this.buscar_unidad.TabIndex = 3;
            this.buscar_unidad.Text = "Buscar";
            this.buscar_unidad.UseVisualStyleBackColor = true;
            this.buscar_unidad.Click += new System.EventHandler(this.buscar_unidad_Click);
            // 
            // cancelar_buscar_unidad
            // 
            this.cancelar_buscar_unidad.Location = new System.Drawing.Point(282, 80);
            this.cancelar_buscar_unidad.Name = "cancelar_buscar_unidad";
            this.cancelar_buscar_unidad.Size = new System.Drawing.Size(75, 44);
            this.cancelar_buscar_unidad.TabIndex = 4;
            this.cancelar_buscar_unidad.Text = "Cancelar";
            this.cancelar_buscar_unidad.UseVisualStyleBackColor = true;
            this.cancelar_buscar_unidad.Click += new System.EventHandler(this.button2_Click);
            // 
            // buscarUnidad
            // 
            this.AcceptButton = this.buscar_unidad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(573, 136);
            this.Controls.Add(this.cancelar_buscar_unidad);
            this.Controls.Add(this.buscar_unidad);
            this.Controls.Add(this.unidad_buscar_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "buscarUnidad";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Unidad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox unidad_buscar_txt;
        private System.Windows.Forms.Button buscar_unidad;
        private System.Windows.Forms.Button cancelar_buscar_unidad;
    }
}