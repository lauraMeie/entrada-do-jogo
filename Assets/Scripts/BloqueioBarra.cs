using UnityEngine;

public class BloqueioItens : MonoBehaviour
{
    public Collider2D[] itensDaBarra;

    public void Bloquear()
    {
        foreach (var c in itensDaBarra) c.enabled = false;
    }

    public void Liberar()
    {
        foreach (var c in itensDaBarra) c.enabled = true;
    }
}