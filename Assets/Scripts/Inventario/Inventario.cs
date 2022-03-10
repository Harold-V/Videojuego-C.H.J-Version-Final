using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    // Titulo
    [Header("Items")]
    
    // Array
    [SerializeField] private InventarioItem[] itemsInventario;

    // Variable
    [SerializeField] private Personaje personaje;

    // Definir numero de Slots en en el Inventario
    [SerializeField] private int numeroDeSlots;

    // Propiedades (Getters)
    public Personaje Personaje => personaje;
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        // El tamaño del Arreglo sera igual al numero de Slots
        itemsInventario = new InventarioItem[numeroDeSlots];
    }

    // Para Añadir un Item, pasamos la Referencia y la Cantidad (Parametros)
    public void AñadirItem(InventarioItem itemPorAñadir, int cantidad)
    {
        // Si el Item es Nulo, Salimos del Metodo 
        if (itemPorAñadir == null)
            return;

        // Verificacion en Caso de Tener ya un Item Similar en el Inventario
        List<int> indexes = VerificarExistencias(itemPorAñadir.ID);

        // Si el Item es Acumulable, podemos Continuar
        if (itemPorAñadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    // Si el Slot que contiene los Items con Id similar, no ha Superado la Cantidad Maxima que se puede Guardar...
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAñadir.AcumulacionMax)
                    {
                        // Se suma la Cantidad de Nuevos Items que se Quieren Guardar
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAñadir.AcumulacionMax)
                        /* Si al Sumar la Nueva Cantidad, se supera la Acumunulacion Maxima,  se debe obtener la diferenicia, y esa diferencia
                           añadirla en otro Slot */ 
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAñadir.AcumulacionMax;
                            // Definimos la Cantidad como su Acumulacion Maxima 
                            itemsInventario[indexes[i]].Cantidad = itemPorAñadir.AcumulacionMax;
                            // Añdimos el mismo Item, pero esta vez con la Diferencia
                            AñadirItem(itemPorAñadir, diferencia);
                        }
                        
                        // Llamamos el Metodo, para Actualizar el Inventario (Mostrar Imagenes)
                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }
        // Si el Item no es similar, es decir, es nuevo Item...
        // Se Verifica que la cantidad a Añadir de ese Nuevo Item no sea cero, de lo Contrario, sale del Metodo
        if (cantidad <= 0)
            return;

        // Si la Cantidad a Añadir supero la Acumulacion Maxima...
        if (cantidad > itemPorAñadir.AcumulacionMax)
        {

            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax);
            // Actualizar la Cantidad que queda despues de Añadir un Item a un Slot vacio
            cantidad -= itemPorAñadir.AcumulacionMax;
            // Añadir el Nuevo Item
            AñadirItem(itemPorAñadir, cantidad);
        }
        else
            // Si el Item nuevo no es Mayor a la Acumulacion Maxima, Simplemente se Añade
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad);
    }

    private List<int> VerificarExistencias(string itemID)
    {
        // Lista
        List<int> indexesDelItem = new List<int>();
        // Utilizar la Longitud del Inventario, para poder Recorrelo y Encontrar Similitudes
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            // Debemos saber si en el Slot hay un Item, si hay un Item, los Compara
            if (itemsInventario[i] != null)
            {
                // Si se encuentra el Item, (ID similar)  
                if (itemsInventario[i].ID == itemID) 
                {
                    // Se añade a la Lista
                    indexesDelItem.Add(i);
                }
            }
        }

        return indexesDelItem;
    }

    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad) // Referencia del Item y Cantidad
    {
        // Recorrer el Inventario
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            // Si un Slot esta Vacio...
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem(); // Crear una Nueva Instancia
                itemsInventario[i].Cantidad = cantidad;
                // Llamado del Metodo
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        // Reducir la Cantidad
        ItemsInventario[index].Cantidad--;

        // Verificar que la cantidad este por debajo de Cero 
        if (itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
            // Actualizar Informacion 
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
            return;
        
        // Copiar el Item en Slot Final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);
        
        // Borramos en el Item de Slot Inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);
    }
    
    private void UsarItem(int index)
    {
        // Verificar si el Item Existe 
        if (itemsInventario[index] == null)
            return;

        // Si se puede usar el Item, eliminamos una unidad
        if (itemsInventario[index].UsarItem())
            EliminarItem(index);
    }

    private void EquiparItem(int index)
    {
        // Verificar si el Item Existe 
        if (itemsInventario[index] == null)
            return;

        // Verificamos que el Item a Equipar es de Tipo Arma, Ya que solo se Pueden Equipar Armas
        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
            return;

        // Equipamos el Item 
        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        // Verificar si el Item Existe 
        if (itemsInventario[index] == null)
            return;

        // Verificamos que el Item a Remover es de Tipo Arma, Ya que solo se Pueden Equipar Armas
        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
            return;

        // Removemos el Item 
        itemsInventario[index].RemoverItem();
    }
    
    #region Eventos

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
                break;
        }
    }
    
    // Se escucha a los Eventos 
    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
