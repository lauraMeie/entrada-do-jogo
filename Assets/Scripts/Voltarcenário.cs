using UnityEngine;
using UnityEngine.SceneManagement;

public class Voltarcenário : MonoBehaviour
{
    public string nomeDaCena = "Entrada do jogo.";


    public void VoltarParaCenario()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
