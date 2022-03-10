using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Enumeracion
public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}

public class InventarioSlot : MonoBehaviour
{
    // Evento, Parametro de TipoDeInteraccion y de Tipo int (referencia al Index) 
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;
    
    // Variables
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP; /* Cuando no hay Nada en los Slots, es decir, no hay imagen, ni cantidad, se debe 
       mostrar algo vacio, en este caso el TMP */

    // Propiedad que Almacena el index de cada Slot 
    public int Index { get; set; }

    // Referencia al Button, Metodo Awake
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    // Metodo que Activa y Desactiva la Imagen del Slot
    public void ActivarSlotUI(bool estado)
    {
        // Si paso True, se Activa, si paso False, se desactiva (Imagenes, fondoCantidad)
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot()
    {
        _button.Select();
    }
    
    public void ClickSlot()
    {
        // Si el Evento no es nulo, se Invoca y se Escucha en InventarioUI
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);
        
        // Mover Item
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            if (InventarioUI.Instance.IndexSlotInicialPorMover != Index)
                // Mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
        }
    }

    public void SlotUsarItem()
    {
        // Si hay un Item en el Slot se puede Lanzar el Evento 
        if (Inventario.Instance.ItemsInventario[Index] != null)
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
    }

    public void SlotEquiparItem()
    {
        // Si este Slot Tiene un Item, Lanzamos el Evento 
        if (Inventario.Instance.ItemsInventario[Index] != null)
            // Verificamos que no sea Nulo y lo Invocamos
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index);
    }
    
    public void SlotRemoverItem()
    {
        // Si este Slot Tiene un Item, Lanzamos el Evento 
        if (Inventario.Instance.ItemsInventario[Index] != null)
            // Verificamos que no sea Nulo y lo Invocamos
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Remover, Index);
    }
}
