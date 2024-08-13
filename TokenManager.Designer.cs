namespace Svalidator
{
    partial class TokenManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenManager));
            label1 = new Label();
            txb_token = new TextBox();
            txb_newtoken = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 82);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "Token";
            // 
            // txb_token
            // 
            txb_token.Enabled = false;
            txb_token.Location = new Point(98, 79);
            txb_token.Name = "txb_token";
            txb_token.Size = new Size(255, 23);
            txb_token.TabIndex = 1;
            // 
            // txb_newtoken
            // 
            txb_newtoken.Location = new Point(98, 133);
            txb_newtoken.Name = "txb_newtoken";
            txb_newtoken.Size = new Size(255, 23);
            txb_newtoken.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(183, 162);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Actualizar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // TokenManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 263);
            Controls.Add(button1);
            Controls.Add(txb_newtoken);
            Controls.Add(txb_token);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TokenManager";
            Text = "Token Manager";
            Load += TokenManager_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txb_token;
        private TextBox txb_newtoken;
        private Button button1;
    }
}