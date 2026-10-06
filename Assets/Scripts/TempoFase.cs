using UnityEngine;
using TMPro;

public class TempoFase : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempoText;
    [SerializeField] float tempoMaximo = 300f;

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
        }

        int minutos = Mathf.FloorToInt(time / 60f);
        int segundos = Mathf.FloorToInt(time % 60f);
        tempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
