using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Input",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetKeyDown : StateCondition {
		[FieldInfo(tooltip="")]
		public IntParameter key;
		
		public override bool Validate ()
		{
			return Input.GetKeyDown((KeyCode)key.Value);
		}
	}
}