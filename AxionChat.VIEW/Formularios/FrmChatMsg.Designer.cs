namespace AxionChat.VIEW {
  partial class FrmChatMsg {
    /// <summary>
    /// Variável de designer necessária.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Limpar os recursos que estão sendo usados.
    /// </summary>
    /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Código gerado pelo Windows Form Designer

    /// <summary>
    /// Método necessário para suporte ao Designer - não modifique 
    /// o conteúdo deste método com o editor de código.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChatMsg));
      this.flpChat = new System.Windows.Forms.FlowLayoutPanel();
      this.pnlRodapeMensagm = new LmCorbieUI.Controls.LmPanel();
      this.lblClose = new System.Windows.Forms.Label();
      this.btnSelectUser = new LmCorbieUI.Controls.LmButton();
      this.panel3 = new System.Windows.Forms.Panel();
      this.btnSendMessage = new LmCorbieUI.Controls.LmButton();
      this.txtMensagem = new LmCorbieUI.Controls.LmTextBox();
      this.lblConversaCom = new LmCorbieUI.Controls.LmLabel();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.tmrMessages = new System.Windows.Forms.Timer(this.components);
      this.tmr = new System.Windows.Forms.Timer(this.components);
      this.pnlRodapeMensagm.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // flpChat
      // 
      this.flpChat.AutoScroll = true;
      this.flpChat.BackColor = System.Drawing.Color.Magenta;
      this.flpChat.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
      this.flpChat.Location = new System.Drawing.Point(0, 0);
      this.flpChat.Name = "flpChat";
      this.flpChat.Size = new System.Drawing.Size(356, 554);
      this.flpChat.TabIndex = 1;
      this.flpChat.WrapContents = false;
      // 
      // pnlRodapeMensagm
      // 
      this.pnlRodapeMensagm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(228)))), ((int)(((byte)(233)))));
      this.pnlRodapeMensagm.Controls.Add(this.lblClose);
      this.pnlRodapeMensagm.Controls.Add(this.btnSelectUser);
      this.pnlRodapeMensagm.Controls.Add(this.panel3);
      this.pnlRodapeMensagm.Controls.Add(this.btnSendMessage);
      this.pnlRodapeMensagm.Controls.Add(this.txtMensagem);
      this.pnlRodapeMensagm.Controls.Add(this.lblConversaCom);
      this.pnlRodapeMensagm.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlRodapeMensagm.IsPanelMenu = false;
      this.pnlRodapeMensagm.Location = new System.Drawing.Point(0, 554);
      this.pnlRodapeMensagm.Margin = new System.Windows.Forms.Padding(0);
      this.pnlRodapeMensagm.Name = "pnlRodapeMensagm";
      this.pnlRodapeMensagm.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
      this.pnlRodapeMensagm.Size = new System.Drawing.Size(336, 76);
      this.pnlRodapeMensagm.TabIndex = 2;
      // 
      // lblClose
      // 
      this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.lblClose.AutoSize = true;
      this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblClose.ForeColor = System.Drawing.Color.Firebrick;
      this.lblClose.Location = new System.Drawing.Point(314, 3);
      this.lblClose.Name = "lblClose";
      this.lblClose.Size = new System.Drawing.Size(21, 20);
      this.lblClose.TabIndex = 42;
      this.lblClose.Text = "X";
      this.lblClose.Click += new System.EventHandler(this.LblClose_Click);
      this.lblClose.MouseEnter += new System.EventHandler(this.LblClose_MouseEnter);
      this.lblClose.MouseLeave += new System.EventHandler(this.LblClose_MouseLeave);
      // 
      // btnSelectUser
      // 
      this.btnSelectUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSelectUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btnSelectUser.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSelectUser.BorderRadius = 15;
      this.btnSelectUser.BorderSize = 0;
      this.btnSelectUser.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnSelectUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSelectUser.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectUser.Image")));
      this.btnSelectUser.Location = new System.Drawing.Point(1, 39);
      this.btnSelectUser.Margin = new System.Windows.Forms.Padding(1);
      this.btnSelectUser.Name = "btnSelectUser";
      this.btnSelectUser.Size = new System.Drawing.Size(31, 31);
      this.btnSelectUser.TabIndex = 6;
      this.btnSelectUser.Tag = "Avançar";
      this.btnSelectUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSelectUser.UseVisualStyleBackColor = false;
      this.btnSelectUser.Click += new System.EventHandler(this.BtnSelectUser_Click);
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(123)))), ((int)(((byte)(123)))));
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 30);
      this.panel3.Margin = new System.Windows.Forms.Padding(0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(336, 1);
      this.panel3.TabIndex = 9;
      // 
      // btnSendMessage
      // 
      this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSendMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btnSendMessage.BorderColor = System.Drawing.Color.PaleVioletRed;
      this.btnSendMessage.BorderRadius = 15;
      this.btnSendMessage.BorderSize = 0;
      this.btnSendMessage.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSendMessage.Image = ((System.Drawing.Image)(resources.GetObject("btnSendMessage.Image")));
      this.btnSendMessage.Location = new System.Drawing.Point(304, 39);
      this.btnSendMessage.Margin = new System.Windows.Forms.Padding(1);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(31, 31);
      this.btnSendMessage.TabIndex = 4;
      this.btnSendMessage.Tag = "Avançar";
      this.btnSendMessage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btnSendMessage.UseVisualStyleBackColor = false;
      this.btnSendMessage.Click += new System.EventHandler(this.BtnSendMessage_Click);
      // 
      // txtMensagem
      // 
      this.txtMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMensagem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
      this.txtMensagem.BorderSize = 2;
      this.txtMensagem.F7ToolTipText = null;
      this.txtMensagem.F8ToolTipText = null;
      this.txtMensagem.F9ToolTipText = null;
      this.txtMensagem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.txtMensagem.IconF7 = ((System.Drawing.Image)(resources.GetObject("txtMensagem.IconF7")));
      this.txtMensagem.IconToolTipText = null;
      this.txtMensagem.Lines = new string[0];
      this.txtMensagem.Location = new System.Drawing.Point(36, 39);
      this.txtMensagem.MaxLength = 32767;
      this.txtMensagem.Multiline = true;
      this.txtMensagem.Name = "txtMensagem";
      this.txtMensagem.PasswordChar = '\0';
      this.txtMensagem.Propriedade = null;
      this.txtMensagem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtMensagem.SelectedText = "";
      this.txtMensagem.SelectionLength = 0;
      this.txtMensagem.SelectionStart = 0;
      this.txtMensagem.ShortcutsEnabled = true;
      this.txtMensagem.ShowButtonF7 = true;
      this.txtMensagem.Size = new System.Drawing.Size(264, 31);
      this.txtMensagem.TabIndex = 0;
      this.txtMensagem.UnderlinedStyle = false;
      this.txtMensagem.UseSelectable = true;
      this.txtMensagem.Valor_Decimais = ((short)(0));
      this.txtMensagem.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
      this.txtMensagem.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
      this.txtMensagem.ButtonClickF7 += new LmCorbieUI.Controls.LmTextBox.ButClick(this.TxtMensagem_ButtonClickF7);
      this.txtMensagem.TextChanged += new System.EventHandler(this.TxtMensagem_TextChanged);
      this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMensagem_KeyDown);
      // 
      // lblConversaCom
      // 
      this.lblConversaCom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblConversaCom.FontWeight = LmCorbieUI.Design.LmLabelWeight.Bold;
      this.lblConversaCom.Location = new System.Drawing.Point(3, 2);
      this.lblConversaCom.Margin = new System.Windows.Forms.Padding(3);
      this.lblConversaCom.Name = "lblConversaCom";
      this.lblConversaCom.Size = new System.Drawing.Size(297, 29);
      this.lblConversaCom.TabIndex = 10;
      this.lblConversaCom.Text = "Para: Leonardo Michalak";
      this.lblConversaCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // pnlMain
      // 
      this.pnlMain.BackColor = System.Drawing.Color.Magenta;
      this.pnlMain.Controls.Add(this.flpChat);
      this.pnlMain.Controls.Add(this.pnlRodapeMensagm);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(0, 0);
      this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(336, 630);
      this.pnlMain.TabIndex = 8;
      // 
      // tmrMessages
      // 
      this.tmrMessages.Enabled = true;
      this.tmrMessages.Interval = 3000;
      this.tmrMessages.Tick += new System.EventHandler(this.TmrMessages_Tick);
      // 
      // tmr
      // 
      this.tmr.Enabled = true;
      this.tmr.Interval = 10000;
      this.tmr.Tag = "";
      this.tmr.Tick += new System.EventHandler(this.Tmr_Tick);
      // 
      // FrmChatMsg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(336, 630);
      this.Controls.Add(this.pnlMain);
      this.Location = new System.Drawing.Point(0, 0);
      this.Name = "FrmChatMsg";
      this.Padding = new System.Windows.Forms.Padding(0);
      this.Resizable = false;
      this.Text = "Chat";
      this.pnlRodapeMensagm.ResumeLayout(false);
      this.pnlRodapeMensagm.PerformLayout();
      this.pnlMain.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flpChat;
    private LmCorbieUI.Controls.LmPanel pnlRodapeMensagm;
    private LmCorbieUI.Controls.LmTextBox txtMensagem;
    private LmCorbieUI.Controls.LmButton btnSendMessage;
    private LmCorbieUI.Controls.LmButton btnSelectUser;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Panel panel3;
    private LmCorbieUI.Controls.LmLabel lblConversaCom;
    private System.Windows.Forms.Label lblClose;
    private System.Windows.Forms.Timer tmrMessages;
    private System.Windows.Forms.Timer tmr;
  }
}

