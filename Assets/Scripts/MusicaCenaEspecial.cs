using UnityEngine;

// Coloque este script em um GameObject vazio, SÓ na cena onde você quer uma música diferente.
public class MusicaCenaEspecial : MonoBehaviour
{
    public AudioClip novaMusica; // arraste aqui a música dessa cena específica

    void Start()
    {
        if (ControleMusica.Instance != null)
            ControleMusica.Instance.TocarMusicaTemporaria(novaMusica);
    }

    void OnDestroy()
    {
        // Quando essa cena for descarregada, volta a música padrão
        if (ControleMusica.Instance != null)
            ControleMusica.Instance.VoltarMusicaPadrao();
    }
}