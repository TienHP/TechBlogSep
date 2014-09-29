using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Clones the object original.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Object.Instantiate.html")]
	[System.Serializable]
	public class Instantiate : StateAction {
		[FieldInfo(tooltip="An existing object that you want to make a copy of.")]
		public ObjectParameter original;
		[FieldInfo(tooltip="Position for the new object.")]
		public Vector3Parameter position;
		[FieldInfo(tooltip="Orientation of the new object.")]
		public Vector3Parameter rotation;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField = false,nullLabel="Don't Store",tooltip = "Instantiated clone of the original.")]
		public ObjectParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			if (original.Value== null) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the Object to instantiate is null. Action disabled!");
				return;
			}
			store.Value=Instantiate (original.Value, position.Value, Quaternion.Euler (rotation.Value));
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value=Instantiate (original.Value, position.Value, Quaternion.Euler (rotation.Value));
		}
	}
}