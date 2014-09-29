﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Audio",   
	       description = "Plays the clip with an optional certain delay.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/AudioSource.Play.html")]
	[System.Serializable]
	public class Play : AudioSourceAction {
		[FieldInfo(tooltip="Delay in seconds.")]
		public FloatParameter delay;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			audio.PlayDelayed (delay.Value);
			Finish ();
		}
	}
}