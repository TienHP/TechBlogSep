using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent",    
	       description = "The desired velocity of the agent including any potential contribution from avoidance.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/NavMeshAgent-desiredVelocity.html")]
	[System.Serializable]
	public class GetDesiredVelocity : NavMeshAgentAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Result to store.")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = agent.desiredVelocity;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = agent.desiredVelocity;
		}
	}
}