using AventuraMatemagicaAPI.ModalQuestoes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AventuraMatemagicaAPI.ModalQuestoes.Repository
{
    internal static class ModalQuestoesMock
    {
        public static List<Questao> GetQuestoes()
        {
            return new List<Questao>
            {
                CriaQuestao(
                    codQuestao: 1,
                    codCapitulo: 1,
                    enunciado: "Quanto é 7 + 5?",
                    tempoResposta: TimeSpan.FromSeconds(30),
                    letraCorreta: "C",
                    descricoes: new[] { "10", "11", "12", "13" }),

                CriaQuestao(
                    codQuestao: 2,
                    codCapitulo: 1,
                    enunciado: "Quanto é 9 - 4?",
                    tempoResposta: TimeSpan.FromSeconds(30),
                    letraCorreta: "B",
                    descricoes: new[] { "4", "5", "6", "7" }),

                CriaQuestao(
                    codQuestao: 3,
                    codCapitulo: 1,
                    enunciado: "Quanto é 6 x 3?",
                    tempoResposta: TimeSpan.FromSeconds(30),
                    letraCorreta: "D",
                    descricoes: new[] { "9", "12", "15", "18" }),

                CriaQuestao(
                    codQuestao: 4,
                    codCapitulo: 1,
                    enunciado: "Quanto é 20/4?",
                    tempoResposta: TimeSpan.FromSeconds(30),
                    letraCorreta: "A",
                    descricoes: new[] { "5", "6", "8", "10" }),

                CriaQuestao(
                    codQuestao: 5,
                    codCapitulo: 1,
                    enunciado: "Qual é o próximo número da sequência: 2, 4, 6, 8, ...?",
                    tempoResposta: TimeSpan.FromSeconds(30),
                    letraCorreta: "C",
                    descricoes: new[] { "9", "11", "10", "12" }),

                CriaQuestao(
                    codQuestao: 6,
                    codCapitulo: 2,
                    enunciado: "Quanto é 15 + 27?",
                    tempoResposta: TimeSpan.FromSeconds(45),
                    letraCorreta: "B",
                    descricoes: new[] { "32", "42", "52", "41" }),

                CriaQuestao(
                    codQuestao: 7,
                    codCapitulo: 2,
                    enunciado: "Quanto é 100 - 37?",
                    tempoResposta: TimeSpan.FromSeconds(45),
                    letraCorreta: "D",
                    descricoes: new[] { "53", "77", "73", "63" }),

                CriaQuestao(
                    codQuestao: 8,
                    codCapitulo: 2,
                    enunciado: "Quanto é 12 x 12?",
                    tempoResposta: TimeSpan.FromSeconds(45),
                    letraCorreta: "A",
                    descricoes: new[] { "144", "124", "154", "134" }),

                CriaQuestao(
                    codQuestao: 9,
                    codCapitulo: 2,
                    enunciado: "Qual é a metade de 50?",
                    tempoResposta: TimeSpan.FromSeconds(45),
                    letraCorreta: "C",
                    descricoes: new[] { "20", "30", "25", "15" }),

                CriaQuestao(
                    codQuestao: 10,
                    codCapitulo: 2,
                    enunciado: "Quanto é 8²?",
                    tempoResposta: TimeSpan.FromSeconds(45),
                    letraCorreta: "B",
                    descricoes: new[] { "16", "64", "56", "72" }),
            };
        }

        private static Questao CriaQuestao(
            int codQuestao,
            int codCapitulo,
            string enunciado,
            TimeSpan tempoResposta,
            string letraCorreta,
            string[] descricoes)
        {
            Questao questao = new Questao
            {
                CodQuestao = codQuestao,
                CodCapitulo = codCapitulo,
                Enunciado = enunciado,
                TempoResposta = tempoResposta,
                Alternativas = new List<Alternativa>()
            };

            string[] letras = { "A", "B", "C", "D" };
            for (int i = 0; i < descricoes.Length; i++)
            {
                questao.Alternativas.Add(new Alternativa
                {
                    CodAlternativa = (codQuestao * 10) + i + 1,
                    CodQuestão = codQuestao,
                    Letra = letras[i],
                    Descricao = descricoes[i],
                    Correta = letras[i] == letraCorreta,
                    Questao = questao
                });
            }

            return questao;
        }
    }
}
