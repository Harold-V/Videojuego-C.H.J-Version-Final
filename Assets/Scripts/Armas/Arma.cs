using UnityEngine;

// Enumeracion 
public enum TipoArma
{
    Magia,
    Melee
}

// Guardar la Clase dentro Personaje y le Ponemos Arma 
[CreateAssetMenu(menuName = "Personaje/Arma")]

// Hcaer la Clase ScribtableObject
public class Arma : ScriptableObject
{
    // Titulo 
    [Header("Config")] 

    // Icono del Arma 
    public Sprite ArmaIcono;

    // Indicar el Proyectil si tiene Proyectil
    public Sprite IconoSkill;

    // Indicar el Tipo de Arma 
    public TipoArma Tipo;

    // Indicar el Daño
    public float Daño;

    [Header("Arma Magica")] 

    // Referencia del Prefab de Proyectil 
    public Proyectil ProyectilPrefab;

    // Indicar el Mana que Requiere el Arma
    public float ManaRequerida;

    [Header("Stats")]
    
    // Indicar la Cantidad de Critico y Bloqueo que Añaden las Armas al Jugador 
    public float ChanceCritico;
    public float ChanceBloqueo;
}
