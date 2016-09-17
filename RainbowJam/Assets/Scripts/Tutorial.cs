using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour 
{
	private PauseScreen pause; 

	public GameObject TutorialSkipButton;
	public GameObject tutorialHeader;
	private Transform[] tutorialImage;


	void awake ()
	{

	}

	// Use this for initialization
	void Start ()
	{
		pause = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreen>();
		tutorialImage = tutorialHeader.GetComponentsInChildren<Transform>();
		//TutorialSkipButton = GameObject.Find("Tutorial").GetComponent<GameObject>();

		tutorialImage[0].position = new Vector3 (gameObject.transform.position.x, Screen.height / 2, gameObject.transform.position.z);
		tutorialImage[1].position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
		tutorialImage[2].position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
		TutorialSkipButton.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height / 2, gameObject.transform.position.z);
		pause.gamePaused = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void SkipButton()
	{
		Debug.Log ("Hello");
		if (tutorialImage [0].position.y < Screen.height) 
		{
			tutorialImage[0].position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
			tutorialImage[1].position = new Vector3 (gameObject.transform.position.x, Screen.height / 2, gameObject.transform.position.z);
		}
		else if (tutorialImage [1].position.y < Screen.height) 
		{
			tutorialImage[1].position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
			tutorialImage[2].position = new Vector3 (gameObject.transform.position.x, Screen.height / 2, gameObject.transform.position.z);
		}
		else if (tutorialImage [2].position.y < Screen.height) 
		{
			tutorialImage[2].position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
			TutorialSkipButton.transform.position = new Vector3 (gameObject.transform.position.x, Screen.height * 2, gameObject.transform.position.z);
			pause.gamePaused = false;
		}
	}
}
