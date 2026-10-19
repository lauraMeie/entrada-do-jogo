using System;
using UnityEngine;
using TMPro;

public class TempoFase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempoText;
    [SerializeField] float tempoMaximo = 300f;

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

    void PausarGame()
    {
        rodando = false;
    }

    void ContinuarGame()
    {
        rodando = true;
    }

    public void SairGame()
    {
        rodando = false;
        time = 0f;
        tempoText.text = "00:00";
    }

    void ReiniciarGame()
    {
        rodando = true;
        time = 0f;
        tempoText.text = "00:00";
    }
}