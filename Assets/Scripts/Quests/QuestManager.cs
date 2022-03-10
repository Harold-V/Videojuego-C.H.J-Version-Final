using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    // Referencia del Personaje 
    [Header("Personaje")] 
    [SerializeField] private Personaje personaje;
    
    [Header("Quests")]
    // Referencia de todos los Quest (Array) 
    [SerializeField] private Quest[] questDisponibles;

    [Header("Inspector Quests")]
    // Referencia del Prefab de Inspector Quests y de donde se van a Cargar (Contener) los Quest
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;
    
    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quest Completado")]
    // Referencia del Panel de Quest Completado 
    [SerializeField] private GameObject panelQuestCompletado;

    // Referencia de Todas las Variables Que Contiene el Panel 
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    // Propiedad para Verificar si el Quest Por Reclamar Existe 
    public Quest QuestPorReclamar { get; private set; }
    
    private void Start()
    {
        CargarQuestEnInspector();
    }

    private void Update()
    {
        // Si oprimimos la Tecla V
        if (Input.GetKeyDown(KeyCode.V))
        {
            // Matamos Enemigos y Ganamos Progreso 
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    // Metodo para Cargar Quest en Inspector 
    private void CargarQuestEnInspector()
    {
        // Recorrer todos los Quest 
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            // Se debe Instanciar el Prefab de Inspector Quest 
            /* Referencia */ InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            
            // Configurar las Tarjetas 
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

    private void AñadirQuestPorCompletar(Quest questPorCompletar) 
    {
        // Instanciar el Prefab y guardarlo en una Referencia 
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa()
    {
        // Si no hay Recompensas Por Reclamar 
        if (QuestPorReclamar == null)
            // Salimos
            return;
        
        // Añadir las Monedas 
        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);
        // Añadir Experiencia 
        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        // Añdir el Item 
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        
        // Cerramos el Panel de Recompensa (PanelQuestCompletado)
        panelQuestCompletado.SetActive(false);
        // La Recompensa ha sido Reclamada 
        QuestPorReclamar = null;
    }
    
    public void AñadirProgreso(string questID, int cantidad) // Pasar el Nombre del Quest y la Cantidad de Progreso
    {
        // Verificar si el Quest Existe 
        Quest questPorActualizar = QuestExiste(questID);

        // Añadir el Progreso a Cada Mision 
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private Quest QuestExiste(string questID)
    {
        // Recorremos el Arreglo 
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            // Si alguno de los Quests disponibles tiene el Mismo ID que el Quest que estamos Buscando...
            if (questDisponibles[i].ID == questID)
                // Regresamos la Referencia 
                return questDisponibles[i];
        }

        // Si el Quest no fue encontrado, Regresamos Nulo, porque no fue Encontrado 
        return null;
    }

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        // Activar el Panel 
        panelQuestCompletado.SetActive(true);
        // Actualizar los textos 
        questNombre.text = questCompletado.Nombre;
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
        // Actualizar la Cantidad del Item 
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        // Actualizar el Icono del Item 
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;
    }
    
    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        // Verificar si el Quest por Reclamar Existe 
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        // Si Existe...
        if (QuestPorReclamar != null)
            // Mostrar el Panel, con el Quest por Reclamar 
            MostrarQuestCompletado(QuestPorReclamar);
    }
    
    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
