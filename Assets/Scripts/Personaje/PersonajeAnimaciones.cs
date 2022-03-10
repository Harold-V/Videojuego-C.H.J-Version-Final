using System;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    // Nombres de los Layers
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    // Referencia a la clase PersonajeMovimiento y al Animator de Unity, Metodo Awake()
    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    private PersonajeAtaque _personajeAtaque;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento = GetComponent<PersonajeMovimiento>();
        _personajeAtaque = GetComponent<PersonajeAtaque>();
    }

    private void Update()
    {
        // Llamada del Metodo
        ActualizarLayers();
        
        // Conocer si el Personaje esta en Movimiento
        if (_personajeMovimiento.EnMovimiento == false)
            return;

        // Acceso a los Parametros X y Y para darles un Valor (Modificar)
        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);
    }

    // Activar o Desactivar Layers
    private void ActivarLayer(string nombreLayer)
    {
        // Desactivar todos los Layers
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }
        
        // Activar el Layer
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    // Activar Layer en base al Movimiento del Personaje
    private void ActualizarLayers()
    {
        //Condicion para saber si esamos atacando y mostrar las respectivas animaciones
        if (_personajeAtaque.Atacando)
            ActivarLayer(layerAtacar);
        else if (_personajeMovimiento.EnMovimiento)
            ActivarLayer(layerCaminar);
        else
            ActivarLayer(layerIdle);
    }

    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }
    
    private void PersonajeDerrotadoRespuesta()
    {
        /* Si el peso del Layer Idle es igual a 1 (activado) Mostrar personaje derrotado*/
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
            _animator.SetBool(derrotado, true);
    }

    // Sobreescribir Clase a un Evento
    // Metodo cuando la Clase esta Activa
    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }

    // Metodo Cuando la Clase esta Desactivada
    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }
}
