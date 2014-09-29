using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter",   
	       description = "Combines two strings into one.",
	       url = "")]
	[System.Serializable]
	public class CombineStrings : StateAction {
		[FieldInfo(tooltip="The first string to use.")]
		public StringParameter first;
		[FieldInfo(tooltip="The second string.")]
		public StringParameter second;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result.")]
		public StringParameter store;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = first.Value + second.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = first.Value + second.Value;
		}
	}
}