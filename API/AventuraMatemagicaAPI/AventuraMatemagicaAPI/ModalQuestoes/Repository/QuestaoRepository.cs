using AventuraMatemagicaAPI.ModalQuestoes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Repository
{
    public class QuestaoRepository
    {
        public Questao GetQuestao(int codQuestao, int codCapitulo)
        {
            throw new NotImplementedException();
        }

        internal List<Questao> GetAll()
        {
            return ModalQuestoesMock.GetQuestoes();
        }
    }
}
