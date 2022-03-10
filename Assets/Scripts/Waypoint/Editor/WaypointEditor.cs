using System;
using UnityEditor;
using UnityEngine;

// Editor Customizado de tipo WayPoint 
[CustomEditor(typeof(Waypoint))]

// Definimos a la Clase como un Editor 
public class WaypointEditor : Editor
{
    // Definir a quien va Dirigido el Editor (target)
    Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        // Mover los Puntos 
        Handles.color = Color.red;

        // Definir si hay Puntos con los cuales Trabajar 
        if (WaypointTarget.Puntos == null)
            return;

        // Recorrer todos los Puntos de la clase Waypoint
        for (int i = 0; i < WaypointTarget.Puntos.Length; i++)
        {
            // Verificar los Cambios hechos en el Editor 
            EditorGUI.BeginChangeCheck();
            
            // Crear Handle
            Vector3 puntoActual = WaypointTarget.PosicionActual + WaypointTarget.Puntos[i];
            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, Quaternion.identity,
                0.7f, new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);
            
            // Crear Texto
            GUIStyle texto = new GUIStyle();
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            Vector3 alineamiento = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(WaypointTarget.PosicionActual + WaypointTarget.Puntos[i] + alineamiento
                , $"{i + 1}", texto);

            // Verificar si hay Cambios en el Editor 
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Monve Handle");
                WaypointTarget.Puntos[i] = nuevoPunto - WaypointTarget.PosicionActual;
            }
        }
    }
}
