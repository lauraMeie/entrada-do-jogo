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
}
