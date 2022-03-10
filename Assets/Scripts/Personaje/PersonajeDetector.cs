using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeDetector : MonoBehaviour
{
    // Evento para decir que se ha Seleccionado a un Enemigo de Tipo Melee
    public static Action<EnemigoInteraccion> EventoEnemigoDetectado;
    // Evento que Indica que Hemos perdido la Deteccion del Enemigo (No se esta Colisionando) 
    public static Action EventoEnemigoPerdido;

    // Propiedad para Guardar el Enemigo que estamos Detectando 
    public EnemigoInteraccion EnemigoDetectado { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si Colosionamos con un Objeto que Tiene el Tag de Enemigo... 
        if (other.CompareTag("Enemigo"))
        {
            // Creamos la Referencia
            EnemigoDetectado = other.GetComponent<EnemigoInteraccion>();
            // Cuando Estamos Detectanto el Enemigo, Lanzamos el Evento 
            if (EnemigoDetectado.GetComponent<EnemigoVida>().Salud > 0)
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si el Enemigo esta Fuera de la Deteccion del Persoanje 
        if (other.CompareTag("Enemigo"))
            // Lanzamos el Enemigo 
            EventoEnemigoPerdido?.Invoke();
    }
}
