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

	public List<GameObject> SectionArray_Easy;
	public List<GameObject> SectionArray_Normal;
	public List<GameObject> SectionArray_Hard;
	public List<GameObject> SectionArray_Wtf;

	private List<GameObject> currentLevel = null;
	// store spawned section here, so we know how many we have and can kill it properly
	private LinkedList<GameObject> SpawnedSectionList;
	private float LastSectionBeginPosition;
	private int MAXSECTIONS = 5;
	
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,100,120), "Loader Menu");

		if(GUI.Button(new Rect(20,40,80,20), "Easy")) {
			currentLevel = SectionArray_Easy;
		}

		if(GUI.Button(new Rect(20,70,80,20), "Normal")) {
			currentLevel = SectionArray_Normal;
		}

		if(GUI.Button(new Rect(20,100,80,20), "Hard")) {
			currentLevel = SectionArray_Hard;
		}
	}
	// Use this for initialization
	void Start () 
	{
		currentLevel = SectionArray_Easy;

		///// grab player
		player = GameObject.Find ("Player");
		playerTransform = player.transform;

		SpawnedSectionList = new LinkedList<GameObject>();

		// init first 2-3 sections here
		for (int i = 0; i < MAXSECTIONS; ++i)
		{
			SpawnSection(currentLevel[Random.Range(0,currentLevel.Count)]);
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
			tmp.Encapsulate(t.GetComponent<BoxCollider2D>().bounds);
		}
		randomSection.GetComponent<BoxCollider2D>().size = tmp.size;
		randomSection.GetComponent<BoxCollider2D>().offset = new Vector2( tmp.center.x, tmp.center.y );

		///// put spawned template in the right position
		if (SpawnedSectionList.Count == 0)
		{
			// spawn at the beginning if we have no section in the list
			randomSection.transform.position = new Vector3(tmp.extents.x*-1,0,0);
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
		randomSection.GetComponent<BoxCollider2D>().enabled = false;
		///// add spawned section to the list
		SpawnedSectionList.AddLast(randomSection);

		LastSectionBeginPosition = SpawnedSectionList.Last.Value.GetComponent<LevelSectionScript>().boundingBox.min.x;
	}
}
