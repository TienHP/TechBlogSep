using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "NavMeshAgent",   
	       description = "Seek a target.",
	       url = "")]
	[System.Serializable]
	public class Seek : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject that has a NavMeshAgent component.")]
		public ObjectParameter gameObject;
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="Target to seek.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Speed of the agent",defaultValue=3.5f)]
		public FloatParameter speed;
		[FieldInfo(tooltip="Angular speed of the agent",defaultValue=120)]
		public FloatParameter angularSpeed;
		[FieldInfo(tooltip="The agent has arrived when the distance to target is less then this value",defaultValue=1.5f)]
		public FloatParameter stoppingDistance;

		
		private NavMeshAgent agent;
		private Transform mTarget;
		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}

			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			mTarget = ((GameObject)target.Value).transform;

			agent = ((GameObject)gameObject.Value).GetComponent<NavMeshAgent> ();
			if (agent == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the NavMeshAgent component on the GameObject. Action disabled! If you added the component in the same state, create a new state to run this action.");
				return;
			}
			
			agent.speed = speed.Value;
			agent.angularSpeed = angularSpeed.Value;
			agent.stoppingDistance = stoppingDistance.Value;
			agent.enabled = true;
			agent.destination = GetTargetPosition ();
		}
		
		public override void OnUpdate ()
		{
			agent.destination = GetTargetPosition (); 
		}
		
		private Vector3 GetTargetPosition(){
			return mTarget.position;
		}
		
	}
}