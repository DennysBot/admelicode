namespace Admeli.Productos.Nuevo.PDetalle
{
    partial class FormOfertaNuevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOfertaNuevo));
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textDescuento = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.lblActive = new System.Windows.Forms.Label();
            this.chkEstado = new Bunifu.Framework.UI.BunifuCheckbox();
            this.label7 = new System.Windows.Forms.Label();
            this.textCodigoOferta = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.btnAddSucursal = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxSucursal = new System.Windows.Forms.ComboBox();
            this.sucursalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAddGrupoCliente = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxGrupoCliente = new System.Windows.Forms.ComboBox();
            this.grupoClienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.cbxProductoAfecto = new System.Windows.Forms.ComboBox();
            this.productoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.presentacionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupoClienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.presentacionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.panel1);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 402);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.panelFooter.Size = new System.Drawing.Size(658, 49);
            this.panelFooter.TabIndex = 35;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(176, 1);
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
            this.panel2.Size = new System.Drawing.Size(658, 42);
            this.panel2.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.label4.Location = new System.Drawing.Point(16, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mantenimiento Oferta Producto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(333, 128);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 16);
            this.label8.TabIndex = 71;
            this.label8.Text = "Fecha Fin";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(337, 148);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(300, 24);
            this.dtpFechaFin.TabIndex = 70;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(12, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 69;
            this.label6.Text = "Fecha Inicio";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(16, 148);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(300, 24);
            this.dtpFechaInicio.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(16, 190);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Descuento";
            // 
            // textDescuento
            // 
            this.textDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textDescuento.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textDescuento.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textDescuento.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textDescuento.BorderThickness = 1;
            this.textDescuento.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textDescuento.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textDescuento.isPassword = false;
            this.textDescuento.Location = new System.Drawing.Point(15, 210);
            this.textDescuento.Margin = new System.Windows.Forms.Padding(4);
            this.textDescuento.Name = "textDescuento";
            this.textDescuento.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textDescuento.Size = new System.Drawing.Size(300, 35);
            this.textDescuento.TabIndex = 63;
            this.textDescuento.Text = "0";
            this.textDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lblActive.Location = new System.Drawing.Point(361, 220);
            this.lblActive.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(43, 16);
            this.lblActive.TabIndex = 60;
            this.lblActive.Text = "Activo";
            // 
            // chkEstado
            // 
            this.chkEstado.BackColor = System.Drawing.Color.DodgerBlue;
            this.chkEstado.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.chkEstado.Checked = true;
            this.chkEstado.CheckedOnColor = System.Drawing.Color.DodgerBlue;
            this.chkEstado.ForeColor = System.Drawing.Color.White;
            this.chkEstado.Location = new System.Drawing.Point(336, 218);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(4);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(20, 20);
            this.chkEstado.TabIndex = 59;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(17, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 57;
            this.label7.Text = "Código";
            // 
            // textCodigoOferta
            // 
            this.textCodigoOferta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.textCodigoOferta.BorderColorFocused = System.Drawing.Color.DodgerBlue;
            this.textCodigoOferta.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.textCodigoOferta.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.textCodigoOferta.BorderThickness = 1;
            this.textCodigoOferta.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textCodigoOferta.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCodigoOferta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textCodigoOferta.isPassword = false;
            this.textCodigoOferta.Location = new System.Drawing.Point(16, 80);
            this.textCodigoOferta.Margin = new System.Windows.Forms.Padding(4);
            this.textCodigoOferta.Name = "textCodigoOferta";
            this.textCodigoOferta.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.textCodigoOferta.Size = new System.Drawing.Size(621, 35);
            this.textCodigoOferta.TabIndex = 58;
            this.textCodigoOferta.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // btnAddSucursal
            // 
            this.btnAddSucursal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddSucursal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddSucursal.FlatAppearance.BorderSize = 0;
            this.btnAddSucursal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddSucursal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSucursal.ForeColor = System.Drawing.Color.White;
            this.btnAddSucursal.Location = new System.Drawing.Point(275, 343);
            this.btnAddSucursal.Name = "btnAddSucursal";
            this.btnAddSucursal.Size = new System.Drawing.Size(40, 26);
            this.btnAddSucursal.TabIndex = 79;
            this.btnAddSucursal.Text = "+";
            this.btnAddSucursal.UseVisualStyleBackColor = false;
            this.btnAddSucursal.Click += new System.EventHandler(this.btnAddSucursal_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label11.Location = new System.Drawing.Point(12, 325);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 16);
            this.label11.TabIndex = 78;
            this.label11.Text = "Sucursal:";
            // 
            // cbxSucursal
            // 
            this.cbxSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSucursal.DataSource = this.sucursalBindingSource;
            this.cbxSucursal.DisplayMember = "nombre";
            this.cbxSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSucursal.FormattingEnabled = true;
            this.cbxSucursal.Location = new System.Drawing.Point(15, 343);
            this.cbxSucursal.Margin = new System.Windows.Forms.Padding(2);
            this.cbxSucursal.Name = "cbxSucursal";
            this.cbxSucursal.Size = new System.Drawing.Size(260, 26);
            this.cbxSucursal.TabIndex = 77;
            this.cbxSucursal.ValueMember = "idSucursal";
            // 
            // sucursalBindingSource
            // 
            this.sucursalBindingSource.DataSource = typeof(Entidad.Sucursal);
            // 
            // btnAddGrupoCliente
            // 
            this.btnAddGrupoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddGrupoCliente.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddGrupoCliente.FlatAppearance.BorderSize = 0;
            this.btnAddGrupoCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddGrupoCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(201)))), ((int)(((byte)(59)))));
            this.btnAddGrupoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddGrupoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddGrupoCliente.ForeColor = System.Drawing.Color.White;
            this.btnAddGrupoCliente.Location = new System.Drawing.Point(596, 282);
            this.btnAddGrupoCliente.Name = "btnAddGrupoCliente";
            this.btnAddGrupoCliente.Size = new System.Drawing.Size(40, 26);
            this.btnAddGrupoCliente.TabIndex = 76;
            this.btnAddGrupoCliente.Text = "+";
            this.btnAddGrupoCliente.UseVisualStyleBackColor = false;
            this.btnAddGrupoCliente.Click += new System.EventHandler(this.btnAddGrupoCliente_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(333, 264);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 16);
            this.label10.TabIndex = 75;
            this.label10.Text = "Grupo de Clientes";
            // 
            // cbxGrupoCliente
            // 
            this.cbxGrupoCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxGrupoCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxGrupoCliente.DataSource = this.grupoClienteBindingSource;
            this.cbxGrupoCliente.DisplayMember = "nombreGrupo";
            this.cbxGrupoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupoCliente.FormattingEnabled = true;
            this.cbxGrupoCliente.Location = new System.Drawing.Point(336, 282);
            this.cbxGrupoCliente.Margin = new System.Windows.Forms.Padding(2);
            this.cbxGrupoCliente.Name = "cbxGrupoCliente";
            this.cbxGrupoCliente.Size = new System.Drawing.Size(260, 26);
            this.cbxGrupoCliente.TabIndex = 74;
            this.cbxGrupoCliente.ValueMember = "idGrupoCliente";
            // 
            // grupoClienteBindingSource
            // 
            this.grupoClienteBindingSource.DataSource = typeof(Entidad.GrupoCliente);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label9.Location = new System.Drawing.Point(12, 264);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 16);
            this.label9.TabIndex = 73;
            this.label9.Text = "Producto Afecto";
            // 
            // cbxProductoAfecto
            // 
            this.cbxProductoAfecto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxProductoAfecto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProductoAfecto.DataSource = this.presentacionBindingSource;
            this.cbxProductoAfecto.DisplayMember = "nombrePresentacion";
            this.cbxProductoAfecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxProductoAfecto.FormattingEnabled = true;
            this.cbxProductoAfecto.Location = new System.Drawing.Point(15, 282);
            this.cbxProductoAfecto.Margin = new System.Windows.Forms.Padding(2);
            this.cbxProductoAfecto.Name = "cbxProductoAfecto";
            this.cbxProductoAfecto.Size = new System.Drawing.Size(299, 26);
            this.cbxProductoAfecto.TabIndex = 72;
            this.cbxProductoAfecto.ValueMember = "idPresentacion";
            // 
            // productoBindingSource
            // 
            this.productoBindingSource.DataSource = typeof(Entidad.Producto);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // presentacionBindingSource
            // 
            this.presentacionBindingSource.DataSource = typeof(Entidad.Presentacion);
            // 
            // FormOfertaNuevo
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(658, 451);
            this.Controls.Add(this.btnAddSucursal);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxSucursal);
            this.Controls.Add(this.btnAddGrupoCliente);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxGrupoCliente);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxProductoAfecto);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textDescuento);
            this.Controls.Add(this.lblActive);
            this.Controls.Add(this.chkEstado);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textCodigoOferta);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOfertaNuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormOfertaNuevo";
            this.Load += new System.EventHandler(this.FormOfertaNuevo_Load);
            this.panelFooter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grupoClienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.presentacionBindingSource)).EndInit();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuMetroTextbox textDescuento;
        private System.Windows.Forms.Label lblActive;
        private Bunifu.Framework.UI.BunifuCheckbox chkEstado;
        private System.Windows.Forms.Label label7;
        private Bunifu.Framework.UI.BunifuMetroTextbox textCodigoOferta;
        private System.Windows.Forms.Button btnAddSucursal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxSucursal;
        private System.Windows.Forms.Button btnAddGrupoCliente;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxGrupoCliente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxProductoAfecto;
        private System.Windows.Forms.BindingSource sucursalBindingSource;
        private System.Windows.Forms.BindingSource grupoClienteBindingSource;
        private System.Windows.Forms.BindingSource productoBindingSource;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource presentacionBindingSource;
    }
}