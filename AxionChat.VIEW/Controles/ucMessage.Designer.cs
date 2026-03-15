namespace AxionChat.VIEW.Controles
{
    partial class ucMessage
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMessage));
      this.lblMensagem = new System.Windows.Forms.Label();
      this.lblDataHora = new System.Windows.Forms.Label();
      this.lblNomeUsuario = new System.Windows.Forms.Label();
      this.pnlBotoes = new System.Windows.Forms.Panel();
      this.btnCopiar = new System.Windows.Forms.Button();
      this.btnEditar = new System.Windows.Forms.Button();
      this.btnExcluir = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.lblRead = new System.Windows.Forms.Label();
      this.pnlBotoes.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblMensagem
      // 
      this.lblMensagem.AutoSize = true;
      this.lblMensagem.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblMensagem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMensagem.Location = new System.Drawing.Point(5, 18);
      this.lblMensagem.MaximumSize = new System.Drawing.Size(280, 0);
      this.lblMensagem.Name = "lblMensagem";
      this.lblMensagem.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this.lblMensagem.Size = new System.Drawing.Size(73, 27);
      this.lblMensagem.TabIndex = 0;
      this.lblMensagem.Text = "Mensagem";
      // 
      // lblDataHora
      // 
      this.lblDataHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblDataHora.AutoSize = true;
      this.lblDataHora.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDataHora.ForeColor = System.Drawing.Color.Gray;
      this.lblDataHora.Location = new System.Drawing.Point(203, 53);
      this.lblDataHora.Name = "lblDataHora";
      this.lblDataHora.Size = new System.Drawing.Size(92, 13);
      this.lblDataHora.TabIndex = 1;
      this.lblDataHora.Text = "15/03/2025 12:00";
      // 
      // lblNomeUsuario
      // 
      this.lblNomeUsuario.AutoSize = true;
      this.lblNomeUsuario.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblNomeUsuario.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblNomeUsuario.Location = new System.Drawing.Point(5, 5);
      this.lblNomeUsuario.Name = "lblNomeUsuario";
      this.lblNomeUsuario.Size = new System.Drawing.Size(99, 13);
      this.lblNomeUsuario.TabIndex = 2;
      this.lblNomeUsuario.Text = "Nome do Usuário";
      // 
      // pnlBotoes
      // 
      this.pnlBotoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlBotoes.Controls.Add(this.btnCopiar);
      this.pnlBotoes.Controls.Add(this.btnEditar);
      this.pnlBotoes.Controls.Add(this.btnExcluir);
      this.pnlBotoes.Location = new System.Drawing.Point(6, 47);
      this.pnlBotoes.Name = "pnlBotoes";
      this.pnlBotoes.Size = new System.Drawing.Size(193, 23);
      this.pnlBotoes.TabIndex = 3;
      this.pnlBotoes.Visible = false;
      // 
      // btnCopiar
      // 
      this.btnCopiar.FlatAppearance.BorderSize = 0;
      this.btnCopiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCopiar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnCopiar.Image = ((System.Drawing.Image)(resources.GetObject("btnCopiar.Image")));
      this.btnCopiar.Location = new System.Drawing.Point(1, 3);
      this.btnCopiar.Name = "btnCopiar";
      this.btnCopiar.Size = new System.Drawing.Size(16, 16);
      this.btnCopiar.TabIndex = 0;
      this.toolTip1.SetToolTip(this.btnCopiar, "Copiar");
      this.btnCopiar.UseVisualStyleBackColor = true;
      this.btnCopiar.Click += new System.EventHandler(this.BtnCopiar_Click);
      // 
      // btnEditar
      // 
      this.btnEditar.FlatAppearance.BorderSize = 0;
      this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
      this.btnEditar.Location = new System.Drawing.Point(26, 3);
      this.btnEditar.Name = "btnEditar";
      this.btnEditar.Size = new System.Drawing.Size(16, 16);
      this.btnEditar.TabIndex = 2;
      this.toolTip1.SetToolTip(this.btnEditar, "Editar");
      this.btnEditar.UseVisualStyleBackColor = true;
      this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
      // 
      // btnExcluir
      // 
      this.btnExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExcluir.FlatAppearance.BorderSize = 0;
      this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnExcluir.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnExcluir.ForeColor = System.Drawing.Color.Red;
      this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
      this.btnExcluir.Location = new System.Drawing.Point(167, 3);
      this.btnExcluir.Name = "btnExcluir";
      this.btnExcluir.Size = new System.Drawing.Size(16, 16);
      this.btnExcluir.TabIndex = 1;
      this.toolTip1.SetToolTip(this.btnExcluir, "Excluir");
      this.btnExcluir.UseVisualStyleBackColor = true;
      this.btnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
      // 
      // lblRead
      // 
      this.lblRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblRead.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRead.ForeColor = System.Drawing.Color.Gray;
      this.lblRead.Image = ((System.Drawing.Image)(resources.GetObject("lblRead.Image")));
      this.lblRead.Location = new System.Drawing.Point(280, 4);
      this.lblRead.Name = "lblRead";
      this.lblRead.Size = new System.Drawing.Size(15, 13);
      this.lblRead.TabIndex = 4;
      // 
      // ucMessage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.lblRead);
      this.Controls.Add(this.lblDataHora);
      this.Controls.Add(this.pnlBotoes);
      this.Controls.Add(this.lblMensagem);
      this.Controls.Add(this.lblNomeUsuario);
      this.Name = "ucMessage";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.Size = new System.Drawing.Size(300, 79);
      this.MouseEnter += new System.EventHandler(this.UcMessage_MouseEnter);
      this.MouseLeave += new System.EventHandler(this.UcMessage_MouseLeave);
      this.pnlBotoes.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        private System.Windows.Forms.Label lblMensagem;
        private System.Windows.Forms.Label lblDataHora;
        private System.Windows.Forms.Label lblNomeUsuario;
        private System.Windows.Forms.Panel pnlBotoes;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEditar;

    #endregion

    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label lblRead;
  }
}
