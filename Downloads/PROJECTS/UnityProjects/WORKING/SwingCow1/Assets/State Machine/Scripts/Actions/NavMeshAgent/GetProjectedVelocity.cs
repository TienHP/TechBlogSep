using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent",  
	       description = "Projects the desired velocity of the NavMeshAgent with transforms forward vector.",
	       url = "")]
	[System.Serializable]
	public class GetProjectedVelocity : NavMeshAgentAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use",tooltip="The projected velocity.")]
		public Vector3Parameter velocity;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use",tooltip="The magnitude of the velocity.")]
		public FloatParameter magnitude;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
	
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			Vector3 vel = Vector3.Project (agent.desiredVelocity, agent.transform.forward);
			velocity.Value = vel;
			magnitude.Value = vel.magnitude;
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			Vector3 vel = Vector3.Project (agent.desiredVelocity, agent.transform.forward);
			velocity.Value = vel;
			magnitude.Value = vel.magnitude;
		}
	}
}