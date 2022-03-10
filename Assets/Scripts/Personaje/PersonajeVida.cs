using System;
using UnityEngine;

public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; }
    public bool PuedeSerCurado => Salud < saludMax;

    // Desactivar Collider en el Momento en el que el Personaje Muere
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Llamado del Metodo Start de la Clase VidaBase para Sobreescribirlo
    protected override void Start()
    {
        base.Start();
        ActualizarBarraVida(Salud, saludMax);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            RecibirDaño(10);
        
        if (Input.GetKeyDown(KeyCode.Y))
            RestaurarSalud(10);
    }

    public void RestaurarSalud(float cantidad)
    {
        // Si el personaje muere no puede Restaurar Salud
        if (Derrotado)
            return;
        
        if (PuedeSerCurado)
        {
            // Aumentar Salud
            Salud += cantidad;

            // Si la Salud sobrepasa la Salud Max
            if (Salud > saludMax)
                // Salud sera igual a la Salud Max
                Salud = saludMax;
            
            ActualizarBarraVida(Salud, saludMax);
        }
    }

    protected override void PersonajeDerrotado()
    {
        // Desactivar Colisionador
        _boxCollider2D.enabled = false;
        Derrotado = true;
        // Si el Evento Personaje Derrotado no es Nulo, se Muestra
        EventoPersonajeDerrotado?.Invoke();
    }

    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }

    /* La clase PersonajeVida hereda de VidaBase, y utilizamos override para 
      sobreescribir los metodos originarios de VidaBase en esta clase, ademas
      llama a la clase UIManager para obtener el metodo de actualizarVidaPersonaje*/
    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
}