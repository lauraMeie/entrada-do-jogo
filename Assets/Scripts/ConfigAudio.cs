using UnityEngine;
using System;

public static class ConfigAudio
{
    public static event Action Mudou;

    static bool carregado;
    static bool musica = true;
    static bool efeitos = true;

    static void Carregar()
    {
        if (carregado) return;
        musica = PlayerPrefs.GetInt("musica_ligada", 1) == 1;
        efeitos = PlayerPrefs.GetInt("efeitos_ligados", 1) == 1;
        carregado = true;
    }

    public static bool MusicaLigada
    {
        get { Carregar(); return musica; }
        set
        {
            Carregar();
            musica = value;
            PlayerPrefs.SetInt("musica_ligada", value ? 1 : 0);
            PlayerPrefs.Save();
            Mudou?.Invoke();
        }
    }

    public static bool EfeitosLigados
    {
        get { Carregar(); return efeitos; }
        set
        {
            Carregar();
            efeitos = value;
            PlayerPrefs.SetInt("efeitos_ligados", value ? 1 : 0);
            PlayerPrefs.Save();
            Mudou?.Invoke();
        }
    }
}