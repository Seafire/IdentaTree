using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : MonoBehaviour 
{
	[HideInInspector] public int posScore;
	[HideInInspector] public int negScore;
	private int totalScore;

	public Text curScore;

	// Use this for initialization
	void Start () 
	{
		posScore = 0;
		negScore = 0;
		totalScore = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("Current Positive Score: " + posScore);
		//Debug.Log ("Current Negative Score: " + negScore);

		totalScore = posScore + negScore;

		curScore.text = posScore.ToString () + "\n" + negScore.ToString () + "\n" + totalScore.ToString () + "\n";
	}
}
