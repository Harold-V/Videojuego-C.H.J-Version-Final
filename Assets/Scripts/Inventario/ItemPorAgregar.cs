using UnityEngine;

// En esta clase se define la referencia del InventarioItem y la Cantidad que se va a añadir
public class ItemPorAgregar : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si se Colisiona con el Jugador 
        if (other.CompareTag("Player"))
        {
            // Se llama al Inventario
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);// Pasamos los Parametros
            // Una vez Recogido el Item, se destruye del Juego
            Destroy(gameObject);
        }
    }
}
