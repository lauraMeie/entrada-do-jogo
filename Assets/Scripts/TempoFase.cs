
using System;
using UnityEngine;
using TMPro;

public class TempoFase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempoText;
    [SerializeField] float tempoMaximo = 10f;

    public event Action AoTempoEsgotado;

    float time;
    bool rodando = true;

    void Start()
    {
        time = tempoMaximo;
        AtualizarTexto();
    }

    void Update()
    {
        if (!rodando)
        {
            return;
        }

        // Diminui o tempo a cada segundo
        time -= Time.deltaTime;

        if (time <= 0f)
        {
            time = 0f;
            rodando = false;

            AtualizarTexto();

            AoTempoEsgotado?.Invoke();
            return;
        }

        AtualizarTexto();
    }

    void AtualizarTexto()
    {
        int minutos = Mathf.FloorToInt(Mathf.Ceil(time) / 60f);
        int segundos = Mathf.CeilToInt(time) % 60;

        tempoText.text = string.Format(
            "{0:00}:{1:00}", minutos, segundos
        );
    }

    public void SairGame()
    {
        rodando = false;
        time = 0f;
        AtualizarTexto();
    }

    public TimeSpan GetTempoAtual()
    {
        return TimeSpan.FromSeconds(time);
    }
}
