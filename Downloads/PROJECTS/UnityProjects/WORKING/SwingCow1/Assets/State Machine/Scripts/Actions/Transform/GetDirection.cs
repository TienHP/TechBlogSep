using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",  
	       description = "Get the direction of the gameObject and target.",
	       url = "")]
	[System.Serializable]
	public class GetDirection : GameObjectAction {
		[FieldInfo(canBeConstant=false,nullLabel="None")]
		public ObjectParameter target;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="The normalized direction")]
		public Vector3Parameter normalized;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="The magnitude of the direction")]
		public FloatParameter magnitude;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="The direction")]
		public Vector3Parameter direction;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="X component of the direction.")]
		public FloatParameter x;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="Y component of the direction.")]
		public FloatParameter y;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="Z component of the direction.")]
		public FloatParameter z;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			Transform gameObjectTransform = ((GameObject)gameObject.Value).transform;
			Transform targetTransform=((GameObject)target.Value).transform;
			Vector3 dir = targetTransform.position - gameObjectTransform.position;
			normalized.Value = dir.normalized;
			magnitude.Value = dir.magnitude;
			direction.Value = dir;
			x.Value = dir.x;
			y.Value = dir.y;
			z.Value = dir.z;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Transform gameObjectTransform = ((GameObject)gameObject.Value).transform;
			Transform targetTransform=((GameObject)target.Value).transform;
			Vector3 dir = targetTransform.position - gameObjectTransform.position;
			normalized.Value = dir.normalized;
			magnitude.Value = dir.magnitude;
			direction.Value = dir;
			x.Value = dir.x;
			y.Value = dir.y;
			z.Value = dir.z;
		}
	}
}