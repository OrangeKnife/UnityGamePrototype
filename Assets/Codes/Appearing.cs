using UnityEngine;
using System.Collections;

public class Appearing : MonoBehaviour {
	public bool AppearOnEnter = true;
	public float Delay = 1.5f;
	public bool WantFading = true;
	public float FadingSpeed = 1f;
	bool bDoAppearing;
	
	SpriteRenderer myRenderer;
	BoxCollider2D box;
	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		Color currentColor = myRenderer.color;
		myRenderer.color = new Color (currentColor.r, currentColor.g, currentColor.b, 0);

	}
	
	// Update is called once per frame
	void Update () {
		if (myRenderer && bDoAppearing && WantFading) {
			Color currentColor = myRenderer.color;
			if(currentColor.a < 1)
				myRenderer.color = new Color(currentColor.r,currentColor.g,currentColor.b,currentColor.a + FadingSpeed * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (AppearOnEnter && collider.gameObject.tag == "Player"  && !bDoAppearing) {
			Invoke("DoAppearing", Delay);
			bDoAppearing = true;
		}
	}
	
	void DoAppearing()
	{
		gameObject.layer = 9;//9 == level
		GetComponent<BoxCollider2D> ().isTrigger = false;
	}
}
