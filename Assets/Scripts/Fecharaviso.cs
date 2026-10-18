using UnityEngine;
using UnityEngine.UI;

// Coloque este script na RAIZ do prefab "Aviso" (o objeto de nome "Aviso").
// Ao clicar na placa, o aviso inteiro é destruído.
public class FecharAviso : MonoBehaviour
{
    private void Awake()
    {
        // Procura o Button dentro do prefab (o da imagem "Aviso fase")
        Button botao = GetComponentInChildren<Button>(true);
        if (botao != null)
            botao.onClick.AddListener(Fechar);
    }

    public void Fechar()
    {
        Destroy(gameObject);
    }
}