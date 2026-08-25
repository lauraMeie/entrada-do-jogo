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
}