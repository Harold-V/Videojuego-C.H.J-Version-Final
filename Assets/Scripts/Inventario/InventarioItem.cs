using UnityEngine;

// Enumeracion 
public enum TiposDeItem
{
    // Tipos de Item
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}

public class InventarioItem : ScriptableObject
{
    // Parametros e Informacion de los Items

    [Header("Parametros")]

    // Identificador
    public string ID;

    public string Nombre;
    public Sprite Icono;

    // Atributo TextArea para Añadir Texto
    [TextArea] public string Descripcion;

    [Header("Informacion")] 
    public TiposDeItem Tipo;
    // Si el Item es consumible se puede usar el Botton "Usar"
    public bool EsConsumible;
    // Decir si el Item es Aculumable o no 
    public bool EsAcumulable;
    // Cuantos Items Podemos Acumular en un slot, Dependiendo del Item
    public int AcumulacionMax;

    /* Atributo para Ocultar una variable en el Ispector, ademas esta variable controla la 
       cantidad que aún tengo disponible para guardar*/
    [HideInInspector] public int Cantidad;

    // Metodo que Regresa una Nueva Instancia del Item
    public InventarioItem CopiarItem()
    {
        // Nueva Instancia 
        InventarioItem nuevaInstancia = Instantiate(this);
        // Retorna la Nueva Instancia
        return nuevaInstancia;
    }

    // Metodos Virtuales para Sobreescribirlos 
    public virtual bool UsarItem()
    {
        return true;
    }

    public virtual bool EquiparItem()
    {
        return true;
    }
    
    public virtual bool RemoverItem()
    {
        return true;
    }
}