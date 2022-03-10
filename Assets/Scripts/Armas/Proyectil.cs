using System;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]

    // Referencia de la Velocidad del Proyectil 
    [SerializeField] private float velocidad;


    public PersonajeAtaque PersonajeAtaque { get; private set; }
    
    // Referencias (Variables)
    private Rigidbody2D _rigidbody2D;
    private Vector2 direccion; // Direccion a la que se debe Mover el Proyectil 
    private EnemigoInteraccion enemigoObjetivo; // Enemigo que Debe seguir el Proyectil 

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Si no Tenemos un Enemigo Objetivo
        if (enemigoObjetivo == null)
            return; // Regresamos 
        
        MoverProyectil();
    }

    private void MoverProyectil()
    {
        // Establecer hacia donde se debe Mover el Proyectil 
        direccion = enemigoObjetivo.transform.position - transform.position;
        // Angulo para que el Proyectil Rote Hacia el Enemigo 
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        // Mover Hacia Adelante 
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        // Mover el Proyectil 
        _rigidbody2D.MovePosition(_rigidbody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    public void InicializarProyectil(PersonajeAtaque ataque)
    {
        //Le pasamos el ataque y a que enemigo queremos hacer el ataque
        PersonajeAtaque = ataque;
        enemigoObjetivo = ataque.EnemigoObjetivo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si Colisionamos con el Enemigo 
        //Hacemos que el enemigo reciba daño y recibimos cuanta vida tiene el enemigo
        if (other.CompareTag("Enemigo"))
        {
            float daño = PersonajeAtaque.ObtenerDaño();
            EnemigoVida enemigoVida = enemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(daño);
            PersonajeAtaque.EventoEnemigoDañado?.Invoke(daño, enemigoVida);
            // Desactivamos el Objeto (Proyectil)
            gameObject.SetActive(false);
        }
    }
}