using UnityEngine;

public class BotaoClicavel : MonoBehaviour
{
    public PainelController painelController;
    [Header("Objetos para esconder enquanto o conteudo está aberto")]
    public GameObject botaoVerde; // arraste aqui o botão verde que precisa sumir


    private SpriteRenderer spriteRenderer;
    private Vector3 escalaOriginal;
    public float escalaHover = 1.1f; // 10% maior ao passar o mouse

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        escalaOriginal = transform.localScale;
    }

    void OnMouseDown()
    {
        painelController.AbrirImagem();
    }

    void OnMouseEnter()
    {
        transform.localScale = escalaOriginal * escalaHover;
    }

    void OnMouseExit()
    {
        transform.localScale = escalaOriginal;
    }
}