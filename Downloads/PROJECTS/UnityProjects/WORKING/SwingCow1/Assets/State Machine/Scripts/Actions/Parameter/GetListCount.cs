using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter",  
	       description = "Gets the list element at index.",
	       url = "")]
	[System.Serializable]
	public class GetListCount : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The List to work with.")]
		public ListParameter list;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The parameter to use.")]
		public IntParameter store;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = list.Value != null ? list.Value.Count : 0;	
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = list.Value != null ? list.Value.Count : 0;	
		}
	}
}