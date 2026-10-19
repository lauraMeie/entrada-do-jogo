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

    }
}
