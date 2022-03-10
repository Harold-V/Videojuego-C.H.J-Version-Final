using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creamos este atributo para crear la accion
[CreateAssetMenu(menuName = "IA/Acciones/Desactivar Camino Movimiento")]
public class AccionDesactivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        //Creamos la condicion si el enemigo no tiene movimiento regrese
        if (controller.EnemigoMovimiento == null)
            return;

        //aqui desactivamos el movimiento del enemigo
        controller.EnemigoMovimiento.enabled = false;
    }
}
