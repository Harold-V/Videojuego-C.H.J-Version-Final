using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Especificar que esta Clase Hereda de QuesrDescripcion 
public class PersonajeQuestDescripcion : QuestDescripcion
{
    // Referencia de las Recompensas y de la Mision (Tarea) para Poder Actualizarlos 
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]

    // Referencia del Sprite que se va a Modificar y la Cantidad a Otorgar 
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        // Mientras el Quest no ha sido Completado, hay que Actualizar el Texto 
        if (QuestPorCompletar.QuestCompletadoCheck)
            // Regresamos 
            return;
        
        // Actualizamos la Cantidad 
        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
    }

    // Sobreercibir el Metodo 
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        // Actulizar textos 
        recompensaOro.text = quest.RecompensaOro.ToString();
        recompensaExp.text = quest.RecompensaExp.ToString();
        // Actualizar la Tarea 
        tareaObjetivo.text = $"{quest.CantidadActual}/{quest.CantidadObjetivo}";

        // Actualizar la Imagen (sprite)
        recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono;
        // Actualizar la Cantidad a Otorgar
        recompensaItemCantidad.text = quest.RecompensaItem.Cantidad.ToString();
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        // Verificar si el Quest que acaba de ser Completado es el Mismo Quest Cargado en Update
        if (questCompletado.ID == QuestPorCompletar.ID)
        {
            // Actualizamos la Tarea Objetivo 
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            // Se elimina del Panel porque ya Fue Completada la Mision 
            gameObject.SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        // Si el Panel de Quest esta desactiavdo y si el Quest por Completar, ya fue Completado
        if (QuestPorCompletar.QuestCompletadoCheck)
            // Quitamos la Mision del Panel de Quest
            gameObject.SetActive(false);
        
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
