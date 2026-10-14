namespace AventuraMatemagicaAPI
{
    public class Utils
    {
        public static string GetScenesFromEnum(scenes scene)
        {
            switch (scene)
            {
                case scenes.entradaJogo:
                    return "Entrada do jogo.";
                case scenes.selecaoPersonagem:
                    return "cenário2";
                case scenes.mapaPrincipal:
                    return "cenário3";
                case scenes.zonaDosLobosCinzentos:
                    return "FaseZonaDosLobosCinzentos";
                case scenes.zonaDaGrandeQueda:
                    return "FaseZonaDaGrandeQueda";
                case scenes.montanhaDaTempestade:
                    return "FaseMontanhaDaTempestade";
                case scenes.valeDasPlantasVenenosas:
                    return "FaseValeDasPlantasVenenosas";
                default:
                    return string.Empty;
            }
        }
    }
}
