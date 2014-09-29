using UnityEngine;
using System.Collections;

namespace StateMachine.Action.MySQL{
	[Info (category = "MySQL",   
	       description = "Save a float to MySQL database.",
	       url = "")]
	[System.Serializable]
	public class SetFloat : StateAction {
		[FieldInfo(tooltip="The key identifier.")]
		public StringParameter key;
		[FieldInfo(tooltip="The value to save.")]
		public FloatParameter value;
		
		public override void OnEnter ()
		{	
			string serverAddress = PlayerPrefs.GetString ("ServerAddress");
			if (string.IsNullOrEmpty (serverAddress)) {
				Debug.Log ("Please initialize the database to save values. Use MySQL.Initialize");
			} else {
				GameObject instance = new GameObject ();
				CoroutineInstance routineHandler = instance.AddComponent<CoroutineInstance> ();
				routineHandler.StartCoroutine (SaveFloat (serverAddress, key.Value, value.Value));
				instance.hideFlags = HideFlags.HideInHierarchy;
			}
			Finish ();
		}
		
		
		private IEnumerator SaveFloat(string serverAddress,string key, float value){
			WWWForm newForm = new WWWForm ();
			newForm.AddField ("key", key);
			newForm.AddField ("value", value.ToString());
			
			WWW w = new WWW (serverAddress + "/saveFloat.php", newForm);
			
			while (!w.isDone) {
				yield return new WaitForEndOfFrame();
			}
			
			if (w.error != null) {
				Debug.LogError (w.error);
			}
		}
	}
}