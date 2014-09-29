using UnityEngine;
using System.Collections;


namespace StateMachine.Action{
	[Info (category = "CharacterController",   
	       description = "A more complex move function taking absolute movement deltas.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/CharacterController.Move.html")]
	[System.Serializable]
	public class Move : StateAction {
		[FieldInfo(canBeConstant=false, requiredField = false, nullLabel = "Owner", tooltip = "A game object that has a CharacterController")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip = "The direction to move towards.")]
		public Vector3Parameter direction;
		[FieldInfo(tooltip= "The direction is relative to the world or the Game Object.")]
		public Space space = Space.World;
		[FieldInfo(tooltip = "The speed to move.")]
		public FloatParameter speed;
		[FieldInfo(requiredField = false,nullLabel="Don't Use",tooltip = "Use gravity during movement?")]
		public BoolParameter useGravity;
		[FieldInfo(requiredField = false, canBeConstant = false, nullLabel = "Don't Jump", tooltip = "The character controller will jump if the value is true.")]
		public BoolParameter jump;
		[FieldInfo(requiredField = false, nullLabel = "Don't Jump", tooltip = "Jump speed.")]
		public FloatParameter jumpSpeed;
		
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
				Debug.LogWarning("Move requires a CharacterController on the game object.");
				Finish();
				return;
			}
			
			Vector3 dir = (space == Space.Self) ? controller.transform.TransformDirection(direction.Value) : direction.Value;
			dir *= speed.Value;
			
			if (useGravity.Value) {
				if (controller.isGrounded)
					dir.y -= 0.9f; 
				else {
					dir += Physics.gravity * Time.deltaTime;
					dir.y += controller.velocity.y;
				}
				
				if (jump.Value) {
					dir.y += jumpSpeed.Value;
					jump.Value = false;
				}
			}
			
			controller.Move(dir * Time.deltaTime);
		}
	}
}