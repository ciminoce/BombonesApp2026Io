namespace BombonesApp2026.Windows
{
    partial class frmCajaAe
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
            components = new System.ComponentModel.Container();
            btnCancelar = new Button();
            btnOK = new Button();
            txtNombreCaja = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtDescripcion = new TextBox();
            label4 = new Label();
            txtPrecio = new TextBox();
            label5 = new Label();
            nudStock = new NumericUpDown();
            chkActivo = new CheckBox();
            errorProvider1 = new ErrorProvider(components);
            panel1 = new Panel();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colBombon = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            btnEditarBombon = new Button();
            btnEliminarBombon = new Button();
            btnAgregarBombon = new Button();
            label1 = new Label();
            txtCantidadBombones = new TextBox();
            label7 = new Label();
            txtSurtida = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Image = Properties.Resources.cancel_24px;
            btnCancelar.Location = new Point(650, 529);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 60);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageAboveText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnOK
            // 
            btnOK.Image = Properties.Resources.ok_24px;
            btnOK.Location = new Point(28, 529);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 60);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.TextImageRelation = TextImageRelation.ImageAboveText;
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtNombreCaja
            // 
            txtNombreCaja.Location = new Point(140, 28);
            txtNombreCaja.MaxLength = 100;
            txtNombreCaja.Name = "txtNombreCaja";
            txtNombreCaja.Size = new Size(334, 23);
            txtNombreCaja.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 31);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 6;
            label2.Text = "Nombre Caja:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 60);
            label3.Name = "label3";
            label3.Size = new Size(72, 15);
            label3.TabIndex = 6;
            label3.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(140, 57);
            txtDescripcion.MaxLength = 300;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(334, 69);
            txtDescripcion.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 429);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 6;
            label4.Text = "Precio:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(138, 424);
            txtPrecio.MaxLength = 100;
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(119, 23);
            txtPrecio.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 141);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 6;
            label5.Text = "Stock:";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(140, 137);
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(134, 23);
            nudStock.TabIndex = 12;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.CheckAlign = ContentAlignment.MiddleRight;
            chkActivo.Location = new Point(318, 487);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(60, 19);
            chkActivo.TabIndex = 13;
            chkActivo.Text = "Activo";
            chkActivo.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            panel1.Controls.Add(splitContainer1);
            panel1.Location = new Point(9, 166);
            panel1.Name = "panel1";
            panel1.Size = new Size(734, 240);
            panel1.TabIndex = 14;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(btnEditarBombon);
            splitContainer1.Panel2.Controls.Add(btnEliminarBombon);
            splitContainer1.Panel2.Controls.Add(btnAgregarBombon);
            splitContainer1.Size = new Size(734, 240);
            splitContainer1.SplitterDistance = 574;
            splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colBombon, colCantidad });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(574, 240);
            dataGridView1.TabIndex = 0;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colBombon
            // 
            colBombon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBombon.HeaderText = "Bombön";
            colBombon.Name = "colBombon";
            colBombon.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            colCantidad.Width = 80;
            // 
            // btnEditarBombon
            // 
            btnEditarBombon.Image = Properties.Resources.edit_property_24px;
            btnEditarBombon.Location = new Point(16, 147);
            btnEditarBombon.Name = "btnEditarBombon";
            btnEditarBombon.Size = new Size(122, 60);
            btnEditarBombon.TabIndex = 9;
            btnEditarBombon.Text = "Editar Bombón";
            btnEditarBombon.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEditarBombon.UseVisualStyleBackColor = true;
            // 
            // btnEliminarBombon
            // 
            btnEliminarBombon.Image = Properties.Resources.cancel_24px;
            btnEliminarBombon.Location = new Point(16, 81);
            btnEliminarBombon.Name = "btnEliminarBombon";
            btnEliminarBombon.Size = new Size(122, 60);
            btnEliminarBombon.TabIndex = 9;
            btnEliminarBombon.Text = "Eliminar Bombón";
            btnEliminarBombon.TextImageRelation = TextImageRelation.ImageAboveText;
            btnEliminarBombon.UseVisualStyleBackColor = true;
            // 
            // btnAgregarBombon
            // 
            btnAgregarBombon.Image = Properties.Resources.ok_24px;
            btnAgregarBombon.Location = new Point(16, 15);
            btnAgregarBombon.Name = "btnAgregarBombon";
            btnAgregarBombon.Size = new Size(122, 60);
            btnAgregarBombon.TabIndex = 9;
            btnAgregarBombon.Text = "Agregar Bombón";
            btnAgregarBombon.TextImageRelation = TextImageRelation.ImageAboveText;
            btnAgregarBombon.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 458);
            label1.Name = "label1";
            label1.Size = new Size(118, 15);
            label1.TabIndex = 6;
            label1.Text = "Cantidad Bombones:";
            // 
            // txtCantidadBombones
            // 
            txtCantidadBombones.Location = new Point(138, 453);
            txtCantidadBombones.MaxLength = 100;
            txtCantidadBombones.Name = "txtCantidadBombones";
            txtCantidadBombones.Size = new Size(119, 23);
            txtCantidadBombones.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 487);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 6;
            label7.Text = "Surtida:";
            // 
            // txtSurtida
            // 
            txtSurtida.Location = new Point(138, 482);
            txtSurtida.MaxLength = 100;
            txtSurtida.Name = "txtSurtida";
            txtSurtida.Size = new Size(119, 23);
            txtSurtida.TabIndex = 8;
            // 
            // frmCajaAe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(751, 609);
            Controls.Add(panel1);
            Controls.Add(chkActivo);
            Controls.Add(nudStock);
            Controls.Add(btnCancelar);
            Controls.Add(btnOK);
            Controls.Add(txtDescripcion);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(txtSurtida);
            Controls.Add(label7);
            Controls.Add(txtCantidadBombones);
            Controls.Add(label1);
            Controls.Add(txtPrecio);
            Controls.Add(label4);
            Controls.Add(txtNombreCaja);
            Controls.Add(label2);
            MaximumSize = new Size(767, 648);
            MinimumSize = new Size(767, 648);
            Name = "frmCajaAe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmCajaAe";
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnOK;
        private TextBox txtNombreCaja;
        private Label label2;
        private Label label3;
        private TextBox txtDescripcion;
        private Label label4;
        private TextBox txtPrecio;
        private Label label5;
        private NumericUpDown nudStock;
        private CheckBox chkActivo;
        private ErrorProvider errorProvider1;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private Button btnAgregarBombon;
        private Button btnEliminarBombon;
        private Button btnEditarBombon;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colBombon;
        private DataGridViewTextBoxColumn colCantidad;
        private TextBox txtSurtida;
        private Label label7;
        private TextBox txtCantidadBombones;
        private Label label1;
    }
}