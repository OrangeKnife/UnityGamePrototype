using UnityEngine;
using System.Collections;

public class Disappearing : MonoBehaviour {
	public bool DisappearOnStand = true;
	public float Delay = 1.5f;
	public bool WantFading = true;
	public float FadingSpeed = 1f;
	bool bDoDisappearing;

	SpriteRenderer myRenderer;
	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myRenderer && bDoDisappearing && WantFading) {
			Color currentColor = myRenderer.color;
			if(currentColor.a > 0)
				myRenderer.color = new Color(currentColor.r,currentColor.g,currentColor.b,currentColor.a - FadingSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (DisappearOnStand && collider.gameObject.tag == "Player" && collider.transform.position.y > transform.position.y && !bDoDisappearing) {
			Invoke("DoDisappearing", Delay);
			bDoDisappearing = true;
		}
	}

	void DoDisappearing()
	{
		gameObject.SetActive(false);
	}
}
