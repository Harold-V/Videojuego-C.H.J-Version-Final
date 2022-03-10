using UnityEngine;

// Crear el ScriptableObject en las carpetas, La pocion Mana, se encuentra en la Carpeta Items
[CreateAssetMenu(menuName = "Items/Pocion Mana")]
public class ItemPocionMana : InventarioItem
{
    [Header("Pocion info")] 
    public float MPRestauracion;

    // Sobreescribir Metodo 
    public override bool UsarItem()
    {
        // Accedemos al Personaje 
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            // Restaurar Mana 
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion);
            // El Item Restaura el Mana 
            return true;
        }
        // El Item no Restauro el Mana
        return false;
    }
}