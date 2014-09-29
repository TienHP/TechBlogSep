using UnityEngine;
using System.Collections;

namespace SwingCow
{
		public class PlayerColliderListener : MonoBehaviour
		{
			
				void OnTriggerEnter2D (Collider2D collider2D)
				{
					
						var go = collider2D.gameObject;
						//check whether player collide with item
						PlayerStateController.FireOnStateChangeEvt (PlayerStateController.PlayerState.collided, new System.Object[] {go}, new object[]{ go.tag }); 
				}//end method
			
				void OnTriggerExit2D (Collider2D collider2D)
				{
						var go = collider2D.gameObject;
						//check whether player collide with item
						if (go.tag.ToLower () == "vane") {
								PlayerStateController.FireOnStateChangeEvt (PlayerStateController.PlayerState.taking_vane, new System.Object[] {go}, new object[]{"vane"}); 
						}//end if
				}//end method

				void OnCollisionEnter2D (Collision2D collision)
				{
						var go = collision.gameObject;
						//check whether player collide with item
						if (go.tag.ToLower() != "ground")
							PlayerStateController.FireOnStateChangeEvt (PlayerStateController.PlayerState.idle, new System.Object[] {go}, new object[]{ go.tag }); 
					
				}//end method
		}//end class
}//end namespace
