namespace Netzwerklabrinth_V_WPF
{
    partial class ManuelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManuelForm));
            this.panel = new System.Windows.Forms.Panel();
            this.pictureBoxFull = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxCutout = new System.Windows.Forms.PictureBox();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFull)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCutout)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.AutoScroll = true;
            this.panel.Controls.Add(this.pictureBoxFull);
            this.panel.Location = new System.Drawing.Point(13, 1);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(768, 531);
            this.panel.TabIndex = 0;
            // 
            // pictureBoxFull
            // 
            this.pictureBoxFull.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxFull.Image")));
            this.pictureBoxFull.Location = new System.Drawing.Point(4, 2);
            this.pictureBoxFull.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBoxFull.Name = "pictureBoxFull";
            this.pictureBoxFull.Size = new System.Drawing.Size(775, 345);
            this.pictureBoxFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFull.TabIndex = 0;
            this.pictureBoxFull.TabStop = false;
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBoxCutout
            // 
            this.pictureBoxCutout.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxCutout.Image")));
            this.pictureBoxCutout.Location = new System.Drawing.Point(3, 1);
            this.pictureBoxCutout.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxCutout.Name = "pictureBoxCutout";
            this.pictureBoxCutout.Size = new System.Drawing.Size(1043, 934);
            this.pictureBoxCutout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCutout.TabIndex = 1;
            this.pictureBoxCutout.TabStop = false;
            // 
            // ManuelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 937);
            this.Controls.Add(this.pictureBoxCutout);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MinimumSize = new System.Drawing.Size(320, 320);
            this.Name = "ManuelForm";
            this.Text = "Netzwerklabyrinth";
            this.Load += new System.EventHandler(this.ManuelForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManuelForm_KeyDown);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFull)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCutout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox pictureBoxFull;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBoxCutout;
    }
}