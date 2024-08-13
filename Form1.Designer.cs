namespace Svalidator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            btn_cargarexcel = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            lbl_message = new Label();
            menuStrip1 = new MenuStrip();
            tokenToolStripMenuItem = new ToolStripMenuItem();
            configurarTokenToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(12, 56);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(487, 355);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btn_cargarexcel
            // 
            btn_cargarexcel.Location = new Point(39, 26);
            btn_cargarexcel.Name = "btn_cargarexcel";
            btn_cargarexcel.Size = new Size(123, 26);
            btn_cargarexcel.TabIndex = 1;
            btn_cargarexcel.Text = "Seleccionar archivo..";
            btn_cargarexcel.UseVisualStyleBackColor = true;
            btn_cargarexcel.Click += btn_cargarexcel_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Chartreuse;
            button2.Location = new Point(216, 26);
            button2.Name = "button2";
            button2.Size = new Size(80, 26);
            button2.TabIndex = 2;
            button2.Text = "✅ Validar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(128, 255, 128);
            button3.Location = new Point(391, 26);
            button3.Name = "button3";
            button3.Size = new Size(108, 26);
            button3.TabIndex = 3;
            button3.Text = "Descargar xlsx";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources._8243067;
            pictureBox1.Location = new Point(12, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(28, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources._8243067;
            pictureBox2.Location = new Point(364, 26);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(28, 26);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // lbl_message
            // 
            lbl_message.AutoSize = true;
            lbl_message.Location = new Point(12, 416);
            lbl_message.Name = "lbl_message";
            lbl_message.Size = new Size(38, 15);
            lbl_message.TabIndex = 7;
            lbl_message.Text = "label1";
            lbl_message.Visible = false;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { tokenToolStripMenuItem });
            menuStrip1.Location = new Point(430, -1);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(81, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // tokenToolStripMenuItem
            // 
            tokenToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { configurarTokenToolStripMenuItem });
            tokenToolStripMenuItem.Image = Properties.Resources._81020;
            tokenToolStripMenuItem.Name = "tokenToolStripMenuItem";
            tokenToolStripMenuItem.Size = new Size(73, 20);
            tokenToolStripMenuItem.Text = "Config.";
            tokenToolStripMenuItem.Click += tokenToolStripMenuItem_Click;
            // 
            // configurarTokenToolStripMenuItem
            // 
            configurarTokenToolStripMenuItem.Name = "configurarTokenToolStripMenuItem";
            configurarTokenToolStripMenuItem.Size = new Size(164, 22);
            configurarTokenToolStripMenuItem.Text = "Configurar Token";
            configurarTokenToolStripMenuItem.Click += configurarTokenToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(513, 435);
            Controls.Add(lbl_message);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(btn_cargarexcel);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Svalidator";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DataGridView dataGridView1;
        private Button btn_cargarexcel;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label lbl_message;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem tokenToolStripMenuItem;
        private ToolStripMenuItem configurarTokenToolStripMenuItem;
    }
}
