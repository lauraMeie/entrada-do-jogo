using UnityEngine;
using UnityEngine.UI;

public class ControleMusica : MonoBehaviour
{
    public static ControleMusica Instance;

    public AudioSource musicaFundo; // Arraste o objeto "MusicaFundo" aqui
    public Image iconeBotao;        // Opcional: arraste a Image do próprio botão aqui
    public Sprite iconeLigado;      // Opcional: ícone de música ligada
    public Sprite iconeDesligado;   // Opcional: ícone de música desligada

    private bool musicaLigada = true;
    private AudioClip musicaPadrao;

    void Awake()
    {
        // Se já existe uma instância tocando (de outra cena), destrói essa nova e para aqui.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicaFundo != null)
            musicaPadrao = musicaFundo.clip;
    }

    // Chame isso na cena onde quer uma música diferente
    public void TocarMusicaTemporaria(AudioClip novaMusica)
    {
        if (musicaFundo == null || novaMusica == null) return;
        musicaFundo.clip = novaMusica;
        musicaFundo.Play();
    }

    // Chame isso ao sair da cena especial, pra voltar a música normal
    public void VoltarMusicaPadrao()
    {
        if (musicaFundo == null || musicaPadrao == null) return;
        musicaFundo.clip = musicaPadrao;
        musicaFundo.Play();
    }

    public void AlternarMusica()
    {
        musicaLigada = !musicaLigada;

        if (musicaLigada)
        {
            musicaFundo.mute = false;
        }
        else
        {
            musicaFundo.mute = true;
        }

        // Se você tiver ícones diferentes pra ligado/desligado, atualiza aqui
        if (iconeBotao != null && iconeLigado != null && iconeDesligado != null)
        {
            iconeBotao.sprite = musicaLigada ? iconeLigado : iconeDesligado;
        }
    }
}