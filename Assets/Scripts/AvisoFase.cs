using UnityEngine;

// Coloque este script em CADA objeto de fase (ex.: "fase floresta dos lobos cinzentos").
// Sempre que a fase for ativada, o prefab do aviso é criado por cima de tudo.
public class AvisoFase : MonoBehaviour
{
    [Tooltip("Arraste aqui o prefab 'Aviso' da pasta Assets.")]
    [SerializeField] private GameObject prefabAviso;

    private GameObject avisoAtual;

    private void OnEnable()
    {
        // Evita criar dois avisos ao mesmo tempo
        if (prefabAviso != null && avisoAtual == null)
            avisoAtual = Instantiate(prefabAviso);
    }

    private void OnDisable()
    {
        // Se a fase fechar com o aviso ainda aberto, remove ele
        if (avisoAtual != null)
            Destroy(avisoAtual);
    }
}