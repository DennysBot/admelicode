namespace Admeli.Productos.Nuevo.PDetalle.sub
{
    partial class FormgGenerar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormgGenerar));
            this.label4 = new System.Windows.Forms.Label();
            this.textCodigo = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.alternativaCombinacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textNombreCombinacion = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.textStock = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label3 = new System.Windows.Forms.Label();
            this.textPrecio = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.textStockMinimo = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.textStockIdeal = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.label7 = new System.Windows.Forms.Label();
            this.textAlertaStock = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.cbxAlmacenes = new System.Windows.Forms.ComboBox();
            this.almacenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.progressBarApp = new System.Windows.Forms.ProgressBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.stockMinimoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockIdealDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreCombinacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigoSkuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alternativasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alertaStockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idCombinacionAlternativaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.alternativaCombinacionBindingSource)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.almacenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(381, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Código Sku";
            // 
            // textCodigo
            // 
            this.textCodigo.BackColor = System.Drawing.Color.White;
            this.textCodigo.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textCodigo.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textCodigo.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textCodigo.BorderThickness = 1;
            this.textCodigo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textCodigo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "codigoSku", true));
            this.textCodigo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textCodigo.isPassword = false;
            this.textCodigo.Location = new System.Drawing.Point(377, 27);
            this.textCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.textCodigo.Name = "textCodigo";
            this.textCodigo.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textCodigo.Size = new System.Drawing.Size(249, 40);
            this.textCodigo.TabIndex = 7;
            this.textCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textCodigo.Validated += new System.EventHandler(this.textCodigo_Validated);
            // 
            // alternativaCombinacionBindingSource
            // 
            this.alternativaCombinacionBindingSource.DataSource = typeof(Entidad.AlternativaCombinacion);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(381, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre Combinación";
            // 
            // textNombreCombinacion
            // 
            this.textNombreCombinacion.BackColor = System.Drawing.Color.White;
            this.textNombreCombinacion.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textNombreCombinacion.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textNombreCombinacion.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textNombreCombinacion.BorderThickness = 1;
            this.textNombreCombinacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textNombreCombinacion.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "nombreCombinacion", true));
            this.textNombreCombinacion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNombreCombinacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textNombreCombinacion.isPassword = false;
            this.textNombreCombinacion.Location = new System.Drawing.Point(377, 88);
            this.textNombreCombinacion.Margin = new System.Windows.Forms.Padding(4);
            this.textNombreCombinacion.Name = "textNombreCombinacion";
            this.textNombreCombinacion.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textNombreCombinacion.Size = new System.Drawing.Size(249, 40);
            this.textNombreCombinacion.TabIndex = 9;
            this.textNombreCombinacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label2.Location = new System.Drawing.Point(381, 220);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 14);
            this.label2.TabIndex = 12;
            this.label2.Text = "Stock";
            // 
            // textStock
            // 
            this.textStock.BackColor = System.Drawing.Color.White;
            this.textStock.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStock.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStock.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStock.BorderThickness = 1;
            this.textStock.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStock.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "stock", true));
            this.textStock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStock.isPassword = false;
            this.textStock.Location = new System.Drawing.Point(377, 216);
            this.textStock.Margin = new System.Windows.Forms.Padding(4);
            this.textStock.Name = "textStock";
            this.textStock.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textStock.Size = new System.Drawing.Size(249, 40);
            this.textStock.TabIndex = 13;
            this.textStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(381, 156);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "Precio";
            // 
            // textPrecio
            // 
            this.textPrecio.BackColor = System.Drawing.Color.White;
            this.textPrecio.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textPrecio.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textPrecio.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textPrecio.BorderThickness = 1;
            this.textPrecio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textPrecio.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "precio", true));
            this.textPrecio.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textPrecio.isPassword = false;
            this.textPrecio.Location = new System.Drawing.Point(377, 152);
            this.textPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.textPrecio.Name = "textPrecio";
            this.textPrecio.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textPrecio.Size = new System.Drawing.Size(249, 40);
            this.textPrecio.TabIndex = 11;
            this.textPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(381, 348);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 14);
            this.label5.TabIndex = 16;
            this.label5.Text = "Stock Mínimo";
            // 
            // textStockMinimo
            // 
            this.textStockMinimo.BackColor = System.Drawing.Color.White;
            this.textStockMinimo.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStockMinimo.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStockMinimo.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStockMinimo.BorderThickness = 1;
            this.textStockMinimo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStockMinimo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "stockMinimo", true));
            this.textStockMinimo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStockMinimo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStockMinimo.isPassword = false;
            this.textStockMinimo.Location = new System.Drawing.Point(377, 344);
            this.textStockMinimo.Margin = new System.Windows.Forms.Padding(4);
            this.textStockMinimo.Name = "textStockMinimo";
            this.textStockMinimo.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textStockMinimo.Size = new System.Drawing.Size(249, 40);
            this.textStockMinimo.TabIndex = 17;
            this.textStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(381, 284);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 14);
            this.label6.TabIndex = 14;
            this.label6.Text = "Stock Ideal";
            // 
            // textStockIdeal
            // 
            this.textStockIdeal.BackColor = System.Drawing.Color.White;
            this.textStockIdeal.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textStockIdeal.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStockIdeal.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textStockIdeal.BorderThickness = 1;
            this.textStockIdeal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textStockIdeal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "stockIdeal", true));
            this.textStockIdeal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textStockIdeal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textStockIdeal.isPassword = false;
            this.textStockIdeal.Location = new System.Drawing.Point(377, 280);
            this.textStockIdeal.Margin = new System.Windows.Forms.Padding(4);
            this.textStockIdeal.Name = "textStockIdeal";
            this.textStockIdeal.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textStockIdeal.Size = new System.Drawing.Size(249, 40);
            this.textStockIdeal.TabIndex = 15;
            this.textStockIdeal.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(381, 412);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Alerta Stock";
            // 
            // textAlertaStock
            // 
            this.textAlertaStock.BackColor = System.Drawing.Color.White;
            this.textAlertaStock.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textAlertaStock.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textAlertaStock.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.textAlertaStock.BorderThickness = 1;
            this.textAlertaStock.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textAlertaStock.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alternativaCombinacionBindingSource, "alertaStock", true));
            this.textAlertaStock.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textAlertaStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textAlertaStock.isPassword = false;
            this.textAlertaStock.Location = new System.Drawing.Point(377, 408);
            this.textAlertaStock.Margin = new System.Windows.Forms.Padding(4);
            this.textAlertaStock.Name = "textAlertaStock";
            this.textAlertaStock.Padding = new System.Windows.Forms.Padding(2, 10, 5, 2);
            this.textAlertaStock.Size = new System.Drawing.Size(249, 40);
            this.textAlertaStock.TabIndex = 19;
            this.textAlertaStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 474);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(650, 49);
            this.panelFooter.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(166, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 48);
            this.panel1.TabIndex = 0;
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
            this.btnAceptar.Location = new System.Drawing.Point(16, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(152, 30);
            this.btnAceptar.TabIndex = 0;
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
            this.btnClose.Location = new System.Drawing.Point(188, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.cbxAlmacenes);
            this.panel12.Controls.Add(this.label8);
            this.panel12.Location = new System.Drawing.Point(12, 24);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(345, 40);
            this.panel12.TabIndex = 22;
            // 
            // cbxAlmacenes
            // 
            this.cbxAlmacenes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxAlmacenes.DataSource = this.almacenBindingSource;
            this.cbxAlmacenes.DisplayMember = "nombre";
            this.cbxAlmacenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxAlmacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAlmacenes.FormattingEnabled = true;
            this.cbxAlmacenes.Location = new System.Drawing.Point(3, 14);
            this.cbxAlmacenes.Name = "cbxAlmacenes";
            this.cbxAlmacenes.Size = new System.Drawing.Size(338, 24);
            this.cbxAlmacenes.TabIndex = 1;
            this.cbxAlmacenes.ValueMember = "idAlmacen";
            // 
            // almacenBindingSource
            // 
            this.almacenBindingSource.DataSource = typeof(Entidad.Almacen);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 14);
            this.label8.TabIndex = 0;
            this.label8.Text = "Almacenes";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(376, 194);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(237, 14);
            this.label9.TabIndex = 23;
            this.label9.Text = "Precio extra del producto por esta combinación.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(375, 386);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 14);
            this.label10.TabIndex = 24;
            this.label10.Text = "Stock mínimo del almacén.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DimGray;
            this.label11.Location = new System.Drawing.Point(375, 450);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 14);
            this.label11.TabIndex = 25;
            this.label11.Text = "Stock de alerta.";
            // 
            // progressBarApp
            // 
            this.progressBarApp.BackColor = System.Drawing.Color.White;
            this.progressBarApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarApp.Location = new System.Drawing.Point(0, 0);
            this.progressBarApp.Margin = new System.Windows.Forms.Padding(2);
            this.progressBarApp.MarqueeAnimationSpeed = 10;
            this.progressBarApp.Maximum = 200;
            this.progressBarApp.Name = "progressBarApp";
            this.progressBarApp.RightToLeftLayout = true;
            this.progressBarApp.Size = new System.Drawing.Size(650, 5);
            this.progressBarApp.TabIndex = 26;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // stockMinimoDataGridViewTextBoxColumn
            // 
            this.stockMinimoDataGridViewTextBoxColumn.DataPropertyName = "stockMinimo";
            this.stockMinimoDataGridViewTextBoxColumn.HeaderText = "stockMinimo";
            this.stockMinimoDataGridViewTextBoxColumn.Name = "stockMinimoDataGridViewTextBoxColumn";
            this.stockMinimoDataGridViewTextBoxColumn.ReadOnly = true;
            this.stockMinimoDataGridViewTextBoxColumn.Visible = false;
            // 
            // stockIdealDataGridViewTextBoxColumn
            // 
            this.stockIdealDataGridViewTextBoxColumn.DataPropertyName = "stockIdeal";
            this.stockIdealDataGridViewTextBoxColumn.HeaderText = "stockIdeal";
            this.stockIdealDataGridViewTextBoxColumn.Name = "stockIdealDataGridViewTextBoxColumn";
            this.stockIdealDataGridViewTextBoxColumn.ReadOnly = true;
            this.stockIdealDataGridViewTextBoxColumn.Visible = false;
            // 
            // stockDataGridViewTextBoxColumn
            // 
            this.stockDataGridViewTextBoxColumn.DataPropertyName = "stock";
            this.stockDataGridViewTextBoxColumn.HeaderText = "stock";
            this.stockDataGridViewTextBoxColumn.Name = "stockDataGridViewTextBoxColumn";
            this.stockDataGridViewTextBoxColumn.ReadOnly = true;
            this.stockDataGridViewTextBoxColumn.Visible = false;
            // 
            // precioDataGridViewTextBoxColumn
            // 
            this.precioDataGridViewTextBoxColumn.DataPropertyName = "precio";
            this.precioDataGridViewTextBoxColumn.HeaderText = "precio";
            this.precioDataGridViewTextBoxColumn.Name = "precioDataGridViewTextBoxColumn";
            this.precioDataGridViewTextBoxColumn.ReadOnly = true;
            this.precioDataGridViewTextBoxColumn.Visible = false;
            // 
            // nombreCombinacionDataGridViewTextBoxColumn
            // 
            this.nombreCombinacionDataGridViewTextBoxColumn.DataPropertyName = "nombreCombinacion";
            this.nombreCombinacionDataGridViewTextBoxColumn.HeaderText = "Nombre Combinacion";
            this.nombreCombinacionDataGridViewTextBoxColumn.Name = "nombreCombinacionDataGridViewTextBoxColumn";
            this.nombreCombinacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codigoSkuDataGridViewTextBoxColumn
            // 
            this.codigoSkuDataGridViewTextBoxColumn.DataPropertyName = "codigoSku";
            this.codigoSkuDataGridViewTextBoxColumn.HeaderText = "codigoSku";
            this.codigoSkuDataGridViewTextBoxColumn.Name = "codigoSkuDataGridViewTextBoxColumn";
            this.codigoSkuDataGridViewTextBoxColumn.ReadOnly = true;
            this.codigoSkuDataGridViewTextBoxColumn.Visible = false;
            // 
            // alternativasDataGridViewTextBoxColumn
            // 
            this.alternativasDataGridViewTextBoxColumn.DataPropertyName = "alternativas";
            this.alternativasDataGridViewTextBoxColumn.HeaderText = "alternativas";
            this.alternativasDataGridViewTextBoxColumn.Name = "alternativasDataGridViewTextBoxColumn";
            this.alternativasDataGridViewTextBoxColumn.ReadOnly = true;
            this.alternativasDataGridViewTextBoxColumn.Visible = false;
            // 
            // alertaStockDataGridViewTextBoxColumn
            // 
            this.alertaStockDataGridViewTextBoxColumn.DataPropertyName = "alertaStock";
            this.alertaStockDataGridViewTextBoxColumn.HeaderText = "alertaStock";
            this.alertaStockDataGridViewTextBoxColumn.Name = "alertaStockDataGridViewTextBoxColumn";
            this.alertaStockDataGridViewTextBoxColumn.ReadOnly = true;
            this.alertaStockDataGridViewTextBoxColumn.Visible = false;
            // 
            // idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn
            // 
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn.DataPropertyName = "idCombinacionAlternativaStockAlmacen";
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn.HeaderText = "idCombinacionAlternativaStockAlmacen";
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn.Name = "idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn";
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn.ReadOnly = true;
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn.Visible = false;
            // 
            // idCombinacionAlternativaDataGridViewTextBoxColumn
            // 
            this.idCombinacionAlternativaDataGridViewTextBoxColumn.DataPropertyName = "idCombinacionAlternativa";
            this.idCombinacionAlternativaDataGridViewTextBoxColumn.HeaderText = "idCombinacionAlternativa";
            this.idCombinacionAlternativaDataGridViewTextBoxColumn.Name = "idCombinacionAlternativaDataGridViewTextBoxColumn";
            this.idCombinacionAlternativaDataGridViewTextBoxColumn.ReadOnly = true;
            this.idCombinacionAlternativaDataGridViewTextBoxColumn.Visible = false;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.ColumnHeadersHeight = 35;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCombinacionAlternativaDataGridViewTextBoxColumn,
            this.idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn,
            this.alertaStockDataGridViewTextBoxColumn,
            this.alternativasDataGridViewTextBoxColumn,
            this.codigoSkuDataGridViewTextBoxColumn,
            this.nombreCombinacionDataGridViewTextBoxColumn,
            this.precioDataGridViewTextBoxColumn,
            this.stockDataGridViewTextBoxColumn,
            this.stockIdealDataGridViewTextBoxColumn,
            this.stockMinimoDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.alternativaCombinacionBindingSource;
            this.dataGridView.GridColor = System.Drawing.Color.LightGray;
            this.dataGridView.Location = new System.Drawing.Point(12, 88);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView.RowHeadersWidth = 30;
            this.dataGridView.Size = new System.Drawing.Size(345, 360);
            this.dataGridView.TabIndex = 21;
            // 
            // FormgGenerar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 523);
            this.Controls.Add(this.progressBarApp);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textAlertaStock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textStockMinimo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textStockIdeal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textStock);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textPrecio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textNombreCombinacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textCodigo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormgGenerar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormgGenerar";
            this.Load += new System.EventHandler(this.FormgGenerar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alternativaCombinacionBindingSource)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.almacenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuMetroTextbox textCodigo;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMetroTextbox textNombreCombinacion;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStock;
        private System.Windows.Forms.Label label3;
        private Bunifu.Framework.UI.BunifuMetroTextbox textPrecio;
        private System.Windows.Forms.Label label5;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStockMinimo;
        private System.Windows.Forms.Label label6;
        private Bunifu.Framework.UI.BunifuMetroTextbox textStockIdeal;
        private System.Windows.Forms.Label label7;
        private Bunifu.Framework.UI.BunifuMetroTextbox textAlertaStock;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.ComboBox cbxAlmacenes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.BindingSource almacenBindingSource;
        private System.Windows.Forms.BindingSource alternativaCombinacionBindingSource;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        protected System.Windows.Forms.ProgressBar progressBarApp;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCombinacionAlternativaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCombinacionAlternativaStockAlmacenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alertaStockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alternativasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoSkuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreCombinacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockIdealDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockMinimoDataGridViewTextBoxColumn;
    }
}