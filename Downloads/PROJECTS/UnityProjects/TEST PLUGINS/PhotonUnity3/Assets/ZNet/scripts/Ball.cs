using UnityEngine;
using System.Collections;


[RequireComponent (typeof(NetworkView))]
public class Ball : MonoBehaviour {

	public float startSpeed = 5f;

	public float maxSpeed = 20f;

	public float speedInscrease = .25f;

	private float currentSpeed;

	private Vector2 currentDir;

	private bool resetting = false;

	void Start(){

		//initialize starting speed
		currentSpeed = startSpeed;

		//initialize direction
		currentDir = Random.insideUnitCircle;

		networkView.observed = this;
	}//end method


	void Update(){

		//dont move the ball if it's resetting
		if (resetting)
			return;

		//don't move the ball if there's nobody to play with
		if (Network.connections.Length == 0){
			return;
		}//end if

		//move the ball in the direction
		Vector2 moveDir = currentDir * currentSpeed * Time.deltaTime;
		transform.Translate(new Vector3( moveDir.x, 0.0f, moveDir.y ));

	}//end method

	void OnTriggerEnter( Collider other ){
		Debug.Log("in trigger :" + other.gameObject.tag);
		if (other.tag.ToLower() == "wall"){

			currentDir.y *= -1;

		}//end if
		else if (other.tag.ToLower() == "player"){

			currentDir.x *= -1;

		}//end else
		else if (other.tag.ToLower() == "goal"){

			StartCoroutine( ResetBall() );
			other.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);

		}//end else

		//increase speed
		currentSpeed += speedInscrease;

		//clamp speed to maximum
		currentSpeed = Mathf.Clamp(currentSpeed, startSpeed, maxSpeed);

	}//end method

	void OnSerializeNetworkView( BitStream stream, NetworkMessageInfo info ){

		if (stream.isWriting){
			Vector3 pos = transform.position;
			Vector3 dir = currentDir;
			float speed = currentSpeed;
			stream.Serialize(ref pos);
			stream.Serialize(ref dir);
			stream.Serialize(ref speed);
		}//end if
		else{
			Vector3 pos = Vector3.zero;
			Vector3 dir = Vector3.zero;
			float speed = 0f;
			stream.Serialize(ref pos);
			stream.Serialize(ref dir);
			stream.Serialize(ref speed);
			transform.position = pos;
			currentDir = dir;
			currentSpeed = speed;
		}//end else

	}//end method

	IEnumerator ResetBall(){

		resetting = true;
		transform.position = Vector3.zero;

		currentDir = Vector3.zero;
		currentSpeed = 0f;
		yield return new WaitForSeconds (3f);

		Start();
		resetting = false;
	}//end method

}//end class
