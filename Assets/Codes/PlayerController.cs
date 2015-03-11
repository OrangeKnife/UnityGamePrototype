using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float threshold;
	private bool isGrounded;
	private bool bActivateGlide;
	private int GlideCount;

	private Rigidbody2D tmpRigidBody;
	private float jumpForce = 500.0f;
	private float moveSpeed = 5.0f;

	private float oriGravity;

	// Use this for initialization
	void Start () 
	{
		tmpRigidBody = GetComponent<Rigidbody2D> ();
		oriGravity = tmpRigidBody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal;
		bool ButtonJumpDown, ButtonJumpHold, ButtonJumpUp;

		horizontal = 0;

		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = Input.GetAxisRaw ("Horizontal");
		if ( Input.GetButton ("Jump") )
		{
			ButtonJumpHold = true;
		}
		else
		{
			ButtonJumpHold = false;
		}

		if ( Input.GetButtonDown ("Jump") )
		{
			ButtonJumpDown = true;
		}
		else
		{
			ButtonJumpDown = false;
		}

		if ( Input.GetButtonUp ("Jump") )
		{
			ButtonJumpUp = true;
		}
		else
		{
			ButtonJumpUp = false;
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

		HandleInput (horizontal, ButtonJumpDown, ButtonJumpHold, ButtonJumpUp);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ground") 
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ground") 
		{
			isGrounded = false;
		}
	}

	void HandleInput(float horizontal, bool bButtonJumpDown, bool bButtonJumpHold, bool bButtonJumpUp)
	{
		float tmpForce;
		Vector2 tmpVec;

		tmpRigidBody.velocity = new Vector2 (horizontal * moveSpeed, tmpRigidBody.velocity.y);

		if (!isGrounded) 
		{
			// in air
			if (bButtonJumpDown)
			{
				// tap jump button
				bActivateGlide = true;
				GlideCount++;
			}

			if (bButtonJumpUp)
			{
				// release jump button
				bActivateGlide = false;
			}

			if (bActivateGlide)
			{
				tmpRigidBody.velocity = new Vector2(tmpRigidBody.velocity.x, 0.0f);
				tmpRigidBody.gravityScale = 0.0f;
			}
			else
			{
				tmpRigidBody.gravityScale = oriGravity;
			}
		} 
		else 
		{
			// on ground
			bActivateGlide = false;
			GlideCount = 0;

			tmpForce = (bButtonJumpDown ? 1.0f : 0.0f) * jumpForce;
			
			if (tmpForce != 0) 
			{
				tmpVec = new Vector2 (0.0f, tmpForce);
				tmpRigidBody.AddForce (tmpVec);
				isGrounded = false;
			}
		}
	}
}
