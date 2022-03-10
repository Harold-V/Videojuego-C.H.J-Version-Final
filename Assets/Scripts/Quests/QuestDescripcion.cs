using TMPro;
using UnityEngine;

// Clase Base
public class QuestDescripcion : MonoBehaviour
{
    // Referencia del Nombre y Descripcion del Quest para Poder Actualizarlo
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questDescripcion;

    // Verificar si la Tarjeta (Mision) esta Creada en el Panel del Inspector 
    public Quest QuestPorCompletar { get; set; }
    

    public virtual void ConfigurarQuestUI(Quest quest) // Pasamos un Parametro de tipo Quest (Clase)
    {
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}
