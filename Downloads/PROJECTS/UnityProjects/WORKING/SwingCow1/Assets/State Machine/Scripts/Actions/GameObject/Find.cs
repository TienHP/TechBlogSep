using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Finds a game object by name.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/GameObject.Find.html")]
	[System.Serializable]
	public class Find : StateAction {
		[FieldInfo(tooltip="The name of the game object to find.")]
		public StringParameter _name;
		[FieldInfo(canBeConstant=false, bindedCanBeConstant=false,nullLabel="None",tooltip = "Store the result.")]
		public ObjectParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			store.Value = GameObject.Find (_name.Value);
			if (!everyFrame) {
				Finish ();		
			}
		}

		public override void OnUpdate ()
		{
			store.Value = GameObject.Find (_name.Value);
		}
	}
}