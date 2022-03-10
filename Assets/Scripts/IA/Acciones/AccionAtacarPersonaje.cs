using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    //Creamos metodo atacar
    private void Atacar(IAController controller)
    {
        //Hacemos las condiciones para verificar si tenemos una referencia del personaje
        if (controller.PersonajeReferencia == null)
            return;

        if (controller.EsTiempoDeAtacar() == false)
            return;

        //Hacemos la condicion para Saber si se [puede atacar si esta en rango
        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado))
        {
            //Hacemos las condiciones de que tipo de ataques podemos utilizar 
            if (controller.TipoAtaque == TiposDeAtaque.Embestida)
                controller.AtaqueEmbestida(controller.Daño);
            else
                controller.AtaqueMelee(controller.Daño);

            //Actualizamos los intervalos entre ataques
            controller.ActualizarTiempoEntreAtaques();
        }
    }
}
