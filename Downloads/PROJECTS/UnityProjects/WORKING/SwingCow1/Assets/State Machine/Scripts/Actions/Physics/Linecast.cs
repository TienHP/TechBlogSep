using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Physics",   
	       description = "Returns true if there is any collider intersecting the line between start and end.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.Linecast.html")]
	[System.Serializable]
	public class Linecast : StateAction {
		[FieldInfo(tooltip="The starting point.")]
		public Vector3Parameter start;
		[FieldInfo(tooltip="The end point.")]
		public Vector3Parameter end;
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
			if (Physics.Raycast (start.Value, end.Value,out hit, layerMask)) {
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
			if (Physics.Raycast (start.Value, end.Value,out hit, layerMask)) {
				hitDistance.Value=hit.distance;
				hitNormal.Value=hit.normal;
				hitPoint.Value=hit.point;
				hitGameObject.Value=hit.transform.gameObject;
			}	
		}
		
	}
}