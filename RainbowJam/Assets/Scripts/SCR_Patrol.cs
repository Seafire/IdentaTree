// Unity includes here.
using UnityEngine;
using System.Collections;

// Patrol IS A character, therefore inherits from it.
// This class will handle moving a character between any two given points.
public class SCR_Patrol : MonoBehaviour 
{
	// Variables.
	//private Animator anime;					// Access to the character animator.

	//Conor Stuff
	public float speed;
	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;
	int fromWaypointIndex;
	float percentBetweenWaypoints;		//percentage between 0 and 1 

	// Functions.
	//////////////////////////////////////////////////
	//                    Start                 	//
	//==============================================//
	// This function will be called for             //
	// initialisation.								//
	//////////////////////////////////////////////////
	void Start () 
	{
		
		// Initialising the variables.
		//character = GetComponent<SCR_Character> ();
		//anime = GetComponent<Animator> ();

		globalWaypoints = new Vector3[localWaypoints.Length];

		for (int i = 0; i < localWaypoints.Length; i++) 
		{
			globalWaypoints[i] = localWaypoints[i] + transform.position;
		}

		// Setting the initial speed value for the animation states.
		//anime.SetFloat ("Speed", 1.0f);
	}

	Vector3 CalculateEnemyMovement()
	{
		//Debug.Log(globalWaypoints.Length);

		int toWaypointIndex = fromWaypointIndex + 1;
		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * speed / distanceBetweenWaypoints;

		Vector3 newPos = Vector3.Lerp (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex], percentBetweenWaypoints);
		//Vector3 newPos = Vector3.Lerp (globalWaypoints [fromWaypointIndex], transform.position., percentBetweenWaypoints);

		if (percentBetweenWaypoints >= 1) 
		{
			percentBetweenWaypoints = 0;
			fromWaypointIndex ++;
			if (fromWaypointIndex >= globalWaypoints.Length - 1)
			{
				//character.FlipSprite (-1.0f);
				fromWaypointIndex = 0;
				System.Array.Reverse(globalWaypoints);
				Debug.Log("should flip" + globalWaypoints.Length);
				//facingRight = false;
				//facingLeft = true;
			}

			else if (fromWaypointIndex >= globalWaypoints.Length - 1)
			{
				Debug.Log("should flipary" + fromWaypointIndex);
				//character.FlipSprite (1.0f);
				fromWaypointIndex = 0;
				System.Array.Reverse(globalWaypoints);
				//facingRight = true;
				//facingLeft = false;
			}
		}

		return newPos - transform.position;
	}

	//////////////////////////////////////////////////
	//             		Update              		//
	//==============================================//
	// Called every frame, here we will do standard	//
	// character updates.							//
	//////////////////////////////////////////////////
	void Update () 
	{
		// Calculating the current velocity based on the enemy movement value calculated.
		Vector3 velocity = CalculateEnemyMovement();
		
		// Move the patrol character by the velocity.
		transform.Translate (velocity);


	}

	//////////////////////////////////////////////////
	//             	  On Draw Gizmos              	//
	//==============================================//
	// This function is more of a benefit for the	//
	// person editing the prefab in the editor. 	//
	// This will draw a marker where the transform	//
	// position of this marker is given.			//
	// Allowing us to track where the empty game	//
	// object would be a lot easier.				//
	//////////////////////////////////////////////////
	void OnDrawGizmos()
	{
		if (localWaypoints != null)
		{
			Gizmos.color = Color.red;
			float size = 0.4f;

			for (int i = 0; i < localWaypoints.Length; i++)
			{
				Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints[i] + transform.position;
				Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
			}
		}
	}
}
