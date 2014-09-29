using UnityEngine;
using System.Collections;
using StateMachine;

public class CameraAuto : MonoBehaviour {

	private Transform m_player;
	private Transform m_transform;
	private float lerpSpeed = 5.0f;
	private Camera m_camera;
	private Vector2 belowOffset;
	private bool isFollowingPlayer = false;
	private StateMachineBehaviour sm;

	void Start(){
		m_camera = Camera.main;
		m_transform = transform;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;
		belowOffset = new Vector2(screenWidth/2, 50.0f);

		//test start follow
		sm = GetComponent<StateMachineBehaviour>();
		//sm.SetParameter(new object[]{"Follow", true});
		sm.stateMachine.SetParameter("Follow", true);
	}//end method

	void LateUpdate(){
		if (isFollowingPlayer)
			FollowPlayer();
	}//end method

	void FollowPlayer(){

		var playerOnScreen = m_camera.WorldToViewportPoint(m_player.position);

		//check whether player go to screen limit
		if (playerOnScreen.x < 0.2f || playerOnScreen.x > 0.8f || playerOnScreen.y > 0.8f || playerOnScreen.y < 0.1f){
			//follow player
			m_transform.position = Vector3.Lerp(m_transform.position, m_player.position, lerpSpeed * Time.deltaTime);
		}//end if
	}//end method

	void StartFollowPlayer(){
		if (m_player == null){
			m_player = GameObject.FindWithTag("Player").transform;
		}//end if
		isFollowingPlayer = true;
	}//end method
}
