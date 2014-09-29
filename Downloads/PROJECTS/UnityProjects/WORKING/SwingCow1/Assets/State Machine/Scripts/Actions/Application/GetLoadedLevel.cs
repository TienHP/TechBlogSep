using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Application",   
	       description = "The name of the level that was last loaded.",
	       url = "http://docs.unity3d.com/ScriptReference/Application-loadedLevelName.html")]
	[System.Serializable]
	public class GetLoadedLevel : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip = "Store the level name.")]
		public StringParameter _name;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			_name.Value = Application.loadedLevelName;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			_name.Value = Application.loadedLevelName;
		}
	}
}