﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject",    
	       description = "Checks LineOfSight between two game objects.",
	       url = "")]
	[System.Serializable]
	public class LineOfSight : StateCondition {
		[FieldInfo(canBeConstant=false,requiredField=false, nullLabel="Owner",tooltip="GameObject to test.")]
		public ObjectParameter gameObject;
		[FieldInfo(canBeConstant=false, nullLabel="None",tooltip="Target GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="")]
		public FloatParameter angle;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;
		[FieldInfo(tooltip="")]
		public Vector3Parameter offset;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+gameObject.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}

			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+target.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}
		}
		
		public override bool Validate ()
		{
			float targetAngle = Vector3.Angle (((GameObject)target.Value).transform.position - ((GameObject)gameObject.Value).transform.position,((GameObject)gameObject.Value).transform.forward);
			if (Mathf.Abs (targetAngle) < angle.Value*0.5f) {
				RaycastHit hit;
				if (Physics.Linecast (((GameObject)gameObject.Value).transform.position + offset.Value, ((GameObject)target.Value).transform.position + offset.Value, out hit, layerMask)) {
					if (hit.transform == ((GameObject)target.Value).transform) {  
						return equals.Value==true;
					}
				}
			}
			return equals.Value == false;
		}
	}
}