using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]

    // Referencias...
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    
    // Obtener Referencia del Inventario, Prefab y el Slot donde se va a Instanciar  
    [SerializeField] private InventarioSlot slotPrefab; /* Variable de Tipo InventarioSlot, porque 
       al Slot se le añadio la Clase Inventario */ 
    [SerializeField] private Transform contenedor;

    // Propiedades
    public int IndexSlotInicialPorMover { get; private set; }
    public InventarioSlot SlotSeleccionado { get; private set; }

    // Crear una lista para Guardar la informacion de cada uno de los Slots Creados
    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    private void Start()
    {
        // Llamado de Metodos
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Si el Slot esta Seleccionado, Movemos el Item
            if (SlotSeleccionado != null)
                IndexSlotInicialPorMover = SlotSeleccionado.Index;
        }
    }

    /* Para poder instanciar los slots se debe conocer cuantos slots esxiten, en este caso
       son 24 */
    private void InicializarInventario()
    {
        /* Ciclo for con la cantidad de slots, el numero se encuentra en la clase Inventario
           (numeroDeSlots) */
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            // Referencia a la Lista (List<InventarioSlot>)
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor); // Instancia
            // Antes de Añadirlo a Lista, se le proporciona un Index
            nuevoSlot.Index = i;
            // Metodo para Añadir nuevo Slot
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        // Usamos la clase EventSystem que regresa cual es el Obejeto Actual que se Selecciona en el Editor
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;

        // Si no hay Nigun Objeto Seleccionado, salimos
        if (goSeleccionado == null)
            return;

        // Si hay un Objeto Seleccionado, se Verifica si el Objeto tiene la clase Slot
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
            SlotSeleccionado = slot;
    }
    
    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        // Referencia del InventarioSlot
        InventarioSlot slot = slotsDisponibles[itemIndex]; // El Slot tiene el Mismo Index que el Item y Viceversa 
        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
            // Si no se Añade ningun Item al Slot, se desactiva el Icono y el Fondo 
            slot.ActivarSlotUI(false);
    }

    private void ActualizarInventarioDescripcion(int index) // Referencia del Index
    {
        // Verificamos si hay un Item en el Slot que lanza el Evento, se crea una Propiedad en Inventario que nos regrese el Array
        if (Inventario.Instance.ItemsInventario[index] != null)
        // Si hay un Item, se puede Actualizar el Panel de Descripcion del Inventario
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            // Activar el Panel 
            panelInventarioDescripcion.SetActive(true);
        }
        else
            // Si en el Slot en el que se hace click, se Desactiva el Panel 
            panelInventarioDescripcion.SetActive(false);
    }

    public void UsarItem()
    {
        // Si hay un Slot Seleccionado 
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        // Si hay un Slot Seleccionado 
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }
    
    public void RemoverItem()
    {
        // Si hay un Slot Selccionado
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotRemoverItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }
    
    #region Evento

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        // Nos aseguramos que el Evento que llamamos es de Tipo Click
        if (tipo == TipoDeInteraccion.Click)
            ActualizarInventarioDescripcion(index);
    }
    
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
