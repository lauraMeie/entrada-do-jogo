using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Models
{
    public class Alternativa
    {
        public int CodAlternativa {  get; set; }
        public int CodQuestão { get; set; }
        public string Value { get; set; }
        public bool Correta { get; set; }
        public Questao Questao { get; set; }
    }
}
