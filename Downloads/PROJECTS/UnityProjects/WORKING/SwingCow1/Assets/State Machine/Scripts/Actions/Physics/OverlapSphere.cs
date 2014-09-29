﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StateMachine.Action{
	[Info (category = "Physics",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class OverlapSphere : StateAction {
		[FieldInfo(tooltip="The starting point.")]
		public Vector3Parameter origin;
		[FieldInfo(tooltip="")]
		public FloatParameter radius;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;
		
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false, nullLabel="Don't Use", tooltip=	"")]
		public ListParameter hitGameObjects;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			Collider[] hits = Physics.OverlapSphere (origin.Value,radius.Value, layerMask);
			List<object> gos = new List<object> ();
			foreach (Collider hit in hits) {
				gos.Add(hit.gameObject);
				Debug.Log(hit.gameObject.name);
			}
			hitGameObjects.Value = gos;
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			Collider[] hits = Physics.OverlapSphere (origin.Value,radius.Value, layerMask);
			List<object> gos = new List<object> ();
			foreach (Collider hit in hits) {
				gos.Add(hit.gameObject);
				Debug.Log(hit.gameObject.name);
			}
			hitGameObjects.Value = gos;	
		}
	}
}