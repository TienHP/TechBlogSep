using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Physics",   
	       description = "True when the ray intersects any collider, otherwise false.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.Raycast.html")]
	[System.Serializable]
	public class Raycast : StateAction {
		[FieldInfo(tooltip="The starting point of the ray in world coordinates.")]
		public Vector3Parameter origin;
		[FieldInfo(tooltip="The direction of the ray.")]
		public Vector3Parameter direction;
		[FieldInfo(tooltip="The length of the ray.")]
		public FloatParameter distance;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;

		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"The distance from the ray's origin to the impact point.")]
		public FloatParameter hitDistance;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"The normal of the surface the ray hit.")]
		public Vector3Parameter hitNormal;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"The impact point in world space where the ray hit the collider.")]
		public Vector3Parameter hitPoint;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"The GameObject of the rigidbody or collider that was hit.")]
		public ObjectParameter hitGameObject;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			RaycastHit hit;
			if (Physics.Raycast (origin.Value, direction.Value,out hit, distance.Value, layerMask)) {
				hitDistance.Value=hit.distance;
				hitNormal.Value=hit.normal;
				hitPoint.Value=hit.point;
				hitGameObject.Value=hit.transform.gameObject;
			}
			
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			RaycastHit hit;
			if (Physics.Raycast (origin.Value, direction.Value,out hit, distance.Value, layerMask)) {
				hitDistance.Value=hit.distance;
				hitNormal.Value=hit.normal;
				hitPoint.Value=hit.point;
				hitGameObject.Value=hit.transform.gameObject;
			}
		}
		
	}
}