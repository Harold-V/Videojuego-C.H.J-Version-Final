using UnityEngine;

public class Portal : MonoBehaviour
{
    // Referencia del Lugar al Cual Queremos Teletransportar al Personaje 
    [SerializeField] private Transform nuevaPos;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si Colisionamos con el Jugador 
        if (other.CompareTag("Player"))
            // Mover el Personaje a la Nueva Posicion 
            other.transform.localPosition = nuevaPos.position;
    }
}
