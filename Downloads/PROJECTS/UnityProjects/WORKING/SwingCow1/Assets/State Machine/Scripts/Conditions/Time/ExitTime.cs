using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Time",    
	       description = "Delay a transition.",
	       url = "")]
	[System.Serializable]
	public class ExitTime : StateCondition {
		[FieldInfo(tooltip="Time in seconds.")]
		public FloatParameter time;
		[FieldInfo(tooltip="Remember the time, switching state will not reset it.")]
		public BoolParameter remember;
		private float exitTime;
		
		public override void OnEnter ()
		{
			if (remember.Value && exitTime > Time.time) {

			} else {
				exitTime = Time.time + time.Value;
			}
			
		}

		public override bool Validate ()
		{
			return Time.time > exitTime;
		}
	}
}