using System;
using UnityEngine;

// Añadirlo a las Carpetas
[CreateAssetMenu]

// Hacer el ScriptableObject
public class Quest : ScriptableObject
{
    // Evento para saber si la Mision fue Completada 
    public static Action<Quest> EventoQuestCompletado;

    [Header("Info")] 

    // Variables
    public string Nombre;
    public string ID; // Buscar el Quest para entregar la Recompensa
    public int CantidadObjetivo;

    [Header("Descripcion")]
    
    // Descripcion de la Mision 
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    
    // Variables 
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;

    // Variables Ocultas en el Inspector 
    [HideInInspector] public int CantidadActual; // Acumular la Cantidad de Condiciones Cumplidas
    [HideInInspector] public bool QuestCompletadoCheck; // Decir si la Mision ha sido Cumplida o no 

    public void AñadirProgreso(int cantidad) // Cuanto Progreso se Añade
    {
        // Le Añadimos la Cantidad de Progreso a la Variable...
        CantidadActual += cantidad;
        // Verificamos si ya se Alcanzo la Cantidad Objetivo 
        VerificarQuestCompletado();
    }

    private void VerificarQuestCompletado()
    {
        // Si la Mision fue Completada 
        if (CantidadActual >= CantidadObjetivo)
        {
            // Igualamos las Variables 
            CantidadActual = CantidadObjetivo;
            // La Mision fue Completada
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {
        // Si la Mision fue Completada, Regresamos 
        if (QuestCompletadoCheck)
            return;

        // La Mision fue Completada 
        QuestCompletadoCheck = true;
        // Lanzamos el Evento siempre y cuando no sea Nulo 
        EventoQuestCompletado?.Invoke(this);
    }

    private void OnEnable()
    {
        // Reseteamos el Progremo en las Misiones cada vez que Jugamos
        QuestCompletadoCheck = false;
        CantidadActual = 0;
    }
}

// Atributo para ver la Clase en el Inspector 
[Serializable]

// Clase para Definir la cantidad del Item que se va a Otorgar
public class QuestRecompensaItem
{
    public InventarioItem Item; // Item a Otorgar 
    public int Cantidad;
}
