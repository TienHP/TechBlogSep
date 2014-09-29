﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter", 
	       description = "Sets the value of a global parameter.",
	       url = "")]
	[System.Serializable]
	public class SetFloat : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The parameter to use.")]
		public FloatParameter parameter;
		[FieldInfo(tooltip="The value to set.")]
		public FloatParameter value;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			parameter.Value = value.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			parameter.Value = value.Value;
		}
	}
}