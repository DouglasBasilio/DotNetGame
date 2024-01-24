using System;
using System.Windows.Forms;

namespace JogoGourmet
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                RunGame();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void RunGame()
        {
            using (var form = new MainForm())
            {
                Application.Run(form);
            }
        }

        private static void HandleException(Exception ex)
        {
            var resposta = MessageBox.Show("Deseja realmente sair do jogo?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resposta == DialogResult.Yes || resposta == DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                Main(); // Reinicia o jogo
            }
        }
    }
}
