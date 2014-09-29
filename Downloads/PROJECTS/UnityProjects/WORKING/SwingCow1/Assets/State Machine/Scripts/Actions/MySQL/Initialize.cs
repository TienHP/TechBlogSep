using UnityEngine;
using System.Collections;

namespace StateMachine.Action.MySQL{
	[Info (category = "MySQL",   
	       description = "Initialize MySQL to use saving actions.",
	       url = "")]
	[System.Serializable]
	public class Initialize : StateAction {
		[FieldInfo(tooltip="Link to the folder with the php scripts on your host.")]
		public StringParameter serverAddress;
		
		public override void OnEnter ()
		{
			PlayerPrefs.SetString ("ServerAddress", serverAddress.Value);
			Finish ();
		}
	}
}