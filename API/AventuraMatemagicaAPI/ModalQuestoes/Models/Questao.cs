using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Models
{
    public class Questao
    {
        public int CodQuestao {  get; set; }
        public int CodCapitulo { get; set; }
        public string Enunciado { get; set; }
        public TimeSpan TempoResposta { get; set; }
        public Alternativa AlternativaSelecionada { get; set; }
        public bool Respondida 
        { 
            get 
            { 
                return AlternativaSelecionada != null; 
            } 
        }
        public List<Alternativa> Alternativas { get; set; }
    }
}
