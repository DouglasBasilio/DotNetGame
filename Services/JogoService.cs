using JogoGourmet.Model;
using Microsoft.VisualBasic;
using System.IO;

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
                _resposta = PerguntaPrato(pratos, pratos.Pratos.Count() - contador, true);
                var tiposCaracteristicas = pratos.Pratos
                                            .Where(p => !string.IsNullOrEmpty(p.Caracteristica))
                                            .GroupBy(p => p.Caracteristica)
                                            .Count();
                var lstCategorias = pratos.Pratos.Where(p => p.Categoria == pratos.Pratos[contador].Categoria).ToList();

                foreach (var categoria in lstCategorias)
                {
                    if (_resposta == DialogResult.Yes && tiposCaracteristicas > 1)
                    {
                        if (PerguntaPrato(pratos, contador, true) == DialogResult.Yes)
                        {
                            // Verificar todos os pratos com a mesma Caracteristica
                            var pratosComMesmaCaracteristica = pratos.Pratos.Where(p => p.Caracteristica == pratos.Pratos[contador].Caracteristica).ToList();

                            foreach (var prato in pratosComMesmaCaracteristica)
                            {
                                var indicePrato = pratos.Pratos.IndexOf(prato);
                                if (PerguntaPrato(pratos, indicePrato, false) == DialogResult.Yes)
                                {
                                    Acertei();
                                    return;
                                }
                            }

                            // Se nenhum prato foi identificado, adicionar um novo
                            AdicionarPrato(pratos, contador - 1);
                            break;
                        }
                        else if (categoria.Caracteristica == categoria.Caracteristica)
                        {
                            var indicePrato = pratos.Pratos.IndexOf(categoria);
                            var pratosComMesmaCaracteristica = pratos.Pratos.Where(p => p.Caracteristica == pratos.Pratos[indicePrato].Caracteristica).ToList();
                            foreach (var prato in pratosComMesmaCaracteristica)
                            {
                                indicePrato = pratos.Pratos.IndexOf(prato);
                                if (PerguntaPrato(pratos, indicePrato, false) == DialogResult.Yes)
                                {
                                    Acertei();
                                    return;
                                }
                            }

                            AdicionarPrato(pratos, contador - 1);
                            break;
                        };
                    }
                    else if (categoria.Caracteristica == categoria.Caracteristica)
                    {
                        if (_resposta == DialogResult.No)
                        {
                            if (PerguntaPrato(pratos, 0, false) == DialogResult.Yes)
                            {
                                Acertei();
                                return;
                            }

                            AdicionarPrato(pratos, 0);
                            break;

                        }

                        var pratosComMesmaCaracteristica = pratos.Pratos.Where(p => p.Caracteristica == pratos.Pratos[contador].Caracteristica).ToList();
                        foreach (var prato in pratosComMesmaCaracteristica)
                        {
                            var indicePrato = pratos.Pratos.IndexOf(prato);
                            if (PerguntaPrato(pratos, indicePrato, false) == DialogResult.Yes)
                            {
                                Acertei();
                                return;
                            }
                        }

                        AdicionarPrato(pratos, contador);
                        break;
                    }
                }
                break;
            }

            if (contador == 0 && tamanhoList == 0)
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
            _pratosMassa.Pratos.Add(new Prato("Lasanha", "", "Massa"));
            _pratosNaoMassa.Pratos.Add(new Prato("Bolo de Chocolate", "", "Sobremesa"));
        }

        private static void AdicionarPrato(ListaPratos pratos, int ordemPrato)
        {
            string descricaoPrato = Interaction.InputBox("Qual prato você pensou?", "Desisto", "", -1, -1);
            string caracteristicaPrato = Interaction.InputBox($"{descricaoPrato} é __________ mas {pratos.Pratos[ordemPrato].Descricao} não.", "Complete", "", -1, -1);

            // Verifica se a categoria já existe na lista de pratos
            Prato pratoExistente = pratos.Pratos.FirstOrDefault(p => p.Categoria == pratos.Pratos[ordemPrato].Caracteristica);

            // Se a categoria existir, usa essa categoria; caso contrário, usa a categoria atual do prato
            string categoria = (pratoExistente != null) ? pratos.Pratos[ordemPrato].Categoria : caracteristicaPrato;

            Prato pratoNovo = new Prato(descricaoPrato, caracteristicaPrato, categoria);

            // Adiciona o novo prato ao final da lista
            pratos.Pratos.Insert(ordemPrato + 1, pratoNovo);
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

        #endregion
    }
}