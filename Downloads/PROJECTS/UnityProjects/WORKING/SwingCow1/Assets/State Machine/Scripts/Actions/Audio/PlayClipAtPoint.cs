using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Audio",   
	       description = "Plays an AudioClip at a given position in world space.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/AudioSource.PlayClipAtPoint.html")]
	[System.Serializable]
	public class PlayClipAtPoint : StateAction {
		[FieldInfo(tooltip="Audio data to play.")]
		public ObjectParameter clip;
		[FieldInfo(tooltip="Position in world space from which sound originates.")]
		public Vector3Parameter position;
		[FieldInfo(tooltip="Playback volume.")]
		public FloatParameter volume;

		public override void OnEnter ()
		{
			AudioSource.PlayClipAtPoint ((AudioClip)clip.Value, position.Value, volume.Value);
			Finish ();
		}
	}
}