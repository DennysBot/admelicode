namespace Admeli.CajaBox
{
    partial class UCEstadoCajas
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEstadoCajas));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvEstadosCuenta = new System.Windows.Forms.DataGridView();
            this.EstadoString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpeedPages = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPageCount = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.lblCurrentPage = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageAllItems = new System.Windows.Forms.Label();
            this.panelTools = new System.Windows.Forms.Panel();
            this.panelMoneda = new System.Windows.Forms.Panel();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cbxSucursales = new System.Windows.Forms.ComboBox();
            this.lbl20 = new System.Windows.Forms.Label();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sucursalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadosCuenta)).BeginInit();
            this.panelNavigation.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelTools.SuspendLayout();
            this.panelMoneda.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Controls.Add(this.panel1);
            this.panelContainer.Controls.Add(this.panelNavigation);
            this.panelContainer.Controls.Add(this.panelTools);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(2);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Padding = new System.Windows.Forms.Padding(1);
            this.panelContainer.Size = new System.Drawing.Size(741, 401);
            this.panelContainer.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvEstadosCuenta);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 53);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(739, 287);
            this.panel1.TabIndex = 16;
            // 
            // dgvEstadosCuenta
            // 
            this.dgvEstadosCuenta.AllowUserToAddRows = false;
            this.dgvEstadosCuenta.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvEstadosCuenta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvEstadosCuenta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEstadosCuenta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEstadosCuenta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(115)))), ((int)(((byte)(220)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEstadosCuenta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvEstadosCuenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EstadoString});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEstadosCuenta.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvEstadosCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEstadosCuenta.GridColor = System.Drawing.Color.Gainsboro;
            this.dgvEstadosCuenta.Location = new System.Drawing.Point(10, 10);
            this.dgvEstadosCuenta.Name = "dgvEstadosCuenta";
            this.dgvEstadosCuenta.ReadOnly = true;
            this.dgvEstadosCuenta.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEstadosCuenta.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvEstadosCuenta.RowHeadersWidth = 40;
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.dgvEstadosCuenta.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvEstadosCuenta.RowTemplate.Height = 30;
            this.dgvEstadosCuenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEstadosCuenta.Size = new System.Drawing.Size(719, 267);
            this.dgvEstadosCuenta.TabIndex = 2;
            // 
            // EstadoString
            // 
            this.EstadoString.DataPropertyName = "EstadoString";
            this.EstadoString.HeaderText = "Estado";
            this.EstadoString.Name = "EstadoString";
            this.EstadoString.ReadOnly = true;
            this.EstadoString.Visible = false;
            // 
            // panelNavigation
            // 
            this.panelNavigation.Controls.Add(this.label2);
            this.panelNavigation.Controls.Add(this.lblSpeedPages);
            this.panelNavigation.Controls.Add(this.panel5);
            this.panelNavigation.Controls.Add(this.lblPageAllItems);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelNavigation.Location = new System.Drawing.Point(1, 340);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(739, 60);
            this.panelNavigation.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.label2.Location = new System.Drawing.Point(625, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 35);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mostrar cada";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSpeedPages
            // 
            this.lblSpeedPages.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSpeedPages.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblSpeedPages.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(211)))), ((int)(((byte)(215)))));
            this.lblSpeedPages.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(158)))), ((int)(((byte)(166)))));
            this.lblSpeedPages.BorderThickness = 1;
            this.lblSpeedPages.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblSpeedPages.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeedPages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblSpeedPages.isPassword = false;
            this.lblSpeedPages.Location = new System.Drawing.Point(684, 13);
            this.lblSpeedPages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblSpeedPages.Name = "lblSpeedPages";
            this.lblSpeedPages.Size = new System.Drawing.Size(45, 35);
            this.lblSpeedPages.TabIndex = 10;
            this.lblSpeedPages.Text = "10";
            this.lblSpeedPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.lblPageCount);
            this.panel5.Controls.Add(this.lblCurrentPage);
            this.panel5.Controls.Add(this.btnFirst);
            this.panel5.Controls.Add(this.btnLast);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.btnPrevious);
            this.panel5.Controls.Add(this.btnNext);
            this.panel5.Location = new System.Drawing.Point(197, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(344, 55);
            this.panel5.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.label5.Location = new System.Drawing.Point(146, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "Página";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPageCount
            // 
            this.lblPageCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageCount.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblPageCount.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(211)))), ((int)(((byte)(215)))));
            this.lblPageCount.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(158)))), ((int)(((byte)(166)))));
            this.lblPageCount.BorderThickness = 1;
            this.lblPageCount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblPageCount.Enabled = false;
            this.lblPageCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblPageCount.isPassword = false;
            this.lblPageCount.Location = new System.Drawing.Point(186, 15);
            this.lblPageCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(45, 35);
            this.lblPageCount.TabIndex = 1;
            this.lblPageCount.Text = "1";
            this.lblPageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentPage.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.lblCurrentPage.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(211)))), ((int)(((byte)(215)))));
            this.lblCurrentPage.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(158)))), ((int)(((byte)(166)))));
            this.lblCurrentPage.BorderThickness = 1;
            this.lblCurrentPage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblCurrentPage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblCurrentPage.isPassword = false;
            this.lblCurrentPage.Location = new System.Drawing.Point(114, 15);
            this.lblCurrentPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(45, 35);
            this.lblCurrentPage.TabIndex = 0;
            this.lblCurrentPage.Text = "1";
            this.lblCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFirst.Location = new System.Drawing.Point(8, 15);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(45, 35);
            this.btnFirst.TabIndex = 11;
            this.btnFirst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFirst.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLast.BackColor = System.Drawing.Color.White;
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLast.Location = new System.Drawing.Point(292, 15);
            this.btnLast.Margin = new System.Windows.Forms.Padding(0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 35);
            this.btnLast.TabIndex = 10;
            this.btnLast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLast.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(162, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "DE";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrevious.Location = new System.Drawing.Point(61, 15);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(45, 35);
            this.btnPrevious.TabIndex = 7;
            this.btnPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnNext.BackColor = System.Drawing.Color.White;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNext.Location = new System.Drawing.Point(239, 15);
            this.btnNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 35);
            this.btnNext.TabIndex = 6;
            this.btnNext.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // lblPageAllItems
            // 
            this.lblPageAllItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageAllItems.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageAllItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(110)))), ((int)(((byte)(122)))));
            this.lblPageAllItems.Location = new System.Drawing.Point(7, 13);
            this.lblPageAllItems.Name = "lblPageAllItems";
            this.lblPageAllItems.Size = new System.Drawing.Size(70, 35);
            this.lblPageAllItems.TabIndex = 1;
            this.lblPageAllItems.Text = "10 Registros";
            this.lblPageAllItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelTools
            // 
            this.panelTools.Controls.Add(this.panelMoneda);
            this.panelTools.Controls.Add(this.btnConsultar);
            this.panelTools.Controls.Add(this.panel10);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(1, 1);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(739, 52);
            this.panelTools.TabIndex = 27;
            // 
            // panelMoneda
            // 
            this.panelMoneda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMoneda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMoneda.Controls.Add(this.cbxMoneda);
            this.panelMoneda.Controls.Add(this.label4);
            this.panelMoneda.ForeColor = System.Drawing.Color.Black;
            this.panelMoneda.Location = new System.Drawing.Point(190, 7);
            this.panelMoneda.Name = "panelMoneda";
            this.panelMoneda.Size = new System.Drawing.Size(166, 40);
            this.panelMoneda.TabIndex = 18;
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxMoneda.DataSource = this.monedaBindingSource;
            this.cbxMoneda.DisplayMember = "moneda";
            this.cbxMoneda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(3, 12);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(156, 24);
            this.cbxMoneda.TabIndex = 1;
            this.cbxMoneda.ValueMember = "idMoneda";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(2, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Moneda";
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(225)))), ((int)(((byte)(228)))));
            this.btnConsultar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.btnConsultar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnConsultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnConsultar.ForeColor = System.Drawing.Color.White;
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(372, 7);
            this.btnConsultar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnConsultar.Size = new System.Drawing.Size(108, 40);
            this.btnConsultar.TabIndex = 17;
            this.btnConsultar.Text = "  Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.cbxSucursales);
            this.panel10.Controls.Add(this.lbl20);
            this.panel10.Location = new System.Drawing.Point(10, 7);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(165, 40);
            this.panel10.TabIndex = 13;
            // 
            // cbxSucursales
            // 
            this.cbxSucursales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxSucursales.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxSucursales.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxSucursales.DataSource = this.sucursalBindingSource;
            this.cbxSucursales.DisplayMember = "nombre";
            this.cbxSucursales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxSucursales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSucursales.FormattingEnabled = true;
            this.cbxSucursales.Location = new System.Drawing.Point(3, 14);
            this.cbxSucursales.Name = "cbxSucursales";
            this.cbxSucursales.Size = new System.Drawing.Size(158, 24);
            this.cbxSucursales.TabIndex = 1;
            this.cbxSucursales.ValueMember = "idSucursal";
            // 
            // lbl20
            // 
            this.lbl20.AutoSize = true;
            this.lbl20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl20.ForeColor = System.Drawing.Color.DimGray;
            this.lbl20.Location = new System.Drawing.Point(3, 3);
            this.lbl20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl20.Name = "lbl20";
            this.lbl20.Size = new System.Drawing.Size(62, 14);
            this.lbl20.TabIndex = 0;
            this.lbl20.Text = "Sucursales";
            // 
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(Entidad.Configuracion.Moneda);
            // 
            // sucursalBindingSource
            // 
            this.sucursalBindingSource.DataSource = typeof(Entidad.Sucursal);
            // 
            // UCEstadoCajas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Name = "UCEstadoCajas";
            this.Size = new System.Drawing.Size(741, 401);
            this.panelContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstadosCuenta)).EndInit();
            this.panelNavigation.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelTools.ResumeLayout(false);
            this.panelMoneda.ResumeLayout(false);
            this.panelMoneda.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sucursalBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvEstadosCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoString;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuMetroTextbox lblSpeedPages;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private Bunifu.Framework.UI.BunifuMetroTextbox lblPageCount;
        private Bunifu.Framework.UI.BunifuMetroTextbox lblCurrentPage;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblPageAllItems;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ComboBox cbxSucursales;
        private System.Windows.Forms.Label lbl20;
        private System.Windows.Forms.Panel panelMoneda;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.BindingSource sucursalBindingSource;
    }
}
