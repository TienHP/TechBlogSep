using UnityEngine;

namespace StateMachine.Action{
	[Info (category = "Transform",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class LookAt2D : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject to look at.")]
		public ObjectParameter target;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (gameObject.Value == target.Value) {
				disabled=true;	
				return;
			}
			Transform transform = gameObject.Value != null ? ((GameObject)gameObject.Value).transform : null;
			if (transform != null && target.Value != null) {
				Vector3 position = ((GameObject)target.Value).transform.position;
				Vector3 dir = position - transform.position;
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			Transform transform = gameObject.Value != null ? ((GameObject)gameObject.Value).transform : null;
			if (transform != null && target.Value != null) {
				Vector3 position = ((GameObject)target.Value).transform.position;
				Vector3 dir = position - transform.position;
				float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}
		}
	}
}