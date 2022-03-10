using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//para crear un estado en el juego le damos este atributo y le pasamos donde se encuentra 
[CreateAssetMenu(menuName = "IA/Estado")]
public class IAEstado : ScriptableObject
{
    //creamos las variables que van a contener todas las acciones y decisiones en forma de array
    public IAAccion[] Acciones;
    public IATransicion[] Transiciones;

    //Creamos el metodo para ejecutar las acciones y decisiones
    public void EjecutarEstado(IAController controller)
    {
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }

    //creamos este metodo para ejecutar todas las acciones del estado 
    private void EjecutarAcciones(IAController controller)
    {
        //condicion para no ejecutar las acciones cuando no haya acciones por ejecutar
        if (Acciones == null || Acciones.Length <= 0)
        {
            return;
        }

        //ciclo for para ejecutar todas las acciones que esten en el array de acciones
        for (int i = 0; i < Acciones.Length; i++)
        {
            Acciones[i].Ejecutar(controller);
        }
    }

    //Creamos este metodo para realizar la transicion dependiendo la decision
    private void RealizarTransiciones(IAController controller)
    {
        //condicion para no hacer transiciones cuando no haya transiciones por ejecutar
        if (Transiciones == null || Transiciones.Length <= 0)
        {
            return;
        }

        /*ciclo for para obtener el valor de la decision dependiendo si es verdadero o falso
        y haga su respectiva transicion*/
        for (int i = 0; i < Transiciones.Length; i++)
        {
            bool decisionValor = Transiciones[i].Decision.Decidir(controller);
            if (decisionValor)
            {
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso);
            }
        }
    }
}
