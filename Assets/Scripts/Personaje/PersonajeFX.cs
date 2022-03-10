using System;
using System.Collections;
using UnityEngine;

//Enum de player y los enemigos
public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosicion;

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private EnemigoVida _enemigoVida;

    private void Awake()
    {
        //Obtenemos el componente de la variable
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        //Le pasamos el objeto que queremos creear
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        //Obtenemos la instancia y actualizamos el texto con el daño que recibe el personaje
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>();
        texto.EstablecerTexto(cantidad, color);
        //Posicion del texto Comparada con el del personaje para que se muestren en la misma posicion
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion);
        nuevoTextoGO.transform.position = canvasTextoPosicion.position;
        nuevoTextoGO.SetActive(true);

        //Regresamos el texto al pooler y desactivandolo y modificandolo en la lista contenedor
        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform);
    }

    private void RespuestaDañoRecibidoHaciaPlayer(float daño)
    {
        //Condicion para mostrar el daño en el personaje
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            //Mostrar el texto en el personaje
            StartCoroutine(IEMostrarTexto(daño, Color.black));
        }
    }

    private void RespuestaDañoHaciaEnemigo(float daño, EnemigoVida enemigoVida)
    {
        //Condicion para mostrar el daño hecho hacia el enemigo
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            //Mostrar el texto del daño que realizo
            StartCoroutine(IEMostrarTexto(daño, Color.red));
        }
    }

    private void OnEnable()
    {
        //Respondemos los metodos heredados con los metodos de esta clase 
        //Daño realizado hacia el personaje
        IAController.EventoDañoRealizado += RespuestaDañoRecibidoHaciaPlayer;
        //Daño realizado hacia el enemigo
        PersonajeAtaque.EventoEnemigoDañado += RespuestaDañoHaciaEnemigo;
    }

    private void OnDisable()
    {
        //Respondemos los metodos heredados con los metodos de esta clase 
        //Daño realizado hacia el personaje
        IAController.EventoDañoRealizado -= RespuestaDañoRecibidoHaciaPlayer;
        //Daño realizado hacia el enemigo
        PersonajeAtaque.EventoEnemigoDañado -= RespuestaDañoHaciaEnemigo;
    }
}
