using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	Renderer tmpRenderer;
	Color currentColor;
	float OriAlpha;

	private float FadingLerpSpeed = 6.0f;
	public float MinAlpha = 0.2f;
	public bool bIsFadeOut = true;

	// Use this for initialization
	void Start () {
		tmpRenderer = GetComponent<Renderer>();
		currentColor = tmpRenderer.material.color;
		OriAlpha = currentColor.a;
	}
	
	// Update is called once per frame
	void Update () {
		if (bIsFadeOut)
		{
			// fade out
			currentColor.a = Mathf.Lerp(currentColor.a, MinAlpha, Time.deltaTime*FadingLerpSpeed);
			tmpRenderer.material.color = currentColor;
		}
		else
		{
			// fade in
			currentColor.a = Mathf.Lerp(currentColor.a, OriAlpha, Time.deltaTime*FadingLerpSpeed);
			tmpRenderer.material.color = currentColor;
		}
	}
}
