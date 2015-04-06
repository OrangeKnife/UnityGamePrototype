using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;


[Serializable]
public struct CharacterInfo
{
	public Sprite CharacterSprite;
	public int CharacterId;
	public int CharacterPassiveAbilityId;
	public string CharacterName;
	public string CharacterDescription;
	public string CharacterProductId;
	public int Cost;
	public float Cost_Dollar;

};

[Serializable]
public struct AbilityInfo
{
	public Sprite AbilitySprite;
	public int AbilityId;
	public string AbilityName;
	public string AbilityDescription;
	public string AbilityProductId;
	public int Cost;
	public float Cost_Dollar;
};

public class CharacterSelector : MonoBehaviour
{


	public List<CharacterInfo> CharacterInfoList = new List<CharacterInfo> ();
	public GameObject CharacterInfoDisplayTemplate;

	public List<AbilityInfo> AbilityInfoList = new List<AbilityInfo> ();
	public GameObject AbilityInfoDisplayTemplate;
	public int maxCharacterInfoDisplayNum = 5;
	public int maxAbilityInfoDisplayNum = 5;

	private List<GameObject> CharObjectList = new List<GameObject>();
	private List<GameObject> AbilityObjectList = new List<GameObject> ();
	private int currentCharacterIndex = -1;
	private int lastCharacterIndex = -1;
	private int currentAbilityIndex = -1;
	private int lastAbilityIndex = -1;
	GameManager gameMgr;

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
		
	}

	void SelectAbility(int idx)
	{
		idx = Math.Min(Math.Max(0,idx),AbilityObjectList.Count-1);
		currentAbilityIndex = idx;
		if (currentAbilityIndex != lastAbilityIndex) {
			gameMgr.RemoveAbilityByIndex(lastAbilityIndex);
			gameMgr.AddAbilityByIndex (currentAbilityIndex);
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

		CharObjectList [idx].GetComponent<CharacterInfoDisplay> ().SetPosition (0, 2.56f, 10, 10);

		for(int i = 1; i <= maxCharacterInfoDisplayNum/2; i++)
		{
			if(idx - i  >= 0)
			{
				CharObjectList[idx - i].GetComponent<CharacterInfoDisplay>().SetPosition(-i * 4, 2.56f, 5, 5);
			}

			if(idx + i < CharObjectList.Count )
			{
				CharObjectList[idx + i].GetComponent<CharacterInfoDisplay>().SetPosition(i * 4, 2.56f, 5, 5);
			}
		}

		lastCharacterIndex = idx;
	}

	void ReArrangeAbility(int idx)
	{
		foreach (GameObject gobject in AbilityObjectList) {
			gobject.GetComponent<SpriteRenderer> ().enabled = false;
		}
		
		AbilityObjectList [idx].GetComponent<AbilityInfoDisplay> ().SetPosition (0, -0.62f, 4, 4);
		
		for(int i = 1; i <= maxAbilityInfoDisplayNum/2; i++)
		{
			if(idx - i  >= 0)
			{
				AbilityObjectList[idx - i].GetComponent<AbilityInfoDisplay>().SetPosition(-i * 4, -0.62f, 2, 2);
			}
			
			if(idx + i < AbilityObjectList.Count )
			{
				AbilityObjectList[idx + i].GetComponent<AbilityInfoDisplay>().SetPosition(i * 4, -0.62f, 2, 2);
			}
		}
		
		lastAbilityIndex = idx;
	}
}
