using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UnityAnimatin{
	[Info (category = "Animation",    
	       description = "Is the animation named name playing?",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.IsPlaying.html")]
	[System.Serializable]
	public class IsPlaying : StateCondition {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Name of the animation.")]
		public StringParameter _name;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		private Animation animation;
		public override void OnEnter ()
		{
			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+target.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}
			
			animation = ((GameObject)target.Value).animation;
			if (animation == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the Animation component on the GameObject. Condition disabled!");
			}
		}
		
		public override bool Validate ()
		{
			if(animation != null){
				
				return (animation.IsPlaying(_name.Value) == equals.Value);
			}
			return false;
		}
	}
}