using TMPro;
using UnityEngine;

// Hereda de la Clase Base 
public class InspectorQuestDescripcion : QuestDescripcion
{
    // Referencia del Texto que Muestra las Recompensas 
    [SerializeField] private TextMeshProUGUI questRecompensa;
    
    // Sobreescribimos el Metodo 
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        // Actualizar las Recompensas 
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +
                               $"\n-{quest.RecompensaExp} exp" +
                               $"\n-{quest.RecompensaItem.Item.Nombre} x {quest.RecompensaItem.Cantidad}";
    }

    // Para poder Aceptar la Mision 
    public void AceptarQuest()
    {
        if (QuestPorCompletar == null)
            return;
        
        QuestManager.Instance.AñadirQuest(QuestPorCompletar);
        // Remover la Mision del Panel del Inspector 
        gameObject.SetActive(false);
    }
}