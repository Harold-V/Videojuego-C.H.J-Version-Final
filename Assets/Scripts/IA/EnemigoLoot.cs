using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemigoLoot : MonoBehaviour
{
    [Header("Loot")]
    //Array del loot disponible cuando matamos a un enemigo
    [SerializeField] private DropItem[] lootDisponible;

    //Variables
    private List<DropItem> lootSeleccionado = new List<DropItem>();

    //Prpiedades
    public List<DropItem> LootSeleccionado => lootSeleccionado;

    private void Start()
    {
        //Ejecutamos el metodo en el start
        SeleccionarLoot();
    }

    private void SeleccionarLoot()
    {
        //Recorremos nuestro loot disponible 
        foreach (DropItem item in lootDisponible)
        {
            //Probabilidad de mostrar un item de aparecer en el panel
            float probabilidad = Random.Range(0, 100);
            //Condicion para añadir el item a nuestro loot
            if (probabilidad <= item.PorcentajeDrop)
            {
                lootSeleccionado.Add(item);
            }
        }
    }
}