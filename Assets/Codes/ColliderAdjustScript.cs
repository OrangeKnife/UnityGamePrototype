using UnityEngine;
using System.Collections;

public class ColliderAdjustScript : MonoBehaviour {
	


	// Use this for initialization
	void Start () 
	{
		GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x / transform.localScale.x, GetComponent<SpriteRenderer>().bounds.size.y / transform.localScale.y);
		GetComponent<BoxCollider2D>().offset = GetComponent<SpriteRenderer>().bounds.center;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
