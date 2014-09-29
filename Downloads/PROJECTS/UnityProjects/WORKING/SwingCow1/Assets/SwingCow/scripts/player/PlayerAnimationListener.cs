using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animation))]
public class PlayerAnimationListener : MonoBehaviour {

	//happen at end of Jump_Animation
	public void End_Of_Jump_Funny(){
		Debug.Log("End_Of_Jump_Funny");
		Invoke("PlayRotateFunny", 0.1f);
	}//end method

	void PlayRotateFunny(){
		animation.CrossFade("rotate_funny");
		Invoke("StopRotateFunny", 1.0f);
	}//end method

	void StopRotateFunny(){
		animation.Stop();
		StartCoroutine(PlayOpenCloseEyes(1.0f));
	}//end method

	IEnumerator PlayOpenCloseEyes(float time){
		animation.Play("close_open_eyes");
		yield return new WaitForSeconds(time);
		animation.Stop();
	}//end method
}//end class
