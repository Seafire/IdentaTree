using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour 
{
	[HideInInspector]public bool gamePaused;

	private bool isGamePaused;

	// Use this for initialization
	void Start () 
	{
		//gamePaused = false;
		isGamePaused = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isGamePaused) 
		{
			if (Input.GetKeyDown ("escape")) 
			{
				Debug.Log("I should go back to the main menu");
				// Do pause menu stuff here
				//gamePaused = true;
				//isGamePaused = true;
				Application.LoadLevel(0);
			}
		} 
	//	else 
	//	{
	//		if (Input.GetKeyDown ("escape")) 
	//		{
				//gamePaused = false;
				//isGamePaused = false;
			//}
	//	}
	}

	public void ResumePressed()
	{

	}

	public void QuitPressed()
	{

	}
}
