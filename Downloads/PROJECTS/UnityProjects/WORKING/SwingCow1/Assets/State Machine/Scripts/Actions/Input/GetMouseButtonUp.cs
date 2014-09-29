using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Input",    
	       description = "Returns true during the frame the user releases the given mouse button.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Input.GetMouseButtonUp.html")]
	[System.Serializable]
	public class GetMouseButtonUp : StateAction {
		[FieldInfo(tooltip="Button values are 0 for left button, 1 for right button, 2 for the middle button.")]
		public IntParameter button;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, tooltip="Store the result.")]
		public BoolParameter store;
		
		public override void OnUpdate ()
		{
			store.Value = Input.GetMouseButtonUp (button.Value);	
		}
	}
}