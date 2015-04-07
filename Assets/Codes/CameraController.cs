using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	//public float smooth = 5;
	public Vector3 CamOffset;
	public bool interpolation;
	public float interpSpeed;

	private GameObject player;
	private Transform playerTransform;

	GameManager gameMgr;

	// Use this for initialization
	void Start () 
	{
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player == null)
		{
			//player = GameObject.FindGameObjectWithTag ("Player");
			player = gameMgr.GetCurrentPlayer();
			playerTransform = player.transform;
		}

		if(interpolation)
			transform.position = Vector3.Lerp(transform.position,new Vector3( playerTransform.position.x, playerTransform.position.y, -10 ) + CamOffset, interpSpeed);
		else
			transform.position = new Vector3( playerTransform.position.x, playerTransform.position.y, -10 ) + CamOffset;

//		transform.position = new Vector3( Mathf.Lerp(transform.position.x,target.position.x,Time.deltaTime*smooth), 
//		                                 Mathf.Lerp(transform.position.y,target.position.y,Time.deltaTime*smooth), 
//		                                 -10 );

//		Camera.main.transform.position.x = Mathf.Lerp(Camera.main.transform.position.x,target.position.x,Time.deltaTime*smooth); 
//		Camera.main.transform.position.y = Mathf.Lerp(Camera.main.transform.position.y,target.position.y,Time.deltaTime*smooth);
	}
}
