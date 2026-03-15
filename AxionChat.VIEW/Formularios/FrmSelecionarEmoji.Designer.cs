namespace AxionChat.VIEW.Formularios
{
    partial class FrmSelecionarEmoji
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
      this.flpEmojis = new System.Windows.Forms.FlowLayoutPanel();
      this.SuspendLayout();
      // 
      // flpEmojis
      // 
      this.flpEmojis.AutoScroll = true;
      this.flpEmojis.BackColor = System.Drawing.Color.White;
      this.flpEmojis.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpEmojis.Location = new System.Drawing.Point(2, 30);
      this.flpEmojis.Name = "flpEmojis";
      this.flpEmojis.Size = new System.Drawing.Size(416, 379);
      this.flpEmojis.TabIndex = 0;
      // 
      // FrmSelecionarEmoji
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(420, 411);
      this.Controls.Add(this.flpEmojis);
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "FrmSelecionarEmoji";
      this.Padding = new System.Windows.Forms.Padding(2, 30, 2, 2);
      this.Text = "Selecionar Emoji";
      this.ResumeLayout(false);

        }

        private System.Windows.Forms.FlowLayoutPanel flpEmojis;

        #endregion
    }
}
