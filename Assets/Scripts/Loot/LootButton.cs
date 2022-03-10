using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;

    //Propiedades
    public DropItem ItemPorRecoger { get; set; }

    public void ConfigurarLootItem(DropItem dropItem)
    {
        //Definimos la proiedad
        ItemPorRecoger = dropItem;
        itemIcono.sprite = dropItem.Item.Icono;
        itemNombre.text = $"{dropItem.Item.Nombre} x{dropItem.Cantidad}";
    }

    public void RecogerItem()
    {
        //Verificamos que un item esta cargado en una targeta
        if (ItemPorRecoger == null)
        {
            return;
        }
        //Si la condicion anterior no se cumple instanciamos un item en nuestro inventario
        Inventario.Instance.AñadirItem(ItemPorRecoger.Item, ItemPorRecoger.Cantidad);
        //Cambiamos el estado a item recogido para que no aparezca en el loot
        ItemPorRecoger.ItemRecogido = true;
        //Destruimos la targeta que muestra el item en el lot
        Destroy(gameObject);
    }
}
