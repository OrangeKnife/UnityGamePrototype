using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float threshold;
	private bool bActivateGlide;
	private int MaxGlideAllow = 2;
	private int GlideCount;
	private float MaxGlideTime = 3.0f;
	private float GlideSpeedModifier = 2.3f; // increased speed when gliding

	private ParticleSystem tmpJetParticle;
	private ConstantForce2D tmpGravityForce;
	private Rigidbody2D tmpRigidBody;

	private float jumpForce = 1500.0f;
	private float moveSpeed = 10.0f;
	private float PlayerGravity = 80.0f;

	private float distToGround = 0.0f;

	private Animator animator;
	private bool isDead;
	private bool isPlayDying;

	private float DeadBounceForce = 500.0f;
	private Vector3 DeadBounceVelocity;

//	public GameObject groundObj;
//	private GameObject currentGround;



	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();

		tmpRigidBody = GetComponent<Rigidbody2D> ();
		tmpRigidBody.gravityScale = 0.0f;

		tmpJetParticle = GetComponentInChildren<ParticleSystem>();
		tmpGravityForce = GetComponent<ConstantForce2D>();
		SetGravity(-PlayerGravity);

		distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;

//		print ("test start");
//		currentGround = groundObj;
//		//GameObject clone;
//		//clone = (GameObject)Instantiate(tmpRigidBody, transform.position, transform.rotation);
//		groundObj.transform.position = transform.position;
	}

	public void setMoveSpeed(float speed) {
		moveSpeed = speed;
	}
	public float getMoveSpeed() {
		return moveSpeed;
	}
	
	public void setPlayerGravity(float gravity) {
		PlayerGravity = gravity;
	}
	public float getPlayerGravity() {
		return PlayerGravity;
	}

	void SetGravity(float val)
	{
		tmpGravityForce.force = new Vector2(0, val);
	}

	void UpdatePlayer()
	{
		///// force no tilt/spinning cause by rigid body
		transform.rotation = Quaternion.identity;

		///// if your face bump into something, you die
		Debug.DrawRay(transform.position, transform.right, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		if ( hit.collider != null )
		{
			isDead = true;
			//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, transform.right);
			DeadBounceVelocity = tmpRigidBody.velocity;
			DeadBounceVelocity.x = -1;
		}

		///// also your back
		Debug.DrawRay(transform.position, -transform.right, Color.green);
		hit = Physics2D.Raycast(transform.position, -transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		if ( hit.collider != null )
		{
			isDead = true;
			//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, transform.right);
			DeadBounceVelocity = tmpRigidBody.velocity;
			DeadBounceVelocity.x = 1;
		}

		///// also your head
		Debug.DrawRay(transform.position, transform.up, Color.green);
		hit = Physics2D.Raycast(transform.position, transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		if ( hit.collider != null )
		{
			isDead = true;
			//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, -transform.up);
			DeadBounceVelocity = tmpRigidBody.velocity;
			DeadBounceVelocity.y = -1;
		}

		///// don't touch kill volume
		Debug.DrawRay(transform.position, -transform.up, Color.green);
		hit = Physics2D.Raycast(transform.position, -transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		if ( hit.collider != null )
		{
			if (hit.collider.gameObject.tag == "KillVolume")
			{
				isDead = true;
				//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, -transform.up);
				DeadBounceVelocity = tmpRigidBody.velocity;
				DeadBounceVelocity.y = -1;
			}
		}
	}

	void UpdateAnimator()
	{
		animator.SetBool("IsOnGround", IsGrounded());
		animator.SetBool("IsDead", isDead);
	}

	void Died()
	{
		isPlayDying = true;

		///// make player bounce
		tmpRigidBody.velocity = new Vector2(0,0);
		//tmpRigidBody.velocity = DeadBounceVelocity;
		tmpRigidBody.AddForce( DeadBounceVelocity.normalized * DeadBounceForce );
		SetGravity(-PlayerGravity);

		bActivateGlide = false;
		tmpJetParticle.Stop();

		StartCoroutine(WaitForRespawn());

	}

	void Respawn()
	{
		GameObject tmp = GameObject.Find("GameManager");
		tmp.GetComponent<GameManager>().RespawnPlayer();

		//Destroy(this.gameObject);

		transform.position = new Vector3(-10.0f, 5.0f, 0.0f);
		transform.rotation = Quaternion.identity;
		
		tmpRigidBody.angularVelocity = 0.0f;
		isDead = false;
		isPlayDying = false;

		bActivateGlide = false;
		GlideCount = 0;
		
		animator.SetTrigger("Respawn");

		GameObject tmpPlayer = GameObject.FindGameObjectWithTag("Player");
		tmpPlayer.GetComponent<PlayerManager>().setPlayerScore(0);
	}

	// Update is called once per frame
	void Update () 
	{
		float horizontal;
		bool ButtonJumpDown, ButtonJumpHold, ButtonJumpUp;

		UpdatePlayer();
		UpdateAnimator();

		if (isDead && !isPlayDying)
		{
			Died();
		}

		if (isDead)
		{
			return;
		}

		horizontal = 0;
		ButtonJumpDown = false;
		ButtonJumpHold = false;
		ButtonJumpUp = false;



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
				ButtonJumpDown = true;
			}
			else
			{
				ButtonJumpDown = false;
			}

			if (touch.phase == TouchPhase.Ended)
			{
				ButtonJumpUp = true;
			}
			else
			{
				ButtonJumpUp = false;
			}

			if (touch.phase == TouchPhase.Moved)
			{
				ButtonJumpHold = true;
			}
			else
			{
				ButtonJumpHold = false;
			}
		}
		
		#endif

		horizontal = 1.0f; ///// force player to move forward only
		HandleInput (horizontal, ButtonJumpDown, ButtonJumpHold, ButtonJumpUp);
	}

	bool IsGrounded()
	{
		//Debug.DrawRay(transform.position, -transform.up, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		if ( hit.collider != null )
		{
			if (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Wall")
				return true;
		}
		return false;
	}

	void OnOutSection() 
	{
			
	}

	void HandleInput(float horizontal, bool bButtonJumpDown, bool bButtonJumpHold, bool bButtonJumpUp)
	{
		float tmpForce;
		Vector2 tmpVec;

		tmpRigidBody.velocity = new Vector2 (horizontal * moveSpeed * (bActivateGlide?GlideSpeedModifier:1.0f), tmpRigidBody.velocity.y);

//		print (transform.position.x + "," + groundObj.transform.position.x);
//		if(transform.position.x >= currentGround.transform.position.x)
//		{
//
//			//print (transform.position.x + "," + groundObj.transform.position.x);
//			//float addX = groundObj.transform.localScale.x;
//			//currentGround.transform.position.x+=addX;
//			//groundObj.transform.position.x+=addX;
//			//print("bound: "+groundObj.mesh.bounds.size.x);
//			//float fx = groundObj.GetComponent(MeshFilter).mesh.bounds.extents.x; 
//			Vector3 addX = new Vector3(groundObj.transform.Find ("Platform").localScale.x,0,0);
//			//Instantiate(groundObj, groundObj.transform.position+addX, groundObj.transform.rotation);
//			currentGround.transform.position += addX;
//		}

		if (!IsGrounded()) 
		{
			// in air
			if (bButtonJumpDown)
			{
				// tap jump button
				if (GlideCount < MaxGlideAllow)
				{
					bActivateGlide = true;
					GlideCount++;

					tmpJetParticle.Play();
				}
			}

			if (bButtonJumpUp)
			{
				// release jump button
				bActivateGlide = false;

				tmpJetParticle.Stop();
			}

			if (bActivateGlide)
			{
				tmpRigidBody.velocity = new Vector2(tmpRigidBody.velocity.x, 0.0f);
				SetGravity(0.0f);
			}
			else
			{
				SetGravity(-PlayerGravity);
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
			}
		}
	}

	IEnumerator WaitForRespawn()
	{
		yield return new WaitForSeconds(3.0f);

		Respawn();
	}
}
