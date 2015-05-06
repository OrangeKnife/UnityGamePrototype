using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;
using Soomla.Store;

[Serializable]
public struct CharacterInfo
{
	public Sprite CharacterSprite;
	public int CharacterId;
	public int CharacterBuildInAbilityInfoIndex;
	public string CharacterName;
	public string CharacterDescription;
	public string CharacterSoomlaId;
	public int Cost_Coin;

};

[Serializable]
public struct AbilityInfo
{
	public Sprite AbilitySprite;
	public int AbilityId;
	public string AbilityName;
	public string AbilityDescription;
	public string AbilitySoomlaId;
	public int Cost_Coin;
	public bool canSelect;
};

public class CharacterSelector : MonoBehaviour
{


	public List<CharacterInfo> CharacterInfoList = new List<CharacterInfo> ();
	public GameObject CharacterInfoDisplayTemplate;
	public Vector2 CharacterInfoDisplayPosOffset;

	public List<AbilityInfo> AbilityInfoList = new List<AbilityInfo> ();
	public GameObject AbilityInfoDisplayTemplate;
	public Vector2 AbilityInfoDisplayPosOffset;

	public int maxCharacterInfoDisplayNum = 5;
	public int maxAbilityInfoDisplayNum = 5;
	public Vector2 selectedAbilityScale = new Vector2(2.5f,2.5f);
	public Vector2 selectedCharScale = new Vector2 (2.5f, 2.5f);

	public UnityEngine.UI.Text CharInfoText, CharBuindInAbilityInfoText, AbilityInfoText;

	private List<GameObject> CharObjectList = new List<GameObject>();
	private List<GameObject> AbilityObjectList = new List<GameObject> ();
	private int currentCharacterIndex = -1;
	private int lastCharacterIndex = -1;
	private int currentAbilityIndex = -1;
	private int lastAbilityIndex = -1;
	GameManager gameMgr;
	CharacterSelectionEvents eventHandler;
	ShopEventHandler shopEvents;

	private int savedCharacterIndex = 0,savedAbilityIndex = 0;
	private SaveObject mysave;

#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
	private float TouchMovementX = 0;
#endif
	void Start () {

		GameFile.Load ("save.data", ref mysave);
		SetupSavedSelection (mysave.lastSelectedCharacterIndex, mysave.lastSelectedAbilityIndex);

		CreateCharacters ();
		CreateAbilites ();

		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		gameMgr.CleanUpAbilityNames ();

		eventHandler = GameObject.Find ("CharacterSelectionEvents").GetComponent<CharacterSelectionEvents> ();

		shopEvents = new ShopEventHandler ();
		shopEvents.setUpCharacterSelector (this);

		if (CharObjectList.Count > 0) {
			SelectCharacter(savedCharacterIndex);
		}

		if (AbilityObjectList.Count > 0) {
			SelectAbility(savedAbilityIndex);
		}

		gameMgr.PlayBackgroundMusic ();
	}

	void OnDestroy()
	{
		shopEvents.RemoveCallbacks ();
	}

	void SetupSavedSelection(int inCharIndex, int inAbilIndex)
	{
		savedCharacterIndex = inCharIndex;
		savedAbilityIndex = inAbilIndex;
	}

	public void SaveSelection ()
	{
		mysave.lastSelectedCharacterIndex = currentCharacterIndex;
		mysave.lastSelectedAbilityIndex = currentAbilityIndex;
		GameFile.Save ("save.data", mysave);
	}

	void CreateCharacters ()
	{
		foreach (CharacterInfo c in CharacterInfoList)
		{
			CharObjectList.Add(CreateCharacterObject2D(c));

		}
	}

	void CreateAbilites()
	{
		for (int i = 0; i < AbilityInfoList.Count; ++i) {
			if(AbilityInfoList[i].canSelect)
			{
				GameObject go = CreateAbilityObject2D(AbilityInfoList[i]);
				go.GetComponent<AbilityInfoDisplay>().AbilityInfoIndex = i;
				AbilityObjectList.Add (go);
			}
		
		}

	}

	GameObject CreateCharacterObject2D(CharacterInfo CharInfo )
	{
		GameObject obj = Instantiate (CharacterInfoDisplayTemplate);
		obj.GetComponent<SpriteRenderer> ().sprite = CharInfo.CharacterSprite;

		return obj;

	}

	GameObject CreateAbilityObject2D(AbilityInfo inAbilityInfo)
	{
		GameObject obj = Instantiate (AbilityInfoDisplayTemplate);
		obj.GetComponent<SpriteRenderer> ().sprite = inAbilityInfo.AbilitySprite;
		return obj;
	}

	void SelectCharacter(int idx)
	{
		idx = Math.Min(Math.Max(0,idx),CharObjectList.Count-1);
		currentCharacterIndex = idx;

		if (currentCharacterIndex != lastCharacterIndex) {
			if(lastCharacterIndex >= 0)
				gameMgr.RemoveAbilityById(  AbilityInfoList[CharacterInfoList [lastCharacterIndex].CharacterBuildInAbilityInfoIndex].AbilityId );
			gameMgr.SetCurrentPlayerTemplateByIdx (CharacterInfoList [currentCharacterIndex].CharacterId);
			gameMgr.AddAbilityById (AbilityInfoList[CharacterInfoList [currentCharacterIndex].CharacterBuildInAbilityInfoIndex].AbilityId);
		}

		CharInfoText.text = CharacterInfoList [currentCharacterIndex].CharacterDescription;
		CharBuindInAbilityInfoText.text = GetAbilityDescriptionByAbilityInfoIndex(CharacterInfoList[currentCharacterIndex].CharacterBuildInAbilityInfoIndex);

		eventHandler.OnSelectedACharacter(CharacterInfoList[currentCharacterIndex], StoreInventory.GetItemBalance (CharacterInfoList [currentCharacterIndex].CharacterSoomlaId) > 0,CharacterInfoList[currentCharacterIndex].Cost_Coin < StoreInventory.GetItemBalance(ShopAssets.COIN_CURRENCY_ITEM_ID));


		if(currentCharacterIndex != lastCharacterIndex)
		{
			ReArrangeCharacter(currentCharacterIndex);
		}
	}

	string GetAbilityDescriptionByAbilityInfoIndex(int AbilityInfoIndex)
	{
		return AbilityInfoList [AbilityInfoIndex].AbilityDescription;
	}

	void SelectAbility(int idx)
	{


		idx = Math.Max(0,idx);
		currentAbilityIndex = idx;



		if (currentAbilityIndex != lastAbilityIndex && AbilityInfoList[currentAbilityIndex].canSelect) 
		{
			if(lastAbilityIndex >= 0)
				gameMgr.RemoveAbilityById(AbilityInfoList[lastAbilityIndex].AbilityId);

			if(StoreInventory.GetItemBalance(AbilityInfoList[currentAbilityIndex].AbilitySoomlaId) > 0)
				gameMgr.AddAbilityById (AbilityInfoList[currentAbilityIndex].AbilityId);
		}

		AbilityInfoText.text = GetAbilityDescriptionByAbilityInfoIndex(currentAbilityIndex);

		if(AbilityInfoList[currentAbilityIndex].AbilitySoomlaId != "")
			eventHandler.OnSelectedAnAbility(AbilityInfoList[currentAbilityIndex], StoreInventory.GetItemBalance(AbilityInfoList[currentAbilityIndex].AbilitySoomlaId) > 0, AbilityInfoList[currentAbilityIndex].Cost_Coin < StoreInventory.GetItemBalance(ShopAssets.COIN_CURRENCY_ITEM_ID));

		//////////

		if(currentAbilityIndex != lastAbilityIndex)
		{
			int AbilityDisplayObjectIndex = 0;
			for (int i = 0; i < AbilityObjectList.Count; ++i)
			{
				if(idx == AbilityObjectList[i].GetComponent<AbilityInfoDisplay>().AbilityInfoIndex)
				{
					AbilityDisplayObjectIndex = i;
					break;
				}
			}
			ReArrangeAbility(AbilityDisplayObjectIndex);

			
			lastAbilityIndex = currentAbilityIndex;
		}
	}

	public void LeftCharacter()
	{
		SelectCharacter(--currentCharacterIndex);
		if(currentCharacterIndex != lastCharacterIndex)
		{
			ReArrangeCharacter(currentCharacterIndex);
		}
	}
	public void RightCharacter()
	{
		SelectCharacter(++currentCharacterIndex);
		if(currentCharacterIndex != lastCharacterIndex)
		{
			ReArrangeCharacter(currentCharacterIndex);
		}
	}
	public void LeftAbility()
	{
		int nextAbilityIndex = currentAbilityIndex;
		while (nextAbilityIndex > -1) {
			nextAbilityIndex--;
			if(nextAbilityIndex == 0 || 
			   (nextAbilityIndex >=0 && AbilityInfoList[nextAbilityIndex].canSelect))
			{
				currentAbilityIndex = nextAbilityIndex;
				break;
			}
		}
		SelectAbility(currentAbilityIndex);
	}
	public void RightAbility()
	{
		int nextAbilityIndex = currentAbilityIndex;
		while (nextAbilityIndex < AbilityInfoList.Count) {
			nextAbilityIndex++;
			if(nextAbilityIndex == AbilityInfoList.Count -1 || 
			   (nextAbilityIndex < AbilityInfoList.Count && AbilityInfoList[nextAbilityIndex].canSelect))
			{
				currentAbilityIndex = nextAbilityIndex;
				break;
			}
		}
		SelectAbility(currentAbilityIndex);
	}
	public bool UnlockBySoomlaItemId(string soomlaItemId)
	{
		for (int idx = 0; idx < CharacterInfoList.Count; ++ idx) {
			if(CharacterInfoList[idx].CharacterSoomlaId == soomlaItemId)
			{
				DoUnlockCharacter(idx);
				return true;
			}
		}

		for (int idx = 0; idx < AbilityInfoList.Count; ++ idx) {
			if(AbilityInfoList[idx].AbilitySoomlaId == soomlaItemId)
			{
				DoUnlockAbility(idx);
				return true;
			}
		}

		return false;
	}

	public void CancelPurchase(string soomlaItemId)
	{
		CancelInvoke("processingEnd");
		Invoke ("processingEnd", 1f);
	}

	public void DoUnlockCharacter(int characterIndex)
	{
		CancelInvoke("processingEnd");
		Invoke ("processingEnd", 1f);

		eventHandler.OnSelectedACharacter (CharacterInfoList [characterIndex], true);
	}

	public void DoUnlockAbility(int abilityIdx)
	{
		CancelInvoke("processingEnd");
		Invoke ("processingEnd", 1f);

		if (AbilityInfoList[abilityIdx].canSelect)
		   // && StoreInventory.GetItemBalance(AbilityInfoList[abilityIdx].AbilitySoomlaId) > 0) 
		{
			gameMgr.AddAbilityById (AbilityInfoList[abilityIdx].AbilityId);
		}

		eventHandler.OnSelectedAnAbility (AbilityInfoList [abilityIdx], true);
	}

	
	public void UnlockCurrentCharUsingCoin()
	{
		if (CharacterInfoList [currentCharacterIndex].Cost_Coin <= StoreInventory.GetItemBalance(ShopAssets.COIN_CURRENCY_ITEM_ID)) {

			StoreInventory.TakeItem("coin1",CharacterInfoList [currentCharacterIndex].Cost_Coin);
			StoreInventory.GiveItem(CharacterInfoList [currentCharacterIndex].CharacterSoomlaId,1);

			DoUnlockCharacter(currentCharacterIndex);
		}
	}
	
	public void UnlockCurrentAbilityUsingCoin()
	{
		if (AbilityInfoList [currentAbilityIndex].Cost_Coin <= StoreInventory.GetItemBalance(ShopAssets.COIN_CURRENCY_ITEM_ID)) {

			StoreInventory.TakeItem("coin1",AbilityInfoList [currentAbilityIndex].Cost_Coin);
			StoreInventory.GiveItem(AbilityInfoList [currentAbilityIndex].AbilitySoomlaId,1);

			DoUnlockAbility(currentAbilityIndex);

		}
	}

	public void UnlockCurrentCharUsingSoomla(){
		try{
			StoreInventory.BuyItem(CharacterInfoList [currentCharacterIndex].CharacterSoomlaId);
			eventHandler.showPorcessingPanel(true);

			Invoke("processingEnd",10f);
		} catch (Exception e) {
			Debug.LogError ("SOOMLA/UNITY " + e.Message);
			CancelInvoke("processingEnd");
			processingEnd();
		}
	}

	
	public void UnlockCurrentAbilityUsingSoomla(){
		try{
			StoreInventory.BuyItem(AbilityInfoList [currentAbilityIndex].AbilitySoomlaId);
			eventHandler.showPorcessingPanel(true);

			Invoke("processingEnd",10f);
		} catch (Exception e) {
			Debug.LogError ("SOOMLA/UNITY " + e.Message);
			CancelInvoke("processingEnd");
			processingEnd();
		}
	}

	void processingEnd()
	{
		eventHandler.showPorcessingPanel (false);
	}


	// Update is called once per frame
	void Update () {

		 

	}

	void ReArrangeCharacter(int idx)
	{
		foreach (GameObject gobject in CharObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}

		CharObjectList [idx].GetComponent<CharacterInfoDisplay> ().SetPosition ( 0, CharacterInfoDisplayPosOffset.y, selectedCharScale.x, selectedCharScale.y);
		CharObjectList[idx].GetComponent<CharacterInfoDisplay>().SetTranslucency(1f);
		for(int i = 1; i <= maxCharacterInfoDisplayNum/2; i++)
		{
			CharacterInfoDisplay tempCID;
			if(idx - i  >= 0)
			{
				tempCID = CharObjectList[idx - i].GetComponent<CharacterInfoDisplay>();
				tempCID.SetPosition(-i * CharacterInfoDisplayPosOffset.x /2, CharacterInfoDisplayPosOffset.y, 1, 1);
				tempCID.GetComponent<CharacterInfoDisplay>().SetTranslucency(0.5f);
			}

			if(idx + i < CharObjectList.Count )
			{
				tempCID = CharObjectList[idx + i].GetComponent<CharacterInfoDisplay>();
				tempCID.GetComponent<CharacterInfoDisplay>().SetPosition(i * CharacterInfoDisplayPosOffset.x / 2, CharacterInfoDisplayPosOffset.y, 1, 1);
				tempCID.GetComponent<CharacterInfoDisplay>().SetTranslucency(0.5f);
			}


		}

		lastCharacterIndex = idx;
	}

	void ReArrangeAbility(int idx)
	{
		foreach (GameObject gobject in AbilityObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}
		
		AbilityObjectList [idx].GetComponent<AbilityInfoDisplay> ().SetPosition (0, AbilityInfoDisplayPosOffset.y, selectedAbilityScale.x, selectedAbilityScale.y);
		AbilityObjectList [idx].GetComponent<AbilityInfoDisplay> ().SetTranslucency (1f);

		for(int i = 1; i <= maxAbilityInfoDisplayNum/2; i++)
		{
			AbilityInfoDisplay temp_AID;
			if(idx - i  >= 0)
			{
				temp_AID = AbilityObjectList[idx - i].GetComponent<AbilityInfoDisplay>();
				temp_AID.SetPosition(-i * AbilityInfoDisplayPosOffset.x / 2, AbilityInfoDisplayPosOffset.y, 1, 1);
				temp_AID.SetTranslucency(0.5f);
			}
			
			if(idx + i < AbilityObjectList.Count )
			{
				temp_AID = AbilityObjectList[idx + i].GetComponent<AbilityInfoDisplay>();
				temp_AID.SetPosition(i * AbilityInfoDisplayPosOffset.x / 2, AbilityInfoDisplayPosOffset.y, 1, 1);
				temp_AID.SetTranslucency(0.5f);
			}
		}

	}

	public void ClearLog()
	{
		Utils.clearLog();
	}
	public void TestBuy100Coins()
	{
		StoreInventory.BuyItem ("coins100");
	}
}
