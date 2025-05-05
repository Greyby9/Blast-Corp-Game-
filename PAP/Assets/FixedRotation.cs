using UnityEngine;

public class FixedRotation : MonoBehaviour
{  
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position; // Guarda la distancia inicial
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset; // Mantiene la distancia
        }
    }
}
