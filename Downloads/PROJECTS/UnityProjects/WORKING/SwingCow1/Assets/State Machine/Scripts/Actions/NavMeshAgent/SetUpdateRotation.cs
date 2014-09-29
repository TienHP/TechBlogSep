using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent",   
	       description = "Should the agent update the transform orientation?",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/NavMeshAgent-updateRotation.html")]
	[System.Serializable]
	public class SetUpdateRotation : NavMeshAgentAction {
		[FieldInfo(tooltip="The value to set.")]
		public BoolParameter value;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			agent.updateRotation = value.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			agent.updateRotation = value.Value;
		}
	}
}