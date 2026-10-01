using AventuraMatemagicaAPI.ModalQuestoes.Models;
using AventuraMatemagicaAPI.ModalQuestoes.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Services
{
    public class FluxoQuestoesFaseService
    {
        private readonly QuestaoService _questaoService;

        public FluxoQuestoesFaseService(QuestaoService questaoService)
        {
            _questaoService = questaoService;
        }

        public async Task<FluxoQuestoesFase> GeraFluxo(int codCapitulo)
        {
            FluxoQuestoesFase fluxoQuestoesFase = new FluxoQuestoesFase();
            fluxoQuestoesFase.Questoes = await _questaoService.GetAll(codCapitulo);
            return fluxoQuestoesFase;
        }
        public void ResponderQuestao(Questao questao, Alternativa alternativaSelecionada)
        {
            questao.AlternativaSelecionada = alternativaSelecionada;
        }
        public void PularQuestao(Questao questao)
        {
            Alternativa alternativaCorreta = questao.Alternativas.Find(a => a.Correta);
            ResponderQuestao(questao, alternativaCorreta);
        }
    }
}
