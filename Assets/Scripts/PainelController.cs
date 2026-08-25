using UnityEngine;

public class PainelController : MonoBehaviour
{
    [SerializeField] private GameObject painelImagem;

    public void AbrirImagem()
    {
        painelImagem.SetActive(true);
    }

    public void FecharImagem()
    {
        painelImagem.SetActive(false);
    }
}