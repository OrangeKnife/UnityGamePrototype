using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMathQuestionGenerator : MonoBehaviour {

	public int min = 0,max = 10;
	public int factorNum = 3;
	public TextMesh resultTextMesh1;
	public TextMesh resultTextMesh2;

	public List<GameObject> rewardList1;
	public List<GameObject> rewardList2;

	List<int> factorArray;
	List<int> operatorArray;
	int result;
	void Start () {

		//force draw on layer 3
		GetComponent<MeshRenderer> ().sortingOrder = 3;
		if (resultTextMesh1)
			resultTextMesh1.GetComponent<MeshRenderer> ().sortingOrder = 3;
		
		if (resultTextMesh2)
			resultTextMesh2.GetComponent<MeshRenderer> ().sortingOrder = 3;

		if (factorNum < 2)
			factorNum = 2;

		factorArray = new List<int> ();
		operatorArray = new List<int> ();

		for(int i = 0; i < factorNum;++i)
			factorArray.Add( Random.Range (min, max) );

		result = factorArray [0];
		for (int i = 0; i < factorNum - 1; ++i) {
			operatorArray.Add( Random.Range (0,3));
		}

		operatorArray.Sort ();

		result = factorArray [0];
		string questionText = result.ToString();
		for(int i = 1; i < factorNum; ++i)
		{
			switch (operatorArray [operatorArray.Count - i]) 
			{
				case 0:
					questionText += " + " + factorArray [i].ToString();
					result = plus (result, factorArray [i]);
					break;
				case 1:
					questionText += " - " + factorArray [i].ToString();
					result = minus (result, factorArray [i]);
					break;
				case 2:
					questionText += " * " + factorArray [i].ToString();
					result = multiple (result, factorArray [i]);
					break;
			}
		}
		questionText += " = ?";
		GetComponent<UnityEngine.TextMesh> ().text = questionText;

		if (Random.Range (0, 2) == 0) {
			if (resultTextMesh1)
				resultTextMesh1.text = result.ToString ();

			if (resultTextMesh2)
				resultTextMesh2.text = Random.Range (-Mathf.Abs (result) + 1, Mathf.Abs (result) - 1).ToString ();

			foreach(GameObject go in rewardList2)
				go.SetActive(false);

		} else {
			
			if (resultTextMesh2)
				resultTextMesh2.text = result.ToString ();
			
			if (resultTextMesh1)
				resultTextMesh1.text = Random.Range (-Mathf.Abs (result) + 1, Mathf.Abs (result) - 1).ToString ();

			foreach(GameObject go in rewardList1)
				go.SetActive(false);
		}
	}



	int plus(int left,int right)
	{
		return left + right;
	}

	int minus(int left, int right)
	{
		return left - right;
	}

	int multiple(int left, int right)
	{
		return left * right;
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
