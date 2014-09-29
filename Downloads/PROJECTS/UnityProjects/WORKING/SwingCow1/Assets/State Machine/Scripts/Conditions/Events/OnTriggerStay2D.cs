using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UEvent{
	[Info (category = "Event",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class OnTriggerStay2D : StateCondition {
		[FieldInfo(canBeConstant=false,requiredField=false, nullLabel="Owner",tooltip="GameObject to use.")]
		public ObjectParameter gameObject;
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
			handler.onTriggerStay+=OnTrigger;
		}
		
		public override void OnExit ()
		{
			if (isTrigger) {
				handler.onTriggerStay -= OnTrigger;
			}
			isTrigger = false;
		}
		
		private void OnTrigger(GameObject other){
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