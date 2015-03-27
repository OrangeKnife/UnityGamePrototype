using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public float MoveSpeed = 0.5f;
	public Vector2 OffsetPointA; // first destination
	public Vector2 OffsetPointB; // second destination
	public bool bMoveOnce = false;

	private float currentTimer;
	private Vector2 tmpOffset;

	// Use this for initialization
	void Start () {
		currentTimer = 0.0f;
		OffsetPointA = (Vector2)transform.position + OffsetPointA;
		OffsetPointB = (Vector2)transform.position + OffsetPointB;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentTimer += Time.deltaTime;
		transform.position = Vector2.Lerp(OffsetPointA, OffsetPointB, currentTimer * MoveSpeed);

		if (!bMoveOnce)
		{
			// move back and forth between pointA and pointB
			if (transform.position.x == OffsetPointB.x && transform.position.y == OffsetPointB.y)
			{
				currentTimer = 0.0f;
				tmpOffset = OffsetPointA;
				OffsetPointA = OffsetPointB;
				OffsetPointB = tmpOffset;
			}
		}
	}

//	var pointB : Vector3;
//	
//	function Start () { var pointA = transform.position; while (true) { yield MoveObject(transform, pointA, pointB, 3.0); yield MoveObject(transform, pointB, pointA, 3.0); } }
//	
//	function MoveObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) { var i = 0.0; var rate = 1.0/time; while (i < 1.0) { i += Time.deltaTime * rate; thisTransform.position = Vector3.Lerp(startPos, endPos, i); yield; } }
}
