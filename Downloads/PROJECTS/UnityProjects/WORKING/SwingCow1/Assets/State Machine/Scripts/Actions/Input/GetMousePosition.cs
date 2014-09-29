using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Input",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetMousePosition : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use", tooltip="Store the screen position.")]
		public Vector2Parameter screenPosition;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use", tooltip="Store the world position.")]
		public Vector3Parameter worldPosition;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use", tooltip="Store the hit game object.")]
		public ObjectParameter hitObject;
		public LayerMask mask;

		public override void OnUpdate ()
		{
			Vector2 mousePosition = Input.mousePosition;
			screenPosition.Value = mousePosition;
			Ray ray = Camera.main.ScreenPointToRay (mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit,Mathf.Infinity,mask)) {
				worldPosition.Value = hit.point;
				hitObject.Value=hit.transform.gameObject;
			}
		}
	}
}