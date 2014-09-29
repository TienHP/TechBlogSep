
namespace StateMachine.Action{
	[Info (category = "GameObject",    
	       description = "Removes a gameobject, component or asset.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Object.Destroy.html")]
	[System.Serializable]
	public class Destroy : GameObjectAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.",dirtyField="gameObject")]
		public ObjectParameter target;

		[FieldInfo(tooltip="Delay")]
		public FloatParameter delay;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			Destroy (gameObject.Value,delay.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Destroy (gameObject.Value,delay.Value);
		}
	}
}