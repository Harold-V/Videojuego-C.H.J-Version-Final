using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creamos este atributo para crear la accion
[CreateAssetMenu(menuName = "IA/Acciones/Activar Camino Movimiento")]
public class AccionActivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        //Creamos la condicion si el enemigo no tiene movimiento regrese
        if (controller.EnemigoMovimiento == null)
            return;
        //aqui activamos el movimiento del enemigo
        controller.EnemigoMovimiento.enabled = true;
    }
}
