using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Linq;
using System;
namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetProperty : GameObjectAction {
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Int field to store.")]
		public IntParameter storeInt;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Float field to store.")]
		public FloatParameter storeFloat;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="String field to store.")]
		public StringParameter storeString;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Bool field to store.")]
		public BoolParameter storeBool;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Object field to store.")]
		public ObjectParameter storeObj;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Color field to store.")]
		public ColorParameter storeColor;
		[FieldInfo( canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Vector3 field to store.")]
		public Vector3Parameter storeVector3;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public string property;
		public string component;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty (component)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the component parameter is empty. Action disabled!");
				return;
			}

			if (string.IsNullOrEmpty (property)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the property parameter is empty. Action disabled!");
				return;
			}
			DoGetProperty ();
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			DoGetProperty ();
		}
	
		private void DoGetProperty(){
			Type componentType=UnityTools.GetType(component);
			if(componentType == null){
				componentType=UnityTools.GetType("UnityEngine."+component);
			}
			if (componentType != null && gameObject.Value != null && ((GameObject)gameObject.Value).GetComponent (componentType) != null) {
				
				Component behaviour = ((GameObject)gameObject.Value).GetComponent (componentType);
				FieldInfo info = behaviour.GetType ().GetField (property);
				if (info != null) {
					if (info.FieldType == typeof(int) || info.FieldType==typeof(KeyCode)) {
						storeInt.Value = (int)info.GetValue (behaviour);
					} else if (info.FieldType == typeof(float)) {
						storeFloat.Value = (float)info.GetValue (behaviour);
					} else if (info.FieldType == typeof(bool)) {
						storeBool.Value = (bool)info.GetValue (behaviour);
					} else if (info.FieldType == typeof(string)) {
						storeString.Value = (string)info.GetValue (behaviour);
					} else if (info.FieldType == typeof(Color)) {
						storeColor.Value = (Color)info.GetValue (behaviour);
					} else if (info.FieldType == typeof(Vector3)) {
						storeVector3.Value = (Vector3)info.GetValue (behaviour);
					}
				} else {
					PropertyInfo propertyInfo = behaviour.GetType ().GetProperty (property);
					if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType==typeof(KeyCode)) {
						storeInt.Value = (int)propertyInfo.GetValue (behaviour, null);
					} else if (propertyInfo.PropertyType == typeof(float)) {
						storeFloat.Value = (float)propertyInfo.GetValue (behaviour, null);
					} else if (propertyInfo.PropertyType == typeof(bool)) {
						storeBool.Value = (bool)propertyInfo.GetValue (behaviour, null);
					} else if (propertyInfo.PropertyType == typeof(string)) {
						storeString.Value = (string)propertyInfo.GetValue (behaviour, null);
					} else if (propertyInfo.PropertyType == typeof(Color)) {
						storeColor.Value = (Color)propertyInfo.GetValue (behaviour, null);
					} else if (propertyInfo.PropertyType == typeof(Vector3)) {
						storeVector3.Value = (Vector3)propertyInfo.GetValue (behaviour, null);
					}
				}
			} 
		}
	}
}