using UnityEngine;

public class ControleMusica : MonoBehaviour
{
    public static ControleMusica Instance;

    public AudioSource musicaFundo;
    private AudioClip musicaPadrao;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicaFundo != null)
            musicaPadrao = musicaFundo.clip;

        Aplicar();
    }

    void OnEnable() { ConfigAudio.Mudou += Aplicar; }
    void OnDisable() { ConfigAudio.Mudou -= Aplicar; }

    void Aplicar()
    {
        if (musicaFundo != null)
            musicaFundo.mute = !ConfigAudio.MusicaLigada;
    }

    public void TocarMusicaTemporaria(AudioClip novaMusica)
    {
        if (musicaFundo == null || novaMusica == null) return;
        musicaFundo.clip = novaMusica;
        Aplicar();
        musicaFundo.Play();
    }

    public void VoltarMusicaPadrao()
    {
        if (musicaFundo == null || musicaPadrao == null) return;
        musicaFundo.clip = musicaPadrao;
        Aplicar();
        musicaFundo.Play();
    }
}