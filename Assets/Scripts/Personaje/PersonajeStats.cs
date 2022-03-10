using UnityEngine;

/* Propiedad para que el ScriptableObject pueda ser creado en el 
  menu, porque ScriptableObject es un Asset*/
[CreateAssetMenu(menuName = "Stats")]

/* Clase ScriptableObjetc (almacena grandes cantidades de datos
   compartidos independientes de instancias de script */
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]

    /* Si se pueden Añadir variables de tipo publico ya que esta 
     clase debe compartir su informacion constantemente */
    public float Daño = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;

    // Atributo que Permite Establecer un Rango
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")] 
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    // Se Oculta en el Inspector
    [HideInInspector] public int PuntosDisponibles;

    public void AñadirBonudPorAtributoFuerza()
    {
        Daño += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
    }

    public void AñadirBonusPorAtributoInteligencia()
    {
        Daño += 3f;
        PorcentajeCritico += 0.2f;
    }
    
    public void AñadirBonusPorAtributoDestreza()
    {
        Velocidad += 0.1f;
        PorcentajeBloqueo += 0.05f;
    }

    public void AñadirBonusPorArma(Arma arma) // Referencia del Arma 
    {
        // Al Daño le Sumamos el Daño que Otorga el Arma 
        Daño += arma.Daño;
        // Al Porcentaje Critico le Sumamos el Porcentaje que Otorga el Arma 
        PorcentajeCritico += arma.ChanceCritico;
        // Al Porcentaje de Bolqueo le Sumamos el Porcentaje de Bloqueo que Otorga el Arma 
        PorcentajeBloqueo += arma.ChanceBloqueo;
    }
    
    // Metodo similar al AñadirBonusPorArma, solo que en este se Remueven los Bonus cuando dejamos de usar el Arma
    public void RemoverBonusPorArma(Arma arma)
    {
        Daño -= arma.Daño;
        PorcentajeCritico -= arma.ChanceCritico;
        PorcentajeBloqueo -= arma.ChanceBloqueo;
    }
    
    public void ResetearValores()
    {
        Daño = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequeridaSiguienteNivel = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;

        PuntosDisponibles = 0;
    }
}