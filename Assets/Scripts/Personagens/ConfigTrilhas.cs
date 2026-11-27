using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pontos de pergunta de cada fase, medidos em PIXELS da arte de fundo
/// (origem no canto superior esquerdo da imagem, como em qualquer editor de imagem).
/// Os pontos já estão desenhados nas artes (anéis, discos, velas, nós da corda).
///
/// Para ajustar um ponto: abra a imagem de fundo num editor, veja o pixel onde o
/// boneco deve pisar e troque o número aqui. Não depende de onde o fundo está na cena.
/// </summary>
public static class ConfigTrilhas
{
    public class Ponto
    {
        /// <summary>Onde o boneco pisa (pixel da arte).</summary>
        public Vector2 Posicao;
        /// <summary>Pontos intermediários para seguir a trilha desenhada até chegar aqui.</summary>
        public Vector2[] Caminho;
    }

    public class Trilha
    {
        /// <summary>Tamanho da imagem de fundo em pixels.</summary>
        public Vector2 TamanhoImagem;
        /// <summary>Onde o boneco começa a fase.</summary>
        public Vector2 Inicio;
        /// <summary>Um ponto por pergunta, na ordem da trilha.</summary>
        public Ponto[] Pontos;
        /// <summary>Porta de saída, para onde o boneco anda depois da última pergunta.</summary>
        public Ponto Saida;
        /// <summary>Altura do boneco como fração da altura do fundo.</summary>
        public float AlturaPersonagem = 0.15f;
    }

    static Ponto P(float x, float y, params float[] caminho)
    {
        var via = new Vector2[caminho.Length / 2];
        for (int i = 0; i < via.Length; i++)
            via[i] = new Vector2(caminho[i * 2], caminho[i * 2 + 1]);
        return new Ponto { Posicao = new Vector2(x, y), Caminho = via };
    }

    /// <summary>Chave = nome da cena.</summary>
    public static readonly Dictionary<string, Trilha> PorCena = new Dictionary<string, Trilha>
    {
        // Anéis brilhantes na trilha de pedra.
        ["FaseZonaDosLobosCinzentos"] = new Trilha
        {
            TamanhoImagem = new Vector2(1473, 1068),
            Inicio = new Vector2(700, 800),
            Pontos = new[]
            {
                P(708, 703),
                P(560, 578, 705, 790, 580, 825, 420, 822, 300, 780, 262, 712, 320, 650, 440, 615),
                P(731, 451, 625, 545, 640, 480),
                P(1206, 527, 830, 462, 878, 535, 872, 610, 975, 662, 1150, 655, 1275, 605, 1290, 555),
                P(1051, 340, 1100, 470, 975, 425, 935, 375),
            },
            Saida = P(1255, 285, 1150, 322, 1235, 300),
            AlturaPersonagem = 0.17f,
        },

        // Nós da corda, subindo da balsa até a saída.
        ["FaseZonaDaGrandeQueda"] = new Trilha
        {
            TamanhoImagem = new Vector2(1536, 1024),
            Inicio = new Vector2(1100, 880),
            Pontos = new[]
            {
                P(1107, 760),
                P(1107, 662),
                P(1107, 560),
                P(1107, 455),
                P(1107, 345),
            },
            Saida = P(1240, 160, 1107, 190, 1200, 185),
            AlturaPersonagem = 0.14f,
        },

        // Velas acesas no caminho até a porta.
        ["FaseValeDasPlantasVenenosas"] = new Trilha
        {
            TamanhoImagem = new Vector2(1536, 1024),
            Inicio = new Vector2(535, 598),
            Pontos = new[]
            {
                P(590, 560),
                P(918, 610, 760, 600),
                P(1075, 405, 1000, 510),
                P(1220, 300, 1150, 350),
                P(1322, 225, 1270, 262),
            },
            Saida = P(1335, 150),
            AlturaPersonagem = 0.15f,
        },

        // Discos dourados subindo a trilha da montanha.
        ["FaseMontanhaDaTempestade"] = new Trilha
        {
            TamanhoImagem = new Vector2(1280, 856),
            Inicio = new Vector2(640, 790),
            Pontos = new[]
            {
                P(752, 530, 700, 690, 730, 600),
                P(968, 428, 860, 485),
                P(813, 326, 900, 372),
                P(944, 250, 880, 285),
                P(868, 186, 910, 218),
            },
            Saida = P(938, 135, 900, 160),
            AlturaPersonagem = 0.15f,
        },
    };
}
