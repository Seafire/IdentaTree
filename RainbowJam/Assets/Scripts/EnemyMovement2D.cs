using UnityEngine;
using System.Collections;

public class EnemyMovement2D : MonoBehaviour 
{
	public float maxSpeed = 2.0f;
	private PauseScreen pauseScreen;

	// Use this for initialization
	void Start ()
	{
		pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreen>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (pauseScreen.gamePaused == false)
		{
			//Move the enemy
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, maxSpeed * Time.deltaTime, 0);
			pos += transform.rotation * velocity;

			transform.position = pos;
		}
	}
}
