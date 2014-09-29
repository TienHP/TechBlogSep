using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Math{
	[Info (category = "Math",    
	       description = "Computes the product of its operands",
	       url = "http://msdn.microsoft.com/en-us/library/z19tbbca.aspx")]
	[System.Serializable]
	public class Multiply : StateAction {
		[FieldInfo(tooltip="First operand.")]
		public FloatParameter first;
		[FieldInfo(tooltip="Second operand")]
		public FloatParameter second;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public FloatParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = first.Value * second.Value;
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value = first.Value * second.Value;
		}
	}
}