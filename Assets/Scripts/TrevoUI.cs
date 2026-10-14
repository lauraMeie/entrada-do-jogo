using UnityEngine;
using TMPro;

public class TrevoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;

    void OnEnable()
    {
        if (TrevoManager.Instance == null) return;
        TrevoManager.Instance.AoMudar += Atualizar;
        Atualizar(TrevoManager.Instance.Trevos);
    }

    void OnDisable()
    {
        if (TrevoManager.Instance != null)
            TrevoManager.Instance.AoMudar -= Atualizar;
    }

    void Atualizar(int total) => texto.text = "x " + total;
}