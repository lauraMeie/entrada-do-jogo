using AventuraMatemagicaAPI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string personagemEscolhido;
   
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AbrirEscolhaPersonagem()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.selecaoPersonagem));
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.mapaPrincipal));
    }
    public void AbrirFaseZonaDosLobosCinzentos()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.zonaDosLobosCinzentos));
    }

    public void AbrirFaseZonaDaGrandeQueda()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.zonaDaGrandeQueda));
    }
    public void AbrirFaseMontanhaDaTempestade()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.montanhaDaTempestade));
    }
    public void AbrirFaseValeDasPlantasVenenosas()
    {
        SceneManager.LoadScene(Utils.GetScenesFromEnum(scenes.valeDasPlantasVenenosas));
    }
}
