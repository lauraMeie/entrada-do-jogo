using UnityEngine;
using UnityEngine.UI;

public class BotaoSom : MonoBehaviour
{
    public enum Tipo { Musica, Efeitos }

    public Tipo tipo = Tipo.Musica;
    public Image iconeBotao;
    public Sprite iconeLigado;
    public Sprite iconeDesligado;

    void OnEnable()
    {
        ConfigAudio.Mudou += AtualizarIcone;
        AtualizarIcone();
    }

    void OnDisable()
    {
        ConfigAudio.Mudou -= AtualizarIcone;
    }

    void Start()
    {
        AtualizarIcone();
    }

    public void Clicar()
    {
        if (tipo == Tipo.Musica)
            ConfigAudio.MusicaLigada = !ConfigAudio.MusicaLigada;
        else
            ConfigAudio.EfeitosLigados = !ConfigAudio.EfeitosLigados;
    }

    void AtualizarIcone()
    {
        if (iconeBotao == null) return;
        bool ligado = tipo == Tipo.Musica ? ConfigAudio.MusicaLigada : ConfigAudio.EfeitosLigados;
        iconeBotao.sprite = ligado ? iconeLigado : iconeDesligado;
    }
}