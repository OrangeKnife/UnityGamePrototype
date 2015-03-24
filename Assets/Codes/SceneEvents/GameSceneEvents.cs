using UnityEngine;
using System.Collections;

public class GameSceneEvents : MonoBehaviour {

	[SerializeField]
	GameObject UI_DeathPanel = null;
	[SerializeField]
	GameObject UI_ScorePanel = null;
	[SerializeField]
	GameObject UI_ScoreText = null;

	public GameObject Player;

	// Use this for initialization
	void Start () {
		UI_DeathPanel.SetActive (false);
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public void onPlayerDead() 
	{
		UI_DeathPanel.SetActive (true);
		UI_ScorePanel.SetActive (false);
	}

	public void OnTryAgainButtonClicked()
	{
		Debug.Log("TryAgain!");
		Player.GetComponent<PlayerController> ().Respawn();
		UI_DeathPanel.SetActive (false);
		UI_ScorePanel.SetActive (true);
	}

	public void UpdateUISocre(int newScore)
	{
		UI_ScoreText.GetComponent<UnityEngine.UI.Text>().text = newScore.ToString();
	}
}
