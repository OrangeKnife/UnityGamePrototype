using System;
using UnityEngine;



public class Character: MonoBehaviour {
	public CharacterInfo CharacterInfo; 
	public Vector3 DisplayPosition,DisplayScale;
	public bool bDirty = false;
	void Start()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	void Update()
	{
		if (bDirty) {
			transform.position = DisplayPosition;
			transform.localScale = DisplayScale;
		}

	}

	public void SetPosition(float x, float y, float scaleX, float scaleY, bool wantDirty = true, bool wantSpriteRendering = true)
	{
		DisplayPosition.x = x;
		DisplayPosition.y = y;
		DisplayScale.x = scaleX;
		DisplayScale.y = scaleY;
		bDirty = wantDirty;
		GetComponent<SpriteRenderer> ().enabled = wantSpriteRendering;
	}
	
}


