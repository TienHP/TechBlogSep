using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent",   
	       description = "Sets or updates the destination thus triggering the calculation for a new path.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/NavMeshAgent.SetDestination.html")]
	[System.Serializable]
	public class SetDestination : NavMeshAgentAction {
		[FieldInfo(tooltip="The destination to set.")]
		public Vector3Parameter destination;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			agent.SetDestination (destination.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			agent.SetDestination (destination.Value);
		}
	}
}