using System;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    // Evento que Notifica en Particular que Enemigo se ha Seleccionado 
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;

    // Evento para Notificar que no se ha Seleccionado Ningun Enemigo 
    public static Action EventoObjetoNoSeleccionado;

    // Propiedad para Guardar al Enemigo que se Acaba de Seleccionar 
    public EnemigoInteraccion EnemigoSeleccionado { get; set; }
    
    // Referencia de la Camara
    private Camera camara;
    
    private void Start()
    {
        camara = Camera.main;
    }

    private void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {
        // Verificamos si se esta Haciendo Click sobre el Enemigo 
        if (Input.GetMouseButtonDown(0)) // Cero porque se esta Apretando el lado Izquierdo
        {
            // Seleccionar al Enemigo  
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition) /* Origen del Rayo es la
                posicion del mouse */ , Vector2.zero /* direccion*/ , Mathf.Infinity /* distancia del Rayo*/, 
                LayerMask.GetMask("Enemigo") /* Layer de Enemigo */ );

            // Si con el RayCast colisionamos sobre un Enemigo, haciendole Click 
            if (hit.collider != null)
            {
                // Obtnemos el Componente de tipo EnemigoInteraccion
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                // Verificamos que el enemigo este en estado ¨Muerto¨
                EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>();
                if (enemigoVida.Salud > 0f)
                    // Mostramos el enemigo seleccionado
                    EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                else
                {
                    // Si no se ha Colisionado con un Enemigo, lanzamos el Evento de ObjetoNoSeleccionado
                    EnemigoLoot loot = EnemigoSeleccionado.GetComponent<EnemigoLoot>(); 
                    // Mostramos el loot
                    LootManager.Instance.MostrarLoot(loot);
                }
            }
            else
                // Mostramos el enemigo seleccionado
                EventoObjetoNoSeleccionado?.Invoke();
        }
    }
}
