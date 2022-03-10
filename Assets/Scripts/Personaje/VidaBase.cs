using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float saludInicial;
    [SerializeField] protected float saludMax;

    /* Propiedad que Retorna un Valor y Permite Modificar 
      Damos acceso protected para que solo pueda accedida por la Clase padre y sus Hijas */
    public float Salud { get; protected set; }
    
    protected virtual void Start()
    {
        Salud = saludInicial;
    }

    public void RecibirDaño(float cantidad)
    {
        // Si no hay daño, salimos
        if (cantidad <= 0)
            return;

        // Si hay salud puede recibir daño
        if (Salud > 0f)
        {
            Salud -= cantidad;
            ActualizarBarraVida(Salud, saludMax);

            // Si no hay salud, el personaje muere
            if (Salud <= 0f)
            {
                Salud = 0f;
                ActualizarBarraVida(Salud, saludMax);
                PersonajeDerrotado();
            }
        }
    }

    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        
    }

    protected virtual void PersonajeDerrotado()
    {
        
    }
}
