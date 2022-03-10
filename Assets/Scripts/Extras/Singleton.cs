using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    /* La Clase puede ser heredada por Cualquier cosa, se añade el 
   simbolo T, que es un simbolo generico, y lo que se hereda es un componente
   como un RididBody2D por ejemplo*/

    // Instancia
    private static T _instance;

    // Instancia Publica
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Buscar el Objeto del Tipo que se Busca del Tipo que esta Siendo Heredado
                _instance = FindObjectOfType<T>();

                // Si la Instancia aun es Nula, se debe buscar 
                if (_instance == null)
                {
                    // Creamos el Objeto
                    GameObject nuevoGO = new GameObject();
                    // La instancia va ser igual al Objeto, al cual se le añade el Componente
                    _instance = nuevoGO.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        // Instancia Igual a esta Clase pero Especificamente del Componente T
        _instance = this as T;
    }
}