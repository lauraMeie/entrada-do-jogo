using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoPersonagem : MonoBehaviour
{
    public void EscolherTina()
    {
        GameManager.Instance.personagemEscolhido = "Tina";
        GameManager.Instance.IniciarJogo();
    }

    public void EscolherBeto()
    {
        GameManager.Instance.personagemEscolhido = "Beto";
        GameManager.Instance.IniciarJogo();
    }
}