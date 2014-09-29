using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter",  
	       description = "",
	       url = "")]
	[System.Serializable]
	public class ConvertToGameObject : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The parameter to use.")]
		public SystemObjectParameter parameter;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the GameObject.")]
		public ObjectParameter store;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
	
		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value =(GameObject) parameter.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value =(GameObject) parameter.Value;
		}
	}
}