using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EfeitoMudo : MonoBehaviour
{
    AudioSource fonte;

    void Awake()
    {
        fonte = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        ConfigAudio.Mudou += Aplicar;
        Aplicar();
    }

    void OnDisable()
    {
        ConfigAudio.Mudou -= Aplicar;
    }

    void Aplicar()
    {
        fonte.mute = !ConfigAudio.EfeitosLigados;
    }
}