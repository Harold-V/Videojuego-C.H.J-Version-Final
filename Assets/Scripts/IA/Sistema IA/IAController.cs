//Clase Principal del enemigo

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


//Creamos un enum y le damos los tipos de ataque
public enum TiposDeAtaque
{
    Melee,
    Embestida
}

public class IAController : MonoBehaviour
{
    public static Action<float> EventoDañoRealizado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadDeEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float daño;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoAtaque;
    [SerializeField] private bool mostrarRangoEmbestida;

    private float tiempoParaSiguienteAtaque;
    private BoxCollider2D _boxCollider2D;

    //Creamos las propiedades de los anteriores atributos para poder usarlos en otra clase
    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float Daño => daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;

    private void Start()
    {
        //creamos una variable de tipo collider para el enemigo
        _boxCollider2D = GetComponent<BoxCollider2D>();
        //Inicializamos la propiedad de estado actual con estado inicial 
        EstadoActual = estadoInicial;
        //Variable del enemigo
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
    }

    private void Update()
    {
        //Ejecutamos el estado 
        EstadoActual.EjecutarEstado(this);
    }

    //Creamos el metodo CambiarEstado y le pasamos como parametro el estado al cual queremos ir 
    public void CambiarEstado(IAEstado nuevoEstado)
    {
        //Hacemos la condicion de que se puede cambiar de estado cuando sea diferente al default
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    //Creamos el metodo de ataque mele
    public void AtaqueMelee(float cantidad)
    {
        //hacemos la condicion cuando este cuerpo a cuerpo con el personaje
        if (PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidad);
        }
    }

    //Creamos el metodo ataque embestida
    public void AtaqueEmbestida(float cantidad)
    {
        //ejecutamos el ataque a distancia
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        //Obtenemos la posicion del personaje 
        Vector3 personajePosicion = PersonajeReferencia.position;
        //Posicion del enemigo desde donde hara la embestida
        Vector3 posicionInicial = transform.position;
        //Direccion donde se hara la embestida
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized;
        //Posicion para que no choque el enemigo con el presonaje
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;
        _boxCollider2D.enabled = false;

        float transicionDeAtaque = 0f;
        while (transicionDeAtaque <= 1f)
        {
            //actualizar la trancicion del ataque mediante la velocidad de la embestida
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4f;
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;
        }

        //Condicion para Aplicar el ataque al personaje de referencia mientras no sea null
        if (PersonajeReferencia != null)
        {
            AplicarDañoAlPersonaje(cantidad);
        }

        //activar collider luego de hacer el ataque
        _boxCollider2D.enabled = true;
    }

    public void AplicarDañoAlPersonaje(float cantidad)
    {
        float dañoPorRealizar = 0;
        //Hacemos la condicion de La probabilidad de que haya un bloqueo
        if (Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }
        //Si no se cumple la condicion anterior aplicamos el daño 
        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
        EventoDañoRealizado?.Invoke(dañoPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        //Comparamos La distancias entre el personaje y el enemigo y retorna una variable booleana
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }

        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        //Hacemos la condicion comparando el tiempo y retornando una variable de tipo bool
        if (Time.time > tiempoParaSiguienteAtaque)
        {
            return true;
        }

        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        //Determinamos cada cuanto es tiempo para atacar
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    //creamos este metodo para visualizar el la deteccion del personaje
    private void OnDrawGizmos()
    {
        //hacemos la condicion para mostrar el rango en el que sigue al personaje o ataca a mele o con embestida de el color asignado
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
