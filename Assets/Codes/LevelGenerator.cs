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
	public bool bInitSpawnTutorialSection = true;
	public List<GameObject> SectionArray_TestSection;
	public List<GameObject> SectionArray_Tutorial;
	public List<GameObject> SectionArray_Easy;
	public List<GameObject> SectionArray_Normal;
	public List<GameObject> SectionArray_Hard;
	public List<GameObject> SectionArray_Wtf;

	private List<GameObject> currentLevel = null;
	// store spawned section here, so we know how many we have and can kill it properly
	private LinkedList<GameObject> SpawnedSectionList;
	private float LastSectionBeginPosition;
	private int MAXSECTIONS = 7;
	

	// Use this for initialization
	void Start () 
	{
		currentLevel = SectionArray_Easy;

		///// grab player
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;

		//InitLevel();
	}

	public void InitLevel()
	{
		if (SpawnedSectionList == null)
			SpawnedSectionList = new LinkedList<GameObject>();

		currentLevel = SectionArray_Easy;

		///// grab player
		player = GameObject.FindGameObjectWithTag ("Player");
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
			for (int i = 0; i < SectionArray_TestSection.Count; ++i)
			{
				SpawnSection(SectionArray_TestSection[i]);
			}
		}
		else if (bInitSpawnTutorialSection)
		{
			// spawn tutorial sections
			for (int i = 0; i < SectionArray_Tutorial.Count; ++i)
			{
				SpawnSection(SectionArray_Tutorial[i]);
			}
		}
		else
		{
			// init first 2-3 sections here
			for (int i = 0; i < MAXSECTIONS; ++i)
			{
				SpawnSection(currentLevel[Random.Range(0,currentLevel.Count)]);
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
			SpawnSection(currentLevel[Random.Range(0,currentLevel.Count)]);
		}

		///// kill old sections
		if (SpawnedSectionList.Count > MAXSECTIONS) 
		{
			Destroy (SpawnedSectionList.First.Value);
			SpawnedSectionList.RemoveFirst();
		}
	}

	void SpawnSection(GameObject template)
	{
		///// spawn template
		GameObject randomSection = Instantiate(template);

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
}
