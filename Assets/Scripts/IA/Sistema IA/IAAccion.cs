using UnityEngine;

//abstract para que todas las clases que hereden IAAccion implimente el metodo principal
//Para crear una accion debemos heredar de ScriptableObjet
public abstract class IAAccion : ScriptableObject
{

    //Creamos el metodo ejecutar que para ejecutar una accion necesita como parametro a IAController
    public abstract void Ejecutar(IAController controller);
}
