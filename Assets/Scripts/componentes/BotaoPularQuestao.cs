using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;// Criamos um evento personalizado que permite enviar um número (int)

[System.Serializable] public class EventoPularQuestao : UnityEvent<int> { }



[RequireComponent(typeof(Button))]
public class BotaoPularQuestao : MonoBehaviour

{

    [Header("Configurações de Regra")]

    [SerializeField] private int custoEmTrevos = 3;

    [SerializeField] private float tempoEsperaEntreCliques = 1.0f;



    [Header("Eventos")]

    // Agora o evento envia o custoEmTrevos para o SceneController/Service

    public EventoPularQuestao OnQuestaoPulada;



    private Button botao;

    private bool estaProcessando = false;



    private void Awake()

    {

        botao = GetComponent<Button>();

        botao.onClick.AddListener(TentarPular);

    }



    private void OnDisable()

    {

        estaProcessando = false;

        if (botao != null) botao.interactable = true;

    }



    public void TentarPular()

    {

        if (estaProcessando) return;

        StartCoroutine(RotinaProcessarPulo());

    }



    private IEnumerator RotinaProcessarPulo()

    {

        estaProcessando = true;

        if (botao != null) botao.interactable = false;



        // Apenas dispara o evento enviando o "3". A validação ocorre fora daqui.

        OnQuestaoPulada?.Invoke(custoEmTrevos);



        yield return new WaitForSeconds(tempoEsperaEntreCliques);



        estaProcessando = false;

        if (botao != null) botao.interactable = true;

    }

}