using System;
using UnityEngine;

// Clase de Enumeracion
public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}

// Evento Agregar Atributo
public class AtributoButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }
}
