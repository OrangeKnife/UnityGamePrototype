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
	public int CharacterPassiveAbilityId;
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
};

public class CharacterSelector : MonoBehaviour
{


	public List<CharacterInfo> CharacterInfoList = new List<CharacterInfo> ();
	public GameObject CharacterInfoDisplayTemplate;

	public List<AbilityInfo> AbilityInfoList = new List<AbilityInfo> ();
	public GameObject AbilityInfoDisplayTemplate;
	public int maxCharacterInfoDisplayNum = 5;
	public int maxAbilityInfoDisplayNum = 5;
	public Vector2 selectedAbilityScale = new Vector2(2.5f,2.5f);
	public Vector2 selectedCharScale = new Vector2 (2.5f, 2.5f);

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

		if(currentCharacterIndex != lastCharacterIndex)
		{
			ReArrangeCharacter(currentCharacterIndex);
		}
		
		if(currentAbilityIndex != lastAbilityIndex)
		{
			ReArrangeAbility(currentAbilityIndex);
		}
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
		foreach (AbilityInfo  a in AbilityInfoList) {
			AbilityObjectList.Add (CreateAbilityObject2D (a));
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
		gameMgr.SetCurrentPlayerTemplateByIdx( CharacterInfoList[currentCharacterIndex].CharacterId );
		gameMgr.AddAbilityByIndex (CharacterInfoList [currentCharacterIndex].CharacterPassiveAbilityId);

		eventHandler.OnSelectedACharacter(CharacterInfoList[currentCharacterIndex], mysave.characterUnlockedArray[CharacterInfoList[currentCharacterIndex].CharacterId],CharacterInfoList[currentCharacterIndex].Cost_Coin < mysave.playerGold);
		
	}

	void SelectAbility(int idx)
	{
		idx = Math.Min(Math.Max(0,idx),AbilityObjectList.Count-1);
		currentAbilityIndex = idx;
		if (currentAbilityIndex != lastAbilityIndex) {
			gameMgr.RemoveAbilityByIndex(lastAbilityIndex);
			gameMgr.AddAbilityByIndex (currentAbilityIndex);
		}

		eventHandler.OnSelectedAnAbility(AbilityInfoList[currentAbilityIndex], mysave.abilityUnlockedArray[AbilityInfoList[currentAbilityIndex].AbilityId]);
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
		SelectAbility(--currentAbilityIndex);
		if(currentAbilityIndex != lastAbilityIndex)
		{
			ReArrangeAbility(currentAbilityIndex);
		}
	}
	public void RightAbility()
	{
		SelectAbility(++currentAbilityIndex);
		if(currentAbilityIndex != lastAbilityIndex)
		{
			ReArrangeAbility(currentAbilityIndex);
		}
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

		if (soomlaItemId == "coins100") {
			mysave.playerGold += 100;
			GameFile.Save("save.data",mysave);
		}

		return false;
	}

	public void DoUnlockCharacter(int characterIndex)
	{
		mysave.characterUnlockedArray [CharacterInfoList [characterIndex].CharacterId] = true;
		GameFile.Save("save.data",mysave);
		eventHandler.OnSelectedACharacter (CharacterInfoList [characterIndex], true);
	}

	public void DoUnlockAbility(int abilityIdx)
	{
		mysave.abilityUnlockedArray [AbilityInfoList [abilityIdx].AbilityId] = true;
		GameFile.Save("save.data",mysave);
		eventHandler.OnSelectedAnAbility (AbilityInfoList [abilityIdx], true);
	}

	
	public void UnlockCurrentCharUsingCoin()
	{
		if (CharacterInfoList [currentCharacterIndex].Cost_Coin <= mysave.playerGold) {
			DoUnlockCharacter(currentCharacterIndex);
			mysave.playerGold -= CharacterInfoList [currentCharacterIndex].Cost_Coin;
			GameFile.Save("save.data",mysave);
		}
	}
	
	public void UnlockCurrentAbilityUsingCoin()
	{
		if (AbilityInfoList [currentAbilityIndex].Cost_Coin <= mysave.playerGold) {
			DoUnlockAbility(currentAbilityIndex);

			mysave.playerGold -= AbilityInfoList [currentAbilityIndex].Cost_Coin;
			GameFile.Save("save.data",mysave);
		}
	}

	public void UnlockCurrentCharUsingSoomla(){
		try{
			StoreInventory.BuyItem(CharacterInfoList [currentCharacterIndex].CharacterSoomlaId);
		} catch (Exception e) {
			Debug.LogError ("SOOMLA/UNITY " + e.Message);
		}
	}

	
	public void UnlockCurrentAbilityUsingSoomla(){
		try{
			StoreInventory.BuyItem(AbilityInfoList [currentAbilityIndex].AbilitySoomlaId);
		} catch (Exception e) {
			Debug.LogError ("SOOMLA/UNITY " + e.Message);
		}
	}


	// Update is called once per frame
	void Update () {

		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		if(Input.GetKeyUp ("a"))
			SelectCharacter(--currentCharacterIndex);
		else if(Input.GetKeyUp ("d"))
			SelectCharacter(++currentCharacterIndex);

		if(Input.GetKeyUp("z"))
			SelectAbility(--currentAbilityIndex);
		else if(Input.GetKeyUp("c"))
			SelectAbility(++currentAbilityIndex);


		if(currentCharacterIndex != lastCharacterIndex)
		{
			ReArrangeCharacter(currentCharacterIndex);
		}

		if(currentAbilityIndex != lastAbilityIndex)
		{
			ReArrangeAbility(currentAbilityIndex);
		}

		
		
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		 
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Moved)
			{
				TouchMovementX += touch.deltaPosition.x;
				if(Mathf.Abs(TouchMovementX) > 100)
				{
					if (touch.position.y >= Screen.height * 2f / 3f)
					{
						if(TouchMovementX > 0)
							SelectCharacter(--currentCharacterIndex);
						else
							SelectCharacter(++currentCharacterIndex);
					}
					else if (touch.position.y >= Screen.height * 1f / 3f)
					{
						if(TouchMovementX > 0)
							SelectAbility(--currentAbilityIndex);
						else
							SelectAbility(++currentAbilityIndex);
					}



					TouchMovementX = 0;
				}

				if(currentCharacterIndex != lastCharacterIndex)
				{
					ReArrangeCharacter(currentCharacterIndex);
				}
				if(currentAbilityIndex != lastAbilityIndex)
				{
					ReArrangeAbility(currentAbilityIndex);
				}

			}
 
		}

		#endif

	}

	void ReArrangeCharacter(int idx)
	{
		foreach (GameObject gobject in CharObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}

		CharObjectList [idx].GetComponent<CharacterInfoDisplay> ().SetPosition (0, 3.5f, selectedCharScale.x, selectedCharScale.y);

		for(int i = 1; i <= maxCharacterInfoDisplayNum/2; i++)
		{
			if(idx - i  >= 0)
			{
				CharObjectList[idx - i].GetComponent<CharacterInfoDisplay>().SetPosition(-i * 3, 3.5f, 1, 1);
			}

			if(idx + i < CharObjectList.Count )
			{
				CharObjectList[idx + i].GetComponent<CharacterInfoDisplay>().SetPosition(i * 3, 3.5f, 1, 1);
			}
		}

		lastCharacterIndex = idx;
	}

	void ReArrangeAbility(int idx)
	{
		foreach (GameObject gobject in AbilityObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}
		
		AbilityObjectList [idx].GetComponent<AbilityInfoDisplay> ().SetPosition (0, -0.3f, selectedAbilityScale.x, selectedAbilityScale.y);
		
		for(int i = 1; i <= maxAbilityInfoDisplayNum/2; i++)
		{
			if(idx - i  >= 0)
			{
				AbilityObjectList[idx - i].GetComponent<AbilityInfoDisplay>().SetPosition(-i * 3, -0.3f, 1, 1);
			}
			
			if(idx + i < AbilityObjectList.Count )
			{
				AbilityObjectList[idx + i].GetComponent<AbilityInfoDisplay>().SetPosition(i * 3, -0.3f, 1, 1);
			}
		}
		
		lastAbilityIndex = idx;
	}

	public void ClearLog()
	{
		CharacterSelectionEvents.clearLog();
	}
	public void TestBuy100Coins()
	{
		StoreInventory.BuyItem ("coins100");
	}
}
