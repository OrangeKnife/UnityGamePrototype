using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D tmpRigidBody;
	private float jumpForce = 500.0f;
	private float walkForce = 10.0f;

	// Use this for initialization
	void Start () 
	{
		tmpRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal, vertical;

		horizontal = 0;
		vertical = 0;

		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = Input.GetAxisRaw ("Horizontal");
		if ( Input.GetButtonDown ("Jump") )
		{
			vertical = 1.0f;
		}
		else
		{
			vertical = 0.0f;
		}
			

		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				vertical = 1.0f;
			}
			else
			{
				vertical = 0.0f;
			}
		}
		
		#endif

		HandleInput (horizontal * walkForce, vertical * jumpForce);
	}

	void HandleInput(float horizontal, float vertical)
	{
		Vector2 tmpVec;
		tmpVec = new Vector2 (horizontal, vertical);
		tmpRigidBody.AddForce ( tmpVec );
	}
}
