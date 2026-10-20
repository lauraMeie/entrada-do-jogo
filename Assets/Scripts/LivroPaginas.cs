using UnityEngine;
using System.Collections;

public class LivroPaginas : MonoBehaviour
{
    public GameObject[] spreads;      // cada item = um par de páginas (1/2, 2/2...)
    public RectTransform paginaVirando;
    public float duracao = 0.5f;

    int atual = 0;
    bool virando = false;

    void OnEnable()
    {
        atual = 0;
        virando = false;
        paginaVirando.gameObject.SetActive(false);
        Mostrar();
    }

    void Mostrar()
    {
        for (int i = 0; i < spreads.Length; i++)
            spreads[i].SetActive(i == atual);
    }

    public void Proxima()
    {
        if (!virando && atual < spreads.Length - 1)
            StartCoroutine(Virar(1));
    }

    public void Anterior()
    {
        if (!virando && atual > 0)
            StartCoroutine(Virar(-1));
    }

    IEnumerator Virar(int direcao)
    {
        virando = true;
        paginaVirando.gameObject.SetActive(true);

        float t = 0;
        bool trocou = false;

        while (t < 1)
        {
            t += Time.deltaTime / duracao;
            float x = Mathf.Lerp(1f, -1f, t);
            paginaVirando.localScale = new Vector3(x, 1, 1);

            if (!trocou && t >= 0.5f)
            {
                atual += direcao;
                Mostrar();
                trocou = true;
            }
            yield return null;
        }

        paginaVirando.localScale = Vector3.one;
        paginaVirando.gameObject.SetActive(false);
        virando = false;
    }
}