using UnityEngine;
using System.Collections;

namespace StateMachine.Action.MySQL{
	[Info (category = "MySQL",   
	       description = "Save a bool to MySQL database.",
	       url = "")]
	[System.Serializable]
	public class SetBool : StateAction {
		[FieldInfo(tooltip="The key identifier.")]
		public StringParameter key;
		[FieldInfo(tooltip="The value to save.")]
		public BoolParameter value;
		
		public override void OnEnter ()
		{	
			string serverAddress = PlayerPrefs.GetString ("ServerAddress");
			if (string.IsNullOrEmpty (serverAddress)) {
				Debug.Log ("Please initialize the database to save values. Use MySQL.Initialize");
			} else {
				GameObject instance = new GameObject ();
				CoroutineInstance routineHandler = instance.AddComponent<CoroutineInstance> ();
				routineHandler.StartCoroutine (SaveBool (serverAddress, key.Value, value.Value));
				instance.hideFlags = HideFlags.HideInHierarchy;
			}
			Finish ();
		}
		
		
		private IEnumerator SaveBool(string serverAddress,string key, bool value){
			WWWForm newForm = new WWWForm ();
			newForm.AddField ("key", key);
			newForm.AddField ("value", value?1:0);
			
			WWW w = new WWW (serverAddress + "/saveBool.php", newForm);
			
			while (!w.isDone) {
				yield return new WaitForEndOfFrame();
			}
			
			if (w.error != null) {
				Debug.LogError (w.error);
			}
		}
	}
}