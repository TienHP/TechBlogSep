using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateMachine.Action;
using System.Reflection;
using StateMachine.Condition;

namespace StateMachine{
	[System.Serializable]
	public class StateMachine : ScriptableObject {
		public bool sceneInstancePopups=true;
		public List<NamedParameter> parameters;
		public List<State> states;
		[System.NonSerialized]
		public GameObject owner;
		[System.NonSerialized]
		public StateMachineBehaviour behaviour;
		public string description;


		public void Initialize(StateMachineBehaviour behaviour){
			this.owner = behaviour.gameObject;
			this.behaviour = behaviour;
			//this.behaviour = owner.GetComponent<StateMachineBehaviour> ();
			if (parameters == null) {
				parameters=new List<NamedParameter>();			
			}
			
			/*for (int i=0; i<parameters.Count; i++) {
				parameters [i].stateMachine = this;
				if(parameters[i] is ObjectParameter){
					ObjectParameter mParam=parameters[i] as ObjectParameter;
					if(mParam.FromSceneInstance && SceneInstance.current != null){
						mParam.Value=SceneInstance.current.GetGameObject(mParam.Name);
					}
				}
			}*/
			
			if (states == null) {
				states= new List<State>();			
			}
			
			for (int i=0; i< states.Count; i++) {
				states [i].Initialize (this);
			}

		}
		
		public NamedParameter GetPrameter(string name){
			if (parameters == null) {
				parameters= new List<NamedParameter>();			
			}
			int count = parameters.Count;
			for (int i=0; i<count; i++) {
				if(parameters[i].Name.Equals(name)){
					return parameters[i];
				}			
			}
			return null;//parameters.Find(x=>x.Name==name);
		}


		public void SetParameter(string name, object value){
			NamedParameter parameter = GetPrameter (name);
			if (parameter == null) {
				//Debug.Log("Could not set parameter "+name+"! Skipping.");
				return;
			}
			if (parameter.GetType () == typeof(FloatParameter)) {
				(parameter as FloatParameter).Value=(float)value;		
			} else if (parameter.GetType () == typeof(IntParameter)) {
				(parameter as IntParameter).Value=(int)value;			
			} else if (parameter.GetType () == typeof(BoolParameter)) {
				(parameter as BoolParameter).Value=(bool)value;	
			} else if (parameter.GetType () == typeof(ColorParameter)) {
				(parameter as ColorParameter).Value=(Color)value;	
			} else if (parameter.GetType () == typeof(ObjectParameter)) {
				(parameter as ObjectParameter).Value=(Object)value;	
			} else if (parameter.GetType () == typeof(StringParameter)) {
				(parameter as StringParameter).Value=(string)value;	
			} else if (parameter.GetType () == typeof(Vector2Parameter)) {
				(parameter as Vector2Parameter).Value=(Vector2)value;	
			} else if (parameter.GetType () == typeof(Vector3Parameter)) {
				(parameter as Vector3Parameter).Value=(Vector3)value;	
			}
		}

		public static void Copy(StateMachine from,StateMachine to,bool parent){
			to.DeleteStates ();
			to.DeleteParameters ();
			
			if (from.parameters != null) {
				for (int i=0; i<from.parameters.Count; i++) {
					NamedParameter parameter = (NamedParameter)ScriptableObject.Instantiate (from.parameters [i]);
					parameter.stateMachine = to;
					if (to.parameters == null) {
						to.parameters = new List<NamedParameter> ();
					}
					to.parameters.Add (parameter);
					#if UNITY_EDITOR
					if (parent) {
						UnityEditor.AssetDatabase.AddObjectToAsset (parameter, to);
						UnityEditor.AssetDatabase.SaveAssets ();
					}
					#endif
				}
			}

			for(int i=0;i< from.states.Count;i++){
				State state =(State)ScriptableObject.Instantiate(from.states[i]);
				state.name=state.name.Replace("(Clone)","");
				state.actions.Clear();
				state.transitions.Clear();
				if(to.states== null){
					to.states=new List<State>();
				}
				to.states.Add(state);
				#if UNITY_EDITOR
				if(parent){
					UnityEditor.AssetDatabase.AddObjectToAsset(state,to);
					UnityEditor.AssetDatabase.SaveAssets();
				}
				#endif

				if(from.states[i].actions == null){
					from.states[i].actions= new List<StateAction>();
				}

				for (int a=0; a< from.states[i].actions.Count; a++) {
					StateAction action = (StateAction)ScriptableObject.Instantiate (from.states[i].actions[a]);
					if(action == null){
						Debug.Log("null"+from.states[i].name);
					}
					action.name=action.name.Replace("(Clone)","");
					if(state.actions == null){
						state.actions= new List<StateAction>();
					}
					state.actions.Add(action);
					#if UNITY_EDITOR
					if(parent){
						UnityEditor.AssetDatabase.AddObjectToAsset(action,state);
						UnityEditor.AssetDatabase.SaveAssets();
					}
					#endif
					
					FieldInfo[] fields =action.GetType().GetFields ();
					for (int af=0; af< fields.Length; af++) {
						if(fields[af].FieldType.IsSubclassOf(typeof(NamedParameter))){
							NamedParameter mParameter=(NamedParameter)fields[af].GetValue(action);
							if(mParameter != null){
								NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate(mParameter);
								paramter.stateMachine=to;
								if(paramter.Reference == "Owner"){
									(paramter as ObjectParameter).Value=to.owner;
								}
								if(paramter.Reference == "Don't Use"){
									paramter.isNone=true;
								}
								#if UNITY_EDITOR
								if(parent){
									UnityEditor.AssetDatabase.AddObjectToAsset(paramter,action);
									UnityEditor.AssetDatabase.SaveAssets();
								}
								#endif
								fields[af].SetValue(action,paramter);
							}
						}		
					}
				}

				if(from.states[i].transitions == null){
					from.states[i].transitions=new List<StateTransition>();
				}
				for (int t=0; t< from.states[i].transitions.Count; t++) {
					StateTransition transition = (StateTransition)ScriptableObject.Instantiate (from.states[i].transitions [t]);
					transition.name=transition.name.Replace("(Clone)","");
					transition.conditions.Clear();
					if(state.transitions == null){
						state.transitions= new List<StateTransition>();
					}
					state.transitions.Add(transition);
					#if UNITY_EDITOR
					if(parent){
						UnityEditor.AssetDatabase.AddObjectToAsset(transition,state);
						UnityEditor.AssetDatabase.SaveAssets();
					}
					#endif
					for(int c=0; c<from.states[i].transitions[t].conditions.Count;c++){

						StateCondition condition=(StateCondition)ScriptableObject.Instantiate(from.states[i].transitions[t].conditions[c]);
						condition.name=condition.name.Replace("(Clone)","");
						if(transition.conditions== null){
							transition.conditions= new List<StateCondition>();
						}
						transition.conditions.Add(condition);
						#if UNITY_EDITOR
						if(parent){
							UnityEditor.AssetDatabase.AddObjectToAsset(condition,transition);
							UnityEditor.AssetDatabase.SaveAssets();
						}
						#endif
						
						FieldInfo[] fields =condition.GetType().GetFields ();
						
						for (int af=0; af< fields.Length; af++) {
							if(fields[af].FieldType.IsSubclassOf(typeof(NamedParameter))){
								UnityEngine.Object mParameter=(UnityEngine.Object)fields[af].GetValue(condition);
								if(mParameter != null){
									NamedParameter paramter=(NamedParameter)ScriptableObject.Instantiate(mParameter);
									paramter.stateMachine=to;
									if(paramter.Reference == "Owner"){
										(paramter as ObjectParameter).Value=to.owner;
										//paramter.GetType().GetProperty("Value").SetValue(paramter,to.owner,null);
									}
									#if UNITY_EDITOR
									if(parent){
										UnityEditor.AssetDatabase.AddObjectToAsset(paramter,condition);
										UnityEditor.AssetDatabase.SaveAssets();
									}
									#endif
									fields[af].SetValue(condition,paramter);
								}
							}		
						}
					}
				}
			}

			if (to.states != null) {
				for (int i=0; i< to.states.Count; i++) {
					if (to.states [i].transitions.Count > 0) {
						for (int t=0; t< to.states[i].transitions.Count; t++) {
							
							to.states [i].transitions [t].fromState = to.states.Find (x => x != null&& x.id == from.states [i].transitions [t].fromState.id);
							if(to.states [i].transitions [t].toState ==  null)
							Debug.Log(to.states [i].transitions [t].name);
							to.states [i].transitions [t].toState = to.states.Find (x => x!= null && x.id == from.states [i].transitions [t].toState.id);
							
						}
					}
				}
			}
		}


		public void DeleteParameters(){
			if (parameters != null) {
				foreach (NamedParameter paramter in parameters) {
					DestroyImmediate (paramter, true);
				}
				parameters.Clear ();
			}
		}

		public void DeleteStates(){
			if (states != null) {
				foreach (State state in states) {
					state.DeleteState ();		
				}
				states.Clear ();
			}
		}
	}
}