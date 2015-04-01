using UnityEngine;
using System.Collections;

public class PlayerHud : MonoBehaviour
{
	public bool alwaysShowSlideBar = false ;// = true; 
	public GameObject SlideBarBackground = null;
	public GameObject SlideBar = null;
	public float YOffset = 0.1f;
	SpriteRenderer SliderBarBackgroundSpriteRenderer = null;
	SpriteRenderer SliderBarSpriteRenderer = null;
	GameManager gameMgr;
	GameObject player;

	bool bShowSlideBar;
	float originalSliderBarSpriteRendererlocalScaleX;

	void Start ()
	{
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = gameMgr.GetCurrentPlayer ();
		SliderBarBackgroundSpriteRenderer = SlideBarBackground.GetComponent<SpriteRenderer> ();
		SliderBarSpriteRenderer = SlideBar.GetComponent<SpriteRenderer> ();

		SliderBarBackgroundSpriteRenderer.enabled = false;
		SliderBarSpriteRenderer.enabled = false;

		originalSliderBarSpriteRendererlocalScaleX = SliderBarSpriteRenderer.transform.localScale.x;

	}

	void ResetSliderBarLocation()
	{
		float playerSpriteWidth = player.GetComponent<SpriteRenderer> ().sprite.bounds.size.x * player.transform.localScale.x;
		float playerSpriteHeight = player.GetComponent<SpriteRenderer> ().sprite.bounds.size.y * player.transform.localScale.y;
		SlideBarBackground.transform.position = player.transform.position + new Vector3 (-playerSpriteWidth / 2, playerSpriteHeight / 2 + YOffset, 0);
		SlideBar.transform.position = SlideBarBackground.transform.position + new Vector3 (0.05f, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerController pc = player.GetComponent<PlayerController> ();
		if (pc) {
			bShowSlideBar = pc.bActivateGlide || alwaysShowSlideBar;
			if(bShowSlideBar)
			{
				float sliderPercentLeft = pc.GetCurrentGliderPercentLeft();
				SliderBarSpriteRenderer.transform.localScale =  new Vector3(originalSliderBarSpriteRendererlocalScaleX*sliderPercentLeft,SliderBarSpriteRenderer.transform.localScale.y,SliderBarSpriteRenderer.transform.localScale.z);
				SliderBarBackgroundSpriteRenderer.enabled = true;
				SliderBarSpriteRenderer.enabled = true;
				ResetSliderBarLocation();
			}
			else
			{
				SliderBarBackgroundSpriteRenderer.enabled = false;
				SliderBarSpriteRenderer.enabled = false;
			}
		}
	}

}

