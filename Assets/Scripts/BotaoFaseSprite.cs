using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class BotaoFaseSprite : MonoBehaviour
{
    public UnityEvent aoClicar;

    void OnMouseDown()
    {
        aoClicar.Invoke();
    }
}