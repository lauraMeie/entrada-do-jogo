using AventuraMatemagicaAPI.ModalQuestoes.Models;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ModalQuestoes
{
    public class ModalQuestoesButton : MonoBehaviour
    {

        [SerializeField] private Button button;
        [SerializeField] private string textoAlternativa;
        [SerializeField] private TMP_Text textoDescricao;
        public Alternativa Alternativa { get; private set; }
        public string AlternativaDesc => textoAlternativa;
        public string Descricao { get; private set; }

        public event Action<ModalQuestoesButton> OnAlternativaSelecionada;

        private void SetDescricao(string descricao)
        {
            Descricao = descricao;
            textoDescricao.text = descricao;
        }
        public void SetAlternativa(Alternativa alternativa)
        {
            Alternativa = alternativa;
            SetDescricao(alternativa.Descricao);
        }

        private void Awake()
        {
            button.onClick.AddListener(SelecionarAlternativa);
        }

        private void SelecionarAlternativa()
        {
            OnAlternativaSelecionada?.Invoke(this);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(SelecionarAlternativa);
        }
    }
}