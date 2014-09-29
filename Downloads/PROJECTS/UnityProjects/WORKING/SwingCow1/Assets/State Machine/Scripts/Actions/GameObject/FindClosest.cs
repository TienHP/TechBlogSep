using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",  
	       description = "Finds the closest game object to the target game object.",
	       url = "")]
	[System.Serializable]
	public class FindClosest : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.",dirtyField="gameObject")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The tag of the game object.")]
		public StringParameter tag;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip = "Store the result.")]
		public ObjectParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty (tag.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the tag parameter is empty. Action disabled!");
				return;
			}
			store.Value = FindClosestByTag ((GameObject)gameObject.Value, tag.Value);
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value = FindClosestByTag ((GameObject)gameObject.Value, tag.Value);
		}
		
		private GameObject FindClosestByTag(GameObject target, string tag){
			GameObject[] tagged=GameObject.FindGameObjectsWithTag(tag);
			GameObject closest=null; 
			float distance = Mathf.Infinity; 
			Vector3 position = target.transform.position; 
			foreach (GameObject go in tagged)  { 
				Vector3 diff = (go.transform.position - position);
				float curDistance = diff.sqrMagnitude; 
				if (curDistance < distance && go.transform != target.transform) { 
					closest = go; 
					distance = curDistance; 
				} 
			} 
			return closest;
		}
	}
}