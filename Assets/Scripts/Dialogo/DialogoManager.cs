using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Hacemos la Clase Singlenton para poder LLamarla en Otras Clases 
public class DialogoManager : Singleton<DialogoManager>
{
    // Referencia del Panel de Dialogo para Actualizarlo
    [SerializeField] private GameObject panelDialogo;

    // Referencia del Icono para Actualizarlo
    [SerializeField] private Image npcIcono;

    // Referencia a los Textos para Actualizarlos
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    // Propiedad que Guarda la Informacion del NPC del cual Queremos Mostrar el Dialogo 
    public NPCInteraccion NPCDisponible { get; set; }

    // Mostrar Conversacion, Variable que Almacena todos los Strings (Secuencia de Dialogos)
    private Queue<string> dialogosSecuencia;

    // Variable para Mostrar Letra por Letra los Dialogos 
    private bool dialogoAnimado;

    // Mostrar Despedida
    private bool despedidaMostrada;

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        // Si no hay NPC's, salimos, pero...
        if (NPCDisponible == null)
            return;

        // Si hay NPC's disponibles, podemos apretar la Letra E, para 
        if (Input.GetKeyDown(KeyCode.E))
            // Desplegar el Panel (Llamar a la Clase NPCInteraccion)
            ConfigurarPanel(NPCDisponible.Dialogo);

        // Continuar el Dialogo 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Verificar si el Dialogo Anterior ha Sido Animado 
            if (despedidaMostrada)
            {
                // Cerramos 
                AbrirCerrarPanelDialogo(false);
                // La Despedida fue Mostrada
                despedidaMostrada = false;
                // Salir 
                return;
            }

            // Interaccion Extra...
            if (NPCDisponible.Dialogo.ContieneInteraccionExtra)
            {
                // Abrir el Panel segun la Interaccion 
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                // Cerramos el Panel de Dialogo
                AbrirCerrarPanelDialogo(false);
                // Salimos
                return;
            }
            
            // Si el Dialogo fue Animado y aun hay Dialogos por mostrar...
            if (dialogoAnimado)
                // Continuar con el Dialogo 
                ContinuarDialogo();
        }
    }

    // Metodo para Abrir y Cerrar el Panel de Dialogo
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        // Si Pasamos true se Activa, con false se Desactiva
        panelDialogo.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        // Mostrar el Panel de Dialogo
        AbrirCerrarPanelDialogo(true);

        // Cargar los Dialogos 
        CargarDialogosSencuencia(npcDialogo);
        
        // Actualizar el Icono 
        npcIcono.sprite = npcDialogo.Icono;
        // Actualizar el Nombre
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        // Animar el Saludo del NPC
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void CargarDialogosSencuencia(NPCDialogo npcDialogo) // Pasamos los Dialogos del NPC
    {
        // Si no hay Conversacion, Sale, No Muestra Nada 
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
            return;

        // Recorrer toda la Conversacion 
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
    }

    private void ContinuarDialogo()
    {
        // Si no hay NPC's, salimos
        if (NPCDisponible == null)
            return;

        // Si la Despedida fue Mostrada, salimos
        if (despedidaMostrada)
            return;

        // Si ya no hay Dialogos...
        if (dialogosSecuencia.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            // Animamos la Despedida
            MostrarTextoConAnimacion(despedida);
            // La Despedida fue Mostrada 
            despedidaMostrada = true;
            // Salimos 
            return;
        }
        
        // Obtener el Siguiente Dialogo
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        // Animar el Nuevo Dialogo 
        MostrarTextoConAnimacion(siguienteDialogo);
    }
    
    private IEnumerator AnimarTexto(string oracion) 
    {
        dialogoAnimado = false;
        // Obtener la Cantidad de Letras Presentes en el Dialogo 
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray(); // Poner Todos los Caracteres en el Arreglo

        // Recorrer el Arreglo
        for (int i = 0; i < letras.Length; i++)
        {
            // Añadir Letra por Letra a la Conversacion
            npcConversacionTMP.text += letras[i];
            // Esperar unos Segundos para Imprimir un Letra tras Otra 
            yield return new WaitForSeconds(0.03f);
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimacion(string oracion)
    {
        // LLamar el Coroutine, y Pasarle Cualquier Oracion que queremos Animar dentro del Otro Metodo 
        StartCoroutine(AnimarTexto(oracion));
    }
}
