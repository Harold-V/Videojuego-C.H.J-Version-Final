using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta Clase Hereda de WaypointMovimiento
public class NPCMovimiento : WaypointMovimiento
{
    // Establecer hacia donde se esta Moviendo el Personaje
    [SerializeField] private DireccionMovimiento direccion;

    // Obtner el Hash Creado en el Animator 
    private readonly int caminarAbajo = Animator.StringToHash("CaminarAbajo");
    
    // Sobreescribir el Metodo 
    protected override void RotarPersonaje()
    {
        // Si no hay Movimieto Horizontalmente, salimos del Metodo
        if (direccion != DireccionMovimiento.Horizontal)
            return;

        // Si el Personaje se Mueve hacia la Derecha...
        if (PuntoPorMoverse.x > ultimaPosicion.x)
            // Movemos la Escala del Personaje 
            transform.localScale = new Vector3(1, 1, 1);
        // Si el Personaje se Mueve hacia la Izquierda...
        else
            // Movemos la Escala del Personaje 
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Sobreescribir el Metodo 
    protected override void RotarVertical()
    {
        // Si no hay Movimieto Verticalmente, salimos del Metodo
        if (direccion != DireccionMovimiento.Vertical)
            return;

        // Si el Personaje se Mueve hacia Arriba...
        if (PuntoPorMoverse.y > ultimaPosicion.y)
            // Cambiamos la Animacion del Personaje 
            _animator.SetBool(caminarAbajo, false);
        // Si el Personaje se Mueve hacia Abajo... 
        else
            // Cambiamos la Animacion del Personaje 
            _animator.SetBool(caminarAbajo, true);
    }
}
