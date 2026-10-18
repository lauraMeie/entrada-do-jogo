using UnityEngine;

public static class Progresso
{
    public static bool Concluida(string id)
    {
        return PlayerPrefs.GetInt("fase_" + id, 0) == 1;
    }

    public static void Concluir(string id)
    {
        PlayerPrefs.SetInt("fase_" + id, 1);
        PlayerPrefs.Save();
    }
}