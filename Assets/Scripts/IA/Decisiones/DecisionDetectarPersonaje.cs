using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Detectar Personaje")]
public class DecisionDetectarPersonaje : IADecision
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller);
    }


    private bool DetectarPersonaje(IAController controller)
    {
        //Creamos variable de tipo collider para detectar el personaje con una colision en circulo
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position,
            controller.RangoDeteccion, controller.PersonajeLayerMask);

        //cremos una condicion que detecta al personaje y regresa una variable bool 
        if (personajeDetectado != null)
        {
            controller.PersonajeReferencia = personajeDetectado.transform;
            return true;
        }

        //Cuando salimos del rango le damos como refencia null
        controller.PersonajeReferencia = null;
        return false;
    }
}
