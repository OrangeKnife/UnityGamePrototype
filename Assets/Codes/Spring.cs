using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {
	private PlayerController _player;

	void OnTriggerEnter2D(Collider2D collider)
	{
		/*if (AppearOnEnter && collider.gameObject.tag == "Player"  && !bDoAppearing) {
			Invoke("DoAppearing", Delay);
			bDoAppearing = true;
		}*/
		_player = GetComponent<PlayerController>();

		bool ButtonJumpHold = false;
		if ( Input.GetButton ("Jump") )
		{
			ButtonJumpHold = true;
		}
		//print (_player.getJumpForce ());
		float springForce = 1500 * 1.5f;
		Vector2 tmpVec = new Vector2 (0.0f, springForce);
		collider.attachedRigidbody.velocity = new Vector2 (0, 0);
		collider.attachedRigidbody.AddForce (tmpVec);
			
	}
}
