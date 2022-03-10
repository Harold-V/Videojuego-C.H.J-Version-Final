using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
    // Referenciar la Imagen 
    [SerializeField] private GameObject npcButtonInteractuar;

    // Referencia del ScriptableObject que contiene el NPC en base al Dialogo que tiene 
    [SerializeField] private NPCDialogo npcDialogo;

    // Propiedad (Getter)
    public NPCDialogo Dialogo => npcDialogo;
    
    // Metodos para Saber si Estamos Colisionando con el Player
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si Colisionamos con el Jugador 
        if (other.CompareTag("Player"))
        {
            // Establecer la Propiedad (Cargar Informacion) 
            DialogoManager.Instance.NPCDisponible = this;
            // Activar el Boton de Interactuar 
            npcButtonInteractuar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si ya no Colisionamos con el Jugador 
        if (other.CompareTag("Player"))
        {
            // Establecemos la Propiedad (Cerramos la Informacion) 
            DialogoManager.Instance.NPCDisponible = null;
            // Desactivamos el Boton de Interactuar
            npcButtonInteractuar.SetActive(false);
        }
    }
}
