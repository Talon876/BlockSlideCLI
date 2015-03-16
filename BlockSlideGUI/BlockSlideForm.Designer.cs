namespace BlockSlideGUI
{
    partial class BlockSlideForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockSlideForm));
            this.levelPanel = new BlockSlideGUI.LevelPanel();
            this.SuspendLayout();
            // 
            // levelPanel
            // 
            this.levelPanel.BackColor = System.Drawing.Color.White;
            this.levelPanel.Location = new System.Drawing.Point(0, 0);
            this.levelPanel.Name = "levelPanel";
            this.levelPanel.Size = new System.Drawing.Size(576, 320);
            this.levelPanel.TabIndex = 0;
            this.levelPanel.TileSize = 32;
            // 
            // BlockSlideForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(576, 320);
            this.Controls.Add(this.levelPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BlockSlideForm";
            this.Text = "BlockSlide - Level 1";
            this.ResumeLayout(false);

        }

        #endregion

        private LevelPanel levelPanel;
    }
}

