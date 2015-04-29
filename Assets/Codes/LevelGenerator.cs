using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class do random level generator
/// Start by gen 2-3 sections 
/// then detect where the player is, generate more in front of the player
/// kill the sections that we already passed
/// 
/// contain easy/normal/hard/wtf section arrays
/// also control percentage of each section spawn rate
/// </summary>
public class LevelGenerator : MonoBehaviour {

	private GameObject player;
	private Transform playerTransform;

	public bool bInitSpawnTestSection;
	public bool bInitSpawnAllSections;
	public bool bInitSpawnTutorialSection = false;
	public List<GameObject> SectionArray_TestSection;
	public List<GameObject> SectionArray_Tutorial;
	public List<GameObject> SectionArray_PreGameSection;
	public List<GameObject> SectionArray_Easy;
	public List<GameObject> SectionArray_Normal;
	public List<GameObject> SectionArray_Hard;
	public List<GameObject> SectionArray_Wtf;

	public int SectionAll_Count;
	public List<GameObject> SectionArray_All = new List<GameObject>();

	public int UniqueSectionLimit = 5;
	public List<GameObject> SectionArray_Used = new List<GameObject>();

	private List<GameObject> currentLevel = null;
	// store spawned section here, so we know how many we have and can kill it properly
	private LinkedList<GameObject> SpawnedSectionList;
	private float LastSectionBeginPosition;
	private int MAXSECTIONS = 7;


	/// <summary>
	/// Difficulty related
	/// </summary>
	public struct SectionPicker
	{
		public float[] PercentByDifficulty; // 0=wtf, 1=hard, 2=normal, 3=easy

		public SectionPicker(float wtf, float hard, float normal, float easy)
		{
			PercentByDifficulty = new float[4];
			PercentByDifficulty[0] = wtf;
			PercentByDifficulty[1] = hard;
			PercentByDifficulty[2] = normal;
			PercentByDifficulty[3] = easy;
		}

		// 0=wtf, 1=hard, 2=normal, 3=easy
		public int GetSectionType(int Difficulty) 
		{
			float tmpRand;
			string tmpLog;

			tmpRand = Random.Range(0.0f, 100.0f);
			tmpLog = "UnPickedSection:";
			for (int i = 0; i < 3; ++i)
			{
				tmpLog += GetSectionName(i) + "(" + PercentByDifficulty[i] * Difficulty + "%),";
				if (tmpRand < PercentByDifficulty[i] * Difficulty)
				{
					Utils.addLog(tmpLog);
					tmpLog = "PickedSection: " + GetSectionName(i) + "(" + PercentByDifficulty[i] * Difficulty + "%), Diff: " + Difficulty;
					Utils.addLog(tmpLog);
					return i;
				}
				tmpRand = Random.Range(0.0f, 100.0f);
			}

			// always at least return 'easy'
			Utils.addLog(tmpLog);
			tmpLog = "PickedSection: " + GetSectionName(3) + ", Diff: " + Difficulty;
			Utils.addLog(tmpLog);
			return 3;
		}

		private string GetSectionName(int num)
		{
			if (num == 3)
				return "Easy";
			else if (num == 2)
				return "Normal";
			else if (num == 1)
				return "Hard";
			else if (num == 0)
				return "WTF";
			else 
				return "Error";
		}
	}

	private int NumberOfGeneratedSection;
	private int DifficultyMod = 5; // Difficulty = NumberOfGeneratedSection % DifficultyMod
	private int SurpriseSectionMod = 10; // special section is picked every X sections
	private SectionPicker NormalPicker;
	private SectionPicker SurprisePicker;
	
	GameManager gameMgr;
	// Use this for initialization
	void Start () 
	{
		currentLevel = SectionArray_Normal;

		if (gameMgr == null)
			gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

//		///// grab player
//		player = GameObject.FindGameObjectWithTag ("Player");
//		playerTransform = player.transform;

		//InitLevel();
	}

	public void InitLevel()
	{
		if (SpawnedSectionList == null)
			SpawnedSectionList = new LinkedList<GameObject>();

		SectionArray_Used.Clear();
		NumberOfGeneratedSection = 0;
		currentLevel = SectionArray_Normal;
		NormalPicker = new SectionPicker(0.5f, 2.0f, 10.0f, 9999.0f);
		SurprisePicker = new SectionPicker(10.0f, 9999.0f, 9999.0f, 9999.0f);

		///// grab player
		//player = GameObject.FindGameObjectWithTag ("Player");
		if (gameMgr == null)
			gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		player = gameMgr.GetCurrentPlayer();
		playerTransform = player.transform;

		///// kill old sections
		int count = SpawnedSectionList.Count;
		for (int i = 0; i < count; ++i) 
		{
			Destroy (SpawnedSectionList.First.Value);
			SpawnedSectionList.RemoveFirst();
		}

		if (bInitSpawnTestSection)
		{
			print ("test section");
			for (int i = 0; i < SectionArray_TestSection.Count; ++i)
			{
				SpawnSection(SectionArray_TestSection[i]);
			}
		}
		else if (bInitSpawnAllSections)
		{
			print ("all test sections");

			// init array_all
			SectionArray_All.Clear();
			//SectionAll_Count = 0;
			for (int i = 3; i >= 0; --i)
			{
				for (int j = 0; j < GetSectionArray(i).Count; ++j)
				{
					SectionArray_All.Add(GetSectionArray(i)[j]);
				}
			}

			// spawn blank sections
			for (int i = 0; i < SectionArray_PreGameSection.Count; ++i)
			{
				SpawnSection(SectionArray_PreGameSection[i]);
			}
		}
		else if (bInitSpawnTutorialSection)
		{
			print ("tutorial section");
			// spawn tutorial sections
			for (int i = 0; i < SectionArray_Tutorial.Count; ++i)
			{
				SpawnSection(SectionArray_Tutorial[i]);
			}
		}
		else
		{
			print ("normal section");
			// init first 2-3 sections here

			for (int i = 0; i < SectionArray_PreGameSection.Count; ++i)
			{
				SpawnSection(SectionArray_PreGameSection[i]);
			}

			for (int i = SectionArray_PreGameSection.Count; i < MAXSECTIONS; ++i)
			{
				currentLevel = GetSectionArray( NormalPicker.GetSectionType( GetDifficulty() ) );
				SpawnSection(currentLevel[Random.Range(0,currentLevel.Count)], true);
			}
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		///// check if player enter second to last Section, generate more section
		if (player.transform.position.x > SpawnedSectionList.Last.Previous.Value.GetComponent<LevelSectionScript>().boundingBox.min.x)
		{
			///// player enters last section
			if (!bInitSpawnAllSections)
			{
				if (NumberOfGeneratedSection % SurpriseSectionMod != 0)
				{
					currentLevel = GetSectionArray( NormalPicker.GetSectionType( GetDifficulty() ) );
				}
				else
				{
					// pick surprise section
					Utils.addLog("Surprise section");
					currentLevel = GetSectionArray( SurprisePicker.GetSectionType( GetDifficulty() ) );
				}
				SpawnSection(currentLevel[Random.Range(0,currentLevel.Count)], true);
			}
			else
			{
				// spawn all sections for testing
				Utils.addLog("SectionAll_Count=" + SectionAll_Count);
				SpawnSection(SectionArray_All[SectionAll_Count]);
				SectionAll_Count++;
			}

		}

		///// kill old sections
		if (SpawnedSectionList.Count > MAXSECTIONS) 
		{
			Destroy (SpawnedSectionList.First.Value);
			SpawnedSectionList.RemoveFirst();
		}
	}

	void SpawnSection(GameObject template, bool randomizeSection = false)
	{
		//Utils.addLog( "NumberOfGeneratedSection="+NumberOfGeneratedSection.ToString() );
		///// spawn template
		if (template == null)
			return;

		if (randomizeSection)
		{
			if (SectionArray_Used.Contains(template))
				return;

			SectionArray_Used.Add(template);
			if (SectionArray_Used.Count > UniqueSectionLimit)
			{
				SectionArray_Used.RemoveAt(0);
			}
		}

		GameObject randomSection = Instantiate(template);
		NumberOfGeneratedSection++;

		///// calculate bounding box for each template (for future calculation)
		Bounds tmp = new Bounds();
		foreach (Transform t in randomSection.GetComponentsInChildren<Transform>())
		{
			if (t.GetComponent<BoxCollider2D>() != null)
				tmp.Encapsulate(t.GetComponent<BoxCollider2D>().bounds);
		}
		randomSection.GetComponent<BoxCollider2D>().size = tmp.size / randomSection.transform.localScale.x;
		randomSection.GetComponent<BoxCollider2D>().offset = new Vector2( tmp.center.x, tmp.center.y );

		///// put spawned template in the right position
		if (SpawnedSectionList.Count == 0)
		{
			// spawn at the beginning if we have no section in the list
			//randomSection.transform.position = new Vector3(tmp.extents.x*-1,0,0);
			randomSection.transform.position = new Vector3(0,0,0);
		}
		else
		{
			// spawn next section next to last section
			GameObject lastSection = SpawnedSectionList.Last.Value;
//			Bounds lastSectionBound = lastSection.GetComponent<BoxCollider2D>().bounds;
//			Bounds randomSectionBound = randomSection.GetComponent<BoxCollider2D>().bounds;

			Bounds lastSectionBound = lastSection.GetComponent<LevelSectionScript>().boundingBox;
			Bounds randomSectionBound = randomSection.GetComponent<BoxCollider2D>().bounds;
		
			randomSection.transform.position = new Vector3( lastSectionBound.max.x + randomSectionBound.extents.x, 0, 0 );
		}

		randomSection.GetComponent<LevelSectionScript>().boundingBox = randomSection.GetComponent<BoxCollider2D>().bounds;
		//randomSection.GetComponent<BoxCollider2D>().enabled = false;
		///// add spawned section to the list
		SpawnedSectionList.AddLast(randomSection);

		LastSectionBeginPosition = SpawnedSectionList.Last.Value.GetComponent<LevelSectionScript>().boundingBox.min.x;
	}

	public int GetDifficulty()
	{
		return (int)Mathf.Ceil(NumberOfGeneratedSection / (float)DifficultyMod);
	}

	private List<GameObject> GetSectionArray(int num)
	{
		if (num == 3)
			return SectionArray_Easy;
		else if (num == 2)
			return SectionArray_Normal;
		else if (num == 1)
			return SectionArray_Hard;
		else if (num == 0)
			return SectionArray_Wtf;
		else
			return null; // error
	}
}
