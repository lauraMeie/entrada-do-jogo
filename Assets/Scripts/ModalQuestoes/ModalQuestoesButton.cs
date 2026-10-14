using AventuraMatemagicaAPI.ModalQuestoes.Models;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ModalQuestoes
{
    public class ModalQuestoesButton : MonoBehaviour
    {

        [SerializeField] private Button button;
        public Alternativa Alternativa { get; private set; }

        public event Action<ModalQuestoesButton> OnAlternativaSelecionada;

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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}