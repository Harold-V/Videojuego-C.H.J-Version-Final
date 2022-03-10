using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma : Singleton<ContenedorArma>
{
    // Referencia del Incono del Arma y del Icono del Proyectil para Actualizarlos 
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;

    // Propiedad para Conocer la Referencia del Arma que estamos Equipando en el Panel y la Guarde
    public ItemArma ArmaEquipada { get; set; }
    
    // Metodo Para Actualizar los Iconos Segun el Arma que Equipamos 
    public void EquiparArma(ItemArma itemArma) // Referencia de Item Arma 
    {
        ArmaEquipada = itemArma;
        // Actualizar el Icono del Arma 
        armaIcono.sprite = itemArma.Arma.ArmaIcono;
        // Activar el Arma 
        armaIcono.gameObject.SetActive(true);

        // Si el Tipo del Arma Seleccionado en Magia
        if (itemArma.Arma.Tipo == TipoArma.Magia)
        {
            // Mostrar y Activar el Icono (Solo si es de tipo Magia)
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill;
            armaSkillIcono.gameObject.SetActive(true);
        }
        
        // Equipar Arma al Personaje 
        Inventario.Instance.Personaje.PersonajeAtaque.EquiparArma(itemArma);
    }

    // Remover el Arma 
    public void RemoverArma()
    {
        // Desactivamos el Icono del Arma 
        armaIcono.gameObject.SetActive(false);
        // Desactivamos el Proyectil del Arma
        armaSkillIcono.gameObject.SetActive(false);
        // Quitamos la Referencia del Arma Equipada 
        ArmaEquipada = null;

        // Remover Arma del Personaje 
        Inventario.Instance.Personaje.PersonajeAtaque.RemoverArma();
    }
}
