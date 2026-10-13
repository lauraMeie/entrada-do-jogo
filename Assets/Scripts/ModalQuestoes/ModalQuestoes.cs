using UnityEngine;

using AventuraMatemagicaAPI.ModalQuestoes.Models;
using AventuraMatemagicaAPI.ModalQuestoes.Services;
using AventuraMatemagicaAPI.ModalQuestoes.Repository;
using System.Threading.Tasks;
using TMPro;
using NUnit.Framework;
using Assets.Scripts.ModalQuestoes;
using UnityEngine.UI;

public class ModalQuestoes : MonoBehaviour
{
    private FluxoQuestoesFaseService _service;
    private FluxoQuestoesFase FluxoQuestoesFase;

    [SerializeField]
    public ModalQuestoesButton[] AlternativasButtons;

    [SerializeField]
    public TMP_Text DescricaoQuestao;
    [SerializeField]
    public Button PularQuestaoButton;

    private void Awake()
    {
        _service = new FluxoQuestoesFaseService(
            new QuestaoService(
                new QuestaoRepository()));
        if (FluxoQuestoesFase == null)
            FluxoQuestoesFase = _service.GeraFluxo(1);
    }

    private void OnEnable()
    {
        foreach (ModalQuestoesButton button in AlternativasButtons)
        {
            button.OnAlternativaSelecionada += TratarAlternativaSelecionada;
        }
        //PularQuestaoButton.OnPulouQuestao += PularQuestao;
        carregaProximaQuestao();
    }

    private void OnDisable()
    {
        foreach (ModalQuestoesButton button in AlternativasButtons)
        {
            button.OnAlternativaSelecionada -= TratarAlternativaSelecionada;
        }
        //PularQuestaoButton.OnPulouQuestao -= PularQuestao;
    }
    private void TratarAlternativaSelecionada(ModalQuestoesButton button)
    {
        if (button.Alternativa.Correta)
        {
            carregaProximaQuestao();
        }
        else
        {
            Debug.Log("Resposta incorreta!");
        }
    }
    public async Task carregaProximaQuestao() 
    {
        FluxoQuestoesFase.QuestaoAtual = FluxoQuestoesFase.ProximaQuestao();
        DescricaoQuestao.text = FluxoQuestoesFase.QuestaoAtual.Enunciado;
        foreach (Alternativa alternativa in FluxoQuestoesFase.QuestaoAtual.Alternativas)
        {
            foreach (ModalQuestoesButton button in AlternativasButtons)
            {
                if (button.AlternativaDesc == alternativa.Letra)
                {
                    button.SetAlternativa(alternativa);
                }
            }
        }
    }

    public async Task responderQuestao()
    {

    }

    public async Task pularQuestao()
    {

    }
}
