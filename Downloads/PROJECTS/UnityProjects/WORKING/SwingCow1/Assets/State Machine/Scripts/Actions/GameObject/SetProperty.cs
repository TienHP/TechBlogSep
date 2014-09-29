using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class SetProperty : GameObjectAction {
		[FieldInfo(tooltip="Int to set.")]
		public IntParameter setInt;
		[FieldInfo(tooltip="Float to set.")]
		public FloatParameter setFloat;
		[FieldInfo(tooltip="String to set")]
		public StringParameter setString;
		[FieldInfo( tooltip="Bool to set.")]
		public BoolParameter setBool;
		[FieldInfo(tooltip="Object to set.")]
		public ObjectParameter setObj;
		[FieldInfo(tooltip="Color to set.")]
		public ColorParameter setColor;
		[FieldInfo(tooltip="Vector3 to set.")]
		public Vector3Parameter setVector3;

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

			Type componentType=UnityTools.GetType(component);
			if(componentType == null){
				componentType=UnityTools.GetType("UnityEngine."+component);
			}
			
			if (componentType != null && ((GameObject)gameObject.Value).GetComponent (componentType) != null) {
				behaviour = ((GameObject)gameObject.Value).GetComponent (componentType);
				info = behaviour.GetType ().GetField (property);
				propertyInfo = behaviour.GetType ().GetProperty(property);
			}
			DoSetProperty ();
			if (!everyFrame) {
				Finish ();
			}
		}

		private Component behaviour;
		private FieldInfo info;
		private PropertyInfo propertyInfo;

		public override void OnUpdate ()
		{
			DoSetProperty ();
		}

		private void DoSetProperty(){
//			Debug.Log (setString.Value);
			if(info != null){
				if (info.FieldType == typeof(int)) {
					info.SetValue(behaviour,setInt.Value);	
				} else if (info.FieldType == typeof(float)) {
					info.SetValue(behaviour,setFloat.Value);	
				} else if (info.FieldType == typeof(bool)) {
					info.SetValue(behaviour,setBool.Value);	
				} else if (info.FieldType == typeof(string)) {
					info.SetValue(behaviour,setString.Value);	
				}else if (info.FieldType == typeof(UnityEngine.Object) || info.FieldType.IsSubclassOf(typeof(UnityEngine.Object))) {
					info.SetValue(behaviour,setObj.Value);	
				}else if(info.FieldType == typeof(UnityEngine.Color)){
					info.SetValue(behaviour,setColor.Value);	
				}else if(info.FieldType == typeof(UnityEngine.Vector3)){
					info.SetValue(behaviour,setVector3.Value);	
				}
			}else if(propertyInfo != null){
				if (propertyInfo.PropertyType == typeof(int)) {
					propertyInfo.SetValue(behaviour,setInt.Value,null);	
				} else if (propertyInfo.PropertyType == typeof(float)) {
					propertyInfo.SetValue(behaviour,setFloat.Value,null);	
				} else if (propertyInfo.PropertyType == typeof(bool)) {
					propertyInfo.SetValue(behaviour,setBool.Value,null);	
				} else if (propertyInfo.PropertyType == typeof(string)) {
					propertyInfo.SetValue(behaviour,setString.Value,null);	
				}else if (propertyInfo.PropertyType == typeof(UnityEngine.Object) || propertyInfo.PropertyType.IsSubclassOf(typeof(UnityEngine.Object))) {
					propertyInfo.SetValue(behaviour,setObj.Value,null);	
				}else if(propertyInfo.PropertyType == typeof(UnityEngine.Color)){
					propertyInfo.SetValue(behaviour,setColor.Value,null);	
				}else if(propertyInfo.PropertyType == typeof(UnityEngine.Vector3)){
					propertyInfo.SetValue(behaviour,setVector3.Value,null);	
				}
			}
		}
	}
}