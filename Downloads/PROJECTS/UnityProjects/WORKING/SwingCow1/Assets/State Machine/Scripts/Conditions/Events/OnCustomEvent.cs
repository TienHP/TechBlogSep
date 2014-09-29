using UnityEngine;

namespace StateMachine.Condition.UEvent{
	[Info (category = "Event",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class OnCustomEvent : StateCondition {
		[FieldInfo(tooltip="Event that is received using StateMachine.SendEvent")]
		public StringParameter _event;

		private bool isTrigger;
		
		public override void OnEnter ()
		{
			stateMachine.behaviour.onReceiveEvent+=OnTrigger;
		}
		
		public override void OnExit ()
		{
			//if (isTrigger) {
				stateMachine.behaviour.onReceiveEvent -= OnTrigger;
			//}
			isTrigger = false;
		}
		
		private void OnTrigger(string eventName){
			if (_event.Value == eventName) {
				isTrigger = true;
			}
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