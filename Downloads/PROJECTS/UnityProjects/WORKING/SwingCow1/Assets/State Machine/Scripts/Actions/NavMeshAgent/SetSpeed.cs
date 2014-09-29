using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UNavMeshAgent{
	[Info (category = "NavMeshAgent", 
	       description = "Maximum movement speed when following a path.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/NavMeshAgent-speed.html")]
	[System.Serializable]
	public class SetSpeed : NavMeshAgentAction {
		[FieldInfo(tooltip="The speed to set.")]
		public FloatParameter speed;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			agent.speed = speed.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			agent.speed = speed.Value;
		}
	}
}