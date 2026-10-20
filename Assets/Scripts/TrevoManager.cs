using System;
using UnityEngine;

public class TrevoManager : MonoBehaviour
{
    public static TrevoManager Instance { get; private set; }

    public event Action<int> AoMudar;
    public int Trevos { get; private set; }

    private const string ChaveSave = "Trevos";

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Trevos = PlayerPrefs.GetInt(ChaveSave, 0);
    }

    // Adiciona trevos
    public void GanharTrevo(int quantidade = 1)
    {
        if (quantidade <= 0) return;

        Trevos += quantidade;
        Salvar();
    }

    // Remove trevos. Retorna true se tinha o suficiente; false se não tinha.
    public bool PerderTrevo(int quantidade = 1)
    {
        if (quantidade <= 0 || Trevos < quantidade) return false;

        Trevos -= quantidade;
        Salvar();
        return true;
    }

    // Confere se o jogador tem trevos suficientes, sem remover
    public bool TemTrevos(int quantidade = 1) => Trevos >= quantidade;

    // Zera a contagem (útil para testes)
    public void Zerar()
    {
        Trevos = 0;
        Salvar();
    }

    private void Salvar()
    {
        PlayerPrefs.SetInt(ChaveSave, Trevos);
        PlayerPrefs.Save();
        AoMudar?.Invoke(Trevos);
    }
}