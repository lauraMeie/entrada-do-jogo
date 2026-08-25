using UnityEngine;

public class PersonagemVisual : MonoBehaviour
{
    public Sprite spriteTina;
    public Sprite spriteBeto;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Sprite spriteEscolhida = null;

        if (GameManager.Instance.personagemEscolhido == "Tina")
            spriteEscolhida = spriteTina;
        else
            spriteEscolhida = spriteBeto;

        if (spriteEscolhida != null)
        {
            spriteRenderer.sprite = spriteEscolhida;
        }
    }
}