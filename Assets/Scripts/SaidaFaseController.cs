
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaidaFaseController : MonoBehaviour
{
    [SerializeField] private GameObject cardTempoEsgotado;
    [SerializeField] private GameObject cardVitoria;
    [SerializeField] private GameObject fundoEscuro;

    [SerializeField] private TempoFase temporizador;
    [SerializeField] private TipoFase faseAtual;//diz qual é a fase para posteriormente desbloquear a chave para proxima fase 
    private ConclusaoFaseTeste conclusaoTeste; //teste de respostas para card de respostas corretas


    private bool faseFinalizada = false;

    void Start()
    {
        EsconderSaida();
        ConectarTemporizador();
        ConectarConclusao();


    }

    void Update()
    {
        if (temporizador == null)
        {
            ConectarTemporizador();
        }
    }
    void OnDestroy()
{
    if (temporizador != null)
    {
        temporizador.AoTempoEsgotado -= TempoAcabou;
    }
        if (conclusaoTeste != null)
        {
            conclusaoTeste.AoTodasRespostasCorretas -= FaseConcluida;
        }
    }

public void TempoAcabou()
    {
        if (faseFinalizada) return;

        faseFinalizada = true;

        fundoEscuro.SetActive(true);
        cardTempoEsgotado.SetActive(true);
    }

    public void FaseConcluida()
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

    private void ConectarTemporizador()
    {
        if (temporizador != null) return;

        temporizador = FindAnyObjectByType<TempoFase>(
            FindObjectsInactive.Include
        );

        if (temporizador == null)
        {
            return;
        }

        temporizador.AoTempoEsgotado += TempoAcabou;
    }
    private void ConectarConclusao()
    {
        conclusaoTeste = FindAnyObjectByType<ConclusaoFaseTeste>();

        if (conclusaoTeste == null)
        {
            Debug.LogWarning("Evento de conclusão não encontrado!");
            return;
        }

        conclusaoTeste.AoTodasRespostasCorretas += FaseConcluida;
    }

}
