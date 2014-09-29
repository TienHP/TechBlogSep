﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetInt : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Parameter to test.")]
		public IntParameter parameter;

		[FieldInfo(tooltip="Is the parameter greater or less?")]
		public FloatComparer comparer;
		[FieldInfo(tooltip="Value to test with.")]
		public IntParameter value;

		public override bool Validate ()
		{

			//Debug.Log (parameter.Name + " " + parameter.Value + ">" + value.Name + " " + value.Value);
			switch (comparer) {
			case FloatComparer.Less:
				return parameter.Value < value.Value;
			case FloatComparer.Greater:
				return parameter.Value > value.Value;
			}
			return false;
		}
	}
}