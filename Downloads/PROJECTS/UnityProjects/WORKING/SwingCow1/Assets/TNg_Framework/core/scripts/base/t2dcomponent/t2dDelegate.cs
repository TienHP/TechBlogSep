using UnityEngine;
using System.Collections;

public class t2dDelegate {
	public delegate void OnInitEditor();
	public static event OnInitEditor onInitEditorEvt;

	public static void FireOnInitEditorEvt(){
		if (onInitEditorEvt != null)
			onInitEditorEvt();
	}//end method
}//end class
