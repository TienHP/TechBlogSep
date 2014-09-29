using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using StateMachine.Action;
using StateMachine.Condition;

namespace StateMachine{
	public class StateMachineEditor : GraphEditor {
		public static StateMachineEditor Instance{
			get{
				StateMachineEditor instance;
				StateMachineEditor[] instances=Resources.FindObjectsOfTypeAll<StateMachineEditor>();
				if (instances.Length > 0) {
					instance=instances[0];
				} else {
					instance=(StateMachineEditor)EditorWindow.GetWindow (typeof(StateMachineEditor));
					instance.title="State Machine";
					if(EditorPrefs.GetBool("ShowWelcomeWindow",true)){
						WelcomeWindow.ShowWindow();
					}
				}
				return instance;
			}
		}
		
		public StateMachine stateMachine;
		public State selectedState;
		private bool isDraggingState;
		private int connectionIndex = -1;
		public int transitionIndex = 0;
		private GUIContent iconToolbarMinus = IconContent("Toolbar Minus", "Remove parameter");
		private GUIContent iconToolbarPlus = IconContent("Toolbar Plus", "Add new parameter");
		private float debugProgress;
		
		public static void Show (StateMachine stateMachine)
		{	
			if (stateMachine == null) {
				return;			
			}
			Instance.stateMachine = stateMachine;
			if (stateMachine.states == null) {
				stateMachine.states= new List<State>();
			}
			State anyState=stateMachine.states.Find (x => x.GetType () == typeof(AnyState));

			if (anyState == null) {
				anyState = (State)ScriptableObject.CreateInstance<AnyState> ();
				anyState.id = Guid.NewGuid ().ToString();
				anyState.position = new Rect (Instance.position.center.x-State.kNodeWidth*0.5f,Instance.position.center.y, State.kNodeWidth, State.kNodeHeight);
				anyState.name="Any State";
				if(EditorUtility.IsPersistent(stateMachine)){
					AssetDatabase.AddObjectToAsset (anyState, stateMachine);
				}
				AssetDatabase.SaveAssets ();
				
				stateMachine.states.Add (anyState);
			}
			//Instance.selectedState = anyState;
			//Instance.selectedState = null;
			//Selection.activeObject = null;

			Instance.SelectState (anyState);
			Instance.transitionIndex = 0;
		}
		

		protected override void OnGraphGUI ()
		{
			if (stateMachine== null) {
				return;		
			}
			
			DoDragStateMachine ();	
			if (stateMachine.states != null && stateMachine.states.Count > 0) {
				DrawStates ();
			}

			DrawParameters ();
			DrawAssetInformation ();
			HandleCreateEvents ();
			ShowPreferences ();
			EditorUtility.SetDirty(stateMachine);
		}

		private GUIStyle WrappedLabel{
			get{
				GUIStyle style=new GUIStyle("label");
				style.fixedHeight=0;
				style.wordWrap=true;
				return style;
			}
		}


		private void ShowPreferences(){
			if (!showPreferences) {
				return;			
			}
			GUILayout.BeginArea (new Rect (position.width - 217+scroll.x, 18+scroll.y, 200, 200), "", "OL box");
			//EditorGUIUtility.labelWidth = 230;
			GUILayout.BeginHorizontal ();
			bool displayActionDescription = EditorPrefs.GetBool ("DisplayActionDescription", true);
			displayActionDescription = EditorGUILayout.Toggle (GUIContent.none, displayActionDescription,GUILayout.Width(18));
			EditorPrefs.SetBool ("DisplayActionDescription",displayActionDescription);
			GUILayout.Label ("Display action description?");
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			bool displayParameterDescription = EditorPrefs.GetBool ("DisplayParameterDescription", false);
			displayParameterDescription = EditorGUILayout.Toggle (GUIContent.none, displayParameterDescription,GUILayout.Width(18));
			EditorPrefs.SetBool ("DisplayParameterDescription",displayParameterDescription);
			GUILayout.Label ("Display parameter tooltips?",WrappedLabel);
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			bool displayStateDescription = EditorPrefs.GetBool ("DisplayStateDescription", true);
			displayStateDescription = EditorGUILayout.Toggle (GUIContent.none, displayStateDescription,GUILayout.Width(18));
			EditorPrefs.SetBool ("DisplayStateDescription",displayStateDescription);
			GUILayout.Label ("Display state description?");
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal ();
			bool showWelcomeWindow = EditorPrefs.GetBool ("ShowWelcomeWindow", true);
			showWelcomeWindow = EditorGUILayout.Toggle (GUIContent.none, showWelcomeWindow,GUILayout.Width(18));
			EditorPrefs.SetBool ("ShowWelcomeWindow",showWelcomeWindow);
			GUILayout.Label ("Show welcome window on start?",WrappedLabel);
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
		
		protected override void ToolBarGUI(){
			GUILayout.BeginHorizontal (EditorStyles.toolbar);
			if (GUILayout.Button("Create New", EditorStyles.toolbarButton,GUILayout.Width(70))) {
				StateMachine mStateMachine=UnityEditorTools.CreateAsset<StateMachine>(true);
				Show(mStateMachine);
			}

			if (GUILayout.Button(lastSelected != null?lastSelected.name:"[None Selected]", EditorStyles.toolbarDropDown,GUILayout.Width(100))) {
				GenericMenu toolsMenu = new GenericMenu();
				List<StateMachineBehaviour> behaviours=FindAll<StateMachineBehaviour>();
				foreach(StateMachineBehaviour behaviour in behaviours){
					toolsMenu.AddItem( new GUIContent(behaviour.name),false, OnGameObjectSelectionChanged,behaviour);
				}
				toolsMenu.DropDown(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y,0,Event.current.mousePosition.y));//new Rect(0, 0, 0, 16));
				EditorGUIUtility.ExitGUI();
			}
			
			if (GUILayout.Button(stateMachine != null?stateMachine.name:"[None Selected]", EditorStyles.toolbarDropDown,GUILayout.Width(130)) && lastSelected != null) {
				GenericMenu toolsMenu = new GenericMenu();
				List<StateMachineBehaviour> behaviours= lastSelected.GetComponents<StateMachineBehaviour>().ToList();//FindAll<StateMachineBehaviour>();
				
				foreach(StateMachineBehaviour behaviour in behaviours){
					if(behaviour.stateMachine != null)
					toolsMenu.AddItem( new GUIContent( behaviour.stateMachine.name),false, OnStateMachineSelectionChanged,behaviour.stateMachine);
				}
				toolsMenu.DropDown(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y,0,Event.current.mousePosition.y));//new Rect(102, 0, 0, 16));
				EditorGUIUtility.ExitGUI();
			}

			if (GUILayout.Button("-",EditorStyles.toolbarButton,GUILayout.Width(20))) {
				if(zoomFactor>0.5f){
					zoomFactor-=0.1f;
				}
				Debug.Log("Zoom Factor :"+zoomFactor);
			}
			if (GUILayout.Button("+",EditorStyles.toolbarButton,GUILayout.Width(20))) {
				zoomFactor+=0.1f;
				Debug.Log("Zoom Factor :"+zoomFactor);
			}

			GUILayout.FlexibleSpace ();

			if (GUILayout.Button("Preferences", EditorStyles.toolbarPopup,GUILayout.Width(130))) {
				showPreferences=!showPreferences;
			}
			GUILayout.EndVertical ();
		}

		private bool showPreferences = false;

		private void OnGameObjectSelectionChanged(object data){
			StateMachineBehaviour behaviour = data as StateMachineBehaviour;
			Selection.activeGameObject = behaviour.gameObject;
			lastSelected = behaviour.gameObject;
			Show (behaviour.stateMachine);
		}
		
		private void OnStateMachineSelectionChanged(object data){
			StateMachine mStateMachine = data as StateMachine;
			Show(mStateMachine);
		}
		
		public static List<T> FindAll<T> () where T : Component
		{
			T[] comps = Resources.FindObjectsOfTypeAll(typeof(T)) as T[];
			
			List<T> list = new List<T>();
			
			foreach (T comp in comps)
			{
				if (comp.gameObject.hideFlags == 0)
				{
					string path = AssetDatabase.GetAssetPath(comp.gameObject);
					if (string.IsNullOrEmpty(path)) list.Add(comp);
				}
			}
			return list;
		}
		
//		private SceneInstance sceneInstance;
		private void DrawParameters(){
			GUILayout.BeginArea(new Rect (scroll.x, scroll.y+position.height-497, 250, 500));
			GUILayout.FlexibleSpace ();
			bool state = EditorPrefs.GetBool ("Parameters", false);
			
			Rect rect = GUILayoutUtility.GetRect (new GUIContent("Parameters"), "flow overlay header lower left", GUILayout.ExpandWidth (true));
			rect.x -= 1;
			rect.width += 3;
			Rect rect2 = new Rect (rect.x+225,rect.y+2,25,25);
			
			if (GUI.Button (rect2,"","label")) {
				GenericMenu genericMenu = new GenericMenu ();
				IEnumerable<Type> types= AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()) .Where(type => type.IsSubclassOf(typeof(NamedParameter)));
				foreach(Type type in types){
					genericMenu.AddItem (new GUIContent (type.ToString().Split('.').Last().Replace("Parameter","")), false, this.CreateParameter,type);
					
				}
				genericMenu.ShowAsContext ();
			}
			
			if (GUI.Button (rect,"Parameters", "flow overlay header lower left")) {
				EditorPrefs.SetBool("Parameters",!state);	
			}
			
			GUI.Label (rect2, iconToolbarPlus);
			
			if (state) {
				GUILayout.BeginVertical ((GUIStyle)"PopupCurveSwatchBackground");
				int deleteIndex=-1;
				if(stateMachine.parameters != null && stateMachine.parameters.Count>0){
					for(int i=0;i< stateMachine.parameters.Count;i++){
						NamedParameter parameter= stateMachine.parameters[i];
						SerializedObject paramObject= new SerializedObject(parameter);
						SerializedProperty prop=paramObject.FindProperty("value");
						
						GUILayout.BeginHorizontal();
						bool fromSceneInstance=false;
						if(parameter is ObjectParameter){
							ObjectParameter mParam=parameter as ObjectParameter;

							mParam.FromSceneInstance=EditorGUILayout.Toggle(mParam.FromSceneInstance,GUILayout.Width(20));
							fromSceneInstance=mParam.FromSceneInstance;
						}
						
						paramObject.Update();
						
					/*	if(sceneInstance == null && fromSceneInstance){
							sceneInstance= GameObject.FindObjectOfType<SceneInstance>();
						}*/
					/*	if(fromSceneInstance && stateMachine.sceneInstancePopups && sceneInstance != null ){
							string[] sceneObjects=sceneInstance.sceneObjects.Select(x=>x.name).ToArray();
							parameter.Name=UnityEditorTools.StringPopup(parameter.Name,sceneObjects);
							
						}else{*/
							EditorGUILayout.PropertyField(paramObject.FindProperty("parameterName"),GUIContent.none,true);
					//	}
						
						
						if(!fromSceneInstance && prop != null){
							if(parameter is BoolParameter){
								EditorGUILayout.PropertyField(prop,GUIContent.none,true,GUILayout.Width(20));
							}else{
								EditorGUILayout.PropertyField(prop,GUIContent.none,true);//,GUILayout.Width(138));
							}
						}
						paramObject.ApplyModifiedProperties();
						
						
						GUILayout.FlexibleSpace();
						if(GUILayout.Button(iconToolbarMinus,"label")){
							deleteIndex=i;
						}
						
						GUILayout.EndHorizontal();
						
					}
				}else{
					GUILayout.Label("List is Empty");
				}
				if(deleteIndex != -1){
					DestroyImmediate(stateMachine.parameters[deleteIndex],true);
					stateMachine.parameters.RemoveAt(deleteIndex);
					AssetDatabase.SaveAssets();
				}
				GUILayout.EndVertical ();
			}
			GUILayout.EndArea();
		}
		
		private void CreateParameter(object data){
			Type type = (Type)data;
			NamedParameter parameter = (NamedParameter)ScriptableObject.CreateInstance (type);
			parameter.Name ="New "+ type.ToString ().Split('.').Last().Replace("Parameter","");
			parameter.name = type.ToString ();
			if (EditorUtility.IsPersistent (stateMachine)) {
				AssetDatabase.AddObjectToAsset (parameter, stateMachine);
			}
			AssetDatabase.SaveAssets();
			if (stateMachine.parameters == null) {
				stateMachine.parameters= new List<NamedParameter>();			
			}
			stateMachine.parameters.Add (parameter);
			EditorPrefs.SetBool("Parameters",true);	
		}
		
		private void DrawAssetInformation(){
			GUILayout.BeginArea(new Rect (scroll.x+250, scroll.y+position.height-18, position.width, 20),"","RL Background");
			string info = string.Empty;
			if (EditorUtility.IsPersistent (stateMachine)) {
				info = AssetDatabase.GetAssetPath (stateMachine);			
			} else {
				if (lastSelected != null) {
					info = lastSelected.name + "/" + stateMachine.name;
				}
			}
			GUILayout.Label (info);
			GUILayout.EndArea ();
		}
		
		private void DrawStateTransitions(List<State> states){
			if (connectionIndex != -1) {
				DrawConnection (states [connectionIndex].position.center, Event.current.mousePosition,position, Color.green,false);
			}
			if (Event.current.type == EventType.Repaint) {
				foreach (State node in states) {
					if (node.transitions != null) {
						State to=null;
						foreach (StateTransition target in node.transitions) {
							if(target != null&& target.toState != to && transitionIndex >=0 && node.transitions.Count>0){
								to=target.toState;
								Color color=states.Find (x => x == node) == selectedState && node.transitions[transitionIndex].toState == target.toState ? Color.cyan : Color.white;
								if(target.toState != null && target.fromState != null){
									bool mult=target.toState.transitions != null && target.toState.transitions.Find(x=>x.toState ==target.fromState)!= null;
									DrawConnection (target.fromState.position.center,target.toState.position.center,new Rect(scroll.x,scroll.y+7f,position.width,position.height), color,mult);
								}
							}
						}
					}
				}
			}
		}
		
		private void DrawStates ()
		{
			DrawStateTransitions (stateMachine.states);
			foreach (State state in stateMachine.states) {
				if (!state.Equals (selectedState)) {
					DrawState (state);
				}
			}
			
			DrawState (selectedState);
			if (isDraggingState) {
				AutoPan (selectedState, 1.0f);
			}
		}

		private void DrawState (State state)
		{

			if (state != null) {
				state.position=new Rect(state.position.x,state.position.y,State.kNodeWidth*zoomFactor,State.kNodeHeight*zoomFactor);
				UnityEditor.Graphs.Styles.Color color = state.isDefaultState ? UnityEditor.Graphs.Styles.Color.Orange : state.GetType () == typeof(AnyState) ? UnityEditor.Graphs.Styles.Color.Aqua : UnityEditor.Graphs.Styles.Color.Gray;
				GUIStyle style=new GUIStyle(UnityEditor.Graphs.Styles.GetNodeStyle("node", color, state == selectedState));
				style.contentOffset=new Vector2();
				style.padding= new RectOffset();
				style.alignment=TextAnchor.MiddleCenter;

				GUI.Box (state.position, state.name, style);


				if(!ValidateState(state) && Event.current.type != EventType.Layout){
					GUI.Box(state.position,IconContent ("console.warnicon"),"label");
				}
				DebugState(state);
				if(EditorPrefs.GetBool("DisplayStateDescription",true)){
					GUILayout.BeginArea(new Rect(state.position.x,state.position.y+state.position.height,state.position.width,500));
					GUIStyle style2=new GUIStyle("box");
					style2.normal.textColor=Color.white;
					style2.normal.background=null;
					GUILayout.Label(state.description,style2);
					GUILayout.EndArea();
				}

				HandleStateEvents (state);
			}

		}
		
		private State debugState;
		private void DebugState(State state){
			
			State currentState = (stateMachine.behaviour != null) ? stateMachine.behaviour.currentState : null;
			if (currentState != null && EditorApplication.isPlaying && state.id==currentState.id) {
				if(debugState == null || debugState.id != state.id){
					debugProgress=0;
					debugState=state;
				}
				GUI.Box(new Rect(state.position.x+5,state.position.y+20,debugProgress,5),"", "MeLivePlayBar");
			}		
		}
		
		private void HandleStateEvents (State state)
		{
			Event ev = Event.current;
			switch (ev.type) {
			case EventType.mouseDown:
				if (state.position.Contains (ev.mousePosition) && Event.current.button == 0) {
					isDraggingState = true;
				}
				
				if (state.position.Contains (ev.mousePosition) && Event.current.button == 1) {
					GenericMenu genericMenu = new GenericMenu ();
					genericMenu.AddItem (new GUIContent ("Make Transition"), false, MakeTransition, state);
					bool anyState=state.GetType()== typeof(AnyState);
					
					if (!state.isDefaultState && !anyState) {
						genericMenu.AddItem (new GUIContent ("Set As Default"), false, SetAsDefaultState, state);
					} else {
						genericMenu.AddDisabledItem (new GUIContent ("Set As Default"));
					}
					
					if(!anyState){
						genericMenu.AddItem (new GUIContent ("Delete"), false, DeleteState, state);
					}else{
						genericMenu.AddDisabledItem (new GUIContent ("Delete"));
					}
					genericMenu.ShowAsContext ();
					ev.Use ();
				}
				break;
			case EventType.mouseUp:
				isDraggingState = false;
				break;
			case EventType.mouseDrag:
				if (isDraggingState) {
					selectedState.position.x += Event.current.delta.x;
					selectedState.position.y += Event.current.delta.y;
					
					if (selectedState.position.y < 10) {
						selectedState.position.y = 10;
					}
					if (selectedState.position.x <  10) {
						selectedState.position.x = 10;
					}
					ev.Use ();
				}
				break;
			}
			
			if (state.position.Contains (ev.mousePosition) && (ev.type != EventType.MouseDown || ev.button != 0 ? false : ev.clickCount == 1)) {
				SelectState (state);
			}
		}
		
		private bool ValidateState(State state){
			if (state == null) {
				return false;			
			}
			if (state.actions != null) {
				foreach (StateAction action in state.actions) {
					if(!(action is GetProperty || action is SetProperty)){
						
						FieldInfo[] fields = action.GetType ().GetFields (BindingFlags.Instance | BindingFlags.Public);
						for (int i=0; i< fields.Length; i++) {
							if (fields [i].FieldType.IsSubclassOf (typeof(NamedParameter))) {
								NamedParameter parameter = (NamedParameter)fields [i].GetValue (action);
								if (parameter != null && !parameter.IsConstant && fields [i].IsFieldRequired () && parameter.Reference == fields [i].GetNullLabel ()) {
									return false;
								}
							}
						}
					}
				}
			}
			if ( state.transitions != null && state.transitions.Count > transitionIndex && state.transitions[transitionIndex].conditions != null) {
				foreach (StateCondition condition in state.transitions[transitionIndex].conditions) {
					FieldInfo[] fields = condition.GetType ().GetFields (BindingFlags.Instance | BindingFlags.Public);
					for (int i=0; i< fields.Length; i++) {
						if (fields [i].FieldType.IsSubclassOf (typeof(NamedParameter))) {
							NamedParameter parameter = (NamedParameter)fields [i].GetValue (condition);
							if (parameter!= null && !parameter.IsConstant && fields [i].IsFieldRequired () && parameter.Reference == fields [i].GetNullLabel ()) {
								return false;
							}
						}
					}		
				}
			}
			return true;
		}
		
		private void SelectState(State state){
			if (selectedState != state) {
				transitionIndex = 0;
				selectedState = state;
			}
			
			Selection.activeObject = state;
			if(Event.current != null)
			Event.current.Use ();

		}
		
		private void DeleteState(object data){
			State state = data as State;
			if (state == selectedState) {
				SelectState(null);			
			}
			if (state.isDefaultState && stateMachine.states.Count > 2) {
				stateMachine.states.Find(x=> x.isDefaultState == false && x.GetType()!=typeof(AnyState)).isDefaultState=true;
			}
			
			stateMachine.states.Remove (state);
			state.DeleteState ();
			
			foreach (State mState in stateMachine.states) {
				
				StateTransition tr = null;
				if(mState.transitions != null){
					foreach(StateTransition mTransitin in mState.transitions){
						if(mTransitin.toState== null){
							tr=mTransitin;
							
						}
					}
				}
				
				if(tr != null){
					tr.fromState.transitions.Remove(tr);
					tr.DeleteTransition();
				}
				
			}
			AssetDatabase.SaveAssets();
		}
		
		private void MakeTransition(object data){
			State state = (State)data;
			connectionIndex = stateMachine.states.FindIndex (x => x == state);
		}
		
		private void HandleCreateEvents ()
		{
			Event e = Event.current;
			switch (e.type) {
			case EventType.mouseDown:
				if (e.button == 1) {
					GenericMenu genericMenu = new GenericMenu ();
					genericMenu.AddItem (new GUIContent ( "Create State"), false, CreateState,Event.current.mousePosition);
					genericMenu.AddSeparator("");
					if(Selection.activeGameObject != null){
						genericMenu.AddItem (new GUIContent ( "Add StateMachine"), false, delegate() {
							StateMachineBehaviour behaviour=Selection.activeGameObject.AddComponent<StateMachineBehaviour>();
							behaviour.stateMachine=stateMachine;
						});
					}else{
						genericMenu.AddDisabledItem(new GUIContent("Add StateMachine"));
					}
					genericMenu.ShowAsContext ();
					e.Use ();
				}
				break;
			case EventType.mouseUp:
				if (connectionIndex != -1) {
					foreach (State node in stateMachine.states) {
						if (node.position.Contains (e.mousePosition)) {
							if(node.GetType() == typeof(AnyState)){
								Debug.Log("AnyState is running parallel. You can not make a transition to it.");
								break;
							}
							StateTransition transition = ScriptableObject.CreateInstance<StateTransition>();
							transition.fromState=stateMachine.states [connectionIndex];
							transition.toState=node;
							transition.name=transition.fromState.name +" -> "+transition.fromState.name;
							if (EditorUtility.IsPersistent (stateMachine)) {
								AssetDatabase.AddObjectToAsset (transition, transition.fromState);
							}
							AssetDatabase.SaveAssets();
							if(transition.fromState.transitions == null){
								transition.fromState.transitions= new List<StateTransition>();
							}
							transition.fromState.transitions.Add(transition);
						}
					}
					connectionIndex = -1;
				}
				break;
			}
		}
		
		private void CreateState(object data){
			Vector2 mousePosition = (Vector2)data;
			State state = (State)ScriptableObject.CreateInstance<State> ();
			state.id = Guid.NewGuid ().ToString();
			state.position = new Rect (mousePosition.x, mousePosition.y, State.kNodeWidth, State.kNodeHeight);
			state.name="New State";
			if (EditorUtility.IsPersistent (stateMachine)) {
				AssetDatabase.AddObjectToAsset (state, stateMachine);
			}
			AssetDatabase.SaveAssets ();
			
			if (stateMachine.states.Count == 1) {
				SetAsDefaultState (state);			
			}
			stateMachine.states.Add (state);
			SelectState (state);
		}
		
		
		
		private void SetAsDefaultState(object data){
			stateMachine.states.ForEach(state=>state.isDefaultState=false);
			(data as State).isDefaultState = true;
		}
		
		public void AutoPan (State state, float speed)
		{
			if (Event.current.type != EventType.repaint) {
				return;
			}
			if (Event.current.mousePosition.x > position.width + scroll.x - 50) {
				scroll.x += speed;
				state.position.x += speed;
			}
			
			if ((Event.current.mousePosition.x < scroll.x + 50) && scroll.x > 0) {
				scroll.x -= speed;
				state.position.x -= speed;
			}
			
			if (Event.current.mousePosition.y > position.height + scroll.y - 50) {
				scroll.y += speed;
				state.position.y += speed;
			}
			
			if ((Event.current.mousePosition.y < scroll.y + 50) && scroll.y > 0) {
				scroll.y -= speed;
				state.position.y -= speed;
			}
			Repaint ();
		}
		
		private GameObject lastSelected;
		private void Update () {
			if (EditorApplication.isPlaying) {
				debugProgress += Time.deltaTime * 30;
				if (debugProgress > 142) {
					debugProgress = 0;
				}
			}
			
			if (Selection.activeGameObject != null && lastSelected != Selection.activeGameObject) {
				StateMachineBehaviour runtime = Selection.activeGameObject.GetComponent<StateMachineBehaviour> ();
				if (runtime != null) {
					StateMachine mStateMachine = ((EditorApplication.isPlaying && !EditorApplication.isPaused) ? runtime.executingStateMachine : runtime.stateMachine);
					if (mStateMachine != null) {
						Show(mStateMachine);
						lastSelected=runtime.gameObject;
						selectedStateMachine=stateMachine.name;
					}
				}
			}

			if (stateMachine != null && stateMachine.behaviour != null) {
				if (EditorApplication.isPlaying && stateMachine != stateMachine.behaviour.executingStateMachine) {
					selectedStateMachine=stateMachine.behaviour.stateMachine.name;
					Show (stateMachine.behaviour.executingStateMachine);
				}
			}

			if (!EditorApplication.isPlaying && lastSelected != null && stateMachine == null) {
				StateMachineBehaviour[] behaviours=lastSelected.GetComponents<StateMachineBehaviour>();
				foreach(StateMachineBehaviour behaviour in behaviours){
					if(behaviour.stateMachine != null && behaviour.stateMachine.name== selectedStateMachine){
						Show(behaviour.stateMachine);
					}
				}
			}
			Repaint();
		}

		private string selectedStateMachine;
		
		[DrawGizmo(GizmoType.SelectedOrChild | GizmoType.NotSelected)]
		static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
		{   
			StateMachineBehaviour behaviour = transform.GetComponent<StateMachineBehaviour> ();
			
			if (behaviour != null && behaviour.currentState != null) { 
				var centeredStyle = new GUIStyle( GUI.skin.GetStyle("HelpBox"));
				centeredStyle.alignment = TextAnchor.UpperCenter;
				centeredStyle.fontSize=21;
				Handles.Label (transform.position, behaviour.currentState.name,centeredStyle);
			}
		}
		
		private void RemoveClone(ScriptableObject obj){
			obj.name = obj.name.Replace ("(Clone)", "");
		}
		
		private void DoDragStateMachine(){
			if (Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragPerform) {
				DragAndDrop.visualMode = DragAndDropVisualMode.Link;
				if (Event.current.type == EventType.DragPerform) {
					DragAndDrop.AcceptDrag ();
				}
				this.Focus ();
			}
			
			
			if (Event.current.type == EventType.DragExited && DragAndDrop.objectReferences[0].GetType()== typeof(StateMachine)) {
				
				StateMachine draggedStateMachine=DragAndDrop.objectReferences[0] as StateMachine;
				
				
				for(int i=0;i< draggedStateMachine.states.Count;i++){
					if(stateMachine.states.Find(x=>x.id==draggedStateMachine.states[i].id) != null){
						Debug.LogWarning("Can't add state machine, state with same id already exists.");
						return;
					}
				}
				
				for(int i=0;i<draggedStateMachine.parameters.Count;i++){
					if(stateMachine.GetPrameter(draggedStateMachine.parameters[i].Name) == null){
						NamedParameter parameter=(NamedParameter)ScriptableObject.Instantiate(draggedStateMachine.parameters[i]);
						AssetDatabase.AddObjectToAsset(parameter,stateMachine);
						AssetDatabase.SaveAssets();
						stateMachine.parameters.Add(parameter);
					}
				}
				
				for(int i=0;i< draggedStateMachine.states.Count;i++){
					if(draggedStateMachine.states[i].GetType() != typeof(AnyState)){
						State state =(State)ScriptableObject.Instantiate(draggedStateMachine.states[i]);
						RemoveClone(state);
						state.isDefaultState=false;
						AssetDatabase.AddObjectToAsset(state,stateMachine);
						AssetDatabase.SaveAssets();
						stateMachine.states.Add(state);
						for (int a=0; a< state.actions.Count; a++) {
							state.actions [a] = (StateAction)ScriptableObject.Instantiate (state.actions [a]);
							AssetDatabase.AddObjectToAsset(state.actions[a],state);
							AssetDatabase.SaveAssets();
							RemoveClone(state.actions[a]);
							FieldInfo[] fields =state.actions[a].GetType().GetFields ();
							
							for (int af=0; af< fields.Length; af++) {
								if(fields[af].FieldType.IsSubclassOf(typeof(NamedParameter))){
									NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate((UnityEngine.Object)fields[af].GetValue(state.actions[a]));
									AssetDatabase.AddObjectToAsset(paramter,state.actions[a]);
									AssetDatabase.SaveAssets();
									fields[af].SetValue(state.actions[a],paramter);
								}		
							}
						}
						
						for (int t=0; t< state.transitions.Count; t++) {
							state.transitions [t] = (StateTransition)ScriptableObject.Instantiate (state.transitions [t]);
							AssetDatabase.AddObjectToAsset(state.transitions [t],state);
							AssetDatabase.SaveAssets();
							
							for(int c=0; c<state.transitions[t].conditions.Count;c++){
								state.transitions[t].conditions[c]=(StateCondition)ScriptableObject.Instantiate(state.transitions[t].conditions[c]);
								AssetDatabase.AddObjectToAsset(state.transitions [t].conditions[c],state.transitions[t]);
								AssetDatabase.SaveAssets();
								
								RemoveClone(state.transitions[t].conditions[c]);
								FieldInfo[] fields =state.transitions[t].conditions[c].GetType().GetFields ();
								
								for (int af=0; af< fields.Length; af++) {
									if(fields[af].FieldType.IsSubclassOf(typeof(NamedParameter))){
										NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate((UnityEngine.Object)fields[af].GetValue(state.transitions[t].conditions[c]));
										AssetDatabase.AddObjectToAsset(paramter,state.transitions[t].conditions[c]);
										AssetDatabase.SaveAssets();
										fields[af].SetValue(state.transitions[t].conditions[c],paramter);
									}		
								}
							}
						}
					}
				}
				
				for(int i=0;i< stateMachine.states.Count;i++){
					if(stateMachine.states[i].transitions == null){
						stateMachine.states[i].transitions= new List<StateTransition>();
					}
					for(int t=0;t< stateMachine.states[i].transitions.Count;t++){
						stateMachine.states[i].transitions[t].fromState=stateMachine.states.Find(x=>x.id==stateMachine.states[i].transitions[t].fromState.id);
						stateMachine.states[i].transitions[t].toState=stateMachine.states.Find(x=>x.id==stateMachine.states[i].transitions[t].toState.id);
					}
				}
				Event.current.Use();
			}		
		}
		
		private void OnDestroy(){

			if (Selection.activeObject != null && (Selection.activeObject.GetType () == typeof(State) || Selection.activeObject.GetType () == typeof(AnyState))) {
				Selection.activeObject=null;			
			}
		}
	}
}