using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dañoTexto;

    //Metodo para Actualizar texto sobre el daño
    public void EstablecerTexto(float cantidad, Color color)
    {
        //Convertimos la cantidad a un string
        dañoTexto.text = cantidad.ToString();
        // Le damos Color al Texto del Daño 
        dañoTexto.color = color;
    }
}
