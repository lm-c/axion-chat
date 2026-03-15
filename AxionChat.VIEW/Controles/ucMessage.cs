using System;
using System.Drawing;
using System.Windows.Forms;
using AddinTGM.CORE;
using LmCorbieUI;
using LmCorbieUI.Metodos;

namespace AxionChat.VIEW.Controles {
  public partial class ucMessage : UserControl {
    private chat_msg _mensagem;
    private bool _enviadaPorMim;

    public event EventHandler CopiarClick;
    public event EventHandler ExcluirClick;
    public event EventHandler EditarClick;

    private Timer _timerVerificacao;

    public chat_msg Mensagem => _mensagem;

    public ucMessage(chat_msg msg, bool enviadaPorMim) {
      InitializeComponent();

      _timerVerificacao = new Timer { Interval = 200 };
      _timerVerificacao.Tick += (s, e) => VerificarMouse();

      btnExcluir.Image = btnExcluir.Image.ApplyColor(Color.Firebrick);

      _mensagem = msg;
      _enviadaPorMim = enviadaPorMim;
      ConfigurarMensagem();
    }

    private void ConfigurarMensagem() {
      lblMensagem.Text = _mensagem.mensagem;
      lblDataHora.Text = _mensagem.data_envio.ToShortDateTimeString();

      // Configurar cores e alinhamento
      if (_enviadaPorMim) {
        this.BackColor = Color.FromArgb(220, 248, 198); // Verde claro tipo WhatsApp
        lblNomeUsuario.Text = "Você";
        lblNomeUsuario.ForeColor = Color.DarkGreen;

        if (lblRead.Image != null) {
          lblRead.Visible = true;
          if (_mensagem.lida) {
            lblRead.Image = lblRead.Image.ApplyColor(Color.DodgerBlue);
          } else {
            lblRead.Image = lblRead.Image.ApplyColor(Color.Gray);
          }
        }
        // btnEditar.Visible = true; // Só pode editar msg própria
      } else {
        this.BackColor = Color.White;
        lblNomeUsuario.Text = _mensagem.remetente != null ? _mensagem.remetente.nome : "Desconhecido";
        lblNomeUsuario.ForeColor = Color.DarkBlue;
        btnEditar.Visible = btnExcluir.Visible = false;
        if (lblRead != null) lblRead.Visible = false;
      }

      AjustarTamanho();
    }

    public void AtualizarStatusLeitura(bool lida) {
      if (_enviadaPorMim && lblRead.Visible) {
        _mensagem.lida = lida;
        if (lida) {
          lblRead.Image = lblRead.Image.ApplyColor(Color.DodgerBlue);
        } else {
          lblRead.Image = lblRead.Image.ApplyColor(Color.Gray);
        }
      }
    }

    private void AjustarTamanho() {
      // Largura máxima da label de mensagem
      int maxWidth = 280;
      lblMensagem.MaximumSize = new Size(maxWidth, 0);

      // Recalcular altura baseado no conteúdo
      int alturaCabecalho = lblNomeUsuario.Height + 10;
      int alturaMensagem = lblMensagem.Height + 10;
      int alturaRodape = lblDataHora.Height + 5;

      this.Height = alturaCabecalho + alturaMensagem + alturaRodape;
    }

    private void VerificarMouse() {
      if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition))) {
        _timerVerificacao.Stop();
        pnlBotoes.Visible = false;
        AjustarTamanho();
      }
    }

    private void UcMessage_MouseEnter(object sender, EventArgs e) {
      pnlBotoes.Visible = true;
      AjustarTamanho();
      _timerVerificacao.Start();
    }

    private void UcMessage_MouseLeave(object sender, EventArgs e) {
      // O Timer cuida disso agora para evitar problemas de saída rápida
    }

    private void BtnCopiar_Click(object sender, EventArgs e) {
      Clipboard.SetText(lblMensagem.Text);
      Toast.Success("Mensagem copiada!");
      CopiarClick?.Invoke(this, e);
    }

    private void BtnExcluir_Click(object sender, EventArgs e) {
      if (MsgBox.Show("Deseja excluir esta mensagem?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
        ExcluirClick?.Invoke(this, e);
      }
    }

    private void btnEditar_Click(object sender, EventArgs e) {
      EditarClick?.Invoke(this, e);
    }
  }
}
