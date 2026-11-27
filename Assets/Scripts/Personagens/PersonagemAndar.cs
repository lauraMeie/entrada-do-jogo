using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Faz o boneco (prefab em Assets/Personagens/Resources/Personagens) andar por uma
/// lista de pontos, com animação feita por código: balança braços e pernas e faz o
/// corpo subir e descer a cada passo. Parado, ele "respira".
///
/// Não usa Animator. As peças são encontradas pelo nome dentro do prefab:
///   Frente  (vista de frente)  -> BracoEsquerdo, BracoDireito, PernaEsquerda, PernaDireita
///   Lado    (vista de lado)    -> BracoLado, Pernalado, PernaTras
///   Corpo   (boneco de imagem única, ex.: Tina) -> balança inteiro
/// Peças que não existem são simplesmente ignoradas.
/// </summary>
public class PersonagemAndar : MonoBehaviour
{
    [Tooltip("Velocidade em alturas do boneco por segundo.")]
    public float velocidade = 1.4f;
    public float passosPorSegundo = 2.2f;
    public float anguloBraco = 22f;
    public float anguloPerna = 16f;
    public float anguloBracoLado = 32f;
    public float anguloPernaLado = 26f;
    public float anguloCorpo = 5f;
    [Tooltip("Quanto o corpo sobe a cada passo, em fração da altura do boneco.")]
    public float alturaPasso = 0.035f;

    public bool Andando { get; private set; }

    class Membro
    {
        public Transform Peca;
        public float RotacaoBase;
        public float Amplitude;
        public float Sinal;
    }

    readonly List<Membro> membros = new List<Membro>();
    Transform frente, lado;
    Vector3 posFrente, posLado, escalaLado;
    float sentidoLado = 1f;
    float alturaMundo = 1f;
    float fase;
    float intensidade;

    void Awake()
    {
        frente = transform.Find("Frente");
        lado = transform.Find("Lado");
        if (frente != null) posFrente = frente.localPosition;
        if (lado != null) { posLado = lado.localPosition; escalaLado = lado.localScale; }

        Adicionar("BracoEsquerdo", anguloBraco, 1f);
        Adicionar("BracoDireito", anguloBraco, -1f);
        Adicionar("PernaEsquerda", anguloPerna, -1f);
        Adicionar("PernaDireita", anguloPerna, 1f);
        Adicionar("BracoLado", anguloBracoLado, 1f);
        Adicionar("Pernalado", anguloPernaLado, -1f);
        Adicionar("PernaTras", anguloPernaLado, 1f);
        Adicionar("Corpo", anguloCorpo, 1f);

        MostrarParado();
    }

    /// <summary>Escala o boneco para ficar com essa altura (unidades de mundo).</summary>
    public void AjustarAltura(float altura)
    {
        transform.localScale = Vector3.one;
        Bounds? caixa = null;
        foreach (var r in GetComponentsInChildren<SpriteRenderer>())
        {
            if (caixa == null) caixa = r.bounds;
            else { var b = caixa.Value; b.Encapsulate(r.bounds); caixa = b; }
        }
        if (caixa != null && caixa.Value.size.y > 0.0001f)
            transform.localScale = Vector3.one * (altura / caixa.Value.size.y);
        alturaMundo = altura;
    }

    /// <summary>Anda pelos pontos em sequência (coordenadas de mundo).</summary>
    public IEnumerator AndarPor(IList<Vector3> caminho)
    {
        Andando = true;
        foreach (var alvo in caminho)
        {
            while (true)
            {
                Vector3 delta = alvo - transform.position;
                delta.z = 0f;
                float passo = velocidade * alturaMundo * Time.deltaTime;
                if (delta.magnitude <= passo)
                {
                    transform.position = new Vector3(alvo.x, alvo.y, transform.position.z);
                    break;
                }
                AtualizarVista(delta);
                transform.position += delta.normalized * passo;
                yield return null;
            }
        }
        Parar();
    }

    public void Parar()
    {
        Andando = false;
        MostrarParado();
    }

    void Adicionar(string nome, float amplitude, float sinal)
    {
        var peca = Buscar(transform, nome);
        if (peca == null) return;
        membros.Add(new Membro
        {
            Peca = peca,
            RotacaoBase = peca.localEulerAngles.z,
            Amplitude = amplitude,
            Sinal = sinal,
        });
    }

    static Transform Buscar(Transform raiz, string nome)
    {
        if (raiz.name == nome) return raiz;
        foreach (Transform filho in raiz)
        {
            var achou = Buscar(filho, nome);
            if (achou != null) return achou;
        }
        return null;
    }

    // Andando na horizontal: vista de lado. Subindo/descendo (ex.: corda): vista de frente.
    void AtualizarVista(Vector3 direcao)
    {
        if (Mathf.Abs(direcao.x) >= Mathf.Abs(direcao.y) * 0.5f) MostrarLado(direcao.x);
        else MostrarFrente();
    }

    void MostrarParado()
    {
        if (frente != null) MostrarFrente();
        else MostrarLado(sentidoLado);
    }

    void MostrarFrente()
    {
        if (frente == null) { if (lado != null) MostrarLado(sentidoLado); return; }
        frente.gameObject.SetActive(true);
        if (lado != null) lado.gameObject.SetActive(false);
    }

    void MostrarLado(float direcaoX)
    {
        if (lado == null) { if (frente != null) MostrarFrente(); return; }
        if (Mathf.Abs(direcaoX) > 0.0001f) sentidoLado = Mathf.Sign(direcaoX);
        lado.gameObject.SetActive(true);
        if (frente != null) frente.gameObject.SetActive(false);
        // As artes de lado olham para a direita; para a esquerda, espelha.
        lado.localScale = new Vector3(Mathf.Abs(escalaLado.x) * sentidoLado, escalaLado.y, escalaLado.z);
    }

    void LateUpdate()
    {
        intensidade = Mathf.MoveTowards(intensidade, Andando ? 1f : 0f, Time.deltaTime * 6f);
        if (Andando) fase += Time.deltaTime * passosPorSegundo * Mathf.PI;

        float balanco = Mathf.Sin(fase) * intensidade;
        foreach (var m in membros)
            m.Peca.localRotation = Quaternion.Euler(0f, 0f, m.RotacaoBase + balanco * m.Amplitude * m.Sinal);

        // Sobe e desce a cada passo; parado, respira de leve.
        float subida = Mathf.Abs(Mathf.Sin(fase)) * alturaPasso * intensidade
                     + Mathf.Sin(Time.time * 2f) * 0.006f * (1f - intensidade);
        float escala = Mathf.Approximately(transform.localScale.y, 0f) ? 1f : transform.localScale.y;
        float dy = subida * alturaMundo / escala;

        if (frente != null) frente.localPosition = posFrente + new Vector3(0f, dy, 0f);
        if (lado != null) lado.localPosition = new Vector3(posLado.x * sentidoLado, posLado.y + dy, posLado.z);
    }
}
