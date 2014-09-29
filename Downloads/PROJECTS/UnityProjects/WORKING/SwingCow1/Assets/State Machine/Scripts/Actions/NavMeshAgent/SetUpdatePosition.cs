using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent",   
	       description = "Should the agent update the transform position?",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/NavMeshAgent-updatePosition.html")]
	[System.Serializable]
	public class SetUpdatePosition : NavMeshAgentAction {
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
			agent.updatePosition = value.Value;
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			agent.updatePosition = value.Value;
		}
	}
}