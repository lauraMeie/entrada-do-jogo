using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Anel dourado pulsante e clicável sobre o próximo ponto da trilha.
/// O sprite do anel é gerado por código (não precisa de arte).
/// </summary>
public class MarcadorPonto : MonoBehaviour
{
    static Sprite spriteAnel;
    static readonly List<RaycastResult> acertosUI = new List<RaycastResult>();

    Action aoClicar;
    SpriteRenderer render;
    Vector3 escalaBase;

    public static MarcadorPonto Criar(Action aoClicar, Vector3 posicao, float diametro, int camada, int ordem)
    {
        var go = new GameObject("MarcadorPonto");
        go.transform.position = posicao;

        var marcador = go.AddComponent<MarcadorPonto>();
        marcador.aoClicar = aoClicar;
        marcador.render = go.AddComponent<SpriteRenderer>();
        marcador.render.sprite = Anel();
        marcador.render.sortingLayerID = camada;
        marcador.render.sortingOrder = ordem;

        // O sprite do anel tem 1 unidade de largura.
        marcador.escalaBase = Vector3.one * diametro;
        go.transform.localScale = marcador.escalaBase;

        // Área de clique um pouco maior que o anel, para facilitar no touch.
        go.AddComponent<CircleCollider2D>().radius = 0.6f;
        return marcador;
    }

    void Update()
    {
        float pulso = Mathf.Sin(Time.time * 4f) * 0.5f + 0.5f;
        transform.localScale = escalaBase * (0.9f + 0.2f * pulso);
        render.color = new Color(1f, 1f, 1f, 0.55f + 0.45f * pulso);
    }

    void OnMouseDown()
    {
        if (CliqueEmBotaoDaInterface()) return;
        aoClicar?.Invoke();
    }

    // Não deixa o clique "atravessar" um botão da interface que esteja por cima.
    static bool CliqueEmBotaoDaInterface()
    {
        var eventos = EventSystem.current;
        if (eventos == null) return false;

        var dados = new PointerEventData(eventos) { position = Input.mousePosition };
        acertosUI.Clear();
        eventos.RaycastAll(dados, acertosUI);
        foreach (var acerto in acertosUI)
            if (acerto.gameObject.GetComponentInParent<Selectable>() != null) return true;
        return false;
    }

    static Sprite Anel()
    {
        if (spriteAnel != null) return spriteAnel;

        const int tamanho = 128;
        var textura = new Texture2D(tamanho, tamanho, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Bilinear,
        };
        var pixels = new Color32[tamanho * tamanho];
        float centro = (tamanho - 1) / 2f;
        for (int y = 0; y < tamanho; y++)
        {
            for (int x = 0; x < tamanho; x++)
            {
                float d = Vector2.Distance(new Vector2(x, y), new Vector2(centro, centro)) / centro;
                float anel = Mathf.Exp(-Mathf.Pow((d - 0.78f) / 0.08f, 2f));
                float brilho = 0.35f * Mathf.Exp(-Mathf.Pow((d - 0.78f) / 0.2f, 2f));
                float alfa = d > 1f ? 0f : Mathf.Clamp01(anel + brilho);
                pixels[y * tamanho + x] = new Color(1f, 0.86f, 0.35f, alfa);
            }
        }
        textura.SetPixels32(pixels);
        textura.Apply();
        spriteAnel = Sprite.Create(textura, new Rect(0, 0, tamanho, tamanho), new Vector2(0.5f, 0.5f), tamanho);
        return spriteAnel;
    }
}
