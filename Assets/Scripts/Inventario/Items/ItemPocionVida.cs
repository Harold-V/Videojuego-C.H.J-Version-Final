using System;
using UnityEngine;

// Crear el ScriptableObject en las carpetas, La pocion Vida, se encuentra en la Carpeta Items
[CreateAssetMenu(menuName = "Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion info")] 
    public float HPRestauracion;

    // Sobreescribir Metodo 
    public override bool UsarItem()
    {
        // Accedemos al Personaje 
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            // Restauramos Salud 
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPRestauracion);
            // El Item Restauro la Vida 
            return true;
        }
        // El Item no Restauro la Vida
        return false;
    }
}