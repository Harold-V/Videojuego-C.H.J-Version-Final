using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTienda : MonoBehaviour
{
    [Header("Config")] 

    // Referencias 
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemCosto;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    // Propiedad que nos Permite Guardar el Item que esta siendo Cargado en la Tarjeta
    public ItemVenta ItemCargado { get; private set; }

    // Variables 
    private int cantidad; // Cuanto Queremos Comprar 
    private int costoInicial; // Comprar un solo Item 
    private int costoActual; // Cual es el Costo de Comprar el Item cierta Cantidad de Veces

    private void Update()
    {
        // Actulizar la Cantidad que Estamos Intentando Comprar y el Item 
        cantidadPorComprar.text = cantidad.ToString();
        itemCosto.text = costoActual.ToString();
    }

    public void ConfigurarItemVenta(ItemVenta itemVenta) // Clase que Tiene la Referencia del Item y su Costo
    {
        ItemCargado = itemVenta;
        // Actualizar Informacion de la Tarjeta 
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();
        cantidad = 1;
        costoInicial = itemVenta.Costo;
        costoActual = itemVenta.Costo;
    }

    public void ComprarItem()
    {
        // Verificamos si Tenemos el Dinero Suficiente para Comprar 
        if (MonedasManager.Instance.MonedasTotales >= costoActual)
        {
            // Podemos Comprar
            // Añadimos el Item al Inventario
            Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
            // Actualizamos la Cantidad de Monedas 
            MonedasManager.Instance.RemoverMonedas(costoActual);
            // Actualizamos la Cantidad y el Costo del Item Nuevamente 
            cantidad = 1;
            costoActual = costoInicial;
        }
    }
    
    public void SumarItemPorComprar()
    {
        int costoDeCompra = costoInicial * (cantidad + 1);
        if (MonedasManager.Instance.MonedasTotales >= costoDeCompra)
        {
            // Aumentamos La cantidad del Item 
            cantidad++;
            costoActual = costoInicial * cantidad;
        }
    }

    public void RestarItemPorComprar()
    {
        // Si Cantidad es Igual a 1
        if (cantidad == 1)
            return; // regresamos 

        // Restamos La cantidad del Item 
        cantidad--;
        costoActual = costoInicial * cantidad;
    }
}
