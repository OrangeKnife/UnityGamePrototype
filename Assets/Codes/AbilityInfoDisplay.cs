using System;
using UnityEngine;



public class AbilityInfoDisplay: MonoBehaviour {
	public Vector3 DisplayPosition,DisplayScale;
	public bool bDirty = false;
	public int AbilityInfoIndex = -1;
	void Awake()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	void Update()
	{
		if (bDirty) {
			transform.position = DisplayPosition;
			transform.localScale = DisplayScale;
			bDirty = false;
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


