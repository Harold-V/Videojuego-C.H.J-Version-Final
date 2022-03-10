using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeracion
public enum TipoDeteccion
{
    Rango,
    Melee
}

public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    public void MostrarEnemigoSeleccionado(bool estado, TipoDeteccion tipo)
    {
        // Si el Tipo de Seleccion es de Tipo Rango 
        if (tipo == TipoDeteccion.Rango)
            // Mostramos el Sprite de Rango 
            seleccionRangoFX.SetActive(estado);
        else
            // De lo contrario, Mostramos el Sprite de Melee
            seleccionMeleeFX.SetActive(estado);
    }

    public void DesactivarSpritesSeleccion()
    {
        // Desactivamos los dos Sprites
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }
}
