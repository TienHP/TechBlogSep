﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine.Action{
	[Info (category = "GameObject", 
	       description = "Calls the method named methodName on every MonoBehaviour in this game object.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/GameObject.SendMessage.html")]
	[System.Serializable]
	public class SendMessage : GameObjectAction {
		[FieldInfo(tooltip="The name of the method to call.")]
		public StringParameter methodName;
		[FieldInfo(tooltip="Use this option to send one of the following parameters inside the SendMessage. Please note that only one parameter can be send. Use None to not use a parameter at all.")]
		public MessageParameterType parameterType;
		[FieldInfo(tooltip="Send the message with a float parameter.")]
		public FloatParameter floatParameter;
		[FieldInfo(tooltip="Send the message with a int parameter.")]
		public IntParameter intParameter;
		[FieldInfo(tooltip="Send the message with a string parameter.")]
		public StringParameter stringParameter;
		[FieldInfo(tooltip="Send the message with a Object parameter.")]
		public ObjectParameter objectParameter;

		[FieldInfo(tooltip="Should an error be raised if the method doesn't exist on the target object?")]
		public SendMessageOptions options;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty (methodName.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the methodName parameter is empty. Action disabled!");
				return;
			}
			DoSendMessage ();
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			DoSendMessage ();
		}

		private void DoSendMessage(){
			switch (parameterType)
			{
			case MessageParameterType.Float:
				((GameObject)gameObject.Value).SendMessage (methodName.Value,floatParameter.Value, options);
				break;
			case MessageParameterType.Int:
				((GameObject)gameObject.Value).SendMessage (methodName.Value,intParameter.Value, options);
				break;
			case MessageParameterType.Object:
				((GameObject)gameObject.Value).SendMessage (methodName.Value,objectParameter.Value, options);
				break;
			case MessageParameterType.String:
				((GameObject)gameObject.Value).SendMessage (methodName.Value,stringParameter.Value, options);
				break;
			default:
				((GameObject)gameObject.Value).SendMessage (methodName.Value, options);
				break;
			}
		}
	}

	
	public enum MessageParameterType{
		None,
		Int,
		Float,
		String,
		Object
	}
}