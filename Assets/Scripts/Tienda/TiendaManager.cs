using System;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    [Header("Config")] 

    // Referencias (Variables) 
    [SerializeField] private ItemTienda itemTiendaPrefab; // (Prefab)
    [SerializeField] private Transform panelContenedor; // Contenedor donde se Guardara el Prefab

    [Header("Items")] 
    // Array de todos los Items Disponibles 
    [SerializeField] private ItemVenta[] itemsDisponibles;

    private void Start()
    {
        CargarItemEnVenta();
    }

    private void CargarItemEnVenta()
    {
        // Recorrer todos los Items Disponibles 
        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            // Crear Itemas Disponibles y Añadir Cada Item en una Tarjeta del Panel 
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemVenta(itemsDisponibles[i]);
        }
    }
}