using System;
using UnityEngine;
using TMPro;

public class TempoFase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempoText;
    [SerializeField] float tempoMaximo = 300f;

    // Evento necessário para avisar o SceneController que o tempo acabou
    public event Action AoTempoEsgotado;

    float time;
    bool rodando = true;

    void Update()
    {
        if (!rodando)
        {
            return;
        }

        time += Time.deltaTime;

        if (time >= tempoMaximo)
        {
            time = tempoMaximo;
            rodando = false;
            AoTempoEsgotado?.Invoke();
        }

        int minutos = Mathf.FloorToInt(time / 60f);
        int segundos = Mathf.FloorToInt(time % 60f);
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void PausarGame()
    {
        rodando = false;
    }

    public void ContinuarGame()
    {
        rodando = true;
    }

    public void SairGame()
    {
        rodando = false;
        time = 0f;
        tempoText.text = "00:00";
    }

    public void ReiniciarGame()
    {
        rodando = true;
        time = 0f;
        tempoText.text = "00:00";
    }
}