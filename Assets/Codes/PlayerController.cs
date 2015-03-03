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

		horizontal = Input.GetAxisRaw ("Horizontal");
		//vertical = Input.GetAxisRaw ("Vertical");
		vertical = (Input.GetButtonDown ("Jump") ? 1.0f : 0.0f );

		HandleInput (horizontal * walkForce, vertical * jumpForce);
	}

	void HandleInput(float horizontal, float vertical)
	{
		Vector2 tmpVec;
		tmpVec = new Vector2 (horizontal, vertical);
		tmpRigidBody.AddForce ( tmpVec );
	}
}
