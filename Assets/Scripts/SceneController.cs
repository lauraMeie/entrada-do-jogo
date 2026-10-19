using AventuraMatemagicaAPI.ModalQuestoes.Models;
using AventuraMatemagicaAPI.ModalQuestoes.Repository;
using AventuraMatemagicaAPI.ModalQuestoes.Services;
using System.Threading;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private ModalQuestoes modalQuestoes;
    [SerializeField]
    private int capitulo;
    private ModalQuestoes modal;
    private FluxoQuestoesFaseService _service;
    private FluxoQuestoesFase FluxoQuestoesFase;

    private void Start()
    {
        _service = new FluxoQuestoesFaseService(
            new QuestaoService(
                new QuestaoRepository()));
        if (FluxoQuestoesFase == null)
            FluxoQuestoesFase = _service.GeraFluxo(capitulo);
        InicializarModal();
        modal.AoOcultar += AoOcultarModal;
        if (FluxoQuestoesFase.QuestaoAtual != null)
        {
            CarregarQuestaoModal(FluxoQuestoesFase.QuestaoAtual);
        }
    }

    private void InicializarModal()
    {
        modal = Instantiate(modalQuestoes);
        modal.RespostaSelecionada += TratarRespostaSelecionada;
        modal.Ocultar();
    }
    private void AoOcultarModal()
    {
        StartCoroutine(proximaQuestaoTeste());
    }
    private System.Collections.IEnumerator proximaQuestaoTeste()
    {
        yield return null;

        Thread.Sleep(5000);
        if (FluxoQuestoesFase.QuestaoAtual != null)
        {
            CarregarQuestaoModal(FluxoQuestoesFase.QuestaoAtual);
        }
    }
    public void CarregarQuestaoModal(Questao questao)
    {
        modal.Exibir(questao);
    }
    public void CarregarProximaQuestaoFluxo()
    {
        FluxoQuestoesFase.QuestaoAtual = FluxoQuestoesFase.ProximaQuestao();
    }
    private void TratarRespostaSelecionada(Alternativa alternativa)
    {
        if(alternativa.Correta)
        {
            CarregarProximaQuestaoFluxo();
            modal.Ocultar();
        }
        else
        {
            Debug.Log("Resposta incorreta!");
        }
    }
    public void PularQuestaoRecebida(int custoTrevos)
    {
        // 1. Validação de segurança: verifica se existe fluxo e questão ativa
        if (FluxoQuestoesFase == null || FluxoQuestoesFase.QuestaoAtual == null)
        {
            Debug.LogWarning("Nenhuma questão ativa encontrada para pular.");
            return;
        }

        // 2. Validação da regra de negócio: verifica se o jogador possui trevos suficientes
        if (!PossuiTrevosSuficientes(custoTrevos))
        {
            Debug.LogWarning($"Saldo insuficiente para pular a questão. Custo necessário: {custoTrevos}.");
            return;
        }

        // 3. Executa o desconto/débito de trevos no serviço de inventário do jogador
        DescontarTrevos(custoTrevos);

        // 4. Aciona a regra de negócio do serviço de fluxo de questões da API
        _service.PularQuestao(FluxoQuestoesFase.QuestaoAtual);

        // 5. Atualiza a interface: avança para a próxima pergunta e oculta o modal
        CarregarProximaQuestaoFluxo();
        modal.Ocultar();
    }

    private bool PossuiTrevosSuficientes(int custo)
    {
        // Interface pronta para integração com a carteira/inventário oficial
        return true;
    }

    private void DescontarTrevos(int custo)
    {
        // Integração para debitar os trevos consumidos no perfil do jogador
    }
}
