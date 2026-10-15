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

    public void GanharTrevo(int quantidade = 1)
    {
        Trevos += quantidade;
        PlayerPrefs.SetInt(ChaveSave, Trevos);
        PlayerPrefs.Save();
        AoMudar?.Invoke(Trevos);
    }
}