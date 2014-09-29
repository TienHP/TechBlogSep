using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",  
	       description = "Enabled Behaviours are Updated, disabled Behaviours are not.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Behaviour-enabled.html")]
	[System.Serializable]
	public class Enabled : GameObjectAction {
		[FieldInfo(tooltip="The name of the MonoBehaviour class to enable/disable.")]
		public StringParameter className;
		[FieldInfo(tooltip="The value to set.")]
		public BoolParameter value;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		private MonoBehaviour behaviour;

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
			behaviour = ((GameObject)gameObject.Value).GetComponent (className.Value) as MonoBehaviour;
			if (behaviour != null) {
				behaviour.enabled=value.Value;	
			}
			if (!everyFrame) {
				Finish();		
			}
		}
		
		public override void OnUpdate ()
		{
			behaviour = ((GameObject)gameObject.Value).GetComponent (className.Value) as MonoBehaviour;
			if (behaviour != null) {
				behaviour.enabled=value.Value;	
			}
		}
	}
}