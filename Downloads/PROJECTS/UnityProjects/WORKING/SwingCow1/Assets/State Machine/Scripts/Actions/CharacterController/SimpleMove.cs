using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "CharacterController",    
	       description = "Moves the character with speed.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/CharacterController.SimpleMove.html")]
	[System.Serializable]
	public class SimpleMove : StateAction {
		[FieldInfo(canBeConstant=false, requiredField = false, nullLabel = "Owner", tooltip = "A game object that has a CharacterController")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip = "The direction to move towards.")]
		public Vector3Parameter direction;
		[FieldInfo(tooltip= "The direction is relative to the world or the Game Object.")]
		public Space space = Space.World;
		
		[FieldInfo(tooltip = "The speed to move.")]
		public FloatParameter speed;
		
		private CharacterController controller;
		
		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			controller = ((GameObject)gameObject.Value).GetComponent<CharacterController> ();
			if (controller == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the CharacterController component on the GameObject. Action disabled!");
			}
		}
		
		public override void OnUpdate ()
		{
			if (controller == null) {
				Debug.LogWarning("Simple Move requires a CharacterController on the game object.");
				Finish();
				return;
			}
			
			if (space == Space.Self) {
				controller.SimpleMove (controller.transform.TransformDirection (direction.Value) * speed.Value);
			} else {
				controller.SimpleMove (direction.Value * speed.Value);
			}
		}
	}
}