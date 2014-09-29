using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine.Action{
	[Info (category = "Physics",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class SphereCastAll : StateAction {
		[FieldInfo(tooltip="The starting point of the ray in world coordinates.")]
		public Vector3Parameter origin;
		[FieldInfo(tooltip="")]
		public FloatParameter radius;
		[FieldInfo(tooltip="The direction of the ray.")]
		public Vector3Parameter direction;
		[FieldInfo(tooltip="The length of the ray.")]
		public FloatParameter distance;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;

		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"")]
		public ListParameter hitGameObjects;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			RaycastHit[] hits = Physics.SphereCastAll (origin.Value,radius.Value, direction.Value, distance.Value, layerMask);
			List<object> gos = new List<object> ();
			foreach (RaycastHit hit in hits) {
				gos.Add(hit.transform.gameObject);
				Debug.Log(hit.transform.name);
			}
			hitGameObjects.Value = gos;
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			RaycastHit[] hits = Physics.SphereCastAll (origin.Value,radius.Value, direction.Value, distance.Value, layerMask);
			List<object> gos = new List<object> ();
			foreach (RaycastHit hit in hits) {
				gos.Add(hit.transform.gameObject);
				Debug.Log(hit.transform.name);
			}
			hitGameObjects.Value = gos;	
		}
		
	}
}