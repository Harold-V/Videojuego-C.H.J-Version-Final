using UnityEngine;

// Enumeracion 
public enum DireccionMovimiento
{
    // Direcciones a las que un Personaje se Puede Mover 
    Horizontal,
    Vertical
}

public class WaypointMovimiento : MonoBehaviour
{
    // Movimiento del Personaje 
    [SerializeField] protected float velocidad;

    // Propiedad 
    public Vector3 PuntoPorMoverse => _waypoint.ObtenerPosicionMovimiento(puntoActualIndex);
    
    // Referencias 
    protected Waypoint _waypoint;
    protected Animator _animator;

    // Conocer si el Personaje se Mueve Arriba, Abajo, Izq, Dec
    protected Vector3 ultimaPosicion;
    // Controlar el Punto al que nos Queremos Mover
    protected int puntoActualIndex;

    private void Start()
    {
        // El personaje debe moverse al Primer Punto de la Ruta
        puntoActualIndex = 0;

        _animator = GetComponent<Animator>();
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        MoverPersonaje();
        RotarPersonaje();
        RotarVertical();
        // Si Alcanzamos el Punto Determinado 
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }

    // Metodo para Mover el Personaje 
    private void MoverPersonaje()
    {
        // MoveTowards => Mover Hacia. Moverse de la Posicion Inicial a la Posicion Determinada
        transform.position = Vector3.MoveTowards(transform.position, PuntoPorMoverse,
            velocidad * Time.deltaTime);
    }

    // Nos Indica si ya Estamos en el Punto Determinado 
    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHaciaPuntoActual = (transform.position - PuntoPorMoverse).magnitude;
        // Si Estamos Cerca al Punto al que nos Estamos Moviendo 
        if (distanciaHaciaPuntoActual < 0.1f)
        {
            // Hemos Alcanzado el Punto Determinado 
            ultimaPosicion = transform.position;
            // Se regresa verdadero
            return true;
        }
        // No se Alcanzo el Punto Determinado, se Regresa Falso 
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        // Si el Personaje llega al Punto Final de la Ruta, debe Regresar al Punto Inicial, es decir...
        if (puntoActualIndex == _waypoint.Puntos.Length - 1)
            // Resetear a Cero 
            puntoActualIndex = 0;
        // Los Personajes se Mueven Infinitamente 
        else
        /* Si no se Alcanza el Punto Final, Hay que Actualizar el Index para que El personaje siga 
           Caminando hasta el Ultimo punto, sin excederlo */
        {
            if (puntoActualIndex < _waypoint.Puntos.Length - 1)
                // Actualizar el Index
                puntoActualIndex++;
        }
    }

    // Metodos para Cambiar la Animacion del Personaje Segun su Movimiento 
    protected virtual void RotarPersonaje()
    {
        
    }

    protected virtual void RotarVertical()
    {
        
    }
}
