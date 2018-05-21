namespace Admeli.Productos.Nuevo.PDetalle
{
    partial class FormElegirCategoria
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormElegirCategoria));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxCategoriaPrincipal = new System.Windows.Forms.ComboBox();
            this.categoriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bunifuSeparator2 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label5 = new System.Windows.Forms.Label();
            this.treeViewFrom = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.treeViewTo = new System.Windows.Forms.TreeView();
            this.panelBody = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnMovIzquierda = new System.Windows.Forms.Button();
            this.btnMovDerecha = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelHeder = new System.Windows.Forms.Panel();
            this.bunifuSeparator1 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).BeginInit();
            this.panelBody.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelHeder.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbxCategoriaPrincipal);
            this.panel2.Controls.Add(this.bunifuSeparator2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel2.Size = new System.Drawing.Size(633, 70);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(7, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 86;
            this.label7.Text = "Principal";
            // 
            // cbxCategoriaPrincipal
            // 
            this.cbxCategoriaPrincipal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxCategoriaPrincipal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCategoriaPrincipal.DataSource = this.categoriaBindingSource;
            this.cbxCategoriaPrincipal.DisplayMember = "nombreCategoria";
            this.cbxCategoriaPrincipal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCategoriaPrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCategoriaPrincipal.FormattingEnabled = true;
            this.cbxCategoriaPrincipal.Location = new System.Drawing.Point(10, 24);
            this.cbxCategoriaPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.cbxCategoriaPrincipal.Name = "cbxCategoriaPrincipal";
            this.cbxCategoriaPrincipal.Size = new System.Drawing.Size(219, 24);
            this.cbxCategoriaPrincipal.TabIndex = 13;
            this.cbxCategoriaPrincipal.ValueMember = "idCategoria";
            // 
            // categoriaBindingSource
            // 
            this.categoriaBindingSource.DataSource = typeof(Entidad.Categoria);
            // 
            // bunifuSeparator2
            // 
            this.bunifuSeparator2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuSeparator2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.bunifuSeparator2.LineThickness = 1;
            this.bunifuSeparator2.Location = new System.Drawing.Point(0, 57);
            this.bunifuSeparator2.Margin = new System.Windows.Forms.Padding(4);
            this.bunifuSeparator2.Name = "bunifuSeparator2";
            this.bunifuSeparator2.Size = new System.Drawing.Size(633, 12);
            this.bunifuSeparator2.TabIndex = 11;
            this.bunifuSeparator2.Transparency = 255;
            this.bunifuSeparator2.Vertical = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(365, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Seleccione la categoría aquí.";
            // 
            // treeViewFrom
            // 
            this.treeViewFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewFrom.ImageIndex = 0;
            this.treeViewFrom.ImageList = this.imageList;
            this.treeViewFrom.LabelEdit = true;
            this.treeViewFrom.Location = new System.Drawing.Point(10, 43);
            this.treeViewFrom.Name = "treeViewFrom";
            this.treeViewFrom.SelectedImageIndex = 0;
            this.treeViewFrom.Size = new System.Drawing.Size(248, 235);
            this.treeViewFrom.TabIndex = 2;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "label_icon.png");
            this.imageList.Images.SetKeyName(1, "open_icon.png");
            // 
            // treeViewTo
            // 
            this.treeViewTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTo.ImageIndex = 0;
            this.treeViewTo.ImageList = this.imageList;
            this.treeViewTo.Location = new System.Drawing.Point(10, 43);
            this.treeViewTo.Name = "treeViewTo";
            this.treeViewTo.SelectedImageIndex = 0;
            this.treeViewTo.Size = new System.Drawing.Size(269, 235);
            this.treeViewTo.TabIndex = 3;
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.panel6);
            this.panelBody.Controls.Add(this.panel3);
            this.panelBody.Controls.Add(this.panel1);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 119);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(633, 288);
            this.panelBody.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnMovIzquierda);
            this.panel6.Controls.Add(this.btnMovDerecha);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(268, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(76, 288);
            this.panel6.TabIndex = 6;
            // 
            // btnMovIzquierda
            // 
            this.btnMovIzquierda.BackColor = System.Drawing.SystemColors.Control;
            this.btnMovIzquierda.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovIzquierda.FlatAppearance.BorderSize = 0;
            this.btnMovIzquierda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovIzquierda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovIzquierda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovIzquierda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovIzquierda.ForeColor = System.Drawing.Color.White;
            this.btnMovIzquierda.Image = ((System.Drawing.Image)(resources.GetObject("btnMovIzquierda.Image")));
            this.btnMovIzquierda.Location = new System.Drawing.Point(4, 168);
            this.btnMovIzquierda.Name = "btnMovIzquierda";
            this.btnMovIzquierda.Size = new System.Drawing.Size(67, 35);
            this.btnMovIzquierda.TabIndex = 55;
            this.btnMovIzquierda.UseVisualStyleBackColor = false;
            this.btnMovIzquierda.Click += new System.EventHandler(this.btnMovIzquierda_Click);
            // 
            // btnMovDerecha
            // 
            this.btnMovDerecha.BackColor = System.Drawing.SystemColors.Control;
            this.btnMovDerecha.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovDerecha.FlatAppearance.BorderSize = 0;
            this.btnMovDerecha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovDerecha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnMovDerecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovDerecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovDerecha.ForeColor = System.Drawing.Color.Black;
            this.btnMovDerecha.Image = ((System.Drawing.Image)(resources.GetObject("btnMovDerecha.Image")));
            this.btnMovDerecha.Location = new System.Drawing.Point(4, 116);
            this.btnMovDerecha.Name = "btnMovDerecha";
            this.btnMovDerecha.Size = new System.Drawing.Size(67, 35);
            this.btnMovDerecha.TabIndex = 54;
            this.btnMovDerecha.UseVisualStyleBackColor = false;
            this.btnMovDerecha.Click += new System.EventHandler(this.btnMovDerecha_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeViewTo);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(344, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel3.Size = new System.Drawing.Size(289, 288);
            this.panel3.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(269, 43);
            this.panel5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Categorías del Producto";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeViewFrom);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel1.Size = new System.Drawing.Size(268, 288);
            this.panel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(248, 43);
            this.panel4.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Categorías";
            // 
            // panelHeder
            // 
            this.panelHeder.BackColor = System.Drawing.Color.White;
            this.panelHeder.Controls.Add(this.bunifuSeparator1);
            this.panelHeder.Controls.Add(this.label4);
            this.panelHeder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeder.Location = new System.Drawing.Point(0, 0);
            this.panelHeder.Name = "panelHeder";
            this.panelHeder.Size = new System.Drawing.Size(633, 49);
            this.panelHeder.TabIndex = 5;
            // 
            // bunifuSeparator1
            // 
            this.bunifuSeparator1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuSeparator1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.bunifuSeparator1.LineThickness = 1;
            this.bunifuSeparator1.Location = new System.Drawing.Point(0, 37);
            this.bunifuSeparator1.Margin = new System.Windows.Forms.Padding(4);
            this.bunifuSeparator1.Name = "bunifuSeparator1";
            this.bunifuSeparator1.Size = new System.Drawing.Size(633, 12);
            this.bunifuSeparator1.TabIndex = 1;
            this.bunifuSeparator1.Transparency = 255;
            this.bunifuSeparator1.Vertical = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label4.Location = new System.Drawing.Point(19, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "ELEGIR CATEGORIAS";
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.panel7);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 407);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(633, 49);
            this.panelFooter.TabIndex = 55;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.Controls.Add(this.btnAceptar);
            this.panel7.Controls.Add(this.btnClose);
            this.panel7.Location = new System.Drawing.Point(142, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(328, 49);
            this.panel7.TabIndex = 6;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(37, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(116, 30);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnClose.Location = new System.Drawing.Point(180, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormElegirCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 456);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelHeder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormElegirCategoria";
            this.Text = "FormElegirCategoria";
            this.Load += new System.EventHandler(this.FormElegirCategoria_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriaBindingSource)).EndInit();
            this.panelBody.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelHeder.ResumeLayout(false);
            this.panelHeder.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView treeViewFrom;
        private System.Windows.Forms.TreeView treeViewTo;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelHeder;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.BindingSource categoriaBindingSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator2;
        private System.Windows.Forms.ComboBox cbxCategoriaPrincipal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnMovIzquierda;
        private System.Windows.Forms.Button btnMovDerecha;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnClose;
    }
}