using AddinTGM.CORE;
using LmCorbieUI;
using LmCorbieUI.LmForms;
using LmCorbieUI.Metodos;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionChat {
  public partial class FrmLogin : LmSingleForm {
    string login, senha;

    readonly string userPC;
    readonly string hostname;

    static FrmLogin instancia;

    public static FrmLogin Instancia {
      get {
        if (instancia == null)
          instancia = new FrmLogin();

        return instancia;
      }
    }

    public FrmLogin() {
      InitializeComponent();

      string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "axion-chat-icon.ico");
      if (System.IO.File.Exists(iconPath)) {
        this.Icon = new System.Drawing.Icon(iconPath);
      }

      ptb.BringToFront();
      lblCarregando.BringToFront();

      userPC = Environment.UserName;
      hostname = Dns.GetHostName();
    }

    private void FrmLogin_Load(object sender, EventArgs e) {
      instancia = this;
    }

    private async void FrmLogin_Loaded(object sender, EventArgs e) {
      try {
        if (Web.IsConnected()) {
          await Task.Run(() => {
            using (ContextoDados db = new ContextoDados()) {
              var usuarioPC = Environment.UserName;
              var hostName = Dns.GetHostName();

              var usuAlocado = db.usuario_alocados.FirstOrDefault(x => x.hostname == hostName && x.usuario_pc == usuarioPC);

              if (usuAlocado != null) {
                var usuario = db.usuarios.FirstOrDefault(u => u.id == usuAlocado.usuario_id);
                if (usuario != null) {
                  Invoke(new MethodInvoker(delegate () {
                    usuario_alocados.model = usuAlocado;
                    usuario_alocados.model.usuario = usuario;
                    txtUsuario.Text = usuario.login;
                    txtSenha.Text = usuario.senha.DescriptografarAES();

                    Logar();
                  }));
                }
              }
            }
          });
        } else {
          Invoke(new MethodInvoker(delegate () {
            Toast.Warning("Sem Internet");
          }));
        }
      } catch (Exception ex) {
        LmException.ShowException(ex, "Erro ao inicializar login");
      } finally {
        //Invoke(new MethodInvoker(delegate () {
          ptb.Visible = lblCarregando.Visible = false;
        //}));
      }
    }

    private async void BtnLogin_Click(object sender, EventArgs e) {
      await Logar();
    }

    private async Task Logar() {
      if (Controles.PossuiCamposInvalidos(this))
        return;

      using (ContextoDados db = new ContextoDados()) {
        login = txtUsuario.Text;

        try {
          senha = db.usuarios.FirstOrDefault(x => x.login == login)?.senha;

          if (login == txtUsuario.Text && senha == Criptografar.EncryptAES(txtSenha.Text)) {
            var usu = (db.usuarios.Where(x => x.login == txtUsuario.Text)).FirstOrDefault();

            if (usu != null) {
              if (!usu.ativo) {
                Toast.Warning("Usuário Inativo!");
                return;
              }
            } else {
              Toast.Error("Retornou Usuário Inválido.\nLogin Cancelado.");
              return;
            }

            if (usuario_alocados.model == null) {
              usuario_alocados.model = new usuario_alocados();
              usuario_alocados.model.usuario_pc = Environment.UserName;
              usuario_alocados.model.hostname = Dns.GetHostName();
              usuario_alocados.model.usuario = usu;
              usuario_alocados.model.usuario_id = usu.id;

              db.usuario_alocados.Add(usuario_alocados.model);
              db.SaveChanges();
            }

            Invoke(new MethodInvoker(delegate () {
              this.DialogResult = DialogResult.OK;
              this.Close();
            }));
          } else {
            MsgBox.Show("Usuário ou Senha Inválido.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        } catch (Exception ex) {
          LmException.ShowException(ex, "Erro ao Logar no Sistema");

          txtUsuario.Focus();
        }
      }
    }
  }
}
