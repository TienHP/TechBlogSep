using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using StateMachine.Action;
using System.Reflection;

namespace StateMachine{
	public static class FixRefactoring  {
		[MenuItem ("Window/State Machine/Fix Refactoring")]
		static void DoFix () {
			StateMachine[] stateMachines = UnityEditorTools.GetAssetsOfType<StateMachine> (".asset");
			StateMachineBehaviour[] list = UnityTools.FindAll<StateMachineBehaviour> (true);
			List<StateMachine> list0 = new List<StateMachine> ();
			foreach (StateMachineBehaviour b in list) {
				if(b.stateMachine != null){
					list0.Add(b.stateMachine);
				}
			}
			list0.AddRange (stateMachines);
			stateMachines = list0.ToArray ();

			foreach (StateMachine mStateMachine in stateMachines) {
				foreach(State state in mStateMachine.states){
					foreach(StateAction action in state.actions){
						FieldInfo[] fields=action.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
						foreach(FieldInfo field in fields){
							string dirtyField=field.GetDirty();
							if(!string.IsNullOrEmpty(dirtyField)){
								FieldInfo newField=action.GetType().GetField(dirtyField);
								newField.SetValue(action,field.GetValue(action));
							}
						}
						EditorUtility.SetDirty(action);
					}
				}
			}
		}

		private static void FixNullParameter(ScriptableObject node){
			FieldInfo[] fields = node.GetType ().GetFields (BindingFlags.Instance | BindingFlags.Public);
			for (int i=0; i< fields.Length; i++) {
				if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
					NamedParameter parameter = (NamedParameter)fields[i].GetValue (node);
					if(parameter == null){
						parameter=(NamedParameter)ScriptableObject.CreateInstance(fields[i].FieldType);
						fields[i].SetValue(node,parameter);
						if (EditorUtility.IsPersistent (node)) {
							AssetDatabase.AddObjectToAsset(parameter,node);
						}
						AssetDatabase.SaveAssets ();
					}
				}
			}
		}
	}
}