using UnityEngine;

using AventuraMatemagicaAPI.ModalQuestoes.Models;
using AventuraMatemagicaAPI.ModalQuestoes.Services;
using AventuraMatemagicaAPI.ModalQuestoes.Repository;
using System.Threading.Tasks;
using TMPro;
using NUnit.Framework;
using Assets.Scripts.ModalQuestoes;
using UnityEngine.UI;
using System;

public class ModalQuestoes : MonoBehaviour
{
    protected Questao QuestaoAtual;
    [SerializeField]
    public ModalQuestoesButton[] AlternativasButtons;

    [SerializeField]
    public TMP_Text DescricaoQuestao;
    [SerializeField]
    public Button PularQuestaoButton;
    public event Action<Alternativa> RespostaSelecionada;
    public event Action AoOcultar;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        foreach (ModalQuestoesButton button in AlternativasButtons)
        {
            button.OnAlternativaSelecionada += TratarAlternativaSelecionada;
        }
        //PularQuestaoButton.OnPulouQuestao += PularQuestao;
        carregaQuestao();
    }

    private void OnDisable()
    {
        foreach (ModalQuestoesButton button in AlternativasButtons)
        {
            button.OnAlternativaSelecionada -= TratarAlternativaSelecionada;
        }
        //PularQuestaoButton.OnPulouQuestao -= PularQuestao;
        AoOcultar?.Invoke();
    }
    private void TratarAlternativaSelecionada(ModalQuestoesButton button)
    {
        RespostaSelecionada?.Invoke(button.Alternativa);
    }

    private void carregaQuestao()
    {
        if (QuestaoAtual == null) return;
        DescricaoQuestao.text = QuestaoAtual.Enunciado;
        foreach (Alternativa alternativa in QuestaoAtual.Alternativas)
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

    public void responderQuestao()
    {

    }

    public void pularQuestao()
    {

    }
    public void Ocultar()
    {
        gameObject.SetActive(false);
    }
    public void Exibir(Questao questao)
    {
        QuestaoAtual = questao;
        gameObject.SetActive(true);
    }
}
