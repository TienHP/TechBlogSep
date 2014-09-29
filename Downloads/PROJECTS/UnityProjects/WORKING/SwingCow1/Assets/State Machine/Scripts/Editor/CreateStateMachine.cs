using UnityEngine;
using UnityEditor;
using System.Collections;

namespace StateMachine{
	public static class CreateStateMachine {
		[MenuItem("Assets/Create/State Machine/State Machine")]
		public static void CreateAIControllerAsset()
		{
			StateMachine stateMachine=UnityEditorTools.CreateAsset<StateMachine>();
			stateMachine.name="New StateMachine";
		}

		[MenuItem("Window/State Machine/Create State Machine",false,2)]
		public static void CreateStateMachineFromWindowAsset()
		{
			StateMachine stateMachine=	UnityEditorTools.CreateAsset<StateMachine>(true);
			StateMachineEditor.Show (stateMachine);

		}

	}
}