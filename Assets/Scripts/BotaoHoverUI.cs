using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Efeito ao passar o mouse")]
    public float escalaHover = 1.1f; // quanto o botão cresce (1.1 = 10% maior)
    public float velocidadeHover = 8f; // quão rápido a transição acontece

    private Vector3 escalaOriginal;
    private Vector3 escalaAlvo;

    void Start()
    {
        escalaOriginal = transform.localScale;
        escalaAlvo = escalaOriginal;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, escalaAlvo, Time.deltaTime * velocidadeHover);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        escalaAlvo = escalaOriginal * escalaHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        escalaAlvo = escalaOriginal;
    }
}