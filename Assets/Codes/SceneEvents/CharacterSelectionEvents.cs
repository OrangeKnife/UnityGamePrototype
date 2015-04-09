using UnityEngine;
using System.Collections;

public class CharacterSelectionEvents : MonoBehaviour {

	CharacterSelector selector;
	public GameObject CharUIText,AbilityUIText;
	public GameObject GoButton;
	public GameObject UnlockCharButton,UnlockAbilityButton;

	void Start () {
		selector = GameObject.Find ("CharacterSelector").GetComponent<CharacterSelector>();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick()
	{
		selector.SaveSelection ();
		SceneManager.OpenScene("MainMenu");
	}

	public void OnGoButtonClick()
	{
		selector.SaveSelection ();
		SceneManager.OpenScene("TestSceneBank");
	}



	public void OnSelectedACharacter(CharacterInfo charInfo, bool bUnlocked)
	{
		CharUIText.GetComponent<UnityEngine.UI.Text> ().text = charInfo.CharacterName;
		if (bUnlocked) {
			UnlockCharButton.SetActive (false);
			GoButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
		}
		else {
			UnlockCharButton.SetActive (true);
			GoButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
		}


	}

	public void OnSelectedAnAbility(AbilityInfo abilityInfo, bool bUnlocked)
	{
		AbilityUIText.GetComponent<UnityEngine.UI.Text> ().text = abilityInfo.AbilityName;

		if (bUnlocked) {
			UnlockAbilityButton.SetActive (false);
		}
		else {
			UnlockAbilityButton.SetActive (true);
		}
	}
}
