using UnityEngine;
using UnityEngine.UI;

public class CartaoNivel : MonoBehaviour
{
    public string idFase;          // ex: "floresta", "vale", "montanha", "lobos"
    public Image imagem;           // imagem do nível
    public GameObject cadeado;     // imagem do cadeado
    public Material materialBase;  // o MaterialCinza

    Material instancia;

    void Awake()
    {
        instancia = new Material(materialBase);
        imagem.material = instancia;
    }

    void OnEnable()
    {
        Atualizar();
    }

    public void Atualizar()
    {
        bool concluida = Progresso.Concluida(idFase);
        instancia.SetFloat("_Grayscale", concluida ? 0f : 1f);
        cadeado.SetActive(!concluida);
    }

    [ContextMenu("Concluir (teste)")]
    void ConcluirTeste()
    {
        Progresso.Concluir(idFase);
        Atualizar();
    }
}