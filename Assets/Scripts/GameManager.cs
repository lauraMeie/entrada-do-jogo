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
        SceneManager.LoadScene("cenário2");
    }

    public void IniciarJogo()
    {
        SceneManager.LoadScene("cenário3");
    }
    public void AbrirFaseZonaDosLobosCinzentos()
    {
        SceneManager.LoadScene("FaseZonaDosLobosCinzentos");
    }

    public void AbrirFaseZonaDaGrandeQueda()
    {
        SceneManager.LoadScene("FaseZonaDaGrandeQueda");
    }
    public void AbrirFaseMontanhaDaTempestade()
    {
        SceneManager.LoadScene("FaseMontanhaDaTempestade");
    }
    public void AbrirFaseValeDasPlantasVenenosas()
    {
        SceneManager.LoadScene("FaseValeDasPlantasVenenosas");
    }
}
