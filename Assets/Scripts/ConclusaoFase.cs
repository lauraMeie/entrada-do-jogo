using System;
using UnityEngine;

public class ConclusaoFaseTeste : MonoBehaviour
{
    public event Action AoTodasRespostasCorretas;

    // Apenas para teste
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Todas as respostas foram respondidas corretamente!");

            AoTodasRespostasCorretas?.Invoke();
        }
    }
}