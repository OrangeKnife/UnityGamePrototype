using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public bool teleToLoc,teleToObj,delayBeforeTeleporting,delayAfterTeleporting;
	public Vector3 teleLoc;
	public GameObject teleObj;
	public float delayBeforeTeleportingTime,delayAfterTeleportingTime;
	public bool interpolatePlayer;
	public float interpolationSpeed;

	float savedSpeed;
	PlayerController playerCtrl;
	bool startInterpolation;
	Vector3 teleportToLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (interpolatePlayer && startInterpolation && playerCtrl) {
			GameObject playerObj = playerCtrl.gameObject;
			playerObj.transform.position = Vector3.Lerp(playerObj.transform.position,teleportToLocation,interpolationSpeed);

			//if((playerObj.transform.position - teleportToLocation).sqrMagnitude < 0.01f || playerObj.transform.position.x > teleportToLocation.x)
			if((playerObj.transform.position - teleportToLocation).sqrMagnitude < 0.01f)
			{
				startInterpolation = false;
				if (delayAfterTeleporting)
					Invoke ("unFreezePlayer", delayAfterTeleportingTime);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player") {
			playerCtrl = collider.gameObject.GetComponent<PlayerController> ();
			if (playerCtrl)
			{
				freezePlayer();
				if (delayBeforeTeleporting)
					Invoke ("doTeleporting", delayBeforeTeleportingTime);
				else
					doTeleporting();
			}
		}
	
	}

	void doTeleporting()
	{
		

		if(teleToLoc)
			teleportToLocation = teleLoc;
		else if (teleToObj)
			teleportToLocation = teleObj.transform.position;

		if (interpolatePlayer)
			startInterpolation = true;
		else {
			playerCtrl.gameObject.transform.position = teleportToLocation;
			if (delayAfterTeleporting)
				Invoke ("unFreezePlayer", delayAfterTeleportingTime);
			else
				unFreezePlayer();
		}
		
		 


	}

	void freezePlayer()
	{
		playerCtrl.freeze ();
		//playerCtrl.GetComponent<BoxCollider2D>().enabled = false;
	}

	void unFreezePlayer()
	{
		playerCtrl.unFreeze ();
		//playerCtrl.GetComponent<BoxCollider2D>().enabled = true;
	}
}
