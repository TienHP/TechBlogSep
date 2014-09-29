using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Audio",   
	       description = "Sets the default audio clip to play.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/AudioSource-clip.html")]
	[System.Serializable]
	public class SetAudioClip : AudioSourceAction {
		[FieldInfo(tooltip="The clip to set.")]
		public ObjectParameter clip;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			audio.clip = (AudioClip)clip.Value;
			Finish ();
		}
	}
}