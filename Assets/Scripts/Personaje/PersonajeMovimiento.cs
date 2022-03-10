using System;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    /* Velocidad de Movimiento, para definir la variable 
     en el Inspector de Unity se utiliza [SerializeField], ya que de no colocarlo
     no tendremos acceco a ella, debido a que es privada */
    [SerializeField] private float velocidad;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;

    // Metodo getter de _direccionMovimiento con expression body
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private Rigidbody2D _rigidbody2D;

    /* Para guardar la Direccion a la que nos queremos mover
       Inicializamos la Variable Privada _direccionMovimiento */
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    // Referencia del RigidBody2D asociada al GameObject (Metodo Awake())
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Obtencion de X para axis Horizontal y de Y para axis Vertical
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Direccion de Movimiento en X
        if (_input.x > 0.1f)
            // Movimiento a la Derecha
            _direccionMovimiento.x = 1f;
        else if (_input.x < 0f)
            // Movimiento a la Izquierda
            _direccionMovimiento.x = -1f;
        else
            // No hay Movimiento en X
            _direccionMovimiento.x = 0f;

        // Direccion de Movimiento en Y
        if (_input.y > 0.1f)
            // Movimiento hacia Arriba
            _direccionMovimiento.y = 1f;
        else if (_input.y < 0f)
            // Movimiento hacia Abajo
            _direccionMovimiento.y = -1f;
        else
            // No hay Movimiento en Y
            _direccionMovimiento.y = 0f;
    }

    // Mover Personaje (Metodo FixedUpdate porque se trabaja con RigidBody2D
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
    }
}
