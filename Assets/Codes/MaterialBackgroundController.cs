using UnityEngine;
using System.Collections;

public class MaterialBackgroundController : MonoBehaviour {

	private SpriteRenderer tmpSpriteRenderer;
	private GameObject player;
	private Transform playerTransform;

	private float scrollSpeed = 0.1f;
	private Vector2 savedOffset;

	private float currentOffset;
	
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		tmpSpriteRenderer = GetComponent<SpriteRenderer>();

		savedOffset = tmpSpriteRenderer.material.mainTextureOffset;
	}
	
	void Update () 
	{
		currentOffset = currentOffset + (Time.deltaTime * scrollSpeed);
		//float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		float x = Mathf.Repeat (currentOffset, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		tmpSpriteRenderer.material.mainTextureOffset = offset;

		transform.position = new Vector3( playerTransform.position.x, 5, 0 );
	}
	
	void OnDisable () 
	{
		tmpSpriteRenderer.material.mainTextureOffset = savedOffset;
	}

	public void SetScrollingSpeed(float speed)
	{
		scrollSpeed = speed;
	}
}
