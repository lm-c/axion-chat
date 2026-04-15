using AddinTGM.CORE;
using LmCorbieUI;
using LmCorbieUI.Controls;
using LmCorbieUI.LmForms;
using LmCorbieUI.Metodos;
using System;
using System.Data.Entity;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AxionChat.VIEW {
  public partial class FrmChatMsg : LmSingleForm {
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

    private NotifyIcon notifyIcon;
    private ContextMenuStrip contextMenu;

    private int _mensagensCarregadas = 0;
    private const int TAMANHO_PAGINA = 15;
    private int _idUsuarioDestino = 0;
    private int _idMensagemEmEdicao = 0;
    private LmLabel _btnVerMais;

    private int _idUltimaMensagemRecebida = 0;
    private int _idUltimoRemetenteNotificado = 0;

    public FrmChatMsg() {
      InitializeComponent();

      lblConversaCom.Text = string.Empty;

      string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "axion-chat-icon.ico");
      if (File.Exists(iconPath)) {
        this.Icon = new Icon(iconPath);
      }

      this.Load += FrmChatMsg_Load;
      //txtMensagem.TextChanged += TxtMensagem_TextChanged;

      InitializeTray();
      InicializarBotaoVerMais();
    }

    private void ScrollChatParaOFim() {
      if (flpChat == null || flpChat.IsDisposed) return;
      if (flpChat.Controls.Count == 0) return;

      Control alvo = flpChat.FlowDirection == FlowDirection.BottomUp
        ? flpChat.Controls[0]
        : flpChat.Controls[flpChat.Controls.Count - 1];

      if (alvo == null || alvo.IsDisposed) return;

      if (flpChat.IsHandleCreated) {
        flpChat.BeginInvoke(new Action(() => {
          if (flpChat.IsDisposed || alvo.IsDisposed) return;
          flpChat.ScrollControlIntoView(alvo);
        }));
      } else {
        flpChat.ScrollControlIntoView(alvo);
      }
    }

    private void AtualizarIndicadorNaoLidas(ContextoDados db, int meuId) {
      if (notifyIcon == null) return;
      try {
        int totalNaoLidas = db.chat_msg.Count(m => !m.excluida && !m.lida && m.id_destinatario == meuId);
        notifyIcon.Text = totalNaoLidas > 0 ? $"Axion Chat ({totalNaoLidas} novas)" : "Axion Chat";
      } catch {
        notifyIcon.Text = "Axion Chat";
      }
    }

    private void NotificarMensagemNova(string nomeRemetente, string mensagem, int idRemetente, bool mostrarNoTray) {
      _idUltimoRemetenteNotificado = idRemetente;

      string texto = $"Nova mensagem de {nomeRemetente} \r\n\r\n\"{mensagem}\"";
      Toast.Info(texto, autoClose: false);

      if (mostrarNoTray && notifyIcon != null) {
        try {
          notifyIcon.BalloonTipTitle = $"Nova mensagem de {nomeRemetente}";
          notifyIcon.BalloonTipText = mensagem;
          notifyIcon.ShowBalloonTip(5000);
        } catch { }
      }

      FlashWindow(this.Handle, true);
    }

    private void InicializarBotaoVerMais() {
      _btnVerMais = new LmLabel {
        Text = "Ver mais mensagens",
        Name = "lblvmsg",
        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
        FontWeight = LmCorbieUI.Design.LmLabelWeight.Bold,
        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
        Height = 30,
        Width = 150,
        Margin = new Padding(0, 10, 0, 10),
        Anchor = AnchorStyles.None,
        Cursor = Cursors.Hand,
        UseCustomColor = true,
        BackColor = Color.White,
      };
      _btnVerMais.Click += BtnVerMais_Click;
    }

    private void BtnVerMais_Click(object sender, EventArgs e) {
      CarregarMensagens(_idUsuarioDestino, _mensagensCarregadas, 10);
    }

    private void FrmChatMsg_Load(object sender, EventArgs e) {

      // Técnica para ocultar a barra de rolagem mas manter a funcionalidade
      // Move o flpChat para a direita para esconder a barra vertical
      // Requer que o flpChat esteja dentro de um container (panel2)

      flpChat.AutoScroll = true;
      flpChat.VerticalScroll.Visible = false; // Tenta ocultar nativamente
      flpChat.HorizontalScroll.Visible = false;

      // Ajuste fino: Aumentar a largura do FLP para empurrar a scrollbar para fora da visão
      // Assumindo que o panel2 (pai) tem AutoScroll = false e Clip
      flpChat.Width = pnlMain.Width + SystemInformation.VerticalScrollBarWidth;
      flpChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

      // Posicionar no lado direito ocupando 100% da altura
      var screen = Screen.PrimaryScreen;
      this.Width = 400; // Largura padrão para o chat
      this.StartPosition = FormStartPosition.Manual;
      this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Top);
      this.Height = screen.WorkingArea.Height;

      CarregarUltimaConversa();

      // Iniciar minimizado no tray
      this.WindowState = FormWindowState.Minimized;
      this.ShowInTaskbar = false;
      this.Hide();
    }

    private void CarregarUltimaConversa() {
      try {
        if (usuario_alocados.model == null) return;

        using (var db = new ContextoDados()) {
          var meuId = usuario_alocados.model.usuario_id;

          // Buscar a última mensagem enviada ou recebida por mim
          var ultimaMsg = db.chat_msg
              .Where(m => m.id_remetente == meuId || m.id_destinatario == meuId)
              .OrderByDescending(m => m.data_envio)
              .FirstOrDefault();

          if (ultimaMsg != null) {
            // Identificar com quem foi a última conversa
            int idUltimoUsuario = (ultimaMsg.id_remetente == meuId) ? ultimaMsg.id_destinatario : ultimaMsg.id_remetente;

            // Carregar dados do usuário
            var usuario = db.usuarios.FirstOrDefault(u => u.id == idUltimoUsuario);
            if (usuario != null) {
              this.Tag = usuario.id;
              lblConversaCom.Text = $"Para: {usuario.nome}";
              _idUsuarioDestino = usuario.id;

              // Carregar as mensagens
              CarregarMensagens(_idUsuarioDestino, 0, TAMANHO_PAGINA);
            }
          }
        }
      } catch (Exception ex) {
        // Silencioso na inicialização ou logar
        System.Diagnostics.Debug.WriteLine("Erro ao carregar última conversa: " + ex.Message);
      }
    }

    private void InitializeTray() {
      contextMenu = new ContextMenuStrip();

      var openItem = new ToolStripMenuItem("Abrir Chat");
      openItem.Click += (s, e) => ShowForm();
      contextMenu.Items.Add(openItem);

      var exitItem = new ToolStripMenuItem("Sair");
      exitItem.Click += (s, e) => {
        if (notifyIcon != null) notifyIcon.Visible = false;
        Application.Exit();
      };
      contextMenu.Items.Add(exitItem);

      notifyIcon = new NotifyIcon();

      // Load icon from file (fallback to application icon)
      string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "axion-chat-icon.ico");
      if (File.Exists(iconPath)) {
        notifyIcon.Icon = new Icon(iconPath);
      } else {
        notifyIcon.Icon = SystemIcons.Application;
      }

      notifyIcon.ContextMenuStrip = contextMenu;
      notifyIcon.Visible = true;
      notifyIcon.Text = "Axion Chat Tray";

      notifyIcon.MouseClick += (s, e) => {
        if (e.Button == MouseButtons.Left) {
          ShowForm();
        }
      };

      notifyIcon.BalloonTipClicked += (s, e) => {
        ShowForm();
        if (_idUltimoRemetenteNotificado > 0) {
          AbrirConversa(_idUltimoRemetenteNotificado);
        }
      };
    }

    private void AbrirConversa(int idUsuario) {
      try {
        using (var db = new ContextoDados()) {
          var usuario = db.usuarios.FirstOrDefault(u => u.id == idUsuario);
          if (usuario == null) return;

          if (_idUsuarioDestino != usuario.id) {
            flpChat.Controls.Clear();
            _mensagensCarregadas = 0;
            _idMensagemEmEdicao = 0;
            txtMensagem.Text = string.Empty;
          }

          this.Tag = usuario.id;
          lblConversaCom.Text = $"Para: {usuario.nome}";
          _idUsuarioDestino = usuario.id;
          CarregarMensagens(_idUsuarioDestino, 0, TAMANHO_PAGINA);
        }
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine("Erro ao abrir conversa: " + ex);
      }
    }

    private void ShowForm() {
      this.Show();
      this.WindowState = FormWindowState.Normal;
      this.ShowInTaskbar = true;
      this.Activate();

      // Garantir posição ao restaurar
      var screen = Screen.PrimaryScreen;
      this.Width = 400;
      this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Top);
      this.Height = screen.WorkingArea.Height;
    }

    protected override void OnResize(EventArgs e) {
      base.OnResize(e);
      if (this.WindowState == FormWindowState.Minimized) {
        this.ShowInTaskbar = false;
        this.Hide();
        if (notifyIcon != null) {
          notifyIcon.Visible = true;
        }
      }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (e.CloseReason == CloseReason.UserClosing) {
        e.Cancel = true;
        this.Hide();
        if (notifyIcon != null) {
          notifyIcon.Visible = true;
        }
      } else {
        base.OnFormClosing(e);
      }
    }

    private void LblClose_Click(object sender, EventArgs e) {
      this.Close();
    }

    private void LblClose_MouseEnter(object sender, EventArgs e) {
      lblClose.ForeColor = Color.Red;
    }

    private void LblClose_MouseLeave(object sender, EventArgs e) {
      lblClose.ForeColor = Color.Firebrick;
    }

    private void BtnSelectUser_Click(object sender, EventArgs e) {
      FrmConsultaGeral frm = new FrmConsultaGeral(this,
        usuarios.Selecionar(ativo: true), "Selecionar Usuário");
      if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        if (int.TryParse(frm.valor[0], out int ID)) {
          if (_idUsuarioDestino != ID) {
            flpChat.Controls.Clear();
            _mensagensCarregadas = 0;
            _idMensagemEmEdicao = 0;
            txtMensagem.Text = string.Empty;
          }

          this.Tag = frm.valor[0];
          lblConversaCom.Text = $"Para: {frm.valor[1]}";
          _idUsuarioDestino = ID;
          CarregarMensagens(_idUsuarioDestino, 0, TAMANHO_PAGINA);
        }

    }

    private void BtnSendMessage_Click(object sender, EventArgs e) {
      if (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString())) {
        Toast.Warning("Selecione um usuário para enviar a mensagem.");
        return;
      }

      if (string.IsNullOrWhiteSpace(txtMensagem.Text)) {
        Toast.Warning("Digite uma mensagem.");
        return;
      }

      try {
        if (!Web.IsConnected()) {
          Toast.Warning("Sem conexão com a internet.");
          return;
        }

        int idDestinatario = Convert.ToInt32(this.Tag);
        string mensagem = txtMensagem.Text;

        using (var db = new ContextoDados()) {
          // Verificar se o usuário alocado (remetente) está carregado
          if (usuario_alocados.model == null) {
            Toast.Error("Usuário logado não identificado.");
            return;
          }

          if (_idMensagemEmEdicao > 0) {
            // Edição de mensagem existente
            var msgExistente = db.chat_msg.FirstOrDefault(m => m.id == _idMensagemEmEdicao);
            if (msgExistente != null) {
              // Verifica se a mensagem pertence ao usuário logado
              if (msgExistente.id_remetente != usuario_alocados.model.usuario_id) {
                Toast.Warning("Você só pode editar suas próprias mensagens.");
                _idMensagemEmEdicao = 0;
                txtMensagem.Text = string.Empty;
                return;
              }

              msgExistente.mensagem = mensagem;
              db.Entry(msgExistente).State = EntityState.Modified;
              db.SaveChanges();

              Toast.Success("Mensagem atualizada.");

              // Recarregar mensagens para refletir a alteração
              // Poderíamos atualizar apenas o controle visualmente, mas recarregar garante consistência
              CarregarMensagens(_idUsuarioDestino, 0, _mensagensCarregadas > TAMANHO_PAGINA ? _mensagensCarregadas : TAMANHO_PAGINA);
            }

            _idMensagemEmEdicao = 0;
            txtMensagem.Text = string.Empty;
          } else {
            // Nova mensagem
            var novaMsg = new chat_msg {
              id_remetente = usuario_alocados.model.usuario_id,
              id_destinatario = idDestinatario,
              mensagem = mensagem,
              data_envio = DateTime.Now,
              lida = false
            };

            db.chat_msg.Add(novaMsg);
            db.SaveChanges();

            txtMensagem.Text = string.Empty;
            // Adicionar lógica para mostrar a mensagem no chat visualmente (flpChat)
            AdicionarMensagemVisual(novaMsg, true, noTopo: true);
            ScrollChatParaOFim();
          }
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao enviar mensagem");
      }
    }

    private void CarregarMensagens(int usuarioDestino, int skip, int take) {
      try {
        if (usuario_alocados.model == null) return;

        using (var db = new ContextoDados()) {
          var meuId = usuario_alocados.model.usuario_id;

          var mensagens = db.chat_msg
              .Include(m => m.remetente)
              .Where(m => !m.excluida && ((m.id_remetente == meuId && m.id_destinatario == usuarioDestino) ||
                          (m.id_remetente == usuarioDestino && m.id_destinatario == meuId)))
              .OrderByDescending(m => m.data_envio)
              .Skip(skip)
              .Take(take)
              .ToList(); // Traz do banco ordenado decrescente (mais recente primeiro)

          if (mensagens.Any()) {
            // Marcar mensagens recebidas como lidas
            // REGRA: Apenas marcar como lida se o destinatário for o usuário logado (meuId)
            var msgsNaoLidas = mensagens
                .Where(m => m.id_destinatario == meuId && !m.lida)
                .ToList();

            if (msgsNaoLidas.Any()) {
              foreach (var msg in msgsNaoLidas) {
                msg.lida = true;
                db.Entry(msg).State = EntityState.Modified;
              }
              db.SaveChanges();
            }

            // Se for a primeira carga (skip 0), limpamos tudo
            if (skip == 0) {
              if (mensagens.Any())
                _idUltimaMensagemRecebida = mensagens.Max(m => m.id);

              flpChat.Controls.Clear();
              _mensagensCarregadas = 0;

              // Carrega mensagens do mais recente para o mais antigo, inserindo no topo
              // A mais recente deve ficar embaixo (index 0 se BottomUp, ou último se TopDown)
              // Aqui estamos invertendo a lógica para usar SetChildIndex(0) nas novas mensagens

              // Ordena cronologicamente: Msg 1, Msg 2... Msg 30 (Mais recente)
              //var msgsCronologicas = mensagens.OrderBy(m => m.data_envio).ToList();

              foreach (var msg in mensagens) {
                // Adiciona normalmente (vai para o fim da lista)
                AdicionarMensagemVisual(msg, msg.id_remetente == meuId, noTopo: false);
              }

              // Se houver mais mensagens, adiciona o botão "Ver mais" no topo (índice 0)
              VerificarBotaoVerMais(db, meuId, usuarioDestino);

              ScrollChatParaOFim();
            } else {
              // Carregando histórico (paginação) - Botão "Ver Mais" clicado

              flpChat.SuspendLayout();

              // Remover botão ver mais temporariamente
              if (flpChat.Controls.Contains(_btnVerMais))
                flpChat.Controls.Remove(_btnVerMais);

              // As mensagens vieram do banco ordenadas decrescente (mais recente primeiro)
              // Ex: Skip 30, Take 10 -> Traz Msg 20..11
              // Precisamos inseri-las no TOPO, mantendo a ordem cronológica entre elas: 11, 12... 20

              var msgsHistorico = mensagens.OrderByDescending(m => m.data_envio).ToList(); // 20, 19, ... 11

              // Inserimos uma por uma no índice 0. 
              // Para ficar 11, 12... 20 no topo, inserimos primeiro a 20 no indice 0, depois a 19 no indice 0 (empurra a 20 pra baixo)...

              foreach (var msg in msgsHistorico) {
                AdicionarMensagemVisual(msg, msg.id_remetente == meuId, noTopo: false);
              }

              // Recolocar botão ver mais no topo se necessário
              VerificarBotaoVerMais(db, meuId, usuarioDestino);

              flpChat.ResumeLayout();
            }

            _mensagensCarregadas += mensagens.Count;
            AtualizarIndicadorNaoLidas(db, meuId);
          }
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao carregar mensagens");
      }
    }

    private void VerificarBotaoVerMais(ContextoDados db, int meuId, int usuarioDestino) {
      // Verificar se existem mais mensagens além das carregadas
      bool existemMais = db.chat_msg
          .Where(m => !m.excluida && ((m.id_remetente == meuId && m.id_destinatario == usuarioDestino) ||
                      (m.id_remetente == usuarioDestino && m.id_destinatario == meuId)))
          .OrderByDescending(m => m.data_envio)
          .Skip(_mensagensCarregadas + (_mensagensCarregadas == 0 ? TAMANHO_PAGINA : 10)) // Próximo bloco
          .Any();

      if (existemMais) {
        if (!flpChat.Controls.Contains(_btnVerMais))
          flpChat.Controls.Add(_btnVerMais);
      } else {
        if (flpChat.Controls.Contains(_btnVerMais))
          flpChat.Controls.Remove(_btnVerMais);
      }
    }

    private AxionChat.VIEW.Controles.ucMessage CriarControleMensagem(chat_msg msg, bool enviadaPorMim) {
      var uc = new AxionChat.VIEW.Controles.ucMessage(msg, enviadaPorMim);

      // Ajustar largura descontando a barra de rolagem (que está visível)
      // Reduzimos um pouco mais para garantir que não gere scroll horizontal (30px de folga)
      int scrollWidth = SystemInformation.VerticalScrollBarWidth;
      uc.Width = flpChat.Width - scrollWidth - 50;

      // Ajuste de margens para efeito visual "Esquerda/Direita"
      if (enviadaPorMim) {
        // Mensagem minha: Mais à direita (Margin Left maior)
        // Reduzi a margem esquerda para compensar a largura menor e evitar quebra excessiva
        uc.Margin = new Padding(40, 5, 0, 5);
      } else {
        // Mensagem do outro: Mais à esquerda (Margin Right maior)
        uc.Margin = new Padding(0, 5, 40, 5);
      }

      uc.EditarClick += Uc_EditarClick;
      uc.ExcluirClick += Uc_ExcluirClick;

      return uc;
    }

    private void Uc_EditarClick(object sender, EventArgs e) {
      if (sender is AxionChat.VIEW.Controles.ucMessage uc) {
        _idMensagemEmEdicao = uc.Mensagem.id;
        txtMensagem.Text = uc.Mensagem.mensagem;
        txtMensagem.Focus();
        // Opcional: Indicar visualmente que está editando
      }
    }

    private void Uc_ExcluirClick(object sender, EventArgs e) {
      try {
        if (sender is AxionChat.VIEW.Controles.ucMessage uc) {
          using (var db = new ContextoDados()) {
            // Verificar se o usuário alocado (remetente) está carregado
            if (usuario_alocados.model == null) {
              Toast.Error("Usuário logado não identificado.");
              return;
            }

            var msg = db.chat_msg.FirstOrDefault(m => m.id == uc.Mensagem.id);
            if (msg != null) {
              if (msg.id_remetente != usuario_alocados.model.usuario_id) {
                Toast.Warning("Você só pode excluir suas próprias mensagens.");
                return;
              }

              // Exclusão lógica
              msg.excluida = true;
              db.Entry(msg).State = EntityState.Modified;
              db.SaveChanges();

              // Remove visualmente
              flpChat.Controls.Remove(uc);
              uc.Dispose();
              Toast.Success("Mensagem excluída.");
            }
          }
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao excluir mensagem");
      }
    }

    private void AdicionarMensagemVisual(chat_msg msg, bool enviadaPorMim, bool noTopo = false) {
      var uc = CriarControleMensagem(msg, enviadaPorMim);

      flpChat.Controls.Add(uc);

      if (noTopo) {
        flpChat.Controls.SetChildIndex(uc, 0);
        ScrollChatParaOFim();
      }
    }

    private void TxtMensagem_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Escape && _idMensagemEmEdicao > 0) {
        _idMensagemEmEdicao = 0;
        txtMensagem.Text = string.Empty;
        Toast.Info("Edição cancelada.");
        e.SuppressKeyPress = true;
        return;
      }

      if (e.Control && e.KeyCode == Keys.Enter) {
        e.SuppressKeyPress = true;
        BtnSendMessage_Click(sender, e);
      }
    }

    private void TxtMensagem_TextChanged(object sender, EventArgs e) {
      // Usa GetLineFromCharIndex para obter o número real de linhas (incluindo wrap)
      int numLines = txtMensagem.LineCount;

      // Se estiver vazio, força 1 linha
      if (string.IsNullOrEmpty(txtMensagem.Text)) numLines = 1;

      // Altura base do rodapé (padding + altura inicial)
      int baseHeight = 76;

      // Altura aproximada de uma linha
      int lineHeight = txtMensagem.Font.Height + 1;

      // Calcula a altura extra baseada nas linhas adicionais
      int extraHeight = (numLines - 1) * lineHeight;

      int newHeight = baseHeight + extraHeight;

      // Limita a 210px
      if (newHeight > 210) newHeight = 210;

      // Se a altura mudou, aplica
      if (pnlRodapeMensagm.Height != newHeight) {
        pnlRodapeMensagm.Height = newHeight;
        flpChat.Height = pnlMain.Height - newHeight;
      }
    }

    private void TxtMensagem_ButtonClickF7(object sender, EventArgs e) {
      using (var frm = new AxionChat.VIEW.Formularios.FrmSelecionarEmoji()) {
        if (frm.ShowDialog() == DialogResult.OK) {
          int cursorPosition = txtMensagem.SelectionStart;
          txtMensagem.Text = txtMensagem.Text.Insert(cursorPosition, frm.EmojiSelecionado);
          txtMensagem.SelectionStart = cursorPosition + frm.EmojiSelecionado.Length;
          txtMensagem.Focus();
        }
      }
    }

    private void TmrMessages_Tick(object sender, EventArgs e) {
      tmrMessages.Enabled = false;
      try {
        if (usuario_alocados.model == null) return;

        using (var db = new ContextoDados()) {
          var meuId = usuario_alocados.model.usuario_id;

          // Atualizar status de leitura das mensagens enviadas por mim
          var minhasMsgsNaoLidas = db.chat_msg
              .Where(m => !m.excluida && m.id_remetente == meuId && m.lida && m.id_destinatario == _idUsuarioDestino)
              .OrderByDescending(m => m.data_envio)
              .Take(20) // Otimização
              .Select(m => m.id)
              .ToList();

          if (minhasMsgsNaoLidas.Any() && this.Visible && this.WindowState != FormWindowState.Minimized) {
            foreach (Control ctrl in flpChat.Controls) {
              if (ctrl is AxionChat.VIEW.Controles.ucMessage uc && uc.Mensagem.id_remetente == meuId) {
                if (minhasMsgsNaoLidas.Contains(uc.Mensagem.id)) {
                  uc.AtualizarStatusLeitura(true);
                }
              }
            }
          }

          // Verificar se há novas mensagens (id > ultimoIdRecebido) destinadas a mim
          // Traz apenas as novas mensagens
          var novasMensagens = db.chat_msg
              .Include(m => m.remetente)
              .Where(m => !m.excluida && !m.lida && m.id_destinatario == meuId && m.id_remetente != meuId && m.id > _idUltimaMensagemRecebida)
              .OrderBy(m => m.data_envio)
              .ToList();

          if (novasMensagens.Any()) {
            foreach (var msg in novasMensagens) {
              // Atualiza o ID da última mensagem processada
              if (msg.id > _idUltimaMensagemRecebida)
                _idUltimaMensagemRecebida = msg.id;

              string nomeRemetente = msg.remetente != null ? msg.remetente.nome : "Desconhecido";

              bool formAtivo = false;
              try {
                // Verifica se esta janela é a janela em primeiro plano no Windows
                IntPtr foregroundHwnd = GetForegroundWindow();
                formAtivo = (foregroundHwnd == this.Handle);

                // Se não for a própria janela, verifica se é alguma janela filha aberta por ela (ex: emoji, consulta)
                if (!formAtivo && Form.ActiveForm != null) {
                  if (Form.ActiveForm == this || Form.ActiveForm.Owner == this) {
                    formAtivo = true;
                  }
                }
              } catch { }

              // Cenário 1: Chat aberto com o remetente da mensagem
              if (this.Visible && this.WindowState != FormWindowState.Minimized && _idUsuarioDestino == msg.id_remetente) {
                // Marca como lida
                msg.lida = true;
                db.Entry(msg).State = EntityState.Modified;

                // Renderiza a mensagem no chat
                AdicionarMensagemVisual(msg, false, noTopo: true);
                ScrollChatParaOFim();

                // Se a tela está visível mas por trás de outras janelas, mostrar alerta e piscar
                if (!formAtivo) {
                  NotificarMensagemNova(nomeRemetente, msg.mensagem, msg.id_remetente, mostrarNoTray: false);
                }
              }
              // Cenário 2: Chat fechado, minimizado ou aberto com outro usuário
              else {
                bool mostrarNoTray = !this.Visible || this.WindowState == FormWindowState.Minimized || !this.ShowInTaskbar;
                NotificarMensagemNova(nomeRemetente, msg.mensagem, msg.id_remetente, mostrarNoTray: mostrarNoTray);
              }
            }
            db.SaveChanges();
          }

          AtualizarIndicadorNaoLidas(db, meuId);
        }
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine("Erro no timer de mensagens: " + ex);
      } finally {
        tmrMessages.Enabled = true;
      }
    }

    private void Tmr_Tick(object sender, EventArgs e) {
      try {
        string remotePath = @"E:\Engenharia\addin\SETUP ADDIN\axion-chat\AxionChat.exe";
        if (File.Exists(remotePath)) {
          var remoteVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(remotePath);

          Version localVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

          if (Version.TryParse(remoteVersionInfo.FileVersion, out Version remoteVersion)) {
            if (localVersion < remoteVersion) {
              string installerPath = @"E:\Engenharia\addin\SETUP ADDIN\axion-chat-install.bat";
              if (File.Exists(installerPath)) {
                System.Diagnostics.Process.Start(installerPath);
                if (notifyIcon != null) notifyIcon.Visible = false;
                Application.Exit();
              }
            }
          }
        }
      } catch { }
    }
  }
}
