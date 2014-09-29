using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Get the component  in children and store it.",
	       url = "http://docs.unity3d.com/ScriptReference/Component.GetComponentInChildren.html")]
	[System.Serializable]
	public class GetComponentInChildren : GameObjectAction {
		[FieldInfo(tooltip="The name of the class.")]
		public StringParameter className;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip = "Store the component.")]
		public ObjectParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty (className.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the className parameter is empty. Action disabled!");
				return;
			}
			store.Value=((GameObject)gameObject.Value).GetComponentInChildren(UnityTools.GetType(className.Value));
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value=((GameObject)gameObject.Value).GetComponentInChildren(UnityTools.GetType(className.Value));
		}
	}
}