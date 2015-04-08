using UnityEngine;
using System.Collections;

public class RandomOffsetObject : MonoBehaviour {

	public float RandomOffsetMinX;
	public float RandomOffsetMaxX;
	public float RandomOffsetMinY;
	public float RandomOffsetMaxY;

	// Use this for initialization
	void Awake () 
	{
		transform.position = (Vector2)transform.position + new Vector2( Random.Range(RandomOffsetMinX, RandomOffsetMaxX), Random.Range(RandomOffsetMinY, RandomOffsetMaxY) );
	}
}
