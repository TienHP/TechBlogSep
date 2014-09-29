using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter",  
	       description = "Gets the list element at index.",
	       url = "")]
	[System.Serializable]
	public class GetListElementAt : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The List to work with.")]
		public ListParameter list;
		[FieldInfo(tooltip="Index")]
		public IntParameter index;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The parameter to use.")]
		public SystemObjectParameter store;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if(list.Value.Count>index.Value){
				store.Value = list.Value[index.Value] ;
			}
			
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			if(list.Value.Count>index.Value){
				store.Value = list.Value[index.Value] ;
			}
		}
	}
}