using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// Trilha de perguntas da fase: coloca o personagem escolhido no início, marca o
/// próximo ponto (anel pulsante) e, quando o jogador clica nele, faz o boneco andar
/// até lá e avisa o SceneController para abrir a pergunta.
///
/// É criada por código pelo SceneController (TrilhaQuestoes.Criar), usando os pontos
/// de ConfigTrilhas. Cenas sem configuração seguem o fluxo antigo (pergunta direto).
/// </summary>
public class TrilhaQuestoes : MonoBehaviour
{
    /// <summary>O boneco chegou ao ponto da pergunta atual.</summary>
    public event Action ChegouNoPonto;

    ConfigTrilhas.Trilha config;
    Bounds fundo;
    int camadaFundo;
    int ordemPersonagem;
    PersonagemAndar personagem;
    MarcadorPonto marcador;
    int proximoPonto;
    bool bloqueada;

    /// <summary>Cria a trilha da cena atual ou devolve null se a cena não tiver trilha.</summary>
    public static TrilhaQuestoes Criar()
    {
        string cena = SceneManager.GetActiveScene().name;
        if (!ConfigTrilhas.PorCena.TryGetValue(cena, out var config))
        {
            Debug.Log($"[TrilhaQuestoes] Cena '{cena}' sem trilha configurada; perguntas abrem direto.");
            return null;
        }
        Debug.Log($"[TrilhaQuestoes] Iniciando trilha da cena '{cena}'.");

        var fundo = EncontrarFundo(config);
        if (fundo == null)
        {
            Debug.LogWarning($"[TrilhaQuestoes] Fundo não encontrado na cena {cena}; seguindo sem trilha.");
            return null;
        }

        var trilha = new GameObject("TrilhaQuestoes").AddComponent<TrilhaQuestoes>();
        if (!trilha.Iniciar(config, fundo))
        {
            Destroy(trilha.gameObject);
            return null;
        }
        return trilha;
    }

    // O fundo da fase é o sprite da PRÓPRIA cena cuja imagem tem o tamanho configurado.
    // (Ignora sprites que vêm de outras cenas, como o HUD em DontDestroyOnLoad.)
    // Se nenhum bater, usa o maior sprite da cena.
    static SpriteRenderer EncontrarFundo(ConfigTrilhas.Trilha cfg)
    {
        var cena = SceneManager.GetActiveScene();
        SpriteRenderer exato = null, maior = null;
        float maiorArea = 0f;
        foreach (var sr in FindObjectsByType<SpriteRenderer>())
        {
            if (sr.sprite == null || sr.gameObject.scene != cena) continue;

            var rect = sr.sprite.rect;
            if (Mathf.Abs(rect.width - cfg.TamanhoImagem.x) < 2f && Mathf.Abs(rect.height - cfg.TamanhoImagem.y) < 2f)
                exato = sr;

            float area = sr.bounds.size.x * sr.bounds.size.y;
            if (area > maiorArea) { maiorArea = area; maior = sr; }
        }
        var escolhido = exato != null ? exato : maior;
        if (escolhido != null)
            Debug.Log($"[TrilhaQuestoes] Fundo: '{escolhido.name}' ({(exato != null ? "tamanho exato" : "maior sprite")}), área no mundo {escolhido.bounds.size}.");
        return escolhido;
    }

    bool Iniciar(ConfigTrilhas.Trilha cfg, SpriteRenderer fundoRenderer)
    {
        config = cfg;
        fundo = fundoRenderer.bounds;
        camadaFundo = fundoRenderer.sortingLayerID;

        string nome = "Beto";
        if (GameManager.Instance != null && !string.IsNullOrEmpty(GameManager.Instance.personagemEscolhido))
            nome = GameManager.Instance.personagemEscolhido;

        var prefab = Resources.Load<GameObject>("Personagens/" + nome);
        if (prefab == null) prefab = Resources.Load<GameObject>("Personagens/Beto");
        if (prefab == null)
        {
            Debug.LogWarning("[TrilhaQuestoes] Prefab de personagem não encontrado em Resources/Personagens.");
            return false;
        }

        var boneco = Instantiate(prefab, ParaMundo(config.Inicio), Quaternion.identity);
        boneco.name = prefab.name;
        // Logo acima do fundo e ABAIXO do modal de perguntas (Canvas em World Space, ordem 2)
        // e do botão de pular (ordem 3); assim a pergunta sempre cobre o boneco.
        ordemPersonagem = fundoRenderer.sortingOrder + 1;
        var grupo = boneco.AddComponent<SortingGroup>();
        grupo.sortingLayerID = camadaFundo;
        grupo.sortingOrder = ordemPersonagem;

        personagem = boneco.AddComponent<PersonagemAndar>();
        personagem.AjustarAltura(fundo.size.y * config.AlturaPersonagem);
        Debug.Log($"[TrilhaQuestoes] Personagem '{boneco.name}' criado em {boneco.transform.position} (escala {boneco.transform.localScale.x:0.###}).");
        return true;
    }

    /// <summary>Converte um pixel da arte de fundo em posição de mundo.</summary>
    public Vector3 ParaMundo(Vector2 pixel)
    {
        return new Vector3(
            fundo.min.x + pixel.x / config.TamanhoImagem.x * fundo.size.x,
            fundo.max.y - pixel.y / config.TamanhoImagem.y * fundo.size.y,
            fundo.center.z);
    }

    /// <summary>Marca o próximo ponto e espera o clique do jogador.</summary>
    public void LiberarProximoPonto()
    {
        if (bloqueada) return;

        // Mais perguntas do que pontos desenhados: abre a pergunta onde o boneco está.
        if (proximoPonto >= config.Pontos.Length)
        {
            ChegouNoPonto?.Invoke();
            return;
        }

        if (marcador != null) Destroy(marcador.gameObject);
        var ponto = config.Pontos[proximoPonto];
        float diametro = fundo.size.y * config.AlturaPersonagem * 0.6f;
        // Mesma ordem do boneco, mas um pouco mais longe da câmera: o boneco fica por cima do anel.
        var posicao = ParaMundo(ponto.Posicao) + new Vector3(0f, 0f, 0.01f);
        marcador = MarcadorPonto.Criar(AoClicarMarcador, posicao, diametro, camadaFundo, ordemPersonagem);
    }

    void AoClicarMarcador()
    {
        if (bloqueada || personagem == null || personagem.Andando) return;

        if (marcador != null) { Destroy(marcador.gameObject); marcador = null; }
        var ponto = config.Pontos[proximoPonto];
        StartCoroutine(AndarAte(ponto, () =>
        {
            proximoPonto++;
            ChegouNoPonto?.Invoke();
        }));
    }

    /// <summary>Depois da última pergunta: anda até a porta de saída.</summary>
    public void IrParaSaida(Action aoChegar)
    {
        if (bloqueada || config.Saida == null || personagem == null)
        {
            aoChegar?.Invoke();
            return;
        }
        StartCoroutine(AndarAte(config.Saida, aoChegar));
    }

    /// <summary>Tempo esgotado: para o boneco e desliga os cliques.</summary>
    public void Bloquear()
    {
        bloqueada = true;
        StopAllCoroutines();
        if (marcador != null) { Destroy(marcador.gameObject); marcador = null; }
        if (personagem != null) personagem.Parar();
    }

    IEnumerator AndarAte(ConfigTrilhas.Ponto ponto, Action aoChegar)
    {
        var caminho = new List<Vector3>();
        if (ponto.Caminho != null)
            foreach (var via in ponto.Caminho) caminho.Add(ParaMundo(via));
        caminho.Add(ParaMundo(ponto.Posicao));

        yield return personagem.AndarPor(caminho);
        aoChegar?.Invoke();
    }
}
