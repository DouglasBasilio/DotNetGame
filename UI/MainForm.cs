using JogoGourmet.Model;
using JogoGourmet.Services;
using System;
using System.Windows.Forms;

namespace JogoGourmet
{
    public partial class MainForm : Form
    {
        #region Campos e Propriedades

        private readonly JogoService jogoService;
        private readonly ListaPratos pratosMassa;
        private readonly ListaPratos pratosNaoMassa;
        private readonly DialogResult _resposta;
        private bool jogoIniciado;

        #endregion

        #region Construtor

        public MainForm()
        {
            InitializeComponent();

            jogoService = new JogoService();
            pratosMassa = new ListaPratos();
            pratosNaoMassa = new ListaPratos();

            InicializarPratos();
            ExibirTelaInicial();
        }

        #endregion

        #region Métodos Privados

        private void InicializarPratos()
        {
            pratosMassa.Pratos.Add(new Prato("Lasanha", "", "Massa"));
            pratosNaoMassa.Pratos.Add(new Prato("Bolo de Chocolate", "", "Sobremesa"));
        }

        private void ExibirTelaInicial()
        {
            if (IsDisposed)
                return;

            var resposta = ExibirMessageBox("Pense em um prato que gosta", "Início do Jogo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (resposta == DialogResult.Cancel)
            {
                Close();
            }
            else
            {
                jogoIniciado = true;
                ExibirProximaPergunta();
            }
        }

        private static DialogResult ExibirMessageBox(string mensagem, string titulo, MessageBoxButtons botoes, MessageBoxIcon icone)
        {
            return MessageBox.Show(mensagem, titulo, botoes, icone);
        }

        public void ExibirProximaPergunta()
        {
            if (!jogoIniciado || IsDisposed)
                return;

            var pergunta = "O prato que você pensou é massa?";
            var resposta = ExibirMessageBox(pergunta);

            if (resposta == DialogResult.Cancel)
            {
                Close();
            }
            else
            {
                var pratos = (resposta == DialogResult.Yes) ? pratosMassa : pratosNaoMassa;
                jogoService.AdivinharPratos(pratos);
                AtualizarInterface();
            }
        }

        private static DialogResult ExibirMessageBox(string pergunta)
        {
            return MessageBox.Show(pergunta, "Confirme", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void AtualizarInterface()
        {
            switch (_resposta)
            {
                case DialogResult.Yes:
                case DialogResult.No:
                    ExibirProximaPergunta();
                    break;
                case DialogResult.Cancel:
                    Close();
                    break;
                default:
                    ExibirTelaInicial();
                    break;
            }
        }

        #endregion
    }
}
