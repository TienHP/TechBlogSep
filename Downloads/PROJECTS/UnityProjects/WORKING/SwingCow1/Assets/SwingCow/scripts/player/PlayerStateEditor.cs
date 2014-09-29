using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SwingCow
{
		public class PlayerStateEditor : EditorWindow
		{
			
		public static void ShowWindow()
		{
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow.GetWindow(typeof(PlayerStateEditor));
		}

		[DrawGizmo(GizmoType.SelectedOrChild | GizmoType.NotSelected)]
		static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
		{   
			PlayerStateController behaviour = transform.GetComponent<PlayerStateController> ();
			
			if (behaviour != null && behaviour.currentState != null) { 
				var centeredStyle = new GUIStyle( GUI.skin.GetStyle("HelpBox"));
				centeredStyle.alignment = TextAnchor.UpperCenter;
				centeredStyle.fontSize = 32;
				Handles.Label (transform.position, behaviour.currentState.ToString(),centeredStyle);
			}
		}

		}//end class
}//end namespace