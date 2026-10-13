using AventuraMatemagicaAPI.ModalQuestoes.Models;
using AventuraMatemagicaAPI.ModalQuestoes.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Services
{
    public class QuestaoService
    {
        private readonly QuestaoRepository _questaoRepository;
        public QuestaoService(QuestaoRepository questaoRepository)
        {
            _questaoRepository = questaoRepository;
        }
        public Questao GetQuestao(int codQuestao, int codCapitulo)
        {
            Questao questao = _questaoRepository.GetQuestao(codQuestao, codCapitulo);
            return questao;
        }
        public async Task<List<Questao>> GetAll()
        {
            return await _questaoRepository.GetAll();
        }
        public async Task<List<Questao>> GetAll(int codCapitulo)
        {
            List<Questao> questoes = await GetAll();
            return questoes.FindAll(q => q.CodCapitulo == codCapitulo);
        }
    }
}
