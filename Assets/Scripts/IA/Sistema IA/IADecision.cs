using UnityEngine;

//Toda aquella decicion que tomemos herede de esta clase y implimente el metodo principal
public abstract class IADecision : ScriptableObject
{
    //Creamos el metodo Decidir y le pasamos como parametro a IAController
    public abstract bool Decidir(IAController controller);
}
