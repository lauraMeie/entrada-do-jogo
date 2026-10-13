using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class BotaoPularQuestao : MonoBehaviour
{
    [Header("Configurações de Regra")]
    [SerializeField] private int custoEmTrevos = 3;
    [SerializeField] private float tempoEsperaEntreCliques = 1.0f; // Trava para evitar spam de cliques

    [Header("Eventos")]
    public UnityEvent OnQuestaoPulada;

    private Button botao;
    private bool estaProcessando = false;

    private void Awake()
    {
        botao = GetComponent<Button>();
        botao.onClick.AddListener(TentarPular);
    }

    private void OnDisable()
    {
        // Reseta o estado se o botão for ocultado da tela
        estaProcessando = false;
        if (botao != null) botao.interactable = true;
    }

    public void TentarPular()
    {
        // Validação 1: Impede cliques duplos/seguidos enquanto processa
        if (estaProcessando) return;

        // Validação 2: Checagem temporária de Trevos (3 trevos)
        bool temTrevosSuficientes = true; // TODO: conectar com TrevosManager

        if (!temTrevosSuficientes)
        {
            Debug.LogWarning("[BotaoPularQuestao] Trevos insuficientes para pular.");
            return;
        }

        // Bloqueia novos cliques
        StartCoroutine(RotinaProcessarPulo());
    }

    private IEnumerator RotinaProcessarPulo()
    {
        estaProcessando = true;
        if (botao != null) botao.interactable = false;

        Debug.Log("[BotaoPularQuestao] Questão pulada com sucesso! Disparando evento.");
        OnQuestaoPulada?.Invoke();

        // Aguarda o tempo de segurança antes de reativar
        yield return new WaitForSeconds(tempoEsperaEntreCliques);

        estaProcessando = false;
        if (botao != null) botao.interactable = true;
    }
}