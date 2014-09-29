using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",   
	       description = "Rotates the transform smooth towards the target.",
	       url = "")]
	[System.Serializable]
	public class SmoothLookAt : GameObjectAction {
		[FieldInfo(canBeConstant=false,nullLabel="None", tooltip="GameObject to look at.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The angular speed in degrees/second to rotate the game object.")]
		public FloatParameter speed;
		[FieldInfo(tooltip="If set to true, the game object will be rotated only in y axis.")]
		public BoolParameter inY;


		private Quaternion lastRotation;
		private Quaternion desiredRotation;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			lastRotation =((GameObject)gameObject.Value).transform.rotation;
			desiredRotation = lastRotation;
		}
		
		public override void OnUpdate ()
		{
			Transform gameObjectTransform = ((GameObject)gameObject.Value).transform;
			Transform targetTransform=((GameObject)target.Value).transform;
			Vector3 targetPosition = targetTransform.position;
			Vector3 gameObjectPosition = gameObjectTransform.position;

			targetPosition.y = (inY.Value ? gameObjectPosition.y : targetPosition.y);

			Vector3 diff = targetPosition - gameObjectPosition;
			if (diff != Vector3.zero && diff.sqrMagnitude > 0)
			{
				desiredRotation = Quaternion.LookRotation(diff);
			}
			
			lastRotation = Quaternion.Slerp(lastRotation, desiredRotation, speed.Value * Time.deltaTime);
			gameObjectTransform.rotation = lastRotation;
		}
	}
}