using UnityEngine;
using UnityEditor;
using System.Collections;

namespace StateMachine{
	[CustomEditor(typeof(StateMachine))]
	public class StateMachineInspector : Editor {
		
		private void OnEnable(){
			EditorApplication.projectWindowItemOnGUI += OnDoubleClickItem;
		}
		
		private void OnDisable(){
			EditorApplication.projectWindowItemOnGUI -= OnDoubleClickItem;
		}
		
		public override void OnInspectorGUI (){

		}
		
		public virtual void OnDoubleClickItem(string test,Rect r){
			if (Event.current.type == EventType.MouseDown && Event.current.clickCount == 2 && r.Contains (Event.current.mousePosition)) {
				StateMachineEditor.Show((StateMachine)target);
			}
		}
	}
}