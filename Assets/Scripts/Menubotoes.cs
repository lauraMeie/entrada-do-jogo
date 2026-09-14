using UnityEngine;

public class MenuBotoes : MonoBehaviour
{
   
    public void OnClickPlay()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IniciarJogo();
        }
        else
        {
            Debug.LogWarning("GameManager.Instance ainda não existe!");
        }
    }

    public void OnClickEscolhaPersonagem()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AbrirEscolhaPersonagem();
        }
    }
    public void OnClickFaseZonaDosLobosCinzentos()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AbrirFaseZonaDosLobosCinzentos();
        }
    }
    public void OnClickFaseZonaDaGrandeQueda()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AbrirFaseZonaDaGrandeQueda();
        }
    }
    public void OnClickFaseMontanhaDaTempestade()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AbrirFaseMontanhaDaTempestade();
        }
    }
    public void OnClickFaseValeDasPlantasVenenosas()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AbrirFaseValeDasPlantasVenenosas();
        }
    }
}