using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje En Rango de Ataque")]
public class DecisionPersonajeRangoDeAtaque : IADecision
{
    //Llamamos el metodo exclusivo de la clase Decicion
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        //Creamos la condicion si el personaje de referencia no esta en rango regrese false
        if (controller.PersonajeReferencia == null)
            return false;

        //Si no se cumple la condicion anterior el personaje de referncia estara en rango y regresara verdadero
        float distancia = (controller.PersonajeReferencia.position -
                           controller.transform.position).sqrMagnitude;
        //Condicion para saber si el personaje esta en rango
        if (distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2))
            return true;

        return false;
    }
}
