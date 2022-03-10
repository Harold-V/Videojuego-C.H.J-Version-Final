using System;
using UnityEngine;


//le damos el atributo serializable para poderlo ver en el inspector de unity
[Serializable]
public class IATransicion
{
    //Creamos las variables para hacer la trancicion dependiendo del estado(verdadero/Falso)
    public IADecision Decision;
    public IAEstado EstadoVerdadero;
    public IAEstado EstadoFalso;
}
