using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")]

// Para que un Arma sea un Item, debemos Heredar de Inventario Item 
public class ItemArma : InventarioItem
{
    // Referencia del Arma 
    [Header("Arma")] 
    public Arma Arma;

    // Sobreescribimos el Metodo 
    public override bool EquiparItem()
    {
        // Si ya hay un Arma Equipada, No se Puede Equipar Otra Arma 
        if (ContenedorArma.Instance.ArmaEquipada != null)
            // Regresamos Falso 
            return false;
        
        // Si no hay un Arma Equipada, equipamos esta Arma 
        ContenedorArma.Instance.EquiparArma(this);
        return true;
    }

    public override bool RemoverItem()
    {
        // Si no hay ningun Arma Equipada, No se Puede Remover un Arma 
        if (ContenedorArma.Instance.ArmaEquipada == null)
            return false;

        // Si hay un Arma Equipada, Removemos el Arma 
        ContenedorArma.Instance.RemoverArma();
        return true;
    }
}
