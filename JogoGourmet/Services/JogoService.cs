using JogoGourmet.Model;
using Microsoft.VisualBasic;

namespace JogoGourmet.Services
{
    public class JogoService
    {
        #region Campos e Propriedades

        private readonly ListaPratos _pratosMassa;
        private readonly ListaPratos _pratosNaoMassa;
        private DialogResult _resposta;

        #endregion

        #region Construtor

        public JogoService()
        {
            _pratosMassa = new ListaPratos();
            _pratosNaoMassa = new ListaPratos();
            InicializarPratos();
        }

        #endregion

        #region Métodos Públicos

        public void AdivinharPratos(ListaPratos pratos)
        {
            int contador;
            int tamanhoList = pratos.Pratos.Count - 1;

            for (contador = tamanhoList; contador > 0; contador--)
            {
                _resposta = PerguntaPrato(pratos, contador, true);

                if (_resposta == DialogResult.Yes)
                {
                    _resposta = PerguntaPrato(pratos, contador, false);

                    if (_resposta == DialogResult.Yes)
                    {
                        Acertei();
                        break;
                    }
                    else if ((_resposta == DialogResult.No) && (contador == 0))
                    {
                        AdicionarPrato(pratos, contador);
                        break;
                    }
                }
            }

            if (contador == 0)
            {
                _resposta = PerguntaPrato(pratos, contador, false);

                if (_resposta == DialogResult.Yes)
                {
                    Acertei();
                    return;
                }

                AdicionarPrato(pratos, contador);
            }
        }

        #endregion

        #region Métodos Privados

        private void InicializarPratos()
        {
            // Adiciono os pratos iniciais
            _pratosMassa.Pratos.Add(new Prato("Lasanha", ""));
            _pratosNaoMassa.Pratos.Add(new Prato("Bolo de Chocolate", ""));
        }

        private static void AdicionarPrato(ListaPratos pratos, int ordemPrato)
        {
            pratos.Pratos.Add(MontaObjetoPratoNovo(pratos, ordemPrato));
        }

        private static void Acertei()
        {
            MessageBox.Show("Acertei de novo!", "Acertei", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static DialogResult PerguntaPrato(ListaPratos pratos, int contador, bool caracteristica)
        {
            if (caracteristica)
            {
                return MessageBox.Show($"O prato que pensou é {pratos.Pratos[contador].Caracteristica}?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            return MessageBox.Show($"O prato que pensou é {pratos.Pratos[contador].Descricao}?", "Confirme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private static Prato MontaObjetoPratoNovo(ListaPratos pratos, int ordemPrato)
        {
            string descricaoPrato = Interaction.InputBox("Qual prato você pensou?", "Desisto", "", -1, -1);
            string caracteristicaPrato = Interaction.InputBox($"{descricaoPrato} é ________ mas {pratos.Pratos[ordemPrato].Descricao} não.", "Complete", "", -1, -1);

            Prato prato = new(descricaoPrato, caracteristicaPrato);

            return prato;
        }

        #endregion
    }
}
