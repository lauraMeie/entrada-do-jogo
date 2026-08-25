using UnityEngine;

public class MeunuItens : MonoBehaviour
   
{
    [Header("Painel que vai sobrepor a tela")]
    public GameObject Image; // arraste aqui o objeto "Imagediario"

    [Header("Objetos para esconder enquanto o diário está aberto")]
    public GameObject MenuBotoes; // arraste aqui o botão verde que precisa sumir

    [Header("Efeito ao passar o mouse")]
    public float escalaHover = 1.1f; // quanto o ícone cresce (1.1 = 10% maior)
    public float velocidadeHover = 8f; // quão rápido a transição acontece

    private Vector3 escalaOriginal;
    private Vector3 escalaAlvo;
    private SpriteRenderer meuSprite;

    void Start()
    {
        if (Image != null)
            Image.SetActive(false);

        escalaOriginal = transform.localScale;
        escalaAlvo = escalaOriginal;
        meuSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Suaviza a transição de tamanho
        transform.localScale = Vector3.Lerp(transform.localScale, escalaAlvo, Time.deltaTime * velocidadeHover);
    }

    void OnMouseEnter()
    {
        escalaAlvo = escalaOriginal * escalaHover;
    }

    void OnMouseExit()
    {
        escalaAlvo = escalaOriginal;
    }

    // Chamado quando o objeto "diário" (com Box Collider 2D) é clicado
    void OnMouseDown()
    {
        Debug.Log("Cliquei no diário!");
        AbrirPainel();
    }

    public void AbrirPainel()
    {
        if (Image != null)
            Image.SetActive(true);

        if (MenuBotoes != null)
            MenuBotoes.SetActive(false);

        if (meuSprite != null)
            meuSprite.enabled = false;
    }

    public void FecharPainel()
    {
        if (Image != null)
            Image.SetActive(false);

        if (MenuBotoes != null)
            MenuBotoes.SetActive(true);

        if (meuSprite != null)
            meuSprite.enabled = true;
    }
}
