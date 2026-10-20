using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Efeito ao passar o mouse")]
    public float escalaHover = 1.1f;    // 1.1 = cresce 10%
    public float velocidadeHover = 8f;  // velocidade da transição

    private Vector3 escalaOriginal;
    private Vector3 escalaAlvo;

    void Awake()
    {
        escalaOriginal = transform.localScale;
        escalaAlvo = escalaOriginal;
    }

    void OnEnable()
    {
        transform.localScale = escalaOriginal;
        escalaAlvo = escalaOriginal;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            escalaAlvo,
            Time.unscaledDeltaTime * velocidadeHover);
    }

    // Modelo UI (Image + Button)
    public void OnPointerEnter(PointerEventData eventData) { escalaAlvo = escalaOriginal * escalaHover; }
    public void OnPointerExit(PointerEventData eventData) { escalaAlvo = escalaOriginal; }

    // Modelo sprite com Collider 2D
    void OnMouseEnter() { escalaAlvo = escalaOriginal * escalaHover; }
    void OnMouseExit() { escalaAlvo = escalaOriginal; }
}