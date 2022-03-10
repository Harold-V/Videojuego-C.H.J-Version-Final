using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDañado;
    
    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats;

    [Header("Pooler")] 

    /* Referencia del ObjectoPooler para llamar el Metodo de Pooler y pasarle la Referencia 
       del Proyectil Prefab*/ 
    [SerializeField] private ObjectPooler pooler;

    [Header("Ataque")]
    // Variable para Definir el Tiempo entre Disparos 
    [SerializeField] private float tiempoEntreAtaques;

    // Referencia de las Posiciones de Disparo (Array)
    [SerializeField] private Transform[] posicionesDisparo;
    
    // Propiedad para Guardar la Referencia del Arma Equipada 
    public Arma ArmaEquipada { get; private set; }

    // Propiedad para Guardar el Enemigo Seleccionado 
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }
    
    // Referencia a la Clase Personaje Mana
    private PersonajeMana _personajeMana;

    // Seleccionar la Posicion de Disparo 
    private int indexDireccionDisparo;

    // Variable para Definir si ya se Puede Atacar 
    private float tiempoParaSiguienteAtaque;

    private void Awake()
    {
        _personajeMana = GetComponent<PersonajeMana>();
    }

    private void Update()
    {
        ObtenerDireccionDisparo();

        // Si el Tiempo de nuestro juego es Mayor al Tiempo Para Atacar, 
        if (Time.time > tiempoParaSiguienteAtaque)
            // Atacamos
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Verificamos si Tenemos un Arma, y Un Enemigo Selccionado
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                   /* de no ser asi, salimos */ return;
                
                // Usamos el Arma
                UsarArma();
                // Actualizamos el Tiempo entre Ataque 
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
                StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        // Si el Arma es de Tipo Magia...
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            // Miramos si la Cantidad de Mana es la Suficiente para Usar el Arma, de lo Contrario 
            if (_personajeMana.ManaActual < ArmaEquipada.ManaRequerida)
                return; // Salimos

            // Si no es el Caso 

            // Referencia del Proyectil del Pooler
            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            // Establecer desde donde Saldra el Proyectil 
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;

            // Establecer el Enemigo Objetivo
            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);
            
            // Activar el Proyectil 
            nuevoProyectil.SetActive(true);
            // Restamos el Mana que Consume el Arma
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida);
        }
        else
        {
            // Arma Melee
            float daño = ObtenerDaño();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(daño);
            EventoEnemigoDañado?.Invoke(daño, enemigoVida);
        }
    }

    public float ObtenerDaño()
    {
        //Refernciamos el daño que podemos hacer
        float cantidad = stats.Daño;
        //Condicion para Hacer nuestro porcentaje critico
        if (Random.value < stats.PorcentajeCritico / 100)
            cantidad *= 2;

        return cantidad;
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        //Establecemos que luego de un tiempo el estado de atacando cambie
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada = armaPorEquipar.Arma;
        // Si el Arma es de Tipo Magia...
        if (ArmaEquipada.Tipo == TipoArma.Magia)
            // Creamos el Pooler con los Proyectiles del Arma Seleccionada
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        
        // Accedemos a Stats y al Metodo le Pasamos el Arma para Obtener los Bonus 
        stats.AñadirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        // Verificar si Tenemos un Arma Equipada 
        if (ArmaEquipada == null)
            return;

        // Si el Arma es de Tipo Magia 
        if (ArmaEquipada.Tipo == TipoArma.Magia)
            // Destruimos el Pooler o el Proyectil
            pooler.DestruirPooler();

        // Accedemos a Stats y al Metodo le Pasamos el Arma para Remover los Bonus junto con el Arma
        stats.RemoverBonusPorArma(ArmaEquipada);
        // Quitar el Arma
        ArmaEquipada = null;
    }

    private void ObtenerDireccionDisparo()
    {
        // Conocer Hacia donde Se Mueve el Personaje 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Si nos Movemos Hacia la Derecha
        if (input.x > 0.1f)
            indexDireccionDisparo = 1;

        // Si nos Movemos Hacia la Izquierda 
        else if (input.x < 0f)
            indexDireccionDisparo = 3;

        // Si nos Movemos Hacia Arriba 
        else if (input.y > 0.1f)
            indexDireccionDisparo = 0;

        // Si nos Movemos Hacia Abajo 
        else if (input.y < 0f)
            indexDireccionDisparo = 2;
    }
    
    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        // Si arma Equipada es Igual a Nulo, Regresamos
        if (ArmaEquipada == null) { return; }
        // Si el Tipo de Arma es Diferente de Magia, tambien Regresamos 
        if (ArmaEquipada.Tipo != TipoArma.Magia) { return; }

        // Si el Enemigo que ya esta Seleccioando, se vuelve a Seleccionar, salimos
        if (EnemigoObjetivo == enemigoSeleccionado) { return; }

        EnemigoObjetivo = enemigoSeleccionado;
        // Mostramos el Enemigo Seleccionado
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        // Si no hay Ningun Enemigo Seleccionado, regresamos 
        if (EnemigoObjetivo == null) { return; }

        // Ocultar el Enemigo Seleccionado 
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        // Perder la Referencia 
        EnemigoObjetivo = null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        // Si arma Equipada es Igual a Nulo, Regresamos
        if (ArmaEquipada == null) { return; }
        // Si el Tipo de Arma es Diferente de Melee, tambien Regresamos 
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo = enemigoDetectado;
        // Mostrar el Arco de Seleccion de Melee
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    private void EnemigoMeleePerdido()
    {
        // Si arma Equipada es Igual a Nulo, Regresamos
        if (ArmaEquipada == null) { return; }
        // Si no hay Ningun Enemigo Seleccionado, regresamos 
        if (EnemigoObjetivo == null) { return; }

        // Si el Tipo de Arma es Diferente de Melee, tambien Regresamos 
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }

        // Ocultar el Enemigo Seleccionado de Tipo Melee
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        // Perder la Referencia 
        EnemigoObjetivo = null;
    }
    
    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }
}
