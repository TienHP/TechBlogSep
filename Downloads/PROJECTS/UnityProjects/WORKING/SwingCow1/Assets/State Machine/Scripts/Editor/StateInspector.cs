using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using StateMachine.Action;
using StateMachine.Condition;

namespace StateMachine{
	[CustomEditor(typeof(State),true)]
	public class StateInspector : Editor {
		private State state;
		private ReorderableList actionList;
		private ReorderableList transitionList;
		private int transitionIndex;
		private bool transitionIndexChanged;
		private ReorderableList conditionList;


		public void OnEnable(){
			if (target is State) {
				stateMachineEditor = StateMachineEditor.Instance;
						state = (State)target;
						actionList = new ReorderableList (state.actions, "Actions", true, true);
						actionList.onAddCallback = OnAddAction;
						actionList.drawElementCallback = DrawAction;
						actionList.onRemoveCallback = RemoveAction;
						actionList.onHeaderClick = OnActionHeaderClick;

						transitionList = new ReorderableList (state.transitions, "Transition", false, false);
						transitionList.drawElementCallback = DrawTransition;
						transitionList.onSelectCallback = OnTransitionSelected;
						transitionList.onRemoveCallback = RemoveTransition;
						conditionList = null;
				}
		}
		
		public override void OnInspectorGUI ()
		{

			if (stateMachineEditor.stateMachine == null) {
				return;			
			}
			actionList.DoList ();
			GUILayout.Space (5);
			if (state.transitions != null && state.transitions.Count > 0) {
				transitionList.DoList ();
				GUILayout.Space (5);
				if(transitionIndexChanged &&Event.current.type== EventType.Layout){
					conditionList=null;
					transitionIndexChanged=false;
				}
				
				if(conditionList == null){
					conditionList = new ReorderableList (state.transitions [transitionIndex].conditions, "Condition", true, true);
					conditionList.onAddCallback=OnAddCondition;
					conditionList.onRemoveCallback=RemoveCondition;
					conditionList.drawElementCallback=DrawCondition;
					conditionList.onHeaderClick=OnConditionHeaderClick;
				}
				conditionList.DoList();
			}
		}
		
		#region Action
		private void OnAddAction(){
			GenericMenu genericMenu = new GenericMenu ();
			IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(typeof(StateAction)));
			foreach(Type type in types){
				genericMenu.AddItem (new GUIContent (type.GetCategory()+"/" + type.ToString().Split('.').Last()), false, this.AddAction,type);
			}
			genericMenu.ShowAsContext ();
		}
		
		private void AddAction(object data){
			Type type = (Type)data;
			StateAction action = (StateAction)ScriptableObject.CreateInstance(type);
			
			action.name = type.GetCategory () + "." + type.ToString ().Split ('.').Last ();
			if (EditorUtility.IsPersistent (state)) {
				AssetDatabase.AddObjectToAsset (action, state);
			}
			AssetDatabase.SaveAssets();
			if (state.actions == null) {
				state.actions= new List<StateAction>();			
			}
			state.actions.Add(action);
			FieldInfo[] fields = type.GetFields ();
			
			for (int i=0; i< fields.Length; i++) {
				if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
					NamedParameter paramter=(NamedParameter)ScriptableObject.CreateInstance(fields[i].FieldType);
					paramter.name=fields[i].Name;
					fields[i].SetValue(action,paramter);
					if(fields[i].GetDefaultValue() != null){
						paramter.GetType().GetProperty("Value").SetValue(paramter,fields[i].GetDefaultValue(),null);
					}
					if (EditorUtility.IsPersistent (action)) {
						AssetDatabase.AddObjectToAsset (paramter, action);
					}
					AssetDatabase.SaveAssets();
				}		
			}
		
			actionList = new ReorderableList (state.actions, "Actions", true, true);
			actionList.onAddCallback = OnAddAction;
			actionList.drawElementCallback = DrawAction;
			actionList.onRemoveCallback = RemoveAction;
			actionList.onHeaderClick = OnActionHeaderClick;
		}
		
		private void RemoveAction(int index){
			StateAction action = state.actions [index];
			state.actions.Remove (action);
			FieldInfo[] fields = action.GetType().GetFields ();
			
			for (int i=0; i< fields.Length; i++) {
				if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
					DestroyImmediate((UnityEngine.Object)fields[i].GetValue(action),true);
				}		
			}
			
			DestroyImmediate (action, true);
			AssetDatabase.SaveAssets ();
		}
		
		private void DrawAction(int index){
			StateAction action = state.actions [index];
			GUI.enabled = !action.disabled && GUI.enabled;
			DrawNode (action);
			GUI.enabled = true;
		}

		#region Copy/Paste Action
		private void OnActionHeaderClick(){
			GenericMenu genericMenu = new GenericMenu ();
			if(state.actions == null || state.actions.Count == 0 ){
				genericMenu.AddDisabledItem (new GUIContent ("Copy"));
			}else{
				genericMenu.AddItem (new GUIContent ("Copy"), false, new GenericMenu.MenuFunction (this.CopyActions));
			}
			if(copiedActions != null && copiedActions.Count>0 && actionsCopiedState != null && actionsCopiedState.id != state.id){
				genericMenu.AddItem (new GUIContent ("Paste"), false, new GenericMenu.MenuFunction (this.PasteActions));
			}else{
				genericMenu.AddDisabledItem (new GUIContent ("Paste"));
			}
			genericMenu.ShowAsContext ();
			Event.current.Use ();
		}

		public static State actionsCopiedState;
		public static List<StateAction> copiedActions;
		private void CopyActions(){
			actionsCopiedState = state;
			copiedActions = new List<StateAction> (state.actions);
		}
		
		private void PasteActions(){
			if (state.actions == null) {
				state.actions= new List<StateAction>();			
			}
			foreach (StateAction action in state.actions) {
				FieldInfo[] fields = action.GetType ().GetFields ();
				for (int i=0; i< fields.Length; i++) {
					if (fields [i].FieldType.IsSubclassOf (typeof(NamedParameter))) {
						DestroyImmediate ((UnityEngine.Object)fields [i].GetValue (action), true);
					}		
				}
				DestroyImmediate(action,true);
			}

			state.actions.Clear ();
			AssetDatabase.SaveAssets ();
			
			foreach (StateAction action in copiedActions) {
				StateAction copy=(StateAction)ScriptableObject.Instantiate(action);
				copy.name=copy.name.Replace("(Clone)","");
				if (EditorUtility.IsPersistent (state)) {
					AssetDatabase.AddObjectToAsset(copy,state);
				}
				AssetDatabase.SaveAssets ();

				FieldInfo[] fields = action.GetType().GetFields ();
				for (int i=0; i< fields.Length; i++) {
					if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
						NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate((UnityEngine.Object)fields[i].GetValue(action));
						if (EditorUtility.IsPersistent (copy)) {
							AssetDatabase.AddObjectToAsset(paramter,copy);
						}
						AssetDatabase.SaveAssets();
						fields[i].SetValue(copy,paramter);
					}		
				}
				state.actions.Add(copy);
			}
			EditorUtility.SetDirty (state);
			actionList = new ReorderableList (state.actions, "Actions", true, true);
			actionList.onAddCallback = OnAddAction;
			actionList.drawElementCallback = DrawAction;
			actionList.onRemoveCallback = RemoveAction;
			actionList.onHeaderClick = OnActionHeaderClick;
		}
		#endregion
		#endregion
		
		#region Transition
		private void DrawTransition(int index){
			if (state.transitions [index].fromState != null && state.transitions [index].toState != null) {
								GUI.color = transitionIndex == index ? EditorStyles.foldout.focused.textColor : Color.white;
								GUILayout.Label (state.transitions [index].fromState.name + " -> " + state.transitions [index].toState.name);
								GUI.color = Color.white;
						} else {
				RemoveTransition(index);			
			}
		}
		
		private void OnTransitionSelected(int index){
			transitionIndex = index;
			StateMachineEditor.Instance.transitionIndex = index;
			StateMachineEditor.Instance.Repaint ();
			transitionIndexChanged = true;
			Event.current.Use ();
		}
		
		private void RemoveTransition(int index){
			transitionIndex = 0;
			StateMachineEditor.Instance.transitionIndex = 0;
			transitionIndexChanged = true;

			state.transitions [index].DeleteTransition ();
			state.transitions.RemoveAt(index);
			AssetDatabase.SaveAssets();
		}
		#endregion
		
		#region Condition
		private void DrawCondition(int index){
			if (state.transitions [transitionIndex].conditions == null) {
				state.transitions [transitionIndex].conditions=new List<StateCondition>();			
			}
			if (state.transitions [transitionIndex].conditions.Count > index) {
				StateCondition condition = state.transitions [transitionIndex].conditions [index];
				GUI.enabled = !condition.disabled && GUI.enabled;
				DrawNode (condition);
				GUI.enabled=true;
			}
		}	
		
		private void OnAddCondition(){
			GenericMenu genericMenu = new GenericMenu ();
			IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(typeof(StateCondition)));
			foreach(Type type in types){
				genericMenu.AddItem (new GUIContent (type.GetCategory()+"/" + type.ToString().Split('.').Last()), false, this.AddCondition,type);
			}
			genericMenu.ShowAsContext ();
		}
		
		private void AddCondition(object data){
			Type type = (Type)data;
			StateCondition condition = (StateCondition)ScriptableObject.CreateInstance(type);
			
			condition.name = type.GetCategory () + "." + type.ToString ().Split ('.').Last ();
			if (EditorUtility.IsPersistent (state.transitions [transitionIndex])) {
				AssetDatabase.AddObjectToAsset (condition, state.transitions [transitionIndex]);
			}
			AssetDatabase.SaveAssets();
			if (state.transitions [transitionIndex].conditions == null) {
				state.transitions[transitionIndex].conditions= new List<StateCondition>();			
			}
			state.transitions[transitionIndex].conditions.Add(condition);
			FieldInfo[] fields = type.GetFields ();
			
			for (int i=0; i< fields.Length; i++) {
				if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
					NamedParameter paramter=(NamedParameter)ScriptableObject.CreateInstance(fields[i].FieldType);
					paramter.name=fields[i].Name;
					fields[i].SetValue(condition,paramter);
					if (EditorUtility.IsPersistent (condition)) {
						AssetDatabase.AddObjectToAsset (paramter, condition);
					}
					AssetDatabase.SaveAssets();
				}		
			}
			conditionList = null;
		}
		
		private void RemoveCondition(int index){
			StateCondition condition = state.transitions [transitionIndex].conditions [index];

			FieldInfo[] fields = condition.GetType().GetFields ();
			
			for (int i=0; i< fields.Length; i++) {
				if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
					DestroyImmediate((UnityEngine.Object)fields[i].GetValue(condition),true);
				}		
			}

			DestroyImmediate(condition,true);
			state.transitions[transitionIndex].conditions.RemoveAt(index);
			AssetDatabase.SaveAssets();
		}

		#region Copy/Paste Condition
		private void OnConditionHeaderClick(){
			GenericMenu genericMenu = new GenericMenu ();
			if (state.transitions [transitionIndex].conditions == null) {
				state.transitions[transitionIndex].conditions= new List<StateCondition>();			
			}
			if(state.transitions[transitionIndex].conditions.Count == 0 ){
				genericMenu.AddDisabledItem (new GUIContent ("Copy"));
			}else{
				genericMenu.AddItem (new GUIContent ("Copy"), false, new GenericMenu.MenuFunction (this.CopyConditions));
			}
			if(copiedConditions != null && copiedConditions.Count>0 && conditionsCopiedState != null ){//&& conditionsCopiedState.id != state.id){
				genericMenu.AddItem (new GUIContent ("Paste"), false, new GenericMenu.MenuFunction (this.PasteConditions));
			}else{
				genericMenu.AddDisabledItem (new GUIContent ("Paste"));
			}
			genericMenu.ShowAsContext ();
			Event.current.Use ();
		}

		public static State conditionsCopiedState;
		public static List<StateCondition> copiedConditions;
		private void CopyConditions(){
			conditionsCopiedState = state;
			copiedConditions = new List<StateCondition> (state.transitions[transitionIndex].conditions);
		}
		
		private void PasteConditions(){
			foreach (StateCondition condition in state.transitions[transitionIndex].conditions) {
				FieldInfo[] fields = condition.GetType ().GetFields ();
				for (int i=0; i< fields.Length; i++) {
					if (fields [i].FieldType.IsSubclassOf (typeof(NamedParameter))) {
						DestroyImmediate ((UnityEngine.Object)fields [i].GetValue (condition), true);
					}		
				}
				DestroyImmediate(condition,true);
			}
			
			state.transitions[transitionIndex].conditions.Clear ();
			AssetDatabase.SaveAssets ();
			
			foreach (StateCondition condition in copiedConditions) {
				StateCondition copy=(StateCondition)ScriptableObject.Instantiate(condition);
				copy.name=copy.name.Replace("(Clone)","");
				if (EditorUtility.IsPersistent (state)) {
					AssetDatabase.AddObjectToAsset(copy,state);
				}
				AssetDatabase.SaveAssets ();
				
				FieldInfo[] fields = condition.GetType().GetFields ();
				for (int i=0; i< fields.Length; i++) {
					if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
						NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate((UnityEngine.Object)fields[i].GetValue(condition));
						if (EditorUtility.IsPersistent (copy)) {
							AssetDatabase.AddObjectToAsset(paramter,copy);
						}
						AssetDatabase.SaveAssets();
						fields[i].SetValue(copy,paramter);
					}		
				}
				state.transitions[transitionIndex].conditions.Add(copy);
			}
			conditionList = null;
			EditorUtility.SetDirty (state);
		}
		#endregion
		#endregion

		private bool ValidateNode(ScriptableObject node){
			if (node.GetType () == typeof(GetProperty) || node.GetType () == typeof(SetProperty)) {
				return true;			
			}
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
					if(!parameter.IsConstant && fields[i].IsFieldRequired () && parameter.Reference == fields[i].GetNullLabel ()){
						return false;
					}
				}
			}
			return true;
		}

		private void DrawNode(ScriptableObject node){
			bool valid = ValidateNode(node) && Event.current.type == EventType.Repaint;
			FieldInfo[] fields = node.GetType ().GetFields (BindingFlags.Instance | BindingFlags.Public);

			List<FieldInfo> list0 = new List<FieldInfo> ();
			List<FieldInfo> list1 = new List<FieldInfo> ();

			for (int mField=0; mField<fields.Length; mField++) {
				if (fields[mField].DeclaringType.BaseType == typeof(StateAction)) {
					list0.Add(fields[mField]);
				}else{
					list1.Add(fields[mField]);
				}
			}

			list0.AddRange (list1);
			fields = list0.ToArray ();

			bool foldout = EditorPrefs.GetBool (node.GetHashCode ().ToString (), true);
			GUIContent title = new GUIContent (node.name.Replace("/","."), node.GetType ().GetDescription ());
			
			GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();

			bool flag = true;
			for (int i=0; i< fields.Length; i++) {

				foreach (object attr in fields[i].GetCustomAttributes(true)) {
					if (attr.GetType () != typeof(HideInInspector)) {
						flag = false;
					}
				}
			}

			if (fields.Length > 0 && !flag) {
				GUILayout.Space (15);
				foldout = EditorGUILayout.Foldout (foldout, title);		
			} else {
				GUILayout.Label (title);
			}
			EditorPrefs.SetBool (node.GetHashCode().ToString(),foldout);
			GUILayout.FlexibleSpace ();

			if (!valid) {
				GUIStyle style = new GUIStyle ("label");
				style.margin = new RectOffset (0, 0, 1, 0);
				GUILayout.Label (GraphEditor.IconContent ("console.erroricon.sml", "Error"), style);
				this.stateMachineEditor.Repaint();
			} else {
				GUILayout.Space(20);		
			}

			bool enabled = GUI.enabled;
			GUI.enabled = true;
			if (GUILayout.Button (GraphEditor.IconContent ("_help", "Open Reference for " + node.GetType ().ToString ()), "label")) {
				Application.OpenURL(node.GetType().GetInfoUrl());		
			}
			
			if (GUILayout.Button (GraphEditor.IconContent ("_popup", "Settings"), "label")) {
				OnSettingsClick(node);
			}
			GUI.enabled = enabled;
			GUILayout.EndHorizontal ();
		
			if (foldout) {
				
				EditorGUIUtility.labelWidth = 80;

				GUILayout.BeginHorizontal();
				GUILayout.Space(16);
				GUILayout.BeginVertical();

				if(EditorPrefs.GetBool("DisplayActionDescription",true) && !string.IsNullOrEmpty(node.GetType ().GetDescription ())){
					GUILayout.BeginVertical("box");
					GUIStyle style= new GUIStyle("label");
					style.fixedHeight=0;
					style.wordWrap=true;
					GUILayout.Label(node.GetType ().GetDescription (),style);
					GUILayout.EndVertical();
				}
				if(node.GetType()== typeof(GetProperty)){
					DrawGetPropertyAction(node);
				}else if(node.GetType() == typeof(SetProperty)){
					DrawSetPropertyAction(node);
				}else{
					for (int i=0; i< fields.Length; i++) {
						bool hideInInspector=false;
						foreach(object attr in fields[i].GetCustomAttributes(true)){
							if(attr.GetType() == typeof(HideInInspector)){
								hideInInspector=true;
							}
						}

						//Extra check to fix refactoring
						if(!hideInInspector){
							hideInInspector=!string.IsNullOrEmpty(fields[i].GetDirty());
						}
						if(!hideInInspector){
							if(fields[i].FieldType.IsSubclassOf(typeof(NamedParameter))){
								NamedParameter parameter=(NamedParameter)fields[i].GetValue(node);
								DrawParameter(parameter,fields[i]);
							}else{
								DrawField(node,fields[i]);	
							}
						}
					}
				}

				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}

			GUILayout.EndVertical ();
		}

		private void OnSettingsClick(ScriptableObject node){
			GenericMenu genericMenu = new GenericMenu ();
			bool disabled = false;
			if (node is StateAction) {
				disabled=((StateAction)node).disabled;			
			}
			if (node is StateCondition) {
				disabled=((StateCondition)node).disabled;			
			}

			if (disabled) {
				genericMenu.AddItem (new GUIContent ("Enable"), false, this.EnableNode,node);
				genericMenu.AddDisabledItem(new GUIContent("Disable"));
			} else {
				genericMenu.AddDisabledItem(new GUIContent("Enable"));
				genericMenu.AddItem (new GUIContent ("Disable"), false, this.DisableNode,node);
			}
			genericMenu.ShowAsContext ();
			Event.current.Use ();
		}

		private void DisableNode(object node){
			if (node is StateAction) {
				((StateAction)node).disabled=true;			
			}
			if (node is StateCondition) {
				((StateCondition)node).disabled=true;			
			}

			EditorUtility.SetDirty (state);
		}

		private void EnableNode(object node){
			if (node is StateAction) {
				((StateAction)node).disabled=false;			
			}
			if (node is StateCondition) {
				((StateCondition)node).disabled=false;			
			}

			EditorUtility.SetDirty (state);
		}

		private void DrawGetPropertyAction(ScriptableObject node){
			if(node.GetType() == typeof(GetProperty)){
				GetProperty getPropertyAction= node as GetProperty;
				DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("gameObject").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("gameObject"));
				DrawField(node,node.GetType().GetField("component"));

				Type componentType=UnityTools.GetType(getPropertyAction.component);
				if(componentType == null){
					componentType=UnityTools.GetType("UnityEngine."+getPropertyAction.component);
				}
				if(componentType != null){
					FieldInfo[] fields = componentType
						.GetFields (BindingFlags.Public | BindingFlags.Instance)
							.Where (x => x.FieldType.IsPrimitive || x.FieldType == typeof(string) || x.FieldType == typeof(Color) || x.FieldType == typeof(Vector3) || x.FieldType==typeof(UnityEngine.Object) || x.FieldType==typeof(KeyCode))
							.ToArray();
					PropertyInfo[] properties = componentType
						.GetProperties (BindingFlags.Public | BindingFlags.Instance)
							.Where (x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(string) || x.PropertyType == typeof(Color) || x.PropertyType == typeof(Vector3) || x.PropertyType==typeof(UnityEngine.Object) || x.PropertyType==typeof(KeyCode))
							.ToArray();
					
					if(fields.Length>0 || properties.Length >0){
						List<string> names=fields.Select(x=>x.Name).ToList();
						names.AddRange(properties.Select(x=>x.Name).ToList());
						getPropertyAction.property=UnityEditorTools.StringPopup("Property",getPropertyAction.property,names.ToArray());
						
						FieldInfo field=fields.ToList().Find(x=>x.Name==getPropertyAction.property);
						Type type= null;
						if(field != null){
							type=fields.ToList().Find(x=>x.Name==getPropertyAction.property).FieldType;
						}else{
							type=properties.ToList().Find(x=>x.Name==getPropertyAction.property).PropertyType;
						}
						
						if(type == typeof(int) || type== typeof(KeyCode)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeInt").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeInt"));
						}else if(type == typeof(float)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeFloat").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeFloat"));
						}else if(type == typeof(string)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeString").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeString"));
						}else if(type == typeof(bool)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeBool").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeBool"));
						}else if(type == typeof(UnityEngine.Object)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeObj").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeObj"));
						}else if(type == typeof(UnityEngine.Color)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeColor").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeColor"));
						}else if(type == typeof(UnityEngine.Vector3)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("storeVector3").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("storeVector3"));
						}
					}
				}
				DrawField(node,node.GetType().GetField("everyFrame"));
			}	
		}

		private void DrawSetPropertyAction(ScriptableObject node){
			if(node.GetType() == typeof(SetProperty)){
				SetProperty getPropertyAction= node as SetProperty;
				DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("gameObject").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("gameObject"));
				DrawField(node,node.GetType().GetField("component"));
				
				Type componentType=UnityTools.GetType(getPropertyAction.component);
				if(componentType == null){
					componentType=UnityTools.GetType("UnityEngine."+getPropertyAction.component);
				}
				if(componentType != null){
					FieldInfo[] fields = componentType
						.GetFields (BindingFlags.Public | BindingFlags.Instance)
							.Where (x => x.FieldType.IsPrimitive || x.FieldType == typeof(string) || x.FieldType == typeof(Color) || x.FieldType == typeof(Vector3) || x.FieldType == typeof(UnityEngine.Object) || x.FieldType.IsSubclassOf(typeof(UnityEngine.Object)))
							.ToArray();
					PropertyInfo[] properties = componentType
						.GetProperties (BindingFlags.Public | BindingFlags.Instance)
							.Where (x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(string) || x.PropertyType == typeof(Color) || x.PropertyType == typeof(Vector3) || x.PropertyType == typeof(UnityEngine.Object) || x.PropertyType.IsSubclassOf(typeof(UnityEngine.Object)))
							.ToArray();
					
					if(fields.Length>0 || properties.Length >0){
						List<string> names=fields.Select(x=>x.Name).ToList();
						names.AddRange(properties.Select(x=>x.Name).ToList());
		
						getPropertyAction.property=UnityEditorTools.StringPopup("Property",getPropertyAction.property,names.ToArray());
						
						FieldInfo field=fields.ToList().Find(x=>x.Name==getPropertyAction.property);
						Type type= null;
						if(field != null){
							type=fields.ToList().Find(x=>x.Name==getPropertyAction.property).FieldType;
						}else{
							type=properties.ToList().Find(x=>x.Name==getPropertyAction.property).PropertyType;
						}
						if(type == typeof(int)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setInt").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setInt"));
						}else if(type == typeof(float)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setFloat").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setFloat"));
						}else if(type == typeof(string)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setString").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setString"));
						}else if(type == typeof(bool)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setBool").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setBool"));
						}else if(type == typeof(UnityEngine.Object) || type.IsSubclassOf(typeof(UnityEngine.Object))){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setObj").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setObj"));
						}else if(type == typeof(UnityEngine.Color)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setColor").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setColor"));
						}else if(type == typeof(UnityEngine.Vector3)){
							DrawParameter((NamedParameter)getPropertyAction.GetType().GetField("setVector3").GetValue(getPropertyAction),getPropertyAction.GetType().GetField("setVector3"));
						}
					}
				}
				DrawField(node,node.GetType().GetField("everyFrame"));
			}	
		}

		private StateMachineEditor stateMachineEditor;
		private void DrawParameter(NamedParameter parameter,FieldInfo info){
			if(parameter != null){
				if(EditorPrefs.GetBool("DisplayParameterDescription") && !string.IsNullOrEmpty(info.GetToolTip ())){
					GUILayout.BeginVertical("box");
					GUIStyle style= new GUIStyle("label");
					style.fixedHeight=0;
					style.wordWrap=true;
					GUILayout.Label(info.GetToolTip(),style);
					GUILayout.EndVertical();
				}
				GUILayout.BeginHorizontal();
				if(!parameter.IsConstant){
					if(stateMachineEditor == null){
						stateMachineEditor = StateMachineEditor.Instance;
					}

					StateMachine stateMachine=stateMachineEditor.stateMachine;

					List<string> names= new List<string>();
					names.Add(info.GetNullLabel());
					if(stateMachine.parameters == null){
						stateMachine.parameters=new List<NamedParameter>();
					}

			

					if(parameter is StringParameter){
						names.AddRange(stateMachine.parameters.Select(p=>p.Name).ToList());
						names.AddRange(GlobalParameterCollection.GetParamterNames());
					}else{
						names.AddRange(stateMachine.parameters.Where(n=>n.GetType()==parameter.GetType()).Select(p=>p.Name).ToList());
						names.AddRange(GlobalParameterCollection.GetParamterNames(parameter.GetType()));

						if(parameter.GetType() == typeof(FloatParameter)){
							names.AddRange(stateMachine.parameters.Where(n=>n.GetType()==typeof(IntParameter)).Select(p=>p.Name).ToList());
							names.AddRange(GlobalParameterCollection.GetParamterNames(typeof(IntParameter)));
						}
						if(parameter.GetType() == typeof(IntParameter)){
							names.AddRange(stateMachine.parameters.Where(n=>n.GetType()==typeof(FloatParameter)).Select(p=>p.Name).ToList());
							names.AddRange(GlobalParameterCollection.GetParamterNames(typeof(FloatParameter)));
						}
						if(parameter.GetType() == typeof(Vector3Parameter)){
							names.AddRange(stateMachine.parameters.Where(n=>n.GetType()==typeof(Vector2Parameter)).Select(p=>p.Name).ToList());
							names.AddRange(GlobalParameterCollection.GetParamterNames(typeof(Vector2Parameter)));
						}
						if(parameter.GetType() == typeof(Vector2Parameter)){
							names.AddRange(stateMachine.parameters.Where(n=>n.GetType()==typeof(Vector3Parameter)).Select(p=>p.Name).ToList());
							names.AddRange(GlobalParameterCollection.GetParamterNames(typeof(Vector3Parameter)));
						}
					}

					if(info.IsFieldRequired() && parameter.Reference == info.GetNullLabel()){
						GUI.color=Color.red;
					}
					parameter.Reference=UnityEditorTools.StringPopup(new GUIContent(UnityTools.UppercaseFirst (info.Name.Replace("_","")),info.GetToolTip()),parameter.Reference,names.ToArray());
					GUI.color=Color.white;
					EditorUtility.SetDirty(parameter);
				}else{
					SerializedObject parameterObject= new SerializedObject(parameter);
					parameterObject.Update();
					SerializedProperty valueProp=parameterObject.FindProperty("value");
					if(valueProp != null){
						EditorGUILayout.PropertyField (valueProp, new GUIContent (UnityTools.UppercaseFirst (info.Name.Replace("_","")),info.GetToolTip()),true);
					}
					parameterObject.ApplyModifiedProperties();
				}
				if(info.CanBeConstant() || (!EditorUtility.IsPersistent(parameter) && info.BindedCanBeConstant())){
					parameter.IsConstant=EditorGUILayout.Toggle(parameter.IsConstant,EditorStyles.radioButton,GUILayout.Width(20));
				}else{
					parameter.IsConstant=false;
					GUILayout.Space(24);
				}
				GUILayout.EndHorizontal();
			}
		}

		private void DrawField(ScriptableObject node,FieldInfo info){
			if(EditorPrefs.GetBool("DisplayParameterDescription") && !string.IsNullOrEmpty(info.GetToolTip ())){
				GUILayout.BeginVertical("box");
				GUIStyle style= new GUIStyle("label");
				style.fixedHeight=0;
				style.wordWrap=true;
				GUILayout.Label(info.GetToolTip(),style);
				GUILayout.EndVertical();
			}

			SerializedObject obj= new SerializedObject(node);
			obj.Update();
			GUILayout.BeginHorizontal();
			SerializedProperty prop=obj.FindProperty(info.Name);
			if(prop != null){
				EditorGUILayout.PropertyField(obj.FindProperty(info.Name),new GUIContent(UnityTools.UppercaseFirst(info.Name.Replace("_","")),info.GetToolTip()),true);
			}
			GUILayout.Space(24);
			GUILayout.EndHorizontal();
			obj.ApplyModifiedProperties();
		}

		private void Update(){
			Repaint ();		
		}
		
		protected override void OnHeaderGUI ()
		{
			if (state != null) {
				GUILayout.BeginVertical ("IN BigTitle");
				EditorGUIUtility.labelWidth = 50;
				GUI.changed=false;
				state.name = EditorGUILayout.TextField ("Name", state.name);
				GUILayout.Label("Description:");
				state.description = EditorGUILayout.TextArea (state.description, GUILayout.MinHeight (45));
				if(GUI.changed){
					stateMachineEditor.Repaint();
				}
				GUILayout.EndVertical ();
			}
		}
	}
}