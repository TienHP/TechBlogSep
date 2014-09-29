using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UEvent{
	[Info (category = "Event",    
	       description = "OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Collider.OnCollisionEnter.html")]
	[System.Serializable]
	public class OnLevelWasLoaded : StateCondition {
		private UnityEventHandler handler;
		private bool isTrigger;

		public override void OnEnter ()
		{

			handler = stateMachine.owner.GetComponent<UnityEventHandler> ();
			if (handler == null) {
				handler = stateMachine.owner.AddComponent<UnityEventHandler>();	
			}
			handler.onLevelWasLoaded+=OnTrigger;
		}
		
		public override void OnExit ()
		{
			if (isTrigger) {
				handler.onLevelWasLoaded -= OnTrigger;
			}
			isTrigger = false;
		}
		
		private void OnTrigger(int level){
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