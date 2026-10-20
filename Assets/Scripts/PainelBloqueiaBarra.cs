using UnityEngine;

public class PainelBloqueiaBarra : MonoBehaviour
{
    public Collider2D[] itensDaBarra;

    void OnEnable()
    {
        foreach (var c in itensDaBarra)
            if (c != null) c.enabled = false;
    }

    void OnDisable()
    {
        foreach (var c in itensDaBarra)
            if (c != null) c.enabled = true;
    }
}