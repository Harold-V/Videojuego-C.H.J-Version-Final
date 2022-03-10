using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int cantidadPorCrear;

    //Lista para guardar las instancias que hemos creado
    private List<GameObject> lista;
    public GameObject ListaContenedor { get; private set; }

    public void CrearPooler(GameObject objetoPorCrear)
    {
        //inicializamos las listas para agregar el objeto
        lista = new List<GameObject>();
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}");

        //ciclo for que recorre la cantidad de instancias que vamos a crear 
        for (int i = 0; i < cantidadPorCrear; i++)
        {
            //Añadimos las instancias que hemor creado
            lista.Add(AñadirInstancia(objetoPorCrear));
        }
    }

    private GameObject AñadirInstancia(GameObject objetoPorCrear)
    {
        //Creamos una instancia y le pasamos el nuevo objeto que deseamos crear y lo retornamos
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform);
        //instancia desactivada
        nuevoObjeto.SetActive(false);
        return nuevoObjeto;
    }

    public GameObject ObtenerInstancia()
    {
        //Ciclo for que recorre la lista
        for (int i = 0; i < lista.Count; i++)
        {
            //Condicion para buscar objeto que no esta siendo utilizado
            if (lista[i].activeSelf == false)
                return lista[i];
        }

        return null;
    }

    // Metodo que Destruye el Pooler y Limpia la Vista
    public void DestruirPooler()
    {
        Destroy(ListaContenedor);
        lista.Clear();
    }
}
