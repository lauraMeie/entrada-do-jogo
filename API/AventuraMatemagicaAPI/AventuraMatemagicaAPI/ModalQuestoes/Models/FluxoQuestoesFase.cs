using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Models
{
    public class FluxoQuestoesFase
    {
        public Questao QuestaoAtual { get; set; }
        public List<Questao> Questoes { get; set; }
        public int QtdQuestoes { get; set; }
        public int qtdTrevos { get; set; }

        public Questao ProximaQuestao()
        {
            if (Questoes == null || Questoes.Count == 0)
                return null;
            int indexAtual = Questoes.IndexOf(QuestaoAtual);
            if (indexAtual + 1 >= Questoes.Count)
                return null;
            indexAtual = indexAtual == null ? 0 : indexAtual;
            return Questoes[indexAtual + 1];
        }
    }
}
