using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour 
{
	private GameObject gameOverText; 
	public GameObject scoreDisplay;
	private PauseScreen pause; 
	private Score score;
	private int maxScore = 1000;
	private bool gameOver = false;
	private GameObject[] resetScoreVal;
	// Use this for initialization
	void Start () 
	{
		resetScoreVal = GameObject.FindGameObjectsWithTag ("PickUp");

		Debug.Log (resetScoreVal.Length);
		gameOverText = GameObject.Find ("GameOver").GetComponent <GameObject> ();
		//scoreDisplay = GameObject.Find ("ScoreDisplay").GetComponent <GameObject> ();
		pause = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreen>();
		score = GameObject.FindGameObjectWithTag ("Tree").GetComponent<Score> ();

		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
		//score = gameObject.GetComponent <Score> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (score.totalScore >= maxScore && !gameOver) 
		{
			if (score.totalScore != maxScore)
				score.posScore -= 50;

			EndGame();
		}

		if (!gameOver)
			scoreDisplay.transform.position = new Vector3 (scoreDisplay.transform.position.x, Screen.height / 2, scoreDisplay.transform.position.z);
	}

	// Display the final score of the player 
	void EndGame()
	{
		// Pauses all the in game objects
		pause.gamePaused = true;
		gameOver = true;

		// Trans forms UI elements to the aprropiate positions
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height / 2, gameObject.transform.position.z);
		scoreDisplay.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
	}

	// If the play again button is pressed
	public void PlayAgainPressed()
	{
		for (int i = 0; i < resetScoreVal.Length; i ++) 
		{
			resetScoreVal[i].GetComponent<PickUp> ().pickedUpBefore = 0;
		}
		// Setting Score back to 0 so user can play again
		score.totalScore = 0;
		score.posScore = 0;
		score.negScore = 0;

		// Move the end game UI element off screen
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);

		// Allows the game to continue running
		gameOver = false;
		pause.gamePaused = false;
	}

	// Function used to return the game to the main menu
	 public void ReturnToMainMenuPressed()
	{
		// Load the main level
		Application.LoadLevel (0);
	}
}
