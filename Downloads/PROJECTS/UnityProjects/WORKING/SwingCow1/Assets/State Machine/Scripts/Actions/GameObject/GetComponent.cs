using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Get the component and store it.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/GameObject.GetComponent.html")]
	[System.Serializable]
	public class GetComponent : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.",dirtyField="gameObject")]
		public ObjectParameter target;
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
			store.Value=((GameObject)gameObject.Value).GetComponent(className.Value);
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value=((GameObject)gameObject.Value).GetComponent(className.Value);
		}
	}
}