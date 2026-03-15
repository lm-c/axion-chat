using LmCorbieUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxionChat.VIEW {
  internal static class Program {
    /// <summary>
    /// Ponto de entrada principal para o aplicativo.
    /// </summary>
    [STAThread]
    static void Main() {
      string nomeSistem = "Axion Chat";
      string pastaRaiz = "LM Projetos Data";
      string cliente = "TGM";
      string mail = "lm@lm.app.br";

      ValPadrao.DefinirPadrao(pastaRaiz, nomeSistem, cliente, mail);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      FrmLogin frmLogin = new FrmLogin();
      if (frmLogin.ShowDialog() == DialogResult.OK) {
        Application.Run(new FrmChatMsg());
      }
    }
  }
}
