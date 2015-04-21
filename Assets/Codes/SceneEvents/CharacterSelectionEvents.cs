using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class CharacterSelectionEvents : MonoBehaviour {

	CharacterSelector selector;
	public GameObject CharUIText,AbilityUIText;
	public GameObject GoButton;
	public GameObject UnlockCharButton,UnlockAbilityButton,UnlockCharButtonIGP,UnlockAbilityButtonIGP;
	public GameObject UnlockCharButtonText,UnlockAbilityButtonText,UnlockCharButtonIGPText,UnlockAbilityButtonIGPText;
	private bool charUnlocked,abilityUnlocked;

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
		charUnlocked = bUnlocked;
		CharUIText.GetComponent<UnityEngine.UI.Text> ().text = charInfo.CharacterName;
		if (bUnlocked) {
			UnlockCharButton.SetActive (false);
			UnlockCharButtonIGP.SetActive(false);
		}
		else {
			UnlockCharButton.SetActive (true);
			UnlockCharButtonText.GetComponent<UnityEngine.UI.Text>().text = charInfo.Cost_Coin.ToString();

			UnlockCharButtonIGP.SetActive(true);
			VirtualGood vg = StoreInfo.GetItemByItemId(charInfo.CharacterSoomlaId) as VirtualGood;
			UnlockCharButtonIGPText.GetComponent<UnityEngine.UI.Text>().text = ((PurchaseWithMarket)vg.PurchaseType).MarketItem.Price.ToString("0.00");

			
		}
		checkGoButton ();

	}

	public void OnSelectedAnAbility(AbilityInfo abilityInfo, bool bUnlocked)
	{
		abilityUnlocked = bUnlocked;
		AbilityUIText.GetComponent<UnityEngine.UI.Text> ().text = abilityInfo.AbilityName;

		if (bUnlocked) {
			UnlockAbilityButton.SetActive (false);
			UnlockAbilityButtonIGP.SetActive(false);
		}
		else {
			UnlockAbilityButton.SetActive (true);
			UnlockAbilityButtonText.GetComponent<UnityEngine.UI.Text>().text = abilityInfo.Cost_Coin.ToString();

			//UnlockAbilityButton.SetActive(true);
			//VirtualGood vg = StoreInfo.GetItemByItemId(abilityInfo.AbilitySoomlaId) as VirtualGood;
			//UnlockAbilityButtonText.GetComponent<UnityEngine.UI.Text>().text = ((PurchaseWithMarket)vg.PurchaseType).MarketItem.Price.ToString("0.00");

		}

		checkGoButton ();
	}

	void checkGoButton()
	{
		GoButton.GetComponent<UnityEngine.UI.Button> ().interactable = charUnlocked && abilityUnlocked;
	}
}
