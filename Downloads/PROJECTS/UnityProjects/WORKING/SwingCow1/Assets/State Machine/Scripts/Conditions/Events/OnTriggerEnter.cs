using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UEvent{
	[Info (category = "Event",    
	       description = "OnTriggerEnter is called when the Collider enters the trigger.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html")]
	[System.Serializable]
	public class OnTriggerEnter : StateCondition {
		[FieldInfo(canBeConstant=false,requiredField=false, nullLabel="Owner",tooltip="GameObject to use.")]
		public ObjectParameter gameObject;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use",tooltip="Store the other game object to use it in the next state.")]
		public ObjectParameter other;
		private UnityEventHandler handler;
		private bool isTrigger;

		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+gameObject.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}

			handler = ((GameObject)gameObject.Value).GetComponent<UnityEventHandler> ();
			if (handler == null) {
				handler = ((GameObject)gameObject.Value).AddComponent<UnityEventHandler>();	
			}
			handler.onTriggerEnter+=OnTrigger;
		}

		public override void OnExit ()
		{

			handler.onTriggerEnter -= OnTrigger;
			isTrigger = false;
		}

		private void OnTrigger(GameObject other){
			this.other.Value = other;
			isTrigger = true;
		}
		
		public override bool Validate ()
		{
			if (isTrigger) {
				isTrigger=false;	
				return true;
			}
			return isTrigger;
		}
	}
}