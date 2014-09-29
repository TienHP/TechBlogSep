using UnityEngine;
using System.Collections;
using StateMachine;

namespace SwingCow
{
		[RequireComponent(typeof(Rigidbody2D))]
		[RequireComponent(typeof(BoxCollider2D))]
		public class PlayerStateListener : MonoBehaviour
		{

				public Transform root;
				private StateMachineBehaviour[] aiDiagrams;
				private PlayerProperties playerProps;
				public Transform vaneItem;
				private PlayerStateController.PlayerState currentPlayerState = PlayerStateController.PlayerState.idle;

				void Start ()
				{
						PlayerStateController.onStateChange += HandleOnStateChange;
						aiDiagrams = GetComponents<StateMachineBehaviour> ();
				}

				void LateUpdate ()
				{
						
				}
				
				void FixedUpdate ()
				{
						switch (currentPlayerState) {
						case PlayerStateController.PlayerState.taking_vane:
								FlyWithVane ();
								break;
						}//end switch
				}//end method

				void HandleOnStateChange (PlayerStateController.PlayerState newState, System.Object[] exParams1, object[] exParams2)
				{
						Debug.Log("current state: "+ currentPlayerState);
						switch (newState) {
						case PlayerStateController.PlayerState.jump_funny:
									//set flag to 'startfunny' to AI diagram
								SetParameter ("startFunny", true);
								break;
						case PlayerStateController.PlayerState.jump:
								Jump ();
								break;
						case PlayerStateController.PlayerState.collided:
								GameObject go = exParams1 [0] as GameObject;
								string tag = exParams2 [0] as string;
								ExecuteCollision (go, tag);
								break;
						default:
								break;
						}//end switch
				
						if (newState != currentPlayerState) {
								currentPlayerState = newState;
						}//end if
				}	//end method	
				
				/// <summary>
				/// execute when starting jump_funny state
				/// </summary>
				void StartAnimationFunny1 ()
				{
						StartCoroutine (PlayAnimationFunny1 ());						
				}//end method
				
				void StartAnimationFunny2 ()
				{
						StartCoroutine (PlayAnimationFunny2 ());
				}
				
				void StartAnimationFunny3 ()
				{
						StartCoroutine (PlayAnimationFunny3 ());
				}

				void SetParameter (string stateName, string name, object value)
				{
						foreach (var aiDiagram in aiDiagrams) {
								if (aiDiagram.stateMachine.name == stateName)
										aiDiagram.SetParameter (new object[]{name, value});
						}//end foreach
				}//end method

				void SetParameter (string name, object value)
				{
						foreach (var aiDiagram in aiDiagrams) {
								aiDiagram.SetParameter (new object[]{name, value});
						}//end foreach
				}//end method

				IEnumerator PlayAnimationFunny1 ()
				{
						animation.Play ("jump_funny");
						yield return new WaitForSeconds (1.5f);
						animation.Stop ();
						
						animation.Play ("rotate_funny");
						yield return new WaitForSeconds (1.0f);
						animation.Stop ();
						
						animation.Play ("close_open_eyes");
						yield return new WaitForSeconds (4 / 3f);
						animation.Stop ();
				}//end method
				
				IEnumerator PlayAnimationFunny2 ()
				{
						animation.Play ("jump_funny");
						yield return new WaitForSeconds (1.5f);
						animation.Stop ();
			
						animation.Play ("rotate_funny");
						yield return new WaitForSeconds (1.0f);
						animation.Stop ();
			
				}//end method

				IEnumerator PlayAnimationFunny3 ()
				{
						animation.Play ("jump_funny_long");
						yield return new WaitForSeconds (1.5f);
						animation.Stop ();
			
						animation.Play ("rotate_funny");
						yield return new WaitForSeconds (1.0f);
						animation.Stop ();
				}//end method

				public void Jump ()
				{
						playerProps = DataManager.entity.playerProps;
						rigidbody2D.AddForce (new Vector2 (0, 1) * playerProps.JumpSpeed);
				}//end method
				
				public void ExecuteCollision (GameObject go, string tag)
				{
						if (tag.ToLower () == "vane") {
								EvolveVane ();		
						}//end if
						else if (tag.ToLower () == "ground" && currentPlayerState == PlayerStateController.PlayerState.jump) {
								PlayerStateController.FireOnStateChangeEvt (PlayerStateController.PlayerState.idle, null, null);
						}//end if
				}//end method
			
				/// <summary>
				/// Evolves the vane.
				/// </summary>
				void EvolveVane ()
				{	
						if (vaneItem != null && !vaneItem.gameObject.activeSelf) {
								vaneItem.gameObject.SetActive (true);
						}//end if
						PlayerStateController.FireOnStateChangeEvt(PlayerStateController.PlayerState.taking_vane, null, null);
				}//end method

				void FlyWithVane ()
				{
					Debug.LogError("fly with vane");
					rigidbody2D.AddForce(new Vector2(0, 15), ForceMode2D.Force);
				}//end method
		}//end class
}//end namespace
