using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Debug",   
	       description = "Draws a line between specified start and end points.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Debug.DrawLine.html")]
	[System.Serializable]
	public class DrawLine : StateAction {
		[FieldInfo(tooltip="Start position of the line.")]
		public Vector3Parameter start;
		[FieldInfo(tooltip="End position of the line.")]
		public Vector3Parameter end;
		[FieldInfo(tooltip="Color of the line.")]
		public ColorParameter color;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			Debug.DrawLine (start.Value, end.Value, color.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Debug.DrawLine (start.Value, end.Value, color.Value);
		}
	}
}
