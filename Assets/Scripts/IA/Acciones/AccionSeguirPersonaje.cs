using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creamos este atributo para crear la accion
[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : IAAccion
{
    //traemos el metodo ejecutar de IAAccion
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController controller)
    {
        //Hacemos una condicion asegurando que tenemos la referencia del personaje
        if (controller.PersonajeReferencia == null)
            return;

        //Obtenemos la direccion hacia cual debemos mover nuestro enemigo
        Vector3 dirHaciaPersonaje = controller.PersonajeReferencia.position - controller.transform.position;
        Vector3 direccion = dirHaciaPersonaje.normalized;
        float distancia = dirHaciaPersonaje.magnitude;

        //Hacemos la condicion de que si esta en el rango siga al personaje 
        if (distancia >= 1.30f)
            controller.transform.Translate(direccion * controller.VelocidadMovimiento * Time.deltaTime);
    }
}
