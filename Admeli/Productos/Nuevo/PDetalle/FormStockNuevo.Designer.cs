namespace Admeli.Productos.Nuevo.PDetalle
{
    partial class FormStockNuevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStockNuevo));
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textStock = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.textStockIdeal = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.textStockMinimo = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label3 = new System.Windows.Forms.Label();
            this.textStockAlerta = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkActivoStock = new Bunifu.Framework.UI.BunifuCheckbox();
            this.btnAddAlmacen = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxAlmace = new System.Windows.Forms.ComboBox();
            this.almacenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.almacenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 450);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(406, 49);
            this.panelFooter.TabIndex = 33;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(50, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 48);
            this.panel1.TabIndex = 6;
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
            this.btnAceptar.Location = new System.Drawing.Point(10, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(152, 36);
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
            this.btnClose.Location = new System.Drawing.Point(182, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 36);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(406, 42);
            this.panel2.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label4.Location = new System.Drawing.Point(16, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mantenimiento Stock Producto";
            // 
            // textStock
            // 
            this.textStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textStock.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStock.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textStock.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textStock.BorderThickness = 1;
            this.textStock.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStock.isPassword = false;
            this.textStock.Location = new System.Drawing.Point(18, 76);
            this.textStock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStock.Name = "textStock";
            this.textStock.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textStock.Size = new System.Drawing.Size(367, 35);
            this.textStock.TabIndex = 41;
            this.textStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Stock";
            // 
            // textStockIdeal
            // 
            this.textStockIdeal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textStockIdeal.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStockIdeal.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textStockIdeal.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textStockIdeal.BorderThickness = 1;
            this.textStockIdeal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStockIdeal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStockIdeal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStockIdeal.isPassword = false;
            this.textStockIdeal.Location = new System.Drawing.Point(18, 144);
            this.textStockIdeal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStockIdeal.Name = "textStockIdeal";
            this.textStockIdeal.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textStockIdeal.Size = new System.Drawing.Size(367, 35);
            this.textStockIdeal.TabIndex = 43;
            this.textStockIdeal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(15, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 42;
            this.label1.Text = "Stock Ideal";
            // 
            // textStockMinimo
            // 
            this.textStockMinimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textStockMinimo.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStockMinimo.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textStockMinimo.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textStockMinimo.BorderThickness = 1;
            this.textStockMinimo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStockMinimo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStockMinimo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStockMinimo.isPassword = false;
            this.textStockMinimo.Location = new System.Drawing.Point(18, 215);
            this.textStockMinimo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStockMinimo.Name = "textStockMinimo";
            this.textStockMinimo.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textStockMinimo.Size = new System.Drawing.Size(367, 35);
            this.textStockMinimo.TabIndex = 45;
            this.textStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(15, 197);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Stock Mínimo";
            // 
            // textStockAlerta
            // 
            this.textStockAlerta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textStockAlerta.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStockAlerta.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textStockAlerta.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textStockAlerta.BorderThickness = 1;
            this.textStockAlerta.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStockAlerta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStockAlerta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStockAlerta.isPassword = false;
            this.textStockAlerta.Location = new System.Drawing.Point(19, 280);
            this.textStockAlerta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStockAlerta.Name = "textStockAlerta";
            this.textStockAlerta.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textStockAlerta.Size = new System.Drawing.Size(367, 35);
            this.textStockAlerta.TabIndex = 47;
            this.textStockAlerta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(16, 262);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "Alerta Stock";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label9.Location = new System.Drawing.Point(44, 333);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 16);
            this.label9.TabIndex = 57;
            this.label9.Text = "Activo *";
            // 
            // chkActivoStock
            // 
            this.chkActivoStock.BackColor = System.Drawing.Color.DodgerBlue;
            this.chkActivoStock.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.chkActivoStock.Checked = true;
            this.chkActivoStock.CheckedOnColor = System.Drawing.Color.DodgerBlue;
            this.chkActivoStock.ForeColor = System.Drawing.Color.White;
            this.chkActivoStock.Location = new System.Drawing.Point(19, 331);
            this.chkActivoStock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkActivoStock.Name = "chkActivoStock";
            this.chkActivoStock.Size = new System.Drawing.Size(20, 20);
            this.chkActivoStock.TabIndex = 56;
            // 
            // btnAddAlmacen
            // 
            this.btnAddAlmacen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddAlmacen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddAlmacen.FlatAppearance.BorderSize = 0;
            this.btnAddAlmacen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddAlmacen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddAlmacen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAlmacen.ForeColor = System.Drawing.Color.White;
            this.btnAddAlmacen.Location = new System.Drawing.Point(346, 383);
            this.btnAddAlmacen.Name = "btnAddAlmacen";
            this.btnAddAlmacen.Size = new System.Drawing.Size(40, 26);
            this.btnAddAlmacen.TabIndex = 55;
            this.btnAddAlmacen.Text = "+";
            this.btnAddAlmacen.UseVisualStyleBackColor = false;
            this.btnAddAlmacen.Click += new System.EventHandler(this.btnAddAlmacen_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(16, 365);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 54;
            this.label6.Text = "Almacen";
            // 
            // cbxAlmace
            // 
            this.cbxAlmace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxAlmace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAlmace.DataSource = this.almacenBindingSource;
            this.cbxAlmace.DisplayMember = "nombre";
            this.cbxAlmace.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAlmace.FormattingEnabled = true;
            this.cbxAlmace.Location = new System.Drawing.Point(19, 383);
            this.cbxAlmace.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxAlmace.Name = "cbxAlmace";
            this.cbxAlmace.Size = new System.Drawing.Size(328, 26);
            this.cbxAlmace.TabIndex = 53;
            this.cbxAlmace.ValueMember = "idAlmacen";
            // 
            // almacenBindingSource
            // 
            this.almacenBindingSource.DataSource = typeof(Entidad.Almacen);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormStockNuevo
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(406, 499);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkActivoStock);
            this.Controls.Add(this.btnAddAlmacen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxAlmace);
            this.Controls.Add(this.textStockAlerta);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textStockMinimo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textStockIdeal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textStock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStockNuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormStockNuevo";
            this.Load += new System.EventHandler(this.FormStockNuevo_Load);
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.almacenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStock;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStockIdeal;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStockMinimo;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStockAlerta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private Bunifu.Framework.UI.BunifuCheckbox chkActivoStock;
        private System.Windows.Forms.Button btnAddAlmacen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxAlmace;
        private System.Windows.Forms.BindingSource almacenBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}