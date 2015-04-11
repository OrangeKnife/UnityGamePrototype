using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	public float MoveSpeed = 0.5f;
	public Vector2 OffsetPointA; // first destination
	public Vector2 OffsetPointB; // second destination
	public bool bMoveOnce = false;
	public bool spin = false;
	public bool spinInOneDirection = false;
	public bool scale = false;
	public bool scaleLoop = false;
	public Vector3 scaleA = new Vector3 (1, 1, 1);
	public Vector3 scaleB = new Vector3 (1, 1, 1);
	public float scaleSpeed = 0.2f;
	public Vector3 spinRotationVector = new Vector3(0,0,5);

	private float currentTimer,scaleTimer;
	private Vector2 tmpOffset;

	bool direction_AB = true;

	// Use this for initialization
	void Start () {
		currentTimer = 0.0f;
		scaleTimer = 0.0f;
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

				direction_AB = !direction_AB;
			}
		}

		if (spin)
		{
			if (spinInOneDirection)
				transform.Rotate (spinRotationVector);
			else
				transform.Rotate (spinRotationVector * (direction_AB?1:-1));
		}

		if (scale) {
			scaleTimer += Time.deltaTime;
			transform.localScale = Vector3.Lerp (scaleA, scaleB,  scaleTimer * scaleSpeed);

			if(scaleLoop && transform.localScale == scaleB)
			{
				scaleTimer = 0.0f;
				Vector3 tempVec3 = scaleA;
				scaleA = scaleB;
				scaleB = tempVec3;
			}
		}
	}

//	var pointB : Vector3;
//	
//	function Start () { var pointA = transform.position; while (true) { yield MoveObject(transform, pointA, pointB, 3.0); yield MoveObject(transform, pointB, pointA, 3.0); } }
//	
//	function MoveObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) { var i = 0.0; var rate = 1.0/time; while (i < 1.0) { i += Time.deltaTime * rate; thisTransform.position = Vector3.Lerp(startPos, endPos, i); yield; } }
}
