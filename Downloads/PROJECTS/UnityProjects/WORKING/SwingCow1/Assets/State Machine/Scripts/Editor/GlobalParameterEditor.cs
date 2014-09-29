using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace StateMachine{
	public class GlobalParameterEditor : EditorWindow {
		
		[MenuItem("Window/State Machine/Global Parameters", false, 2)]
		public static void ShowWindow()
		{
			GlobalParameterEditor window = EditorWindow.GetWindow<GlobalParameterEditor>(false, "Global Parameters");
			window.minSize = new Vector2(280f, 410f);
			window.wantsMouseMove = true;
			//UnityEngine.Object.DontDestroyOnLoad(window);
			//window.paramterCollection = GetGlobalCollection ();
		}

		private string parameterName;
		private Type mType;
		private GlobalParameterCollection paramterCollection;
		private GUIContent iconToolbarMinus = GraphEditor.IconContent("Toolbar Minus", "Remove parameter");
		private Vector2 scroll;
		private bool sceneReferenceFoldOut;
		//private bool foldOut;

		private void OnGUI(){
			/*if (paramterCollection == null && !EditorApplication.isCompiling && !EditorApplication.isCompiling) {
				paramterCollection = (GlobalParameterCollection)Resources.Load ("GlobalParameterCollection");
				if (paramterCollection == null) {
					if (!System.IO.Directory.Exists(Application.dataPath + "/State Machine/Resources")) {
						AssetDatabase.CreateFolder("Assets/State Machine", "Resources");
					}	
					paramterCollection= UnityEditorTools.CreateAsset<GlobalParameterCollection>("Assets/State Machine/Resources/GlobalParameterCollection.asset");
					EditorUtility.DisplayDialog("Created GlobalParameterCollection!",
					                            "Do not delete or rename the Resource folder and the GlobalParameterCollection asset.", "Ok");
					if(paramterCollection.parameters == null){
						paramterCollection.parameters= new List<NamedParameter>();
					}
				}
			//	return;
			}*/

			if (paramterCollection == null) {
				return;			
			}

			EditorGUILayout.HelpBox ("Global parameters can be accessed from all state machines.", MessageType.Info);
			EditorGUIUtility.labelWidth = 80;

			parameterName = EditorGUILayout.TextField ("Name", parameterName);
			bool flag = !string.IsNullOrEmpty (parameterName);
			if (!flag) {
				EditorGUILayout.HelpBox("Please enter a unique name for the parameter before you continue.",MessageType.Warning);		
			}

			if (flag && paramterCollection != null) {
				if(paramterCollection.parameters == null){
					paramterCollection.parameters= new List<NamedParameter>();
				}
				foreach(NamedParameter mParamter in paramterCollection.parameters){
					if(mParamter != null && mParamter.Name == parameterName){
						flag=false;
					}
				}			
			}

			GUI.enabled = flag;
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Type",GUILayout.Width(76));
			if (mType == null) {
				mType=typeof(BoolParameter);			
			}
			string typeString = mType.ToString ().Split ('.').Last ().Replace ("Parameter", "");
			if (GUILayout.Button (typeString,EditorStyles.toolbarDropDown)) {
				GenericMenu genericMenu = new GenericMenu ();
				IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies ().SelectMany (assembly => assembly.GetTypes ()) .Where (type => type.IsSubclassOf (typeof(NamedParameter)));
				foreach (Type type in types) {
					genericMenu.AddItem (new GUIContent (type.ToString ().Split ('.').Last ().Replace ("Parameter", "")), false, this.SelectParameterType, type);
					
				}
				genericMenu.ShowAsContext ();
			}

			if (GUILayout.Button ("Add", EditorStyles.toolbarButton,GUILayout.Width(70))) {
				CreateParameter();
			}
			GUILayout.Space (5);
			GUILayout.EndHorizontal ();
			GUILayout.Space (3);
			GUILayout.Box(GUIContent.none,"PopupCurveSwatchBackground",GUILayout.Height(2),GUILayout.ExpandWidth(true));

			if (paramterCollection != null) {
				if(!flag){
					GUI.enabled=true;
				}
				DrawParameters();			
			}
		}


		private void SelectParameterType(object data){
			mType = (Type)data;
		}

		private void Update(){
			if (paramterCollection == null && !EditorApplication.isCompiling && !EditorApplication.isCompiling) {
				paramterCollection = (GlobalParameterCollection)Resources.Load ("GlobalParameterCollection");
				if (paramterCollection == null) {
					if (!System.IO.Directory.Exists(Application.dataPath + "/State Machine/Resources")) {
						AssetDatabase.CreateFolder("Assets/State Machine", "Resources");
					}	
					paramterCollection= UnityEditorTools.CreateAsset<GlobalParameterCollection>("Assets/State Machine/Resources/GlobalParameterCollection.asset");
					EditorUtility.DisplayDialog("Created GlobalParameterCollection!",
					                            "Do not delete or rename the Resource folder and the GlobalParameterCollection asset.", "Ok");
					if(paramterCollection.parameters == null){
						paramterCollection.parameters= new List<NamedParameter>();
					}
				}
				//	return;
			}
			Repaint ();
		}


		private void DrawParameters(){

		//	int paramtersCount = paramterCollection.parameters.Count;
			NamedParameter delete = null;
			scroll = GUILayout.BeginScrollView (scroll);
			List<NamedParameter> sceneReferences = paramterCollection.parameters.FindAll (x => x.GetType () == typeof(ObjectParameter) && ((ObjectParameter)x).FromSceneInstance);
			List<NamedParameter> parameters = new List<NamedParameter> (paramterCollection.parameters);
			parameters.RemoveAll (x => x.GetType () == typeof(ObjectParameter) && ((ObjectParameter)x).FromSceneInstance);
			for (int i=0; i< parameters.Count;i++) {
				NamedParameter parameter=parameters[i];//paramterCollection.parameters[i];
				/*if(parameter==null){
					deleteIndex=i;
				}*/
				if(parameter != null){
					SerializedObject paramObject= new SerializedObject(parameter);
					SerializedProperty prop=paramObject.FindProperty("value");	
					GUILayout.BeginHorizontal();
					GUILayout.Label(paramObject.FindProperty("parameterName").stringValue,GUILayout.Width(120));
					if(parameter is ObjectParameter){
						GUI.changed=false;
						ObjectParameter mParam=parameter as ObjectParameter;
						if(!mParam.FromSceneInstance){
							mParam.Value=EditorGUILayout.ObjectField(mParam.Value,typeof(UnityEngine.Object),true);
						}
						if(GUI.changed){
							if(!EditorUtility.IsPersistent(mParam.Value) && mParam.Value is GameObject){
								mParam.FromSceneInstance=true;
								mParam.Reference=mParam.Value.name+"("+EditorApplication.currentScene+")";
								AddToParameterCollection mTemp=((GameObject)mParam.Value).GetComponent<AddToParameterCollection>();
								if(mTemp == null){
									mTemp=((GameObject)mParam.Value).AddComponent<AddToParameterCollection>();
								}
								mTemp.paramterName=mParam.Name;
							}
							EditorUtility.SetDirty(mParam);
						}
					}else{
						paramObject.Update();
						if(prop != null){
							EditorGUILayout.PropertyField(prop,GUIContent.none,true);
						}
						paramObject.ApplyModifiedProperties();
					}
					GUILayout.FlexibleSpace();
					if(GUILayout.Button(iconToolbarMinus,"label",GUILayout.Width(20))){
						//deleteIndex=i;
						delete=parameter;
					}
				
					GUILayout.EndHorizontal();
				}
			}	

			if (sceneReferences.Count > 0) {
				sceneReferenceFoldOut = EditorGUILayout.Foldout (sceneReferenceFoldOut, "Scene References");
				if (sceneReferenceFoldOut) {
					for (int i=0; i< sceneReferences.Count; i++) {
						NamedParameter parameter = sceneReferences [i];
						/*if (parameter == null) {
						deleteIndex = i;
					}*/
						if (parameter != null) {
							SerializedObject paramObject = new SerializedObject (parameter);
							GUILayout.BeginHorizontal ();
							GUILayout.Label (paramObject.FindProperty ("parameterName").stringValue, GUILayout.Width (120));
							ObjectParameter mParam = parameter as ObjectParameter;
							GUI.changed = false;
							
							GUIStyle style = new GUIStyle ("label");
							style.fixedHeight = 0;
							style.wordWrap = true;
							GUILayout.Label (mParam.Reference, style);
							
							GUILayout.FlexibleSpace ();
							if (GUILayout.Button (iconToolbarMinus, "label", GUILayout.Width (20))) {
								//deleteIndex = i;
								delete = parameter;
							}
							
							GUILayout.EndHorizontal ();
						}
					}
				}
			}
			GUILayout.EndScrollView ();

			if (delete != null) {

				paramterCollection.parameters.Remove(delete);
				DestroyImmediate(delete,true);
				EditorUtility.SetDirty(paramterCollection);
			}

		}

		private void CreateParameter(){
			NamedParameter parameter = (NamedParameter)ScriptableObject.CreateInstance (mType);
			parameter.Name = parameterName;
			parameter.name = mType.ToString ();
			//paramterCollection = GetOrCreateGlobalCollection ();
			AssetDatabase.AddObjectToAsset (parameter, paramterCollection);
			AssetDatabase.SaveAssets();
			if (paramterCollection.parameters == null) {
				paramterCollection.parameters=new List<NamedParameter>();			
			}
			paramterCollection.parameters.Add (parameter);
			EditorUtility.SetDirty (paramterCollection);
		}

	/*	public static GlobalParameterCollection GetGlobalCollection(){
			var collections = UnityEditorTools.GetAssetsOfType<GlobalParameterCollection> (".asset");
			GlobalParameterCollection mCollection=null;
			if (collections != null && collections.Length > 0) {
				mCollection=collections[0];			
			}
			return mCollection;
		}


		public static GlobalParameterCollection GetOrCreateGlobalCollection(){
			var collections = UnityEditorTools.GetAssetsOfType<GlobalParameterCollection> (".asset");
			GlobalParameterCollection mCollection=null;
			if (collections != null && collections.Length > 0) {
				mCollection=collections[0];			
			}
			if (mCollection == null) {
				if (!System.IO.Directory.Exists(Application.dataPath + "/State Machine/Resources")) {
					AssetDatabase.CreateFolder("Assets/State Machine", "Resources");
				}	
				mCollection= UnityEditorTools.CreateAsset<GlobalParameterCollection>("Assets/State Machine/Resources/GlobalParameterCollection.asset");
				EditorUtility.DisplayDialog("Created GlobalParameterCollection!",
				                            "Do not delete or rename the Resource folder and the GlobalParameterCollection asset.", "Ok");
			}
			return mCollection;
		}*/
	}
}