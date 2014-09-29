using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "NavMeshAgent",   
	       description = "Flee from target.",
	       url = "")]
	[System.Serializable]
	public class Flee : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject that has a NavMeshAgent component.")]
		public ObjectParameter gameObject;
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="Target to seek.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Speed of the agent",defaultValue=3.5f)]
		public FloatParameter speed;
		[FieldInfo(tooltip="Angular speed of the agent",defaultValue=120)]
		public FloatParameter angularSpeed;
		[FieldInfo(tooltip="The agent has fleed when the distance is greater then this value",defaultValue=10.0f)]
		public FloatParameter fleedDistance;
		
		
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
			agent.enabled = true;
			agent.destination = GetFleePosition ();
		}
		
		public override void OnUpdate ()
		{
			if (Vector3.Distance (agent.transform.position, mTarget.position) < fleedDistance.Value) {
				agent.destination = GetFleePosition (); 
			}
		}
		
		private Vector3 GetFleePosition(){
			return agent.transform.position + (agent.transform.position - mTarget.position).normalized * 5;
		}
		
	}
}