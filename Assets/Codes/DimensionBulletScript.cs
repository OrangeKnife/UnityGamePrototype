using UnityEngine;
using System.Collections;

public class DimensionBulletScript : MonoBehaviour {

	public Vector2 velocity;
	private Rigidbody2D tmpRigid;

	// Use this for initialization
	void Start () {
		tmpRigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		tmpRigid.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Ground")
		{
			coll.gameObject.AddComponent<FadeScript>();
			coll.gameObject.GetComponent<BoxCollider2D>().enabled = false;

			Destroy(gameObject);
		}
	}
}
