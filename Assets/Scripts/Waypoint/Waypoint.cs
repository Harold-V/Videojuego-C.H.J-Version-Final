using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Array de Puntos (Ruta de los NPC´s)
    [SerializeField] private Vector3[] puntos;

    // Propiedad
    public Vector3[] Puntos => puntos;

    // Propiedad para Guardad la Posicion del Personaje
    public Vector3 PosicionActual { get; set; }

    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        PosicionActual = transform.position;
    }

    // Conocer la Posicion a la cual nos Queremos Mover
    public Vector3 ObtenerPosicionMovimiento(int index)
    {
        // Regresamos la Posicion del Personaje + el Punto al que nos Queremos Mover
        return PosicionActual + puntos[index];
    }
    
    // Dibujar Puntos y Lineas en la Escena 
    private void OnDrawGizmos()
    {
        // Mientras no se este Jugando, y se este Cambiando la Posicion del Personaje
        if (juegoIniciado == false && transform.hasChanged)
            // Actualizar la Posicion del Personaje en base a su Transform
            PosicionActual = transform.position;

        // Verificar si hay Puntos o Si el Array no es Nulo
        if (puntos == null || puntos.Length <= 0)
            return;

        for (int i = 0; i < puntos.Length; i++)
        {
            // Definir el Color de los Puntos
            Gizmos.color = Color.blue;
            // Dibujar Esferas en la Posicion de Cada Punto 
            Gizmos.DrawWireSphere(puntos[i] + PosicionActual, 0.5f /* Radio */);

            // Verificar que no se Sobrepasa la Cantida de Puntos que hay en el Array 
            if (i < puntos.Length - 1)
            {
                // Definir el Color de la Lineas 
                Gizmos.color = Color.gray;
                // Dibujar Lineas
                Gizmos.DrawLine(puntos[i] + PosicionActual, puntos[i + 1] + PosicionActual);
            }
        }
    }
}
