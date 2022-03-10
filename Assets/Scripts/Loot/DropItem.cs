using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem
{
    [Header("Info")]
    //Atributos del item añadido al inventario
    public string Nombre;
    public InventarioItem Item;
    public int Cantidad;

    [Header("Drop")]
    //Porcentaje de Probabilidad de un item qeu salga en el loot
    [Range(0, 100)] public float PorcentajeDrop;

    //Propiedad para que nos dice si el item es recogido o no
    public bool ItemRecogido { get; set; }
}
