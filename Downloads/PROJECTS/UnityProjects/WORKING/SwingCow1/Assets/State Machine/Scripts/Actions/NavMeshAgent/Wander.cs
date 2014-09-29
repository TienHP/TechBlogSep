using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "NavMeshAgent",   
	       description = "Wander",
	       url = "")]
	[System.Serializable]
	public class Wander : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject that has a NavMeshAgent component.")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip="Speed of the agent",defaultValue=3.5f)]
		public FloatParameter speed;
		[FieldInfo(tooltip="Angular speed of the agent",defaultValue=120)]
		public FloatParameter angularSpeed;
		[FieldInfo(tooltip="The agent has arrived when the remaining distance is less then this value",defaultValue=0.5f)]
		public FloatParameter threshold;
		[FieldInfo(tooltip="How far away to wander from the current position",defaultValue=20.0f)]
		public FloatParameter wanderDistance;
		[FieldInfo(tooltip="Turn rate.",defaultValue=2.0f)]
		public FloatParameter turnRate;

		private NavMeshAgent agent;
		
		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			agent = ((GameObject)gameObject.Value).GetComponent<NavMeshAgent> ();
			if (agent == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the NavMeshAgent component on the GameObject. Action disabled! If you added the component in the same state, create a new state to run this action.");
				return;
			}

			agent.speed = speed.Value;
			agent.angularSpeed = angularSpeed.Value;
			agent.enabled = true;
			agent.destination = GetNextPosition ();
		}

		public override void OnUpdate ()
		{
			if (agent.remainingDistance < threshold.Value) {
				agent.destination = GetNextPosition ();
			}
		}

		private Vector3 GetNextPosition(){
			Vector3 direction = agent.transform.forward + Random.insideUnitSphere * turnRate.Value;
			return agent.transform.position + direction.normalized * wanderDistance.Value;
		}

	}
}