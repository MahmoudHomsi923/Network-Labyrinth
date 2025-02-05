namespace Netzwerklabrinth_V_WPF
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gameStartbutton = new System.Windows.Forms.Button();
            this.ManuelradioButton = new System.Windows.Forms.RadioButton();
            this.AutomaticradioButton = new System.Windows.Forms.RadioButton();
            this.lblWidth = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudDepth = new System.Windows.Forms.NumericUpDown();
            this.lblDepth = new System.Windows.Forms.Label();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblLabyrinth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // gameStartbutton
            // 
            this.gameStartbutton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gameStartbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gameStartbutton.Location = new System.Drawing.Point(346, 405);
            this.gameStartbutton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gameStartbutton.Name = "gameStartbutton";
            this.gameStartbutton.Size = new System.Drawing.Size(210, 50);
            this.gameStartbutton.TabIndex = 0;
            this.gameStartbutton.Text = "Game Start";
            this.gameStartbutton.UseVisualStyleBackColor = false;
            this.gameStartbutton.Click += new System.EventHandler(this.gameStartbutton_Click);
            // 
            // ManuelradioButton
            // 
            this.ManuelradioButton.Checked = true;
            this.ManuelradioButton.Location = new System.Drawing.Point(346, 344);
            this.ManuelradioButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ManuelradioButton.Name = "ManuelradioButton";
            this.ManuelradioButton.Size = new System.Drawing.Size(115, 25);
            this.ManuelradioButton.TabIndex = 1;
            this.ManuelradioButton.TabStop = true;
            this.ManuelradioButton.Text = "Manuel";
            this.ManuelradioButton.UseVisualStyleBackColor = true;
            // 
            // AutomaticradioButton
            // 
            this.AutomaticradioButton.AutoSize = true;
            this.AutomaticradioButton.Location = new System.Drawing.Point(469, 346);
            this.AutomaticradioButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.AutomaticradioButton.Name = "AutomaticradioButton";
            this.AutomaticradioButton.Size = new System.Drawing.Size(87, 20);
            this.AutomaticradioButton.TabIndex = 2;
            this.AutomaticradioButton.TabStop = true;
            this.AutomaticradioButton.Text = "Automatic";
            this.AutomaticradioButton.UseVisualStyleBackColor = true;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(59, 301);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(111, 16);
            this.lblWidth.TabIndex = 3;
            this.lblWidth.Text = "Width (32 - 65536)";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(188, 299);
            this.nudWidth.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(100, 22);
            this.nudWidth.TabIndex = 4;
            this.nudWidth.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudDepth
            // 
            this.nudDepth.Location = new System.Drawing.Point(718, 299);
            this.nudDepth.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDepth.Name = "nudDepth";
            this.nudDepth.Size = new System.Drawing.Size(125, 22);
            this.nudDepth.TabIndex = 6;
            this.nudDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Location = new System.Drawing.Point(617, 302);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(85, 16);
            this.lblDepth.TabIndex = 5;
            this.lblDepth.Text = "Depth (1 - 16)";
            this.lblDepth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(463, 299);
            this.nudHeight.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(100, 22);
            this.nudHeight.TabIndex = 8;
            this.nudHeight.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(332, 301);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(116, 16);
            this.lblHeight.TabIndex = 7;
            this.lblHeight.Text = "Height (32 - 65536)";
            this.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLabyrinth
            // 
            this.lblLabyrinth.AutoSize = true;
            this.lblLabyrinth.Location = new System.Drawing.Point(12, 24);
            this.lblLabyrinth.Name = "lblLabyrinth";
            this.lblLabyrinth.Size = new System.Drawing.Size(845, 176);
            this.lblLabyrinth.TabIndex = 10;
            this.lblLabyrinth.Text = resources.GetString("lblLabyrinth.Text");
            this.lblLabyrinth.Click += new System.EventHandler(this.lblLabyrinth_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 487);
            this.Controls.Add(this.lblLabyrinth);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.nudDepth);
            this.Controls.Add(this.lblDepth);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.AutomaticradioButton);
            this.Controls.Add(this.ManuelradioButton);
            this.Controls.Add(this.gameStartbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Netzwerklabyrinth";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gameStartbutton;
        private System.Windows.Forms.RadioButton ManuelradioButton;
        private System.Windows.Forms.RadioButton AutomaticradioButton;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudDepth;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblLabyrinth;
    }
}

