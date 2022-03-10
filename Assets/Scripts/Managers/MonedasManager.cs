using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creamos el Singleton 
public class MonedasManager : Singleton<MonedasManager>
{
    // Decir con Cuantas Monedas Queremos Empezar (Prueba) 
    [SerializeField] private int monedasTest;
    
    // Propiedad que Almacena la Cantidad de Monedas
    public int MonedasTotales { get; set; }

    // Llave 
    private string KEY_MONEDAS = "MYJUEGO_MONEDAS";

    private void Start()
    {
        // Prueba 
        PlayerPrefs.DeleteKey(KEY_MONEDAS);
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        // Inicializamos Monedas con el Valor guardado en el KEY_MONEDAS
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest); // Le pasamos las Monedas de Prueba
    }

    public void AñadirMonedas(int cantidad) // Cantidad de Monedas que Deseamos Añadir 
    {
        // A monedas totales le Sumamos la Cantidad de Monedas que queremos Añadir 
        MonedasTotales += cantidad;

        // Guardar el Nuevo Valor de las Monedas 
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales); // Hay que darle una LLave y Una Contraseña 
        // Guardamos 
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad) // Cantidad de Monedas que Deseamos Remover 
    {
        /* Verificar si Podemos Remover Monedas 
           Si hay Menos monedas de las que queremos gastar...*/ 
        if (cantidad > MonedasTotales)
            // Regresamos 
            return;

        // A monedas totales le Restamos la Cantidad de Monedas que queremos Remover
        MonedasTotales -= cantidad;
        // Guardar el Nuevo Valor de las Monedas 
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales); // Hay que darle una LLave y Una Contraseña 
        // Guardamos 
        PlayerPrefs.Save();
    }
}
