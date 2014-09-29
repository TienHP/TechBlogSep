using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",    
	       description = "Rotates the transform so the forward vector points at target's current position.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Transform.LookAt.html")]
	[System.Serializable]
	public class LookAt : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject to look at.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Vector specifying the upward direction.")]
		public Vector3Parameter worldUp;
		[FieldInfo(tooltip="If set to true, the game object will be rotated only in y axis.")]
		public BoolParameter inY;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		private Transform transform;
		private Transform targetTransform;
		public override void OnEnter ()
		{
			base.OnEnter();
			if (disabled) {
				return;
			}

			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}

			if (gameObject.Value == target.Value) {
				disabled=true;	
				return;
			}

			transform = ((GameObject)gameObject.Value).transform;
			targetTransform=((GameObject)target.Value).transform;

			Vector3 position = targetTransform.position;

			Vector3 lookAt = new Vector3 (position.x,
				                         (inY.Value ? transform.position.y : position.y),
				                          position.z
				                           );
				
				transform.LookAt (lookAt, worldUp.Value);

			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			Vector3 position = targetTransform.position;
			
			Vector3 lookAt = new Vector3 (position.x,
			                              (inY.Value ? transform.position.y : position.y),
			                              position.z
			                              );
			
			transform.LookAt (lookAt, worldUp.Value);

		}
	}
}