using System;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    // Variables 
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionPorSegundo;

    // Propiedades...
    public float ManaActual { get; private set; }
    public bool SePuedeRestaurar => ManaActual < manaMax;

    // Referencia a Clase Personaje Vida
    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }

    // Inicializacion 
    private void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();

        // Metodo que llama un Metodo y Lo Repite Las veces que desees por Segundo
        InvokeRepeating(nameof(RegenerarMana),1,1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10f);
        }
    }

    public void UsarMana(float cantidad)
    {
        if (ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    public void RestaurarMana(float cantidad)
    {
        // No se puede Restaurar Mana si...
        if (ManaActual >= manaMax)
            return;

        ManaActual += cantidad;
        // Si se supera la Cantidad de Mana Maxima...
        if (ManaActual > manaMax)
            ManaActual = manaMax;
        
        // Actualizar Barra de Mana
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
    
    private void RegenerarMana()
    {
        if (_personajeVida.Salud > 0f && ManaActual < manaMax)
        {
            ManaActual += regeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }
    
    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}
