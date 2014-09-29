using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Destroys all children of the target.",
	       url = "")]
	[System.Serializable]
	public class DestroyChildren : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.",dirtyField="gameObject")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Should inactive children be destroyed?")]
		public BoolParameter includeInactive;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		private Transform root;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			root = ((GameObject)gameObject.Value).transform;
			foreach(Transform transform in root){
				Destroy(transform.gameObject);
			}
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			foreach(Transform transform in root){
				Destroy(transform.gameObject);
			}
		}
	}
}