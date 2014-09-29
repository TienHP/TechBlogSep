using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Set the name of the game object.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Object-name.html")]
	[System.Serializable]
	public class SetName : GameObjectAction {
		[FieldInfo(tooltip="The new name to set.")]
		public StringParameter _name;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			((GameObject)gameObject.Value).name = _name.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			((GameObject)gameObject.Value).name = _name.Value;
		}
	}
}