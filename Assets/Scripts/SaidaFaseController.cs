
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaFaseController : MonoBehaviour
{
    [SerializeField] private GameObject cardTempoEsgotado;
    [SerializeField] private GameObject cardVitoria;
    [SerializeField] private GameObject fundoEscuro;
    [SerializeField] private TipoFase faseAtual;//diz qual é a fase para posteriormente desbloquear a chave para proxima fase
    private bool faseFinalizada = false;

    public static SaidaFaseController Instance { get; private set; }

    void Start()
    {
        EsconderSaida();
    }

public void TempoAcabou()
    {
        if (faseFinalizada) return;

        faseFinalizada = true;

        fundoEscuro.SetActive(true);
        cardTempoEsgotado.SetActive(true);
    }

    public void FaseConcluida(TempoFase temporizador)
    {
        if (faseFinalizada) return;

        faseFinalizada = true;

        temporizador?.SairGame();
        fundoEscuro.SetActive(true);
        cardVitoria.SetActive(true);

        if (faseFinalizada) return;

        faseFinalizada = true;

        Debug.Log("Fase concluída: " + faseAtual);

        fundoEscuro.SetActive(true);
        cardVitoria.SetActive(true);
    }

    public void EsconderSaida()
    {
        fundoEscuro.SetActive(false);
        cardTempoEsgotado.SetActive(false);
        cardVitoria.SetActive(false);
    }
}
