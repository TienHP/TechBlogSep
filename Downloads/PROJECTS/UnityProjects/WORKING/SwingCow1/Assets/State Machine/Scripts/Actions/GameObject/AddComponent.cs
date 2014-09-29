using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",   
	       description = "Adds a component class named className to the game object.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/GameObject.AddComponent.html")]
	[System.Serializable]
	public class AddComponent : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.",dirtyField="gameObject")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The name of the class to add.")]
		public StringParameter className;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, requiredField=false,nullLabel="Don't Store",tooltip = "Store the component.")]
		public ObjectParameter store;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty(className.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the className parameter is empty. Action disabled!");
				return;
			}
			Component component = ((GameObject)gameObject.Value).GetComponent (className.Value);
			if (component == null) {
				store.Value = ((GameObject)gameObject.Value).AddComponent (className.Value);
			} else {
				store.Value=component;		
			}
			Finish ();
		}
	}
}