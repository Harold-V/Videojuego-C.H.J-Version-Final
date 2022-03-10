using System;
using Cinemachine;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    // Varibale privada de tipo CinemachineVirtualCamera (namespace Cinemachine), llamada Camara
    [SerializeField] private CinemachineVirtualCamera camara;

    /* Para dectetctar si el personaje esta entrando o saliendo del confiner, se utilizan los metodos 
       OnTriggerEnter2D y OnTriggerExit2D */ 
    private void OnTriggerEnter2D(Collider2D other)
    // Se llama cuando al entra
    {
        /* Si el Objeto que Entra tiene el tag (palabra de referencia que puede asignar a uno o más GameObjects) 
           de Player, entonces */ 
        if (other.CompareTag("Player"))
            // Activarlo
            camara.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    // Se llama cuando algo sale 
    {
        /* Si el Objeto que sale tiene el tag (palabra de referencia que puede asignar a uno o más GameObjects) 
           de Player, entonces */
        if (other.CompareTag("Player"))
            // Se desactiva la camara
            camara.gameObject.SetActive(false);
    }
}
