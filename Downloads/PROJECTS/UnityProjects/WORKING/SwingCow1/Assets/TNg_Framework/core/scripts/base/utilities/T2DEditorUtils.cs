using UnityEngine;
using System.Collections;
using UnityEditor;

public class T2DEditorUtils : MonoBehaviour {

	#region Inspector

	public static void DeselectSceneTools(){
		Tools.current = UnityEditor.Tool.None;
	}

	public static bool IsAnySceneToolSelected(){
		return Tools.current > UnityEditor.Tool.None ;
	}

	#endregion

}
