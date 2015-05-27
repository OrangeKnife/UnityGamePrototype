using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float threshold;
	public bool bActivateGlide { get; private set;}
	private float ActivateGlideStartTime = 0;
	private int MaxGlideAllow = 2;
	private int GlideCount;
	private float MaxGlideTime = 2.0f;
	private float GlideSpeedModifier = 2.3f; // increased speed when gliding

	private MaterialBackgroundController tmpBackground;
	private ParticleSystem tmpJetParticle;
	private ConstantForce2D tmpGravityForce;
	private Rigidbody2D tmpRigidBody;
	private BoxCollider2D tmpBoxCollider2D;
	private AbilityManager tmpAbilityManager;

	private float backgroundMoveSpeed = 8.0f;
	private float jumpForce = 1500.0f;
	private float moveSpeed = 6.0f;
	private float moveSpeedMultPerDifficulty = 0.05f;
	private float PlayerGravity = 80.0f;

	private float distToGround = 0.0f;

	private Animator animator;
	private bool isDead;
	private bool isPlayDying;
	private bool CheckDeadRaycast = true;

	private float DeadBounceForce = 500.0f;
	private Vector3 DeadBounceVelocity;

//	public GameObject groundObj;
//	private GameObject currentGround;


	GameSceneEvents eventHandler;
	PlayerManager playerMgr;
	GameManager gameMgr;
	LevelGenerator tmpLevelGen;

	bool freezed;

	void OnGUI () {
//		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
//		myStyle.alignment = TextAnchor.MiddleRight;
//		myStyle.fontSize = 25;
//		GUI.TextField (new Rect (10, 50, 100, 30), GetCurrentGlideTime().ToString("F2"), myStyle );
	}

	// Use this for initialization
	void Start () 
	{
		tmpBackground = GameObject.Find("ScrollingBackground").GetComponent<MaterialBackgroundController>();
		animator = GetComponent<Animator>();

		tmpBoxCollider2D = GetComponent<BoxCollider2D>();

		tmpRigidBody = GetComponent<Rigidbody2D> ();
		tmpRigidBody.gravityScale = 0.0f;

		tmpJetParticle = GetComponentInChildren<ParticleSystem>();
		tmpGravityForce = GetComponent<ConstantForce2D>();
		SetGravity(-PlayerGravity);

		distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
		//distToGround += 0.5f;

//		print ("test start");
//		currentGround = groundObj;
//		//GameObject clone;
//		//clone = (GameObject)Instantiate(tmpRigidBody, transform.position, transform.rotation);
//		groundObj.transform.position = transform.position;

		eventHandler = GameObject.Find("eventHandler").GetComponent<GameSceneEvents>();
		playerMgr = gameObject.GetComponent<PlayerManager>();
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		tmpAbilityManager = GetComponent<AbilityManager>();
		tmpLevelGen = gameMgr.GetComponent<LevelGenerator>();

		//AbilityManager abManager = gameMgr.GetCurrentPlayer().GetComponent<AbilityManager>();
//		abManager.addAbility ("abi1");
//		abManager.addAbility ("abi2");



	}

	public void SetCheckDeadRaycast(bool val)
	{
		CheckDeadRaycast = val;
	}

	public void setMaxGlideTime(float val) {
		MaxGlideTime = val;
	}
	public float getMaxGlideTime() {
		return MaxGlideTime;
	}

	public void setMaxGlideAllow(int val) {
		MaxGlideAllow = val;
	}
	public int getMaxGlideAllow() {
		return MaxGlideAllow;
	}

	public void setMoveSpeed(float speed) {
		moveSpeed = speed;
	}
	public float getMoveSpeed() {
		return moveSpeed;
	}

	public void setJumpForce(float val) {
		jumpForce = val;
	}
	public float getJumpForce() {
		return jumpForce;
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

	void UpdateBackground()
	{
		tmpBackground.SetScrollingSpeed(tmpRigidBody.velocity.x / backgroundMoveSpeed / 10.0f);
	}

	void UpdatePlayer()
	{
		Vector2 tmpPos;

		///// force no tilt/spinning cause by rigid body
		transform.rotation = Quaternion.identity;

		if (CheckDeadRaycast)
		{
			///// if your face bump into something, you die
			Debug.DrawRay(transform.position, transform.right, Color.green);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
			if (hit.collider == null)
			{
				tmpPos = transform.position;
				tmpPos.y += tmpBoxCollider2D.offset.y * transform.localScale.y;
				tmpPos.y -= tmpBoxCollider2D.bounds.extents.y;
				Debug.DrawRay(tmpPos, transform.right, Color.blue);
				hit = Physics2D.Raycast(tmpPos, transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
			}
			
			if (hit.collider == null)
			{
				tmpPos = transform.position;
				tmpPos.y += tmpBoxCollider2D.offset.y * transform.localScale.y;
				tmpPos.y += tmpBoxCollider2D.bounds.extents.y;
				Debug.DrawRay(tmpPos, transform.right, Color.blue);
				hit = Physics2D.Raycast(tmpPos, transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
			}

			if ( hit.collider != null )
			{
				if (hit.collider.gameObject.tag != "Ground")
				{
					isDead = true;
					//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, transform.right);
					DeadBounceVelocity = tmpRigidBody.velocity;
					DeadBounceVelocity.x = -1;
				}
			}

	//		///// also your back
	//		Debug.DrawRay(transform.position, -transform.right, Color.green);
	//		hit = Physics2D.Raycast(transform.position, -transform.right, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
	//		if ( hit.collider != null )
	//		{
	//			isDead = true;
	//			//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, transform.right);
	//			DeadBounceVelocity = tmpRigidBody.velocity;
	//			DeadBounceVelocity.x = 1;
	//		}

	//		///// also your head
	//		Debug.DrawRay(transform.position, transform.up, Color.green);
	//		hit = Physics2D.Raycast(transform.position, transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
	//		if ( hit.collider != null )
	//		{
	//			isDead = true;
	//			//DeadBounceVelocity = Vector3.Reflect(tmpRigidBody.velocity, -transform.up);
	//			DeadBounceVelocity = tmpRigidBody.velocity;
	//			DeadBounceVelocity.y = -1;
	//		}

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
					DeadBounceVelocity.y = 1;
				}
			}

			///// don't touch kill volume on your head too
			Debug.DrawRay(transform.position, transform.up, Color.green);
			hit = Physics2D.Raycast(transform.position, transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
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
	}

	void UpdateAnimator()
	{
		animator.SetBool("IsOnGround", IsGrounded());
		animator.SetBool("IsDead", isDead);
		animator.SetBool("IsGliding", bActivateGlide);
		animator.SetBool("IsFalling", tmpRigidBody.velocity.y < 0);
	}

	void Died()
	{
		isPlayDying = true;

		///// make player bounce
		tmpRigidBody.velocity = new Vector2(0,0);
		//tmpRigidBody.velocity = DeadBounceVelocity;
		tmpRigidBody.AddForce( DeadBounceVelocity.normalized * DeadBounceForce );
		SetGravity(-PlayerGravity);

		SetGlide(false, true);
		gameMgr.playSound ("crash",true);

		//StartCoroutine(WaitForRespawn());
		eventHandler.onPlayerDead();
		tmpAbilityManager.ForceStopActiveAbility();

		playerMgr.checkHighScore ();
	}

	public void Respawn()
	{
		playerMgr.setPlayerScore(0);
		gameMgr.RespawnPlayer();

		//Destroy(this.gameObject);

//		transform.position = new Vector3(-10.0f, 5.0f, 0.0f);
//		transform.rotation = Quaternion.identity;
//		
//		tmpRigidBody.angularVelocity = 0.0f;
//		isDead = false;
//		isPlayDying = false;
//
//		SetGlide(false, true);
//		animator.SetTrigger("Respawn");
//
//
	}

	// Update is called once per frame
	void Update () 
	{
		float horizontal;
		bool ButtonJumpDown, ButtonJumpHold, ButtonJumpUp;
		bool ButtonAbilityDown, ButtonAbilityHold, ButtonAbilityUp;

		UpdateBackground();
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

		ButtonAbilityDown = false;
		ButtonAbilityHold = false;
		ButtonAbilityUp = false;



		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = Input.GetAxisRaw ("Horizontal");

		// jump
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

		// active ability
		if ( Input.GetButton ("Fire1") )
		{
			ButtonAbilityHold = true;
		}
		else
		{
			ButtonAbilityHold = false;
		}
		
		if ( Input.GetButtonDown ("Fire1") )
		{
			ButtonAbilityDown = true;
		}
		else
		{
			ButtonAbilityDown = false;
		}
		
		if ( Input.GetButtonUp ("Fire1") )
		{
			ButtonAbilityUp = true;
		}
		else
		{
			ButtonAbilityUp = false;
		}
			

		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		if (Input.touchCount > 0) 
		{

			for (int i = 0; i < Input.touchCount; ++i)
			{
				Touch touch = Input.GetTouch(i);

				if (touch.position.x >= Screen.width / 2)
				{
					// jump
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
				else
				{
					// ability
					if (touch.phase == TouchPhase.Began)
					{
						ButtonAbilityDown = true;
					}
					else
					{
						ButtonAbilityDown = false;
					}
					
					if (touch.phase == TouchPhase.Ended)
					{
						ButtonAbilityUp = true;
					}
					else
					{
						ButtonAbilityUp = false;
					}
					
					if (touch.phase == TouchPhase.Moved)
					{
						ButtonAbilityHold = true;
					}
					else
					{
						ButtonAbilityHold = false;
					}
				}
			}
		}
		
		#endif

		horizontal = 1.0f; ///// force player to move forward only
		HandleInput (horizontal, ButtonJumpDown, ButtonJumpHold, ButtonJumpUp, ButtonAbilityDown, ButtonAbilityHold, ButtonAbilityUp);
	}

	public bool IsGrounded()
	{
		//Debug.DrawRay(transform.position, -transform.up, Color.green);
		Vector2 tmpPos;
		tmpPos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(tmpPos, -transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));

		if (hit.collider == null)
		{
			tmpPos = transform.position;
			tmpPos.x -= tmpBoxCollider2D.bounds.extents.x;
			Debug.DrawRay(tmpPos, -transform.up, Color.blue);
			hit = Physics2D.Raycast(tmpPos, -transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		}

		if (hit.collider == null)
		{
			tmpPos = transform.position;
			tmpPos.x += tmpBoxCollider2D.bounds.extents.x;
			Debug.DrawRay(tmpPos, -transform.up, Color.blue);
			hit = Physics2D.Raycast(tmpPos, -transform.up, distToGround + 0.1f, 1 << LayerMask.NameToLayer("Level"));
		}

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

	void SetGlide(bool bActivate, bool resetGlideCount = false)
	{
		if (bActivate)
		{
			ActivateGlideStartTime = Time.time;
			bActivateGlide = true;
			GlideCount++;

			tmpJetParticle.Play();
		}
		else
		{
			bActivateGlide = false;
			tmpJetParticle.Stop();

			if (resetGlideCount)
				GlideCount = 0;
		}
	}

	public float GetCurrentGliderPercentLeft()
	{
		return 1 - GetCurrentGlideTime() / MaxGlideTime;
	}

	float GetCurrentGlideTime()
	{
		if (bActivateGlide)
			return Time.time - ActivateGlideStartTime;
		else
			return MaxGlideTime;
	}

	void HandleInput(float horizontal, bool bButtonJumpDown, bool bButtonJumpHold, bool bButtonJumpUp, bool bButtonAbilityDown, bool bButtonAbilityHold, bool bButtonAbilityUp)
	{
		float tmpForce;
		Vector2 tmpVec;

		if (freezed)
			tmpRigidBody.velocity = new Vector2 ();
		else
			tmpRigidBody.velocity = new Vector2 (horizontal * moveSpeed * (bActivateGlide?GlideSpeedModifier:1.0f) * (1+(tmpLevelGen.GetDifficulty()*moveSpeedMultPerDifficulty)), tmpRigidBody.velocity.y);

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

		if (bButtonAbilityDown)
		{
			tmpAbilityManager.ActivateAbility();
		}

		if (bButtonAbilityUp)
		{
			tmpAbilityManager.DeactivateAbility();
		}

		if (!IsGrounded()) 
		{
			// in air
			if (bButtonJumpDown)
			{
				// tap jump button
				if (GlideCount < MaxGlideAllow)
				{
					SetGlide(true);
					gameMgr.playSound("dash", false);
				}
			}

			if (bButtonJumpUp)
			{
				// release jump button
				SetGlide(false);
			}

			///// force stop gliding
			if (GetCurrentGlideTime() > MaxGlideTime)
			{
				SetGlide(false);
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

			if(freezed)
				SetGravity(0f);
		} 
		else 
		{
			// on ground
			SetGlide(false, true);

			tmpForce = (bButtonJumpDown ? 1.0f : 0.0f) * jumpForce;
			
			if (tmpForce != 0) 
			{
				tmpVec = new Vector2 (0.0f, tmpForce);
				tmpRigidBody.AddForce (tmpVec);
				gameMgr.playSound("jump");
			}
		}
	}

	IEnumerator WaitForRespawn()
	{
		yield return new WaitForSeconds(3.0f);

		Respawn();
	}

	public void freeze()
	{
		freezed = true;
	}

	public void unFreeze()
	{
		freezed = false;
	}
}
