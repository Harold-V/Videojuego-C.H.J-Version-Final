using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumarcion 
public enum InteraccionExtraNPC
{
    // Interacciones Extra
    Quests,
    Tienda,
    Crafting
}

// Para Crear el NPCDialogo en las Carpetas A�adi�os el Atributo...
[CreateAssetMenu]

// Crear el ScriptableObject
public class NPCDialogo : ScriptableObject
{
    // Titulo
    [Header("Info")]
    
    // Variables
    public string Nombre;

    // Icono del NPC
    public Sprite Icono;

    // Verificar si Tiene Interacciones Extras
    public bool ContieneInteraccionExtra;
    // Conocer el Tipo de Interaccion 
    public InteraccionExtraNPC InteraccionExtra;

    // Definir...
    [Header("Saludo")] 
    // A�adir el Saludo 
    [TextArea] public string Saludo;

    [Header("Chat")]
    // Array de Oraciones, extraido de la Clase DialogoTexto
    public DialogoTexto[] Conversacion;
    
    [Header("Despedida")] 
    // A�dir la Despedida
    [TextArea] public string Despedida;
}

// A�adir el Atributo para ver la Clase en el Inspector 
[Serializable]
public class DialogoTexto
{
    [TextArea] public string Oracion;
}